using Assignment.Infrastructure;
using Assignment.Infrastructure.DTOs;
using Assignment.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment.Api.Controllers
{
    /// <summary> Product Categories Controller</summary>
    [ApiController]
    [ApiVersion("2")]
    [Route("api/Categories")]
    public class CategoriesController : ControllerBase
    {
        protected internal readonly IUnitOfWork _unitOfWork;
        protected internal readonly IMapper _mapper;

        /// <summary> Controller CTOR </summary>
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>Get All Categories</summary>
        /// <remarks>To fill dropdown list of categories when entering a new product</remarks>
        /// <response code="200">Returns Categories according to filters entered </response>
        /// <response code="500">Server Error occured while fetching data</response> 
        [MapToApiVersion("2")]
        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        [ProducesResponseType(typeof(Response<List<CategoryDto>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
                var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
                return new Response<List<CategoryDto>>(categoriesDto,200);
            }
            catch(Exception ex)
            {
                return new Response<string>("Server Error Occured!, Please try again later",500);
            }
        }
    }
}
