using BLL.DTO.Interfaces.Main;

namespace BLL.DTO.Objects.Review.Main
{
    public class ReviewListDto : IListDto<DAL.EfCore.Models.Review>
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public long UserId { get; set; }

        public string Title { get; set; }

        public void FromModel(DAL.EfCore.Models.Review model)
        {
            Title = model.Title;
            Id = model.Id;
            OrderId = model.OrderId;
            UserId = model.UserId;
        }
    }
}
