using Assignment.Data.Entities;
using Assignment.Infrastructure.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment.Repositories.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        List<ProductDto> GetAllProduct(int pageNumber, int pageSize, long categoryId);
        Task<List<ProductDto>> GetProductByCategory(long categoryId);
        Task<ProductDto> GetProduct(long categoryId);
        Task<string> AddProduct(CreateProductDto dto);
        Task<string> EditProduct(long productId, CreateProductDto dto);
        Task<string> PatchEditProduct(long productId, JsonPatchDocument<PatchProductDto> model, ModelStateDictionary modelState);
        Task<string> DeleteProduct(long ProductId);
    }
}
