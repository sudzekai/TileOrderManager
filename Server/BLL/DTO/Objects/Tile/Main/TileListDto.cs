using BLL.DTO.Interfaces.Main;

namespace BLL.DTO.Objects.Tile.Main
{
    public class TileListDto : IListDto<DAL.EfCore.Models.Tile>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public void FromModel(DAL.EfCore.Models.Tile model)
        {
            Id = model.Id;
            Name = model.Name;
        }
    }
}
