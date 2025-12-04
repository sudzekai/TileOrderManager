using BLL.DTO.Interfaces.Main;

namespace BLL.DTO.Objects.Order.Main
{
    public class OrderFullDto : IFullDto<DAL.EfCore.Models.Order>
    {
        public int Id { get; set; }

        public long UserId { get; set; }

        public int TileId { get; set; }

        public int Amount { get; set; }

        public string Address { get; set; } = null!;

        public int Status { get; set; }

        public decimal TotalPrice { get; set; }

        public void FromModel(DAL.EfCore.Models.Order model)
        {
            Id = model.Id;
            UserId = model.UserId;
            TileId = model.TileId;
            Amount = model.Amount;
            Address = model.Address;
            Status = model.Status;
            TotalPrice = model.TotalPrice;
        }
    }
}
