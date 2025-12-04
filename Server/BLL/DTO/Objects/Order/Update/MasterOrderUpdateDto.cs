using BLL.DTO.Interfaces.Update;
using BLL.DTO.Types.Enums;

namespace BLL.DTO.Objects.Order.Update
{
    public class MasterOrderUpdateDto : IUpdateDto<DAL.EfCore.Models.Order>
    {
        public OrderStatus? Status { get; set; }

        public void UpdateModel(DAL.EfCore.Models.Order model)
        {
            if (Status.HasValue)
                model.Status = (int)Status;
        }
    }
}
