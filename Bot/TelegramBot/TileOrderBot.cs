using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Telegram.Bot;
using TelegramBot.APIServices;
using TelegramBot.AppErrorHandler;

namespace TelegramBot
{
    public class TileOrderBot
    {
        private readonly TelegramBotClient _bot;

        private readonly ServicesContainer service;

        public TileOrderBot(string token)
        {
            BotLogger.InitializeErrorHandler();

            _bot = new TelegramBotClient(token);

            service = new(new());

            TestApi().Wait();
        }

        public async Task TestApi()
        {
            var users = await service.Users.GetAllAsync();
            var orders = await service.Orders.GetAllAsync();
            var reviews = await service.Reviews.GetAllAsync();
            var tiles = await service.Tiles.GetAllAsync();

            var createdUser = await service.Users.CreateAsync(new(543226, "verycoolusername"));
            var createdTile = await service.Tiles.CreateAsync(new("Крутая брусчатка", 1000, "Крутое описание"));
            var createdOrder = await service.Orders.CreateAsync(createdUser.Id, new(createdTile.Id, 100, "Адрес заказчика", createdTile.Price * 100));
            var createdReview = await service.Reviews.CreateAsync(createdUser.Id, new(createdOrder.Id, "Ну вроде круто", "Реально неплохо, сервис хороший, мне понравилось", 10));


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
