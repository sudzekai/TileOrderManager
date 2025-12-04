using BLL.DTO.Objects.Tile.Create;
using BLL.DTO.Objects.Tile.Update;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TilesController : ControllerBase
    {
        private readonly ITileService _service;

        public TilesController(ITileService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetTiles()
        {
            try
            {
                var tiles = await _service.GetTilesAsync();
                return Ok(tiles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTileFull(int id)
        {
            try
            {
                var tile = await _service.GetTileFullByIdAsync(id);
                return Ok(tile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> PostTile(TileCreateDto tile)
        {
            try
            {
                var createdTile = await _service.CreateTileAsync(tile);
                return Ok(createdTile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> PutTile(int id, AdminTileUpdateDto tile)
        {
            try
            {
                var result = await _service.UpdateTileAsync(id, tile);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteTile(int id)
        {
            try
            {
                var result = await _service.DeleteTileAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}