using BLL.DTO.Interfaces.Special;

namespace BLL.DTO.Objects.Review.Special
{
    public class ReviewInfoDto : ISpecialDto<DAL.EfCore.Models.Review>
    {
        public string Title { get; set; } = null!;

        public string Text { get; set; } = null!;

        public int Grade { get; set; }

        public void FromModel(DAL.EfCore.Models.Review model)
        {
            Title = model.Title;
            Text = model.Text;
            Grade = model.Grade;
        }
    }
}
