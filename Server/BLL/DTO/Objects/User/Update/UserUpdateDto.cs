using BLL.DTO.Interfaces.Update;
using BLL.DTO.Types.Enums;

namespace BLL.DTO.Objects.User.Update
{
    public class UserUpdateDto : IUpdateDto<DAL.EfCore.Models.User>
    {
        public UserRole? Role { get; set; }

        public DialogType? DialogType { get; set; }

        public string? Username { get; set; }

        public void UpdateModel(DAL.EfCore.Models.User model)
        {
            if (Role.HasValue)
                model.Role = (int)Role;

            if (DialogType.HasValue)
                model.DialogType = (int)DialogType;

            if (!string.IsNullOrWhiteSpace(Username))
                model.Username = Username;
        }
    }
}
