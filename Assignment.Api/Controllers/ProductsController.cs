using Assignment.Infrastructure;
using Assignment.Infrastructure.DTOs;
using Assignment.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment.Api.Controllers
{
    /// <summary> Product Categories Controller</summary>
    [ApiController]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        protected internal readonly IUnitOfWork _unitOfWork;

        /// <summary> Controller CTOR </summary>
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary> Get All Products [ First version ] </summary>
        /// <remarks> Let's assume first version only supporting filtering with product category </remarks>
        /// <param name="categoryId"> ID of the Category for filtering  </param>
        /// <response code="200">Returns Products according to filters entered </response>
        /// <response code="400">Client Side data error</response>
        /// <response code="404">If category is not found</response>
        /// <response code="500">ServerError occured while fetching data </response> 
        [MapToApiVersion("1")]
        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        [ProducesResponseType(typeof(Response<List<ProductDto>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<IActionResult> GetAllProductsV1([FromQuery] long categoryId)
        {
            try
            {
                if (categoryId < 0)
                    return new Response<string>("Please enter a valid category id", 400);

                if (categoryId > 0 && !await _unitOfWork.CategoryRepository.CategoryExist(categoryId))
                    return new Response<string>("Category doesn't exist", 404);

                var products = _unitOfWork.ProductRepository.GetAllProduct(0, 0, categoryId);

                return new Response<List<ProductDto>>(products, 200);
            }
            catch (Exception ex)
            {
                return new Response<string>("Server Error Occured!, Please try again later", 500);
            }
        }

        /// <summary> Get All Products [ Version 2 of the API ] </summary>
        /// <remarks> Get all products in system with paging </remarks>
        /// <param name="pageNumber"> Page Number for Paging </param>
        /// <param name="pageSize"> Number of records needed </param>
        /// <param name="categoryId"> ID of the Category for filtering  </param>
        /// <response code="200">Returns Products according to filters entered </response>
        /// <response code="400">Client Side data error</response>
        /// <response code="404">If category is not found</response>
        /// <response code="500">ServerError occured while fetching data </response> 
        [MapToApiVersion("2")]
        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        [ProducesResponseType(typeof(Response<List<ProductDto>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<IActionResult> GetAllProductsV2([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] long categoryId)
        {
            try
            {
                if (categoryId < 0)
                    return new Response<string>("Please enter a valid category id", 400);

                if (categoryId > 0 && !await _unitOfWork.CategoryRepository.CategoryExist(categoryId))
                    return new Response<string>("Category doesn't exist", 404);

                if ((pageNumber > 0 && pageSize <= 0) || (pageNumber <= 0 && pageSize > 0))
                    return new Response<string>("Please enter correct values for Page Number or Page size or leave them empty to get all products in system", 400);

                var products = _unitOfWork.ProductRepository.GetAllProduct(pageNumber, pageSize, categoryId);

                return new Response<List<ProductDto>>(products, 200);
            }
            catch (Exception ex)
            {
                return new Response<string>("Server Error Occured!, Please try again later", 500);
            }
        }

        /// <summary> Get Product By ID </summary>
        /// <remarks> Get the product matching a specific ID </remarks>
        /// <param name="id">Id of the product needed </param>
        /// <response code="200">Returns Products according to filters entered </response>
        /// <response code="400">Client Side data error</response>
        /// <response code="404">Product is not found</response>
        /// <response code="500">ServerError occured while fetching data </response> 
        [MapToApiVersion("2")]
        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        [ProducesResponseType(typeof(Response<ProductDto>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<IActionResult> GetProductById([FromRoute] long id)
        {
            try
            {
                if (id <= 0)
                    return new Response<string>("Please enter a valid value for product ID", 400);

                var product = await _unitOfWork.ProductRepository.GetProduct(id);

                if (product is null)
                    return new Response<string>("Product Can't be found", 404);

                return new Response<ProductDto>(product, 200);
            }
            catch (Exception ex)
            {
                return new Response<string>("Server Error Occured!, Please try again later", 500);
            }
        }

        /// <summary>Add a new product [ Requires Authorization ]</summary>
        /// <remarks>Add new product to the system</remarks>
        /// <param name="model"></param>  
        /// <response code="200">Returns product added successfully message</response>
        /// <response code="400">Client Side data error</response>
        /// <response code="401">Authentication Error</response>
        /// <response code="500">ServerError occured while fetching data </response> 
        [MapToApiVersion("2")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductDto model)
        {

            try
            {
                if (!ModelState.IsValid)
                    return new Response<string>(ModelState, 400);

                var res = await _unitOfWork.ProductRepository.AddProduct(model);
                if (res != "Success")
                    return new Response<string>(res, 400);

                return new Response<string>("Product Added Successfully", 201);
            }
            catch (Exception)
            {
                return new Response<string>("Server Error Occured!, Please try again later", 500);
            }
        }

        /// <summary>Update product [ Requires Authorization ]</summary>
        /// <remarks>Update product to the system</remarks>
        /// <param name="id">Product ID to be edited</param>
        /// <param name="model"> CreateProductDto Object Model </param>  
        /// <response code="204">Returns product updated successfully message</response>
        /// <response code="400">Client Side data error</response>
        /// <response code="401">Authentication Error</response>
        /// <response code="500">ServerError occured while fetching data </response> 
        [MapToApiVersion("2")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("{id}")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> EditProduct([FromRoute] long id, [FromForm] CreateProductDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new Response<string>(ModelState, 400);

                var res = await _unitOfWork.ProductRepository.EditProduct(id, model);
                if (res != "Success")
                    return new Response<string>(res, 400);

                return new Response<string>("Product Edited Successfully", 200);
            }
            catch (Exception)
            {
                return new Response<string>("Server Error Occured!, Please try again later", 500);
            }
        }

        /// <summary>Update product [ Requires Authorization ]</summary>
        /// <remarks>Update product to the system</remarks>
        /// <param name="id">Product ID to be edited</param>
        /// <param name="model">JSON Patch Document</param>  
        /// <response code="204">Returns product updated successfully message</response>
        /// <response code="400">Client Side data error</response>
        /// <response code="401">Authentication Error</response>
        /// <response code="500">ServerError occured while editing item </response> 
        [MapToApiVersion("2")]
        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("{id}")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> EditProduct([FromRoute] long id, [FromBody] JsonPatchDocument<PatchProductDto> model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new Response<string>(ModelState, 400);

                var res = await _unitOfWork.ProductRepository.PatchEditProduct(id, model, ModelState);

                if (res == "Model State Error")
                    return new Response<string>(ModelState, 400);

                else if (res != "Success")
                    return new Response<string>(res, 400);

                return new Response<string>("Product Edited Successfully", 200);
            }
            catch (Exception)
            {
                return new Response<string>("Server Error Occured!, Please try again later", 500);
            }
        }

        /// <summary>Delete product [ Requires Authorization ]</summary>
        /// <remarks>Delete product from the system</remarks>
        /// <param name="id">Product Id to be deleted</param>
        /// <response code="204">Returns product deleted successfully message</response>
        /// <response code="400">Client Side data error</response>
        /// <response code="401">Authentication Error</response>
        /// <response code="500">ServerError occured while deleting data </response> 
        [MapToApiVersion("2")]
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("{id}")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> DeleteProduct([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new Response<string>(ModelState, 400);

                var res = await _unitOfWork.ProductRepository.DeleteProduct(id);

                if (res != "Success")
                    return new Response<string>(res, 400);

                return new Response<string>("Product Deleted Successfully", 200);
            }
            catch (Exception)
            {
                return new Response<string>("Server Error Occured!, Please try again later", 500);
            }
        }

    }
}
