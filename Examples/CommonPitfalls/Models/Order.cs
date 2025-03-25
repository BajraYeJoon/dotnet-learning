using System;
using System.Collections.Generic;

namespace CSharpLearning.Examples.CommonPitfalls
{
    // Order class to demonstrate deep vs shallow copying
    public class Order
    {
        public string OrderId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderItem> Items { get; private set; }

        public Order(string orderId, Customer customer)
        {
            OrderId = orderId;
            Customer = customer;
            Items = new List<OrderItem>();
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }

        // Shallow copy - references to Customer and Items are shared
        public Order ShallowCopy()
        {
            Order copy = new Order(OrderId, Customer);
            copy.Items = Items; // Same reference to the Items list
            return copy;
        }

        // Deep copy - everything is duplicated
        public Order DeepCopy()
        {
            Order copy = new Order(OrderId, Customer.Clone());

            // Create new list and clone each item
            copy.Items = new List<OrderItem>();
            foreach (var item in Items)
            {
                copy.Items.Add(item.Clone());
            }

            return copy;
        }

        public void DisplayOrder()
        {
            Console.WriteLine($"Order ID: {OrderId}");
            Console.WriteLine($"Customer: {Customer.FullName}");
            Console.WriteLine("Items:");

            foreach (var item in Items)
            {
                Console.WriteLine($"  - {item.Quantity}x {item.ProductName} @ ${item.UnitPrice} = ${item.TotalPrice}");
            }

            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.TotalPrice;
            }

            Console.WriteLine($"Total: ${total}");
        }
    }

    // OrderItem class
    public class OrderItem
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        public OrderItem(string productName, int quantity, decimal unitPrice)
        {
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public OrderItem Clone()
        {
            return new OrderItem(ProductName, Quantity, UnitPrice);
        }
    }
}