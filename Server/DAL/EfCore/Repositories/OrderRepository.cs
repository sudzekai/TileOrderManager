using DAL.EfCore.Data;
using DAL.EfCore.Models;
using DAL.EfCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.EfCore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        protected readonly DbSet<Order> _dbSet;

        public OrderRepository(TileOrderManagerDbContext context)
        {
            _dbSet = context.Set<Order>();
        }

        public async Task AddAsync(Order entity)
            => await _dbSet.AddAsync(entity);

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            return true;
        }

        public async Task<List<Order>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public async Task<Order?> GetByIdAsync(int id)
            => await _dbSet.FindAsync(id);

        public async Task<List<Order>> GetByTileIdAsync(int id)
            => await _dbSet.Where(o => o.TileId == id).ToListAsync();

        public async Task<List<Order>> GetByUserIdAsync(long id)
            => await _dbSet.Where(o => o.UserId == id).ToListAsync();
    }
}
