using BLL.DTO.Interfaces.Main;

namespace BLL.DTO.Objects.Review.Main
{
    public class ReviewFullDto : IFullDto<DAL.EfCore.Models.Review>
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public long UserId { get; set; }

        public string Title { get; set; } = null!;

        public string Text { get; set; } = null!;

        public int Grade { get; set; }

        public void FromModel(DAL.EfCore.Models.Review model)
        {
            Id = model.Id;
            OrderId = model.OrderId;
            UserId = model.UserId;
            Title = model.Title;
            Text = model.Text;
            Grade = model.Grade;
        }
    }
}
