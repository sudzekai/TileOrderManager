using DAL.EfCore.Data;
using DAL.EfCore.Models;
using DAL.EfCore.Repositories;
using DAL.EfCore.Repositories.Interfaces;
using DAL.EfCore.UOW.Interface;

namespace DAL.EfCore.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TileOrderManagerDbContext _context;

        public IOrderRepository Orders { get; }
        public IReviewRepository Reviews { get; }
        public IRepository<Tile> Tiles { get; }
        public IUserRepository Users { get; }

        public UnitOfWork(TileOrderManagerDbContext context)
        {
            _context = context;
            Orders = new OrderRepository(context);
            Reviews = new ReviewRepository(context);
            Tiles = new Repository<Tile>(context);
            Users = new UserRepository(context);
        }

        public async Task SaveChagesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
