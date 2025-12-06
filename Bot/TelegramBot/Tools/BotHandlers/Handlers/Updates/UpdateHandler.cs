using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Tools.APIServices;
using TelegramBot.Tools.AppErrorHandler;
using TelegramBot.Tools.BotHandlers.Handlers.Updates.Controllers;
using TelegramBot.Tools.Extensions;
using TelegramBot.Types.Messages;

namespace TelegramBot.Tools.BotHandlers.Handlers.Updates
{
    public class UpdateHandler
    {
        private readonly TelegramBotClient _bot;
        private readonly ApiServicesContainer _services;
        private readonly TileQueriesController _tiles;
        private readonly OrderQueriesController _orders;
        private readonly ReviewQueriesController _reviews;

        public UpdateHandler(TelegramBotClient bot, ApiServicesContainer services)
        {
            _bot = bot;
            _services = services;
            _tiles = new(_services.Tiles);
            _orders = new(_services);
            _reviews = new(_services.Reviews);
        }

        public async Task OnUpdate(Telegram.Bot.Types.Update update)
        {
            if (update.CallbackQuery is null
                || update.CallbackQuery.Message is null
                || update.CallbackQuery.Data is null)
                return;

            var userId = update.CallbackQuery.From.Id;
            var messageId = update.CallbackQuery.Message.Id;
            string data = update.CallbackQuery.Data;

            BotLogger.SendLog($"Получен запрос:\n\"{data}\"\nПользователь: https://t.me/{update.CallbackQuery.From.Username ?? "-- Username отсутствует"}");

            MessageModel? answer = null;

            if (data.Equals("start"))
            {
                InlineKeyboardMarkup markup = new InlineKeyboardMarkup();

                markup.AddButton("Заказать", "tiles?page=1")
                      .AddButton("О нас", "about")
                      .AddButton("Отзывы", "reviews?page=1").AddNewRow();

                var user = await _services.Users.GetInfoByIdAsync(update.CallbackQuery.From.Id);
                var orders = await _services.Orders.GetAllByUserIdAsync(update.CallbackQuery.From.Id, 1);

                if (user.FullName is not null)
                    markup.AddButton("Мои данные", "me");

                if (orders.Count > 0)
                    markup.AddButton("Мои заказы", $"orders?page=1&userid={update.CallbackQuery.From.Id}-user");

                answer = new("Это тг бот для заказов", markup);
            }

            else if (data.Contains("reviews"))
            {
                answer = await _reviews.Control(update);
            }
            else if (data.Contains("orders"))
            {
                answer = await _orders.Control(update);
            }
            else if (data.Contains("tiles"))
            {
                answer = await _tiles.Control(update);
            }

            if (answer is not null)
                await _bot.EditMessageModel(update.CallbackQuery.From.Id, update.CallbackQuery.Message.Id, answer);

            else
                await _bot.AnswerCallbackQuery(update.CallbackQuery.Id, "Кнопка не реализована");
        }
    }
}
