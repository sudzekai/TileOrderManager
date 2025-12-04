using BLL.DTO.Objects.Order.Create;
using BLL.DTO.Objects.Order.Main;
using BLL.DTO.Objects.Order.Update;
using BLL.DTO.Objects.Tile.Create;
using BLL.DTO.Objects.Tile.Main;
using BLL.DTO.Objects.Tile.Update;
using Telegram.Bot.Types.Payments;

namespace TelegramBot.APIServices
{
    public class TilesService
    {
        private readonly string _baseUrl = "http://localhost:5293/api/tiles";

        HttpService _httpService;
        public TilesService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<TileListDto>> GetAllAsync()
            => await _httpService.GetAsync<List<TileListDto>>(_baseUrl);

        public async Task<TileFullDto> GetByIdAsync(int id)
            => await _httpService.GetAsync<TileFullDto>($"{_baseUrl}/{id}");

        public async Task<TileFullDto> CreateAsync(TileCreateDto tile)
            => await _httpService.PostAsync<TileFullDto>($"{_baseUrl}/create", tile);

        public async Task<bool> UpdateAsync(int id, AdminTileUpdateDto tile)
            => await _httpService.PutAsync($"{_baseUrl}/{id}/update", tile);

        public async Task<bool> DeleteAsync(int id)
            => await _httpService.DeleteAsync($"{_baseUrl}/{id}/delete");
    }
}
