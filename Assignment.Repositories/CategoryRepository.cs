using Assignment.Data;
using Assignment.Data.Entities;
using Assignment.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Assignment.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public async Task<bool> CategoryExist(long categoryId)
        {
            var count = await DbContext
                .Categories
                .CountAsync(c=>c.CategoryId == categoryId);
            return count > 0;
        }
    }
}
