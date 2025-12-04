using DAL.EfCore.Models;

namespace DAL.EfCore.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order entity);

        Task<bool> DeleteAsync(int id);

        Task<List<Order>> GetAllAsync();

        Task<Order?> GetByIdAsync(int id);

        Task<List<Order>> GetByTileIdAsync(int id);

        Task<List<Order>> GetByUserIdAsync(long id);
    }
}
