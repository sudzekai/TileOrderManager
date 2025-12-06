using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Tools.APIServices;
using TelegramBot.Types.Messages;

namespace TelegramBot.Tools.BotHandlers.Handlers.Messages.Tools
{
    public class CommandsController
    {
        private readonly string StartCommand = "/start";
        private readonly string AdminCommand = "/admin";
        private readonly string MasterCommand = "/master";

        ApiServicesContainer _services;

        public CommandsController(ApiServicesContainer services)
        {
            _services = services;
        }

        public async Task<MessageModel?> Control(Message message)
        {
            if (message.Text.Equals(StartCommand))
            {
                InlineKeyboardMarkup markup = new InlineKeyboardMarkup();

                markup.AddButton("Заказать", "tiles?page=1")
                      .AddButton("О нас", "about")
                      .AddButton("Отзывы", "reviews?page=1").AddNewRow();

                var user = await _services.Users.GetInfoByIdAsync(message.From.Id);
                var orders = await _services.Orders.GetAllByUserIdAsync(message.From.Id, 1);

                if (user == null)
                {
                    await _services.Users.CreateAsync(new(message.From.Id, message.From.Username ?? "Отсутствует"));
                    user = await _services.Users.GetInfoByIdAsync(message.From.Id);
                }

                if (user.FullName is not null)
                    markup.AddButton("Мои данные", "me");

                if (orders.Count > 0)
                    markup.AddButton("Мои заказы", $"orders?page=1&userid={message.From.Id}-user");

                return new("Это тг бот для заказов", markup);
            }

            else if (message.Text.Equals(AdminCommand))
            {
                InlineKeyboardMarkup markup = new InlineKeyboardMarkup();

                markup.AddButton("Пользователи", "users?page=1").AddNewRow()
                      .AddButton("Заказы", "orders?page=1-admin").AddNewRow()
                      .AddButton("Брусчатка", "tiles?page=1").AddNewRow()
                      .AddButton("Отзывы", "reviews?page=1").AddNewRow()
                      .AddButton("Мои данные", "me");

                return new("Это админ-панель", markup);
            }

            else if (message.Text.Equals(MasterCommand))
            {
                InlineKeyboardMarkup markup = new InlineKeyboardMarkup();

                markup.AddButton("Заказы", "orders?page=1-master").AddNewRow()
                      .AddButton("Отзывы", "reviews?page=1").AddNewRow()
                      .AddButton("Мои данные", "me");

                return new("Это мастер-панель", markup);
            }

            return null;
        }
    }
}
