using BLL.DTO.Interfaces.Update;

namespace BLL.DTO.Objects.Tile.Update
{
    public class AdminTileUpdateDto : IUpdateDto<DAL.EfCore.Models.Tile>
    {
        public decimal? Price { get; set; }

        public string? Description { get; set; }

        public void UpdateModel(DAL.EfCore.Models.Tile model)
        {
            model.Price = Price ?? model.Price;

            if (!string.IsNullOrEmpty(Description))
                model.Description = Description;
        }
    }
}
