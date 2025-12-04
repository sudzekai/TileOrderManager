using DAL.EfCore.Models;

namespace DAL.EfCore.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(long id);
        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
        Task<bool> DeleteAsync(long id);
    }
}
