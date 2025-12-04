namespace BLL.DTO.Interfaces.Special
{
    public interface ISpecialDto<T> where T : class
    {
        void FromModel(T model);
    }
}
