using BLL.DTO.Interfaces.Create;
using BLL.DTO.Interfaces.Update;
using BLL.DTO.Objects.Tile.Main;
using DAL.EfCore.Models;

namespace BLL.Services.Interfaces
{
    public interface ITileService
    {
        Task<List<TileListDto>> GetTilesAsync();

        Task<TileFullDto> GetTileFullByIdAsync(int id);

        Task<TileFullDto> CreateTileAsync(ICreateDto<Tile> tile);

        Task<bool> UpdateTileAsync(int id, IUpdateDto<Tile> tile);

        Task<bool> DeleteTileAsync(int id);
    }
}
