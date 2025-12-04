using BLL.DTO.Interfaces.Create;
using BLL.DTO.Types.Enums;

namespace BLL.DTO.Objects.Order.Create
{
    public class OrderCreateDto(int tileId, int amount, string address, decimal totalPrice, OrderStatus status = OrderStatus.Pending) : ICreateDto<DAL.EfCore.Models.Order>
    {
        public int TileId { get; set; } = tileId;

        public int Amount { get; set; } = amount;

        public string Address { get; set; } = address;

        public decimal TotalPrice { get; set; } = totalPrice;

        public OrderStatus Status { get; set; } = status;

        public DAL.EfCore.Models.Order ToModel()
            => new()
            {
                TileId = TileId,
                Amount = Amount,
                Address = Address,
                Status = (int)Status,
                TotalPrice = TotalPrice
            };
    }
}
