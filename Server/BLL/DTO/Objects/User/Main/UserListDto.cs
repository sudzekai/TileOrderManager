using BLL.DTO.Interfaces.Main;
using BLL.DTO.Types.Enums;

namespace BLL.DTO.Objects.User.Main
{
    public class UserListDto : IListDto<DAL.EfCore.Models.User>
    {
        public long Id { get; set; }

        public DialogType DialogType { get; set; }

        public UserRole Role { get; set; }

        public void FromModel(DAL.EfCore.Models.User model)
        {
            Id = model.Id;
            DialogType = (DialogType)(model.DialogType);
            Role = (UserRole)(model.Role);
        }
    }
}
