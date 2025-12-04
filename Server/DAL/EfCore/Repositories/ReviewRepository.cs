using DAL.EfCore.Data;
using DAL.EfCore.Models;
using DAL.EfCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.EfCore.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        protected readonly DbSet<Review> _dbSet;

        public ReviewRepository(TileOrderManagerDbContext context)
        {
            _dbSet = context.Set<Review>();
        }

        public async Task AddAsync(Review entity)
            => await _dbSet.AddAsync(entity);

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            return true;
        }

        public async Task<List<Review>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public async Task<Review?> GetByIdAsync(int id)
            => await _dbSet.FindAsync(id);

        public async Task<List<Review>> GetAllByUserIdAsync(long id)
            => [.. _dbSet.Where(r => r.UserId == id)];
    }
}
