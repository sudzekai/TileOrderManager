using BLL.DTO.Interfaces.Main;

namespace BLL.DTO.Objects.Review.Main
{
    public class UserReviewListDto : IListDto<DAL.EfCore.Models.Review>
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public void FromModel(DAL.EfCore.Models.Review model)
        {
            Id = model.Id;
            OrderId = model.OrderId;
        }
    }
}
