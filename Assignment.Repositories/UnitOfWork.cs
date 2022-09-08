using Assignment.Data;
using Assignment.Data.Entities;
using Assignment.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Repositories
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public IProductRepository _productRepository { get; set; }
        public ICategoryRepository _categoryRepository { get; set; }
        public IApplicationUserRepository _applicationUserRepository { get; set; }
        public IMapper _mapper{ get; set; }
        protected UserManager<ApplicationUser> _userManager;
        protected IConfiguration _config;

        public UnitOfWork(ApplicationContext context, IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
            _userManager = userManager;
        }

        public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(_context,_mapper, CategoryRepository);
        public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(_context);
        public IApplicationUserRepository ApplicationUserRepository => _applicationUserRepository ?? new ApplicationUserRepository(_context, _mapper,_userManager,_config);

        public void SaveChanges()
        {
            bool saveFailed;
            do
            {
                saveFailed = false;

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    saveFailed = true;
                    ex.Entries.Single().Reload();
                }

            } while (saveFailed);
        }

        public async Task SaveChangesAsync()
        {
            bool saveFailed;
            do
            {
                saveFailed = false;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    saveFailed = true;
                    ex.Entries.Single().Reload();
                }

            } while (saveFailed);
        }

    }
}
