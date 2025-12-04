using BLL.DTO.Interfaces.Main;

namespace BLL.DTO.Objects.Tile.Main
{
    public class TileFullDto : IFullDto<DAL.EfCore.Models.Tile>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public void FromModel(DAL.EfCore.Models.Tile model)
        {
            Id = model.Id;
            Name = model.Name;
            Price = model.Price;
            Description = model.Description;
        }
    }
}
