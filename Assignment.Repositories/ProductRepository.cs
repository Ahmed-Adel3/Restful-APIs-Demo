using Assignment.Data;
using Assignment.Data.Entities;
using Assignment.Infrastructure.DTOs;
using Assignment.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        protected internal readonly IMapper _mapper;
        protected internal readonly ICategoryRepository _categoryRepo;
        public ProductRepository(ApplicationContext dbContext, IMapper mapper, ICategoryRepository categoryRepo)
          : base(dbContext)
        {
            _mapper = mapper;
            _categoryRepo = categoryRepo;
        }
        public List<ProductDto> GetAllProduct(int pageNumber, int pageSize, long categoryId)
        {
            var products = GetAll();

            if (categoryId > 0)
                products = products.Where(p => p.Category.CategoryId == categoryId);
            
            if(pageNumber !=0 && pageSize !=0 )
                products = products.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            products = products.Include(a=>a.Category);

            return _mapper.Map<List<ProductDto>>(products.ToList());

        }
        public async Task<ProductDto> GetProduct(long productId)
        {
            var product = FindBy(a=>a.ProductId == productId).Include(a=>a.Category).FirstOrDefault();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<List<ProductDto>> GetProductByCategory(long categoryId)
        {
            var products = await FindByAsnc(a => a.Category.CategoryId == categoryId);
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<string> AddProduct(CreateProductDto dto)
        {
            try
            {
                if (!await _categoryRepo.CategoryExist(dto.CategoryId))
                    return "Please enter a valid value for Category ID";

                string imagePath = UploadImage(dto.Image);

                var product = _mapper.Map<Product>(dto);
                product.ImageUrl = "/Uploads/" + imagePath;

                await AddAsync(product);
                return "Success";
            }
            catch(Exception ex)
            {
                return "An error occrured while inserting new product, please try again later";
            }
        }

        public async Task<string> EditProduct(long ProductId,CreateProductDto dto)
        {
            try
            {
                if (!await _categoryRepo.CategoryExist(dto.CategoryId))
                    return "Please enter a valid value for Category ID";

                var oldProduct = FindBy(f=>f.ProductId == ProductId).FirstOrDefault();
                if (oldProduct is null)
                    return "Product does not exist";

                DeleteImage(oldProduct.ImageUrl);

                string imagePath = UploadImage(dto.Image);

                var product = _mapper.Map<Product>(dto);
                product.ProductId = ProductId;
                product.ImageUrl = "/Uploads/" + imagePath;

                await EditAsync(product, ProductId);
                return "Success";
            }
            catch (Exception ex)
            {
                return "An error occrured while updating product, please try again later";
            }
        }

        public async Task<string> DeleteProduct(long ProductId)
        {
            try
            {
                var product = FindBy(f => f.ProductId == ProductId).FirstOrDefault();
                if (product is null)
                    return "Product Not Found";

                DeleteImage(product.ImageUrl);
                await DeleteAsync(product);
                return "Success";
            }
            catch (Exception ex)
            {
                return "An error occrured while deleting product, please try again later";
            }
        }

        private string UploadImage (IFormFile imageName)
        {
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads");
            string fileName = DateTime.Now.Ticks + "_" + Path.GetFileName(imageName.FileName);
            using (FileStream stream = new FileStream(Path.Combine(pathToSave, fileName), FileMode.Create))
                imageName.CopyTo(stream);
            return fileName;
        }

        private void DeleteImage(string imagePath)
        {
            if (imagePath is null)
                return;

            FileInfo file = new FileInfo(imagePath);
            if (file.Exists)
            {
                file.Delete();
            }
        }

        public async Task<string> PatchEditProduct(long productId, JsonPatchDocument<PatchProductDto> model, ModelStateDictionary modelState)
        {
            try
            {
                var product = FindBy(f => f.ProductId == productId).FirstOrDefault();

                var productDto = _mapper.Map<PatchProductDto>(product);

                model.ApplyTo(productDto, modelState);

                if (!await _categoryRepo.CategoryExist(productDto.CategoryId))
                   return "Please enter a valid value for Category ID";

                if (!modelState.IsValid)
                    return "Model State Error";

                _mapper.Map(productDto, product);
                product.ProductId = productId;
                await EditAsync(product,productId);
                return "Success";
            }
            catch (Exception ex)
            {
                return "An error occrured while updating product, please try again later";
            }
        }
    }
}
