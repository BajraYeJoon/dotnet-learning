using System;
using System.Collections.Generic;

namespace CSharpLearning.Examples.CommonPitfalls
{
    public class CommonPitfallsDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("=== Common C# Pitfalls Demo ===\n");

            // SECTION 1: REFERENCE VS VALUE TYPES
            Console.WriteLine("REFERENCE VS VALUE TYPES");
            Console.WriteLine("------------------------");

            // Value types demonstration
            Console.WriteLine("\nValue Types Example:");
            int number1 = 10;
            int number2 = number1; // Creates a copy

            Console.WriteLine($"Original values: number1 = {number1}, number2 = {number2}");

            // Modify number2
            number2 = 20;
            Console.WriteLine($"After modifying number2: number1 = {number1}, number2 = {number2}");
            Console.WriteLine("Notice that number1 is unchanged when number2 is modified");

            // Reference types demonstration
            Console.WriteLine("\nReference Types Example:");
            Customer customer1 = new Customer("John", "Doe");
            Customer customer2 = customer1; // Both variables reference the same object

            Console.WriteLine($"Original values: customer1 = {customer1.FullName}, customer2 = {customer2.FullName}");

            // Modify customer2
            customer2.LastName = "Smith";
            Console.WriteLine($"After modifying customer2: customer1 = {customer1.FullName}, customer2 = {customer2.FullName}");
            Console.WriteLine("Notice that customer1 is also changed when customer2 is modified");

            // SECTION 2: DEEP VS SHALLOW COPYING
            Console.WriteLine("\nDEEP VS SHALLOW COPYING");
            Console.WriteLine("----------------------");

            // Create an order with items
            Order originalOrder = new Order("ORD-001", new Customer("Alice", "Johnson"));
            originalOrder.AddItem(new OrderItem("Product A", 2, 10.99m));
            originalOrder.AddItem(new OrderItem("Product B", 1, 24.99m));

            // Shallow copy
            Order shallowCopy = originalOrder.ShallowCopy();

            // Deep copy
            Order deepCopy = originalOrder.DeepCopy();

            Console.WriteLine("\nOriginal Order:");
            originalOrder.DisplayOrder();

            Console.WriteLine("\nShallow Copy:");
            shallowCopy.DisplayOrder();

            Console.WriteLine("\nDeep Copy:");
            deepCopy.DisplayOrder();

            // Modify the original order's customer and items
            Console.WriteLine("\nModifying original order's customer and adding an item...");
            originalOrder.Customer.LastName = "Williams";
            originalOrder.AddItem(new OrderItem("Product C", 3, 5.99m));

            Console.WriteLine("\nAfter Modification - Original Order:");
            originalOrder.DisplayOrder();

            Console.WriteLine("\nAfter Modification - Shallow Copy:");
            shallowCopy.DisplayOrder();
            Console.WriteLine("Notice that customer name changed but items didn't (shallow copy behavior)");

            Console.WriteLine("\nAfter Modification - Deep Copy:");
            deepCopy.DisplayOrder();
            Console.WriteLine("Notice that deep copy remains completely unchanged");

            // SECTION 3: OBJECT COMPARISON
            Console.WriteLine("\nOBJECT COMPARISON");
            Console.WriteLine("-----------------");

            Product product1 = new Product("P001", "Laptop", 999.99m);
            Product product2 = new Product("P001", "Laptop", 999.99m);
            Product product3 = product1;


            // Reference equality (==)
            Console.WriteLine("\nReference Equality (==):");
            Console.WriteLine($"product1 == product2: {product1 == product2}");
            Console.WriteLine($"product1 == product3: {product1 == product3}");

            // Reference equality (ReferenceEquals)
            Console.WriteLine("\nReference Equality (ReferenceEquals):");
            Console.WriteLine($"ReferenceEquals(product1, product2): {ReferenceEquals(product1, product2)}");
            Console.WriteLine($"ReferenceEquals(product1, product3): {ReferenceEquals(product1, product3)}");

            // Value equality (Equals)
            Console.WriteLine("\nValue Equality (Equals):");
            Console.WriteLine($"product1.Equals(product2): {product1.Equals(product2)}");
            Console.WriteLine($"product1.Equals(product3): {product1.Equals(product3)}");

            // GetHashCode
            Console.WriteLine("\nGetHashCode:");
            Console.WriteLine($"product1.GetHashCode(): {product1.GetHashCode()}");
            Console.WriteLine($"product2.GetHashCode(): {product2.GetHashCode()}");
            Console.WriteLine($"product3.GetHashCode(): {product3.GetHashCode()}");

            // SECTION 4: BOXING AND UNBOXING
            Console.WriteLine("\nBOXING AND UNBOXING");
            Console.WriteLine("------------------");

            // Boxing (value type to reference type)
            int valueType = 42;
            object boxed = valueType; // Boxing

            Console.WriteLine($"\nOriginal value: {valueType}");
            Console.WriteLine($"Boxed value: {boxed}");

            // Unboxing (reference type back to value type)
            int unboxed = (int)boxed; // Unboxing
            Console.WriteLine($"Unboxed value: {unboxed}");

            // Performance implications
            Console.WriteLine("\nPerformance Test:");
            BoxingPerformanceTest();

            // SECTION 5: MEMORY LEAKS
            Console.WriteLine("\nMEMORY LEAKS");
            Console.WriteLine("------------");

            Console.WriteLine("\nCommon causes of memory leaks in C#:");
            Console.WriteLine("1. Event handlers not properly unsubscribed");
            Console.WriteLine("2. Static collections holding references to objects");
            Console.WriteLine("3. Improper IDisposable implementation");

            // Demonstrate event handler memory leak
            Console.WriteLine("\nEvent Handler Example:");
            EventPublisher publisher = new EventPublisher();

            // Create subscribers that register but don't unregister
            for (int i = 0; i < 3; i++)
            {
                EventSubscriber subscriber = new EventSubscriber($"Subscriber {i + 1}");
                subscriber.Subscribe(publisher);

                // In a real scenario, if we lose the reference to subscriber here
                // but don't unsubscribe, it can't be garbage collected
            }

            // Trigger the event
            publisher.RaiseEvent("Test event");

            // Proper disposal example
            Console.WriteLine("\nProper Disposal Example:");
            using (ResourceManager resource = new ResourceManager("Important Resource"))
            {
                resource.PerformOperation();
            } // Automatically calls Dispose() when exiting the block

            Console.WriteLine("Resource has been properly disposed");

            // SECTION 6: CIRCULAR REFERENCES
            Console.WriteLine("\nCIRCULAR REFERENCES");
            Console.WriteLine("------------------");

            // Create objects with circular references
            Parent parent = new Parent("Parent Object");
            Child child = new Child("Child Object");

            // Create circular reference
            parent.SetChild(child);
            child.SetParent(parent);

            Console.WriteLine("\nCreated circular reference between:");
            Console.WriteLine($"- Parent: {parent.Name}");
            Console.WriteLine($"- Child: {child.Name}");

            Console.WriteLine("\nGarbage collection can still handle this in .NET,");
            Console.WriteLine("but it can cause issues with serialization and deep copying.");

            // Demonstrate serialization issue
            Console.WriteLine("\nTo avoid serialization issues with circular references:");
            Console.WriteLine("1. Use [JsonIgnore] or [XmlIgnore] attributes");
            Console.WriteLine("2. Use reference handling in serializers");
            Console.WriteLine("3. Break circular references before serialization");

            // SECTION 7: OVER-ENGINEERING
            Console.WriteLine("\nOVER-ENGINEERING");
            Console.WriteLine("----------------");

            Console.WriteLine("\nSimple vs. Over-engineered Approach:");

            // Simple approach
            SimpleCalculator simpleCalc = new SimpleCalculator();
            decimal simpleResult = simpleCalc.Add(5, 3);
            Console.WriteLine($"Simple calculator result: {simpleResult}");

            // Over-engineered approach
            IOperationStrategy addStrategy = new AdditionStrategy();
            CalculationContext complexCalc = new CalculationContext(addStrategy);
            decimal complexResult = complexCalc.ExecuteStrategy(5, 3);
            Console.WriteLine($"Complex calculator result: {complexResult}");

            Console.WriteLine("\nThe simple approach is more readable and maintainable for this case.");
            Console.WriteLine("Over-engineering adds unnecessary complexity for simple problems.");

            // SECTION 8: TIGHT COUPLING
            Console.WriteLine("\nTIGHT COUPLING");
            Console.WriteLine("-------------");

            // Tight coupling example
            Console.WriteLine("\nTight Coupling Example:");
            OrderProcessor tightProcessor = new OrderProcessor();
            tightProcessor.ProcessOrder(new Order("ORD-002", new Customer("Bob", "Taylor")));

            // Loose coupling example
            Console.WriteLine("\nLoose Coupling Example:");
            IEmailService emailService = new EmailService();
            IPaymentProcessor paymentProcessor = new PaymentProcessor();
            IInventoryService inventoryService = new InventoryService();

            OrderProcessorImproved looseProcessor = new OrderProcessorImproved(
                emailService, paymentProcessor, inventoryService);

            looseProcessor.ProcessOrder(new Order("ORD-003", new Customer("Carol", "Davis")));

            Console.WriteLine("\nLoose coupling makes the code more testable and flexible.");
            Console.WriteLine("Dependencies can be easily replaced or mocked for testing.");

            Console.WriteLine("\n=== End of Common Pitfalls Demo ===");
        }

        private static void BoxingPerformanceTest()
        {
            const int iterations = 1000000;

            // Without boxing
            DateTime start = DateTime.Now;
            int sum1 = 0;
            for (int i = 0; i < iterations; i++)
            {
                sum1 += i;
            }
            TimeSpan withoutBoxing = DateTime.Now - start;

            // With boxing
            start = DateTime.Now;
            object sum2 = 0;
            for (int i = 0; i < iterations; i++)
            {
                sum2 = (int)sum2 + i; // Boxing and unboxing in each iteration
            }
            TimeSpan withBoxing = DateTime.Now - start;

            Console.WriteLine($"Without boxing: {withoutBoxing.TotalMilliseconds} ms");
            Console.WriteLine($"With boxing: {withBoxing.TotalMilliseconds} ms");
            Console.WriteLine($"Boxing is approximately {withBoxing.TotalMilliseconds / withoutBoxing.TotalMilliseconds:F2}x slower");
        }
    }
}