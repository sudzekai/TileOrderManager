using BLL.DTO.Interfaces.Main;
using BLL.DTO.Types.Enums;

namespace BLL.DTO.Objects.User.Main
{
    public class UserFullDto : IFullDto<DAL.EfCore.Models.User>
    {
        public long Id { get; set; }

        public string Username { get; set; } = null!;

        public string? FullName { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public UserRole? Role { get; set; }

        public DialogType? DialogType { get; set; }

        public int? Step { get; set; }

        public int? TileId { get; set; }

        public int? LastMessageId { get; set; }

        public void FromModel(DAL.EfCore.Models.User model)
        {
            Id = model.Id;
            Username = model.Username;
            FullName = model.FullName;
            Phone = model.Phone;
            Email = model.Email;
            Role = (UserRole)model.Role;
            DialogType = (DialogType)model.DialogType;
            Step = model.Step;
            TileId = model.TileId;
            LastMessageId = model.LastMessageId;
        }
    }
}
