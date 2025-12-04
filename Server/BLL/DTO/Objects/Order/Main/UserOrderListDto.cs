using BLL.DTO.Interfaces.Main;
using BLL.DTO.Types.Enums;

namespace BLL.DTO.Objects.Order.Main
{
    public class UserOrderListDto : IListDto<DAL.EfCore.Models.Order>
    {
        public int Id { get; set; }
        public int TileId { get; set; }
        public OrderStatus Status { get; set; }

        public void FromModel(DAL.EfCore.Models.Order model)
        {
            Id = model.Id;
            TileId = model.TileId;
            Status = (OrderStatus)model.Status;
        }
    }
}
