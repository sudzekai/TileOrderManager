using DAL.EfCore.Data;
using DAL.EfCore.Models;
using DAL.EfCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.EfCore.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly DbSet<User> _users;

        public UserRepository(TileOrderManagerDbContext context)
        {
            _users = context.Users;
        }

        public async Task AddAsync(User user)
            => await _users.AddAsync(user);

        public async Task<bool> DeleteAsync(long id)
        {
            var user = await _users.FindAsync(id);

            if (user != null)
            {
                _users.Remove(user);
                return true;
            }

            return false;
        }

        public async Task<List<User>> GetAllAsync()
            => await _users.ToListAsync();

        public async Task<User?> GetByIdAsync(long id)
            => await _users.FindAsync(id);

    }
}
