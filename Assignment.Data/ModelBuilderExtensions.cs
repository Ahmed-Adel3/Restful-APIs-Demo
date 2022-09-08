using Assignment.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Data
{
    public static  class ModelBuilderExtensions
    {     
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                    new Category() { CategoryId = 1, Name = "Cat 1" },
                    new Category() { CategoryId = 2, Name = "Cat 2" },
                    new Category() { CategoryId = 3, Name = "Cat 3" },
                    new Category() { CategoryId = 4, Name = "Cat 4" },
                    new Category() { CategoryId = 5, Name = "Cat 5" },
                    new Category() { CategoryId = 6, Name = "Cat 6" });

            modelBuilder.Entity<Product>().HasData(
                    new Product() { ProductId = 1, Name = "Product 1", ProductCategoryId = 1 },
                    new Product() { ProductId = 2, Name = "Product 2", ProductCategoryId = 2 },
                    new Product() { ProductId = 3, Name = "Product 3", ProductCategoryId = 3 },
                    new Product() { ProductId = 4, Name = "Product 4", ProductCategoryId = 4 },
                    new Product() { ProductId = 5, Name = "Product 5", ProductCategoryId = 5 },
                    new Product() { ProductId = 6, Name = "Product 6", ProductCategoryId = 6 },
                    new Product() { ProductId = 11, Name = "Product 11", ProductCategoryId = 1 },
                    new Product() { ProductId = 21, Name = "Product 21", ProductCategoryId = 2 },
                    new Product() { ProductId = 31, Name = "Product 31", ProductCategoryId = 3 },
                    new Product() { ProductId = 41, Name = "Product 41", ProductCategoryId = 4 },
                    new Product() { ProductId = 51, Name = "Product 51", ProductCategoryId = 5 },
                    new Product() { ProductId = 61, Name = "Product 61", ProductCategoryId = 6 },
                    new Product() { ProductId = 12, Name = "Product 12", ProductCategoryId = 1 },
                    new Product() { ProductId = 22, Name = "Product 22", ProductCategoryId = 2 },
                    new Product() { ProductId = 32, Name = "Product 32", ProductCategoryId = 3 },
                    new Product() { ProductId = 42, Name = "Product 42", ProductCategoryId = 4 },
                    new Product() { ProductId = 52, Name = "Product 52", ProductCategoryId = 5 },
                    new Product() { ProductId = 62, Name = "Product 62", ProductCategoryId = 6 },
                    new Product() { ProductId = 13, Name = "Product 13", ProductCategoryId = 1 },
                    new Product() { ProductId = 23, Name = "Product 23", ProductCategoryId = 2 },
                    new Product() { ProductId = 33, Name = "Product 33", ProductCategoryId = 3 },
                    new Product() { ProductId = 43, Name = "Product 43", ProductCategoryId = 4 },
                    new Product() { ProductId = 53, Name = "Product 53", ProductCategoryId = 5 },
                    new Product() { ProductId = 63, Name = "Product 63", ProductCategoryId = 6 },
                    new Product() { ProductId = 14, Name = "Product 14", ProductCategoryId = 1 },
                    new Product() { ProductId = 24, Name = "Product 24", ProductCategoryId = 2 },
                    new Product() { ProductId = 34, Name = "Product 34", ProductCategoryId = 3 },
                    new Product() { ProductId = 44, Name = "Product 44", ProductCategoryId = 4 },
                    new Product() { ProductId = 54, Name = "Product 54", ProductCategoryId = 5 },
                    new Product() { ProductId = 64, Name = "Product 64", ProductCategoryId = 6 },
                    new Product() { ProductId = 15, Name = "Product 15", ProductCategoryId = 1 },
                    new Product() { ProductId = 25, Name = "Product 25", ProductCategoryId = 2 },
                    new Product() { ProductId = 35, Name = "Product 35", ProductCategoryId = 3 },
                    new Product() { ProductId = 45, Name = "Product 45", ProductCategoryId = 4 },
                    new Product() { ProductId = 55, Name = "Product 55", ProductCategoryId = 5 },
                    new Product() { ProductId = 65, Name = "Product 65", ProductCategoryId = 6 },
                    new Product() { ProductId = 16, Name = "Product 16", ProductCategoryId = 1 },
                    new Product() { ProductId = 26, Name = "Product 26", ProductCategoryId = 2 },
                    new Product() { ProductId = 36, Name = "Product 36", ProductCategoryId = 3 },
                    new Product() { ProductId = 46, Name = "Product 46", ProductCategoryId = 4 },
                    new Product() { ProductId = 56, Name = "Product 56", ProductCategoryId = 5 },
                    new Product() { ProductId = 66, Name = "Product 66", ProductCategoryId = 6 }
                 );
        }
    }
}
