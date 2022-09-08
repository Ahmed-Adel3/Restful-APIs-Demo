using System.Collections.Generic;

namespace Assignment.Data.Entities
{
    public class Category
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
