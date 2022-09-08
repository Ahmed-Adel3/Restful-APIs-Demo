using Assignment.Data;
using Assignment.Data.Entities;
using Assignment.Infrastructure.Options;
using Assignment.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.WebApiViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Repositories
{
    class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
    {
        protected internal readonly IMapper _mapper;
        protected internal readonly UserManager<ApplicationUser> _userManager;
        protected internal readonly IConfiguration _config;

        public ApplicationUserRepository(ApplicationContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager,IConfiguration config)
        : base(dbContext)
        {
            _mapper = mapper;
            _config = config;
            _userManager = userManager;
        }

        public async Task<LoginResponse> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return null;

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                return null;

            var loginRes = _mapper.Map<LoginResponse>(user);
            loginRes.Token = BuildToken(user);
            return loginRes;
        }

        private string BuildToken(ApplicationUser user)
        {
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var jwtConfig = _config.GetSection("Jwt").Get<JwtOptions>();
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwtConfig.Issuer,
                jwtConfig.Issuer,
                claims,
                expires: DateTime.Now.AddMinutes(double.Parse(jwtConfig.ExpiryTime)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> RegisterAsync(RegisterDto model)
        {
            var user = _mapper.Map<ApplicationUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);
            if (result.Succeeded)
                return "Success";

            foreach (var error in result.Errors)
            {
                if (error.Code == "DuplicateUserName")
                    return "This e-mail has been registered before";

                if (error.Code == "PasswordTooShort")
                    return "Password is too short";
            }
            return "Some error occurred, please try again later";
        }
    }
}
