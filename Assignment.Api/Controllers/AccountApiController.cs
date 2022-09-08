using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Models.WebApiViewModels;
using Microsoft.AspNetCore.Mvc;
using Assignment.Data.Entities;
using Assignment.Repositories;
using Assignment.Infrastructure;


namespace Assignment.Api.Controllers
{
    /// <summary> Account Categories Controller</summary>
    [ApiController]
    [ApiVersion("2")]
    [Route("api/Account")]
    public class AccountApiController : ControllerBase
    {
        protected internal readonly IUnitOfWork _unitOfWork;
        protected internal readonly UserManager<ApplicationUser> _userManager;
        protected internal readonly IConfiguration _config;

        /// <summary> Controller CTOR </summary>
        public AccountApiController(UserManager<ApplicationUser> userManager, IConfiguration config, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _config = config;
        }


        /// <summary> Normal Login - Anonymous API </summary>
        /// <remarks>Login with credentials </remarks>
        /// <param name="model"></param>  
        /// <response code="200">Returns User data and access token</response>
        /// <response code="400">If login failed</response> 
        /// <response code="500">Server Error</response> 
        [MapToApiVersion("2")]
        [HttpGet]
        [AllowAnonymous]
        [Route("Login")]
        [ProducesResponseType(typeof(Response<LoginResponse>),200)]
        [ProducesResponseType(typeof(Response<string>),400)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<IActionResult> Login([FromQuery]LoginDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new Response<string>(ModelState, 400);

                var res = await _unitOfWork.ApplicationUserRepository.LoginAsync(model);
                if (res == null)
                    return new Response<string>("Wrong username or password", 400);

                return new Response<LoginResponse>(res, 200);
            }
            catch (Exception ex)
            {
                return new Response<string>("Server Error Occured!, Please try again later", 500);
            }
        }


        /// <summary> Normal Register - Anonymous API </summary>
        /// <remarks> New user to be added </remarks>
        /// <param name="model"></param>  
        /// <response code="201">Returns Message to check email </response>
        /// <response code="400">If register failed</response> 
        /// <response code="500">Server Error</response> 
        [MapToApiVersion("2")]
        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        [ProducesResponseType(typeof(Response<string>), 201)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            try
            {
                if (!ModelState.IsValid) 
                    return new Response<string>(ModelState,400);

                var res = await _unitOfWork.ApplicationUserRepository.RegisterAsync(model);
                if( res == "Success")
                    return new Response<string>("Account created successfully",201);

                else
                    return new Response<string>(res,400);
            }
            catch (Exception ex)
            {
                return new Response<string>(ex.Message,500);
            }
        }
    }
}