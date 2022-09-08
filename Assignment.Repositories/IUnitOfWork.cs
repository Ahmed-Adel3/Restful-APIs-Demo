using Assignment.Repositories.IRepositories;
using System.Threading.Tasks;

namespace Assignment.Repositories
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IApplicationUserRepository ApplicationUserRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
