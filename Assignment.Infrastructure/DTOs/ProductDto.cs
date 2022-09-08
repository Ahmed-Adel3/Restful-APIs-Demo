using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Infrastructure.DTOs
{
    /// <summary> Base model for all product models</summary>
    public class ProductDtoBase
    {
        /// <summary> Name of the Product </summary>
        [Required(ErrorMessage = "Product Name is required")]
        [MaxLength(100, ErrorMessage = "Name can't exceed 100 characters")]
        public string Name { get; set; }

        /// <summary> Price of the Product </summary>
        [Required(ErrorMessage = "Product Price is required")]
        public decimal Price { get; set; }

        /// <summary> Quantity of the Product </summary>
        [Required(ErrorMessage = "Product Quantity is required")]
        public int Quantity { get; set; }
    }

    /// <summary> Response model for Get All Products or Get Product details</summary>
    public class ProductDto : ProductDtoBase
    {
        /// <summary> Product Id</summary>
        public long ProductId { get; set; }

        /// <summary> Image URL</summary>
        public string ImageUrl { get; set; }

        /// <summary> category Object</summary>
        public CategoryDto Category { get; set; }
    }

    /// <summary> Model for create and edit Product</summary>
    public class CreateProductDto : ProductDtoBase
    {
        /// <summary> Image of the Product - IFormFile </summary>
        [Required(ErrorMessage = "Product Image is required")]
        public IFormFile Image { get; set; }

        /// <summary> Category of the Product </summary>
        [Required(ErrorMessage = "Product Category is required")]
        public long CategoryId { get; set; }
    }


    /// <summary> Model for Patch edititng Product</summary>
    public class PatchProductDto : ProductDtoBase
    {
        /// <summary> Category of the Product </summary>
        [Required(ErrorMessage = "Product Category is required")]
        public long CategoryId { get; set; }
    }

}
