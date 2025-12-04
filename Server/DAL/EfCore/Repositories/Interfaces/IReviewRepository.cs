using DAL.EfCore.Models;

namespace DAL.EfCore.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task AddAsync(Review entity);

        Task<bool> DeleteAsync(int id);

        Task<List<Review>> GetAllAsync();

        Task<Review?> GetByIdAsync(int id);

        Task<List<Review>> GetAllByUserIdAsync(long id);
    }
}
