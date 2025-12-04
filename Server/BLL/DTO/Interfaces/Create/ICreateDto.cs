namespace BLL.DTO.Interfaces.Create
{
    public interface ICreateDto<T> where T : class
    {
        T ToModel();
    }
}
