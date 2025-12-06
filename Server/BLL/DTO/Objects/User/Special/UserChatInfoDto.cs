using BLL.DTO.Interfaces.Special;
using BLL.DTO.Interfaces.Update;
using BLL.DTO.Types.Enums;

namespace BLL.DTO.Objects.User.Special
{
    public class UserChatInfoDto : ISpecialDto<DAL.EfCore.Models.User>, IUpdateDto<DAL.EfCore.Models.User>
    {
        public int? Step { get; set; }

        public int? TileId { get; set; }

        public int? LastMessageId { get; set; }

        public DialogType? DialogType { get; set; }

        public void FromModel(DAL.EfCore.Models.User model)
        {
            Step = model.Step;
            TileId = model.TileId;
            LastMessageId = model.LastMessageId;
            DialogType = (DialogType)model.DialogType;
        }

        public void UpdateModel(DAL.EfCore.Models.User model)
        {
            if (Step.HasValue)
                model.Step = Step;

            if (TileId.HasValue)
                model.TileId = TileId;

            if (LastMessageId.HasValue)
                model.LastMessageId = LastMessageId;

            if (DialogType.HasValue)
                model.DialogType = (int)DialogType;
        }
    }
}
