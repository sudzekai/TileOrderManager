using BLL.DTO.Interfaces.Special;
using BLL.DTO.Interfaces.Update;

namespace BLL.DTO.Tools
{
    public static class DtoMapper
    {
        public static TDto ToDto<TModel, TDto>(this TModel model)
            where TModel : class
            where TDto : ISpecialDto<TModel>, new()
        {
            TDto dto = new();
            dto.FromModel(model);
            return dto;
        }

        public static TModel UpdateModel<TModel>(this TModel model, IUpdateDto<TModel> dto)
            where TModel : class
        {
            dto.UpdateModel(model);
            return model;
        }
    }
}
