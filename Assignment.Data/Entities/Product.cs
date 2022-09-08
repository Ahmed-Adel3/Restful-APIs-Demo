using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Data.Entities
{
    public class Product
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey("Category")]
        public long ProductCategoryId { get; set; }
        public Category Category { get; set; }
    }
}
