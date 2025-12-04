using BLL.DTO.Interfaces.Special;
using BLL.DTO.Interfaces.Update;

namespace BLL.DTO.Objects.User.Special
{
    public class UserInfoDto : ISpecialDto<DAL.EfCore.Models.User>, IUpdateDto<DAL.EfCore.Models.User>
    {
        public string? FullName { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public void FromModel(DAL.EfCore.Models.User model)
        {
            FullName = model.FullName;
            Phone = model.Phone;
            Email = model.Email;
        }

        public void UpdateModel(DAL.EfCore.Models.User model)
        {
            if (!string.IsNullOrEmpty(FullName))
                model.FullName = FullName;

            if (!string.IsNullOrEmpty(Phone))
                model.Phone = Phone;

            if (!string.IsNullOrEmpty(Email))
                model.Email = Email;
        }
    }
}
