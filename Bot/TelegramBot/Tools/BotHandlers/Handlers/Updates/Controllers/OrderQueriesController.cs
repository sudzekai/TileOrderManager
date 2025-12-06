using BLL.DTO.Objects.Order;
using BLL.DTO.Types.Enums.Tools;
using BLL.Services;
using System.Text.RegularExpressions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Tools.APIServices;
using TelegramBot.Tools.APIServices.Services;
using TelegramBot.Tools.Extensions;
using TelegramBot.Types.Messages;

namespace TelegramBot.Tools.BotHandlers.Handlers.Updates.Controllers
{
    public class OrderQueriesController
    {
        private Regex Orders = new(@"orders\?page=\d+-[A-Za-zА-Яа-я0-9\-]+");
        private Regex UserOrders = new(@"orders\?page=\d+&userid=\d+-[A-Za-zА-Яа-я0-9\-]+");
        private Regex TileOrders = new(@"orders\?page=\d+&tileid=\d+-[A-Za-zА-Яа-я0-9\-]+");

        private Regex OrderInfo = new(@"orders\?id=\d+-[A-Za-zА-Яа-я0-9\-]+");

        private Regex OrderCreate = new(@"orders\?tileid=\d+-create");

        private Regex OrderUpdate = new(@"orders\?id=\d+");

        private OrdersApiService _service;
        private UsersApiService _userService;
        private TilesApiService _tileService;

        public OrderQueriesController(ApiServicesContainer services)
        {
            _service = services.Orders;
            _userService = services.Users;
            _tileService = services.Tiles;
        }

        public async Task<MessageModel?> Control(Update update)
        {
            string data = update.CallbackQuery.Data;

            if (Orders.IsMatch(data))
            {
                int page = Convert.ToInt32(data.GetDigitsAfter("page="));

                var orders = await _service.GetAllAsync(page);
                var nextPageOrders = await _service.GetAllAsync(page + 1);

                string appender = "-" + data.Split("-").Last();
                return GetOrdersListAsMarkup(page, orders, nextPageOrders, appender);
            }

            else if (UserOrders.IsMatch(data))
            {
                int page = Convert.ToInt32(data.GetDigitsAfter("page="));
                long userId = data.GetDigitsAfter("userid=");

                var orders = await _service.GetAllByUserIdAsync(userId, page);
                var nextPageOrders = await _service.GetAllByUserIdAsync(userId, page + 1);

                string appender = "-" + data.Split("-").Last();
                return GetOrdersListAsMarkup(page, orders, nextPageOrders, appender);
            }

            else if (TileOrders.IsMatch(data))
            {
                int page = Convert.ToInt32(data.GetDigitsAfter("page="));
                int tileId = Convert.ToInt32(data.GetDigitsAfter("tileid="));

                var orders = await _service.GetAllByTileIdAsync(tileId, page);
                var nextPageOrders = await _service.GetAllByTileIdAsync(tileId, page);

                string appender = "-" + data.Split("-").Last();
                return GetOrdersListAsMarkup(page, orders, nextPageOrders, appender);
            }

            else if (OrderInfo.IsMatch(data))
            {
                int id = Convert.ToInt32(data.GetDigitsAfter("id="));

                var order = await _service.GetByIdAsync(id);
                var user = await _userService.GetInfoByIdAsync(order.UserId);
                var tile = await _tileService.GetByIdAsync(id);

                string appender = data.Split("-").Last();

                MessageModel model = new MessageModel();

                if (appender.Equals("admin") || appender.Equals("client"))
                    model.Text = $"Заказ *№{order.Id}*\n*{tile.Name}*\nИтоговая стоимость: *{order.TotalPrice}*\nФИО получателя: *{user.FullName}*\nАдрес получателя: *{order.Address}*\nСтатус: *{order.Status.ToLocalizedString()}*";

                else if (appender.Equals("master"))
                    model.Text = $"Заказ *№{order.Id}*\n*{tile.Name}*\nИнициалы получателя: *{user.FullName.GetOnlyFirstLetters()}*\nСтатус: *{order.Status.ToLocalizedString()}*";

                model.Markup = new InlineKeyboardMarkup().AddButton("Назад", "start");

                return model;
            }

            else if (OrderCreate.IsMatch(data))
            {
                return new("логика создания заказа", new());
            }

            else if (OrderUpdate.IsMatch(data))
            {
                return new("логика обновления заказа", new());
            }

            return null;
        }

        private static MessageModel GetOrdersListAsMarkup<T>(int page, List<T> orders, List<T> nextPageOrders, string appender)
            where T : OrderListBase
        {
            InlineKeyboardMarkup markup = new InlineKeyboardMarkup();

            foreach (var order in orders)
                markup.AddButton($"{order.Id}", $"orders?id={order.Id}{appender}").AddNewRow();

            if (page > 1)
                markup.AddButton("⏪", $"orders?page={page - 1}{appender}");
            else
                markup.AddButton("-");

            markup.AddButton("Назад", "start");

            if (nextPageOrders.Any())
                markup.AddButton("⏩", $"orders?page={page - 1}{appender}");
            else
                markup.AddButton("-");

            MessageModel model = new MessageModel("Список отзывов", new());
            return model;
        }
    }
}
