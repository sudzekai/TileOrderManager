using BLL.DTO.Interfaces.Main;
using BLL.DTO.Types.Enums;

namespace BLL.DTO.Objects.Order.Main
{
    public class TileOrderListDto : OrderListBase, IListDto<DAL.EfCore.Models.Order>
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public OrderStatus Status { get; set; }

        public void FromModel(DAL.EfCore.Models.Order model)
        {
            Id = model.Id;
            UserId = model.UserId;
            Status = (OrderStatus)model.Status;
        }
    }
}
