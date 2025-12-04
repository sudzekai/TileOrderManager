using DAL.EfCore.Models;
using DAL.EfCore.Repositories.Interfaces;

namespace DAL.EfCore.UOW.Interface
{
    public interface IUnitOfWork
    {
        IOrderRepository Orders { get; }
        IReviewRepository Reviews { get; }
        IRepository<Tile> Tiles { get; }
        IUserRepository Users { get; }

        Task SaveChagesAsync();
    }
}
