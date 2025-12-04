using BLL.DTO.Interfaces.Create;

namespace BLL.DTO.Objects.Tile.Create
{
    public class TileCreateDto(string name, decimal price, string? description = null) : ICreateDto<DAL.EfCore.Models.Tile>
    {
        public string Name { get; set; } = name;

        public decimal Price { get; set; } = price;

        public string? Description { get; set; } = description;

        public DAL.EfCore.Models.Tile ToModel()
            => new()
            {
                Name = Name,
                Price = Price,
                Description = Description
            };
    }
}
