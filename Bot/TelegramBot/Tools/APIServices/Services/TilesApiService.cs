using BLL.DTO.Objects.Tile.Create;
using BLL.DTO.Objects.Tile.Main;
using BLL.DTO.Objects.Tile.Update;

namespace TelegramBot.Tools.APIServices.Services
{
    public class TilesApiService
    {
        private readonly string _baseUrl = "http://localhost:5293/api/tiles";

        HttpService _httpService;
        public TilesApiService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<TileListDto>> GetAllAsync(int page)
            => await _httpService.GetAsync<List<TileListDto>>($"{_baseUrl}?page={page}");

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
