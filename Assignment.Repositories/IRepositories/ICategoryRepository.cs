using Assignment.Data.Entities;
using System.Threading.Tasks;

namespace Assignment.Repositories.IRepositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<bool> CategoryExist(long categoryId);
    }
}
