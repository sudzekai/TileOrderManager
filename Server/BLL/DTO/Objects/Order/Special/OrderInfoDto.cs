using BLL.DTO.Interfaces.Special;
using BLL.DTO.Types.Enums;

namespace BLL.DTO.Objects.Order.Special
{
    public class OrderInfoDto : ISpecialDto<DAL.EfCore.Models.Order>
    {
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }
        public string Address { get; set; } = null!;
        public OrderStatus OrderStatus { get; set; }

        public void FromModel(DAL.EfCore.Models.Order model)
        {
            OrderStatus = (OrderStatus)model.Status;
            Amount = model.Amount;
            TotalPrice = model.TotalPrice;
            Address = model.Address;
        }
    }
}
