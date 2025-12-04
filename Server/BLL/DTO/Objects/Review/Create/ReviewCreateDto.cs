using BLL.DTO.Interfaces.Create;

namespace BLL.DTO.Objects.Review.Create
{
    public class ReviewCreateDto(int orderId, string title, string text, int grade) : ICreateDto<DAL.EfCore.Models.Review>
    {
        public int OrderId { get; set; } = orderId;

        public string Title { get; set; } = title;

        public string Text { get; set; } = text;

        public int Grade { get; set; } = grade;

        public DAL.EfCore.Models.Review ToModel()
            => new()
            {
                OrderId = this.OrderId,
                Title = this.Title,
                Text = this.Text,
                Grade = this.Grade
            };
    }
}
