using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Telegram.Bot;
using TelegramBot.Tools.APIServices;
using TelegramBot.Tools.AppErrorHandler;
using TelegramBot.Tools.BotHandlers.Container;
using TelegramBot.Types.Messages;

namespace TelegramBot
{
    public class TileOrderBot
    {
        private readonly TelegramBotClient _bot;

        private readonly ApiServicesContainer _services;
        private readonly HandlersContainer handlers;

        public TileOrderBot(string token)
        {
            BotLogger.InitializeErrorHandler();

            _bot = new TelegramBotClient(token);

            _services = new(new());

            handlers = new(_bot, _services);

            _bot.OnMessage += handlers.Messages.OnMessage;
            _bot.OnUpdate += handlers.Updates.OnUpdate;
            _bot.OnError += handlers.Errors.OnError;

            var responce = _bot.GetMe();
            var botInfo = responce.Result;

            TestApi().Wait();

            BotLogger.SendInfo($"Бот запущен\nИмя: {botInfo.FirstName ?? ""} {botInfo.LastName ?? ""}\nId: {botInfo.Id}\nURL: https://t.me/{botInfo.Username}");
        }

        public async Task TestApi()
        {
            var users = await _services.Users.GetAllAsync(1);
            var orders = await _services.Orders.GetAllAsync(1);
            var reviews = await _services.Reviews.GetAllAsync(1);
            var tiles = await _services.Tiles.GetAllAsync(1);

            var createdUser = await _services.Users.CreateAsync(new(535125343226, "verycoolusername"));
            var createdTile = await _services.Tiles.CreateAsync(new("Крутая брусчатка", 1000, "Крутое описание"));
            var createdOrder = await _services.Orders.CreateAsync(createdUser.Id, new(createdTile.Id, 100, "Адрес заказчика", createdTile.Price * 100));
            var createdReview = await _services.Reviews.CreateAsync(createdUser.Id, new(createdOrder.Id, "Ну вроде круто", "Реально неплохо, сервис хороший, мне понравилось", 10));


            JsonSerializerOptions options = new() { WriteIndented = true, Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic) };

            Console.WriteLine("Users");
            Console.WriteLine(JsonSerializer.Serialize(users, options: options));

            Console.WriteLine();
            Console.WriteLine("Orders");
            Console.WriteLine(JsonSerializer.Serialize(orders, options: options));

            Console.WriteLine();
            Console.WriteLine("Tiles");
            Console.WriteLine(JsonSerializer.Serialize(tiles, options: options));

            Console.WriteLine();
            Console.WriteLine("Reviews");
            Console.WriteLine(JsonSerializer.Serialize(reviews, options: options));

            Console.WriteLine();
            Console.WriteLine("Created user");
            Console.WriteLine(JsonSerializer.Serialize(createdUser, options: options));

            Console.WriteLine();
            Console.WriteLine("Created tile");
            Console.WriteLine(JsonSerializer.Serialize(createdTile, options: options));

            Console.WriteLine();
            Console.WriteLine("Created order");
            Console.WriteLine(JsonSerializer.Serialize(createdOrder, options: options));

            Console.WriteLine();
            Console.WriteLine("Created review");
            Console.WriteLine(JsonSerializer.Serialize(createdReview, options: options));
        }
    }
}
