namespace BLL.DTO.Interfaces.Update
{
    public interface IUpdateDto<T> where T : class
    {
        void UpdateModel(T model);
    }
}
