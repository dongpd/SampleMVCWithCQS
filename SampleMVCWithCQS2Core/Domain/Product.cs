using SampleMVCWithCQS2Core.DataAccess;
using System;

namespace SampleMVCWithCQS2Core.Domain
{
    public enum Colors
    {
        Red, Green, Blue
    }
    public class Product : DbBaseModel
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public Colors Color { get; set; }
        public bool InStock { get; set; }

        public Product(string name, string category, decimal price, Colors color, bool inStock)
        {
            Name = name;
            Category = category;
            Price = price;
            Color = color;
            InStock = inStock;
        }

        public Product(){}
    }
    
}