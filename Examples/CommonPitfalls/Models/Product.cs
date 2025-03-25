using System;

namespace CSharpLearning.Examples.CommonPitfalls
{
    // Product class to demonstrate object comparison
    public class Product
    {
        public string ProductId { get; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(string productId, string name, decimal price)
        {
            ProductId = productId;
            Name = name;
            Price = price;
        }

        // Override Equals for value equality
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Product other = (Product)obj;
            return ProductId == other.ProductId &&
                   Name == other.Name &&
                   Price == other.Price;
        }

        // Override GetHashCode when overriding Equals
        public override int GetHashCode()
        {
            return HashCode.Combine(ProductId, Name, Price);
        }
    }
}