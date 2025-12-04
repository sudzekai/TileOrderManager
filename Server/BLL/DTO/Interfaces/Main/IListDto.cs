using BLL.DTO.Interfaces.Special;

namespace BLL.DTO.Interfaces.Main
{
    public interface IListDto<T> : ISpecialDto<T> where T : class
    {
    }
}
