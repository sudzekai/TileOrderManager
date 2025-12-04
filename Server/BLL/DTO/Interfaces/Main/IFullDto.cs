using BLL.DTO.Interfaces.Special;

namespace BLL.DTO.Interfaces.Main
{
    public interface IFullDto<T> : ISpecialDto<T> where T : class
    {
    }
}
