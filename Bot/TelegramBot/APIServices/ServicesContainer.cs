using TelegramBot.AppErrorHandler;

namespace TelegramBot.APIServices
{
    public class ServicesContainer
    {
        public readonly OrdersService Orders;
        public readonly UsersService Users;
        public readonly ReviewsService Reviews;
        public readonly TilesService Tiles;

        public ServicesContainer(HttpClient httpClient)
        {
            HttpService httpService = new(httpClient);

            Orders = new(httpService);
            Users = new(httpService);
            Reviews = new(httpService);
            Tiles = new(httpService);

            BotLogger.InfoOutput("Сервисы подключены");
        }
    }
}
