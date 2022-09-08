using Assignment.Data.Entities;
using Models.WebApiViewModels;
using System.Threading.Tasks;

namespace Assignment.Repositories.IRepositories
{
    public interface IApplicationUserRepository : IGenericRepository<ApplicationUser>
    {
        Task<LoginResponse> LoginAsync(LoginDto model);
        Task<string> RegisterAsync(RegisterDto model);
    }
}
