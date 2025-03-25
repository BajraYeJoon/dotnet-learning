using System;

namespace CSharpLearning.Examples.CommonPitfalls
{
    // Tightly coupled order processor
    public class OrderProcessor
    {
        private EmailService _emailService;
        private PaymentProcessor _paymentProcessor;
        private InventoryService _inventoryService;

        public OrderProcessor()
        {
            // Hard dependencies created inside the class
            _emailService = new EmailService();
            _paymentProcessor = new PaymentProcessor();
            _inventoryService = new InventoryService();
        }

        public void ProcessOrder(Order order)
        {
            Console.WriteLine("Tightly coupled order processing:");

            // Direct calls to concrete implementations
            _paymentProcessor.ProcessPayment(order);
            _inventoryService.UpdateInventory(order);
            _emailService.SendOrderConfirmation(order);
        }
    }

    // Interfaces for loose coupling
    public interface IEmailService
    {
        void SendOrderConfirmation(Order order);
    }

    public interface IPaymentProcessor
    {
        void ProcessPayment(Order order);
    }

    public interface IInventoryService
    {
        void UpdateInventory(Order order);
    }

    // Loosely coupled order processor
    public class OrderProcessorImproved
    {
        private readonly IEmailService _emailService;
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IInventoryService _inventoryService;

        // Dependencies injected through constructor
        public OrderProcessorImproved(
            IEmailService emailService,
            IPaymentProcessor paymentProcessor,
            IInventoryService inventoryService)
        {
            _emailService = emailService;
            _paymentProcessor = paymentProcessor;
            _inventoryService = inventoryService;
        }

        public void ProcessOrder(Order order)
        {
            Console.WriteLine("Loosely coupled order processing:");

            // Calls through interfaces
            _paymentProcessor.ProcessPayment(order);
            _inventoryService.UpdateInventory(order);
            _emailService.SendOrderConfirmation(order);
        }
    }

    // Concrete implementations
    public class EmailService : IEmailService
    {
        public void SendOrderConfirmation(Order order)
        {
            Console.WriteLine($"Sending order confirmation email for order {order.OrderId} to {order.Customer.FullName}");
        }
    }

    public class PaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(Order order)
        {
            Console.WriteLine($"Processing payment for order {order.OrderId}");
        }
    }

    public class InventoryService : IInventoryService
    {
        public void UpdateInventory(Order order)
        {
            Console.WriteLine($"Updating inventory for order {order.OrderId}");
        }
    }
}