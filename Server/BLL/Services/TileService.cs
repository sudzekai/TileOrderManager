using BLL.DTO.Interfaces.Create;
using BLL.DTO.Interfaces.Update;
using BLL.DTO.Objects.Tile.Main;
using BLL.DTO.Tools;
using BLL.Services.Interfaces;
using DAL.EfCore.Models;
using DAL.EfCore.UOW.Interface;

namespace BLL.Services
{
    public class TileService : ITileService
    {
        private readonly IUnitOfWork _uow;

        public TileService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<TileFullDto> CreateTileAsync(ICreateDto<Tile> tile)
        {
            var createdTile = tile.ToModel();

            await _uow.Tiles.AddAsync(createdTile);
            await _uow.SaveChagesAsync();

            return createdTile.ToDto<Tile, TileFullDto>();
        }

        public async Task<bool> DeleteTileAsync(int id)
        {
            var result = await _uow.Tiles.DeleteAsync(id);

            if (!result)
                throw new Exception($"Брусчатка с таким Id {id} не найдена");

            await _uow.SaveChagesAsync();
            return true;
        }

        public async Task<TileFullDto> GetByIdAsync(int id)
        {
            var tile = await _uow.Tiles.GetByIdAsync(id);

            return tile == null ? throw new Exception($"Брусчатка с таким Id {id} не найдена")
                                : tile.ToDto<Tile, TileFullDto>();
        }

        public async Task<List<TileListDto>> GetTilesAsync()
        {
            var tiles = await _uow.Tiles.GetAllAsync();

            return [.. tiles.Select(t => t.ToDto<Tile, TileListDto>())];
        }

        public async Task<bool> UpdateTileAsync(int id, IUpdateDto<Tile> tile)
        {

            var foundTile = await _uow.Tiles.GetByIdAsync(id) ?? throw new Exception($"Брусчатка с таким Id {id} не найдена");

            foundTile.UpdateModel(tile);
            await _uow.SaveChagesAsync();

            return true;
        }
    }
}
