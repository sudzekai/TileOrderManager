using TelegramBot.Tools.APIServices.Services;
using TelegramBot.Tools.AppErrorHandler;

namespace TelegramBot.Tools.APIServices
{
    public class ApiServicesContainer
    {
        public readonly OrdersApiService Orders;
        public readonly UsersApiService Users;
        public readonly ReviewsApiService Reviews;
        public readonly TilesApiService Tiles;

        public ApiServicesContainer(HttpClient httpClient)
        {
            HttpService httpService = new(httpClient);

            Orders = new(httpService);
            Users = new(httpService);
            Reviews = new(httpService);
            Tiles = new(httpService);

            BotLogger.SendInfo("Сервисы подключены");
        }

        private async Task Fire()
        {
            await Orders.GetAllAsync(1);
            await Users.GetAllAsync(1);
            await Reviews.GetAllAsync(1);
            await Tiles.GetAllAsync(1);
        }
    }
}
