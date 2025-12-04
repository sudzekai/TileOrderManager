using BLL.DTO.Objects.Order.Create;
using BLL.DTO.Objects.Order.Main;
using BLL.DTO.Objects.Order.Update;
using Telegram.Bot.Types.Payments;

namespace TelegramBot.APIServices
{
    public class OrdersService
    {
        private readonly string _baseUrl = "http://localhost:5293/api/orders";

        HttpService _httpService;
        public OrdersService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<OrderListDto>> GetAllAsync()
            => await _httpService.GetAsync<List<OrderListDto>>(_baseUrl);

        public async Task<List<UserOrderListDto>> GetAllByUserIdAsync(long userId)
            => await _httpService.GetAsync<List<UserOrderListDto>>($"{_baseUrl}/user/{userId}");

        public async Task<List<TileOrderListDto>> GetAllByTileIdAsync(int tileId)
            => await _httpService.GetAsync<List<TileOrderListDto>>($"{_baseUrl}/tile/{tileId}");

        public async Task<OrderFullDto> GetByIdAsync(int id)
            => await _httpService.GetAsync<OrderFullDto>($"{_baseUrl}/{id}");

        public async Task<OrderInfo> GetInfoByIdAsync(int id)
            => await _httpService.GetAsync<OrderInfo>($"{_baseUrl}/{id}");

        public async Task<OrderFullDto> CreateAsync(long userId, OrderCreateDto order)
            => await _httpService.PostAsync<OrderFullDto>($"{_baseUrl}/user/{userId}/create", order);

        public async Task<bool> UpdateAsync(int id, MasterOrderUpdateDto order)
            => await _httpService.PutAsync($"{_baseUrl}/{id}/update", order);

        public async Task<bool> DeleteAsync(int id)
            => await _httpService.DeleteAsync($"{_baseUrl}/{id}/delete");

    }
}
