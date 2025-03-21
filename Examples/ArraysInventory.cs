using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagment
{
    public class Program
    {
        // This will be our main entry point for the inventory example
        public static void RunInventoryManagment()
        {
            Console.Clear();
            Console.WriteLine("=== Inventory Management System ===\n");

            // We'll implement the inventory system here
            InventorySystem inventory = new InventorySystem();
            inventory.Run();
        }
    }

    public class InventorySystem
    {
        // We'll use different collection types to store inventory data
        private Product[] productCatalog;  // Fixed-size array for product catalog
        private List<Product> inventory;   // Dynamic list for inventory items
        private Dictionary<string, Supplier> suppliers; // Dictionary for quick supplier lookup
        private Queue<Order> pendingOrders; // Queue for processing orders in sequence
        private Stack<InventoryAction> actionHistory; // Stack for undo functionality

        public InventorySystem()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            // Initialize Array - fixed size collection
            productCatalog = new Product[5]
            {
                new Product { Id = "P001", Name = "Laptop", Category = "Electronics", Price = 999.99m },
                new Product { Id = "P002", Name = "Smartphone", Category = "Electronics", Price = 699.99m },
                new Product { Id = "P003", Name = "Headphones", Category = "Accessories", Price = 149.99m },
                new Product { Id = "P004", Name = "Mouse", Category = "Accessories", Price = 24.99m },
                new Product { Id = "P005", Name = "Keyboard", Category = "Accessories", Price = 49.99m }
            };

            // Initialize List - dynamic size collection
            inventory = new List<Product>
            {
                new Product { Id = "P001", Name = "Laptop", Category = "Electronics", Price = 999.99m, Quantity = 10 },
                new Product { Id = "P002", Name = "Smartphone", Category = "Electronics", Price = 699.99m, Quantity = 15 },
                new Product { Id = "P003", Name = "Headphones", Category = "Accessories", Price = 149.99m, Quantity = 20 }
            };

            // Initialize Dictionary - key/value pairs for fast lookup
            suppliers = new Dictionary<string, Supplier>
            {
                { "S001", new Supplier { Id = "S001", Name = "TechSupplies Inc.", ContactEmail = "contact@techsupplies.com" } },
                { "S002", new Supplier { Id = "S002", Name = "Gadget Wholesale", ContactEmail = "sales@gadgetwholesale.com" } }
            };

            // Initialize Queue - FIFO (First In, First Out)
            pendingOrders = new Queue<Order>();
            pendingOrders.Enqueue(new Order { OrderId = "O001", ProductId = "P001", Quantity = 5 });
            pendingOrders.Enqueue(new Order { OrderId = "O002", ProductId = "P003", Quantity = 10 });

            // Initialize Stack - LIFO (Last In, First Out)
            actionHistory = new Stack<InventoryAction>();
        }

        public void Run()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nInventory Management Menu:");
                Console.WriteLine("1. View Product Catalog (Array)");
                Console.WriteLine("2. View Inventory (List)");
                Console.WriteLine("3. Search Product by ID (List methods)");
                Console.WriteLine("4. Add New Product to Inventory (List methods)");
                Console.WriteLine("5. Update Product Quantity (List methods)");
                Console.WriteLine("6. View Suppliers (Dictionary)");
                Console.WriteLine("7. Process Next Order (Queue)");
                Console.WriteLine("8. Undo Last Action (Stack)");
                Console.WriteLine("9. Advanced Collection Operations");
                Console.WriteLine("0. Exit");

                Console.Write("\nEnter your choice: ");
                string choice = Console.ReadLine() ?? "0";

                switch (choice)
                {
                    case "1":
                        ViewProductCatalog();
                        break;
                    case "2":
                        ViewInventory();
                        break;
                    case "3":
                        SearchProduct();
                        break;
                    case "4":
                        AddNewProduct();
                        break;
                    case "5":
                        UpdateProductQuantity();
                        break;
                    case "6":
                        ViewSuppliers();
                        break;
                    case "7":
                        ProcessNextOrder();
                        break;
                    case "8":
                        UndoLastAction();
                        break;
                    case "9":
                        ShowAdvancedOperations();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

            Console.WriteLine("\nExiting Inventory Management System...");
        }

        // Array demonstration
        private void ViewProductCatalog()
        {
            Console.WriteLine("\n=== Product Catalog (Array) ===");
            Console.WriteLine("ID\tName\t\tCategory\tPrice");
            Console.WriteLine("----------------------------------------");

            // Loop through array using for loop
            for (int i = 0; i < productCatalog.Length; i++)
            {
                Console.WriteLine($"{productCatalog[i].Id}\t{productCatalog[i].Name}\t{productCatalog[i].Category}\t${productCatalog[i].Price}");
            }

            Console.WriteLine("\nArray Operations:");
            Console.WriteLine($"- Array Length: {productCatalog.Length}");
            Console.WriteLine("- Arrays have fixed size once initialized");
            Console.WriteLine("- Fast access by index: O(1) time complexity");
        }

        // List demonstration
        private void ViewInventory()
        {
            Console.WriteLine("\n=== Current Inventory (List) ===");
            Console.WriteLine("ID\tName\t\tCategory\tPrice\t\tQuantity");
            Console.WriteLine("--------------------------------------------------------");

            // Loop through list using foreach loop
            foreach (var item in inventory)
            {
                Console.WriteLine($"{item.Id}\t{item.Name}\t{item.Category}\t${item.Price}\t{item.Quantity}");
            }

            Console.WriteLine("\nList Operations:");
            Console.WriteLine($"- List Count: {inventory.Count}");
            Console.WriteLine($"- List Capacity: {inventory.Capacity}");
            Console.WriteLine("- Lists can grow dynamically");
            Console.WriteLine("- Common methods: Add, Remove, Find, Sort, etc.");
        }

        // List search methods
        private void SearchProduct()
        {
            Console.Write("\nEnter product ID to search: ");
            string searchId = Console.ReadLine() ?? "";

            // Using Find method with a predicate (lambda expression)
            Product? foundProduct = inventory.Find(p => p.Id == searchId);

            if (foundProduct != null)
            {
                Console.WriteLine("\n=== Product Found ===");
                Console.WriteLine($"ID: {foundProduct.Id}");
                Console.WriteLine($"Name: {foundProduct.Name}");
                Console.WriteLine($"Category: {foundProduct.Category}");
                Console.WriteLine($"Price: ${foundProduct.Price}");
                Console.WriteLine($"Quantity: {foundProduct.Quantity}");

                // Demonstrate other search methods
                Console.WriteLine("\nOther List Search Methods:");
                Console.WriteLine($"- FindIndex: {inventory.FindIndex(p => p.Id == searchId)}");
                Console.WriteLine($"- Exists: {inventory.Exists(p => p.Category == foundProduct.Category)}");
                Console.WriteLine($"- FindAll: Found {inventory.FindAll(p => p.Category == foundProduct.Category).Count} items in same category");
            }
            else
            {
                Console.WriteLine("\nProduct not found in inventory.");
            }
        }

        // List add method
        private void AddNewProduct()
        {
            Console.WriteLine("\n=== Add New Product to Inventory ===");

            Console.Write("Enter Product ID: ");
            string id = Console.ReadLine() ?? "";

            // Check if product already exists
            if (inventory.Any(p => p.Id == id))
            {
                Console.WriteLine("Product with this ID already exists!");
                return;
            }

            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter Category: ");
            string category = Console.ReadLine() ?? "";

            Console.Write("Enter Price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price format!");
                return;
            }

            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid quantity format!");
                return;
            }

            // Create new product and add to inventory
            Product newProduct = new Product
            {
                Id = id,
                Name = name,
                Category = category,
                Price = price,
                Quantity = quantity
            };

            // Add to list
            inventory.Add(newProduct);

            // Record action for undo
            actionHistory.Push(new InventoryAction
            {
                ActionType = "Add",
                ProductId = id,
                Description = $"Added {quantity} {name} to inventory"
            });

            Console.WriteLine($"\nProduct '{name}' added to inventory successfully!");
            Console.WriteLine($"Current inventory count: {inventory.Count}");
        }

        // List update method
        private void UpdateProductQuantity()
        {
            Console.Write("\nEnter product ID to update: ");
            string updateId = Console.ReadLine() ?? "";

            // Find the product index
            int index = inventory.FindIndex(p => p.Id == updateId);

            if (index >= 0)
            {
                Product product = inventory[index];
                Console.WriteLine($"Current quantity for {product.Name}: {product.Quantity}");

                Console.Write("Enter new quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int newQuantity))
                {
                    Console.WriteLine("Invalid quantity format!");
                    return;
                }

                // Record old quantity for undo
                int oldQuantity = product.Quantity;

                // Update the product
                product.Quantity = newQuantity;
                inventory[index] = product;

                // Record action for undo
                actionHistory.Push(new InventoryAction
                {
                    ActionType = "Update",
                    ProductId = updateId,
                    OldQuantity = oldQuantity,
                    NewQuantity = newQuantity,
                    Description = $"Updated {product.Name} quantity from {oldQuantity} to {newQuantity}"
                });

                Console.WriteLine($"\nQuantity updated successfully for {product.Name}!");
            }
            else
            {
                Console.WriteLine("\nProduct not found in inventory.");
            }
        }

        // Dictionary demonstration
        private void ViewSuppliers()
        {
            Console.WriteLine("\n=== Suppliers (Dictionary) ===");
            Console.WriteLine("ID\tName\t\t\tContact Email");
            Console.WriteLine("--------------------------------------------------------");

            // Loop through dictionary
            foreach (var kvp in suppliers)
            {
                Console.WriteLine($"{kvp.Key}\t{kvp.Value.Name}\t{kvp.Value.ContactEmail}");
            }

            Console.WriteLine("\nDictionary Operations:");
            Console.WriteLine($"- Dictionary Count: {suppliers.Count}");
            Console.WriteLine("- Fast lookup by key: O(1) time complexity");

            // Demonstrate dictionary lookup
            Console.Write("\nEnter supplier ID to look up: ");
            string supplierId = Console.ReadLine() ?? "";

            if (suppliers.TryGetValue(supplierId, out Supplier? supplier))
            {
                Console.WriteLine($"Found: {supplier.Name} ({supplier.ContactEmail})");
            }
            else
            {
                Console.WriteLine("Supplier not found.");
            }
        }

        // Queue demonstration
        private void ProcessNextOrder()
        {
            Console.WriteLine("\n=== Process Next Order (Queue) ===");

            if (pendingOrders.Count > 0)
            {
                // Dequeue the next order (FIFO)
                Order nextOrder = pendingOrders.Dequeue();
                Console.WriteLine($"Processing Order: {nextOrder.OrderId}");
                Console.WriteLine($"Product ID: {nextOrder.ProductId}");
                Console.WriteLine($"Quantity: {nextOrder.Quantity}");

                // Find the product
                Product? product = inventory.Find(p => p.Id == nextOrder.ProductId);

                if (product != null)
                {
                    int index = inventory.IndexOf(product);
                    int oldQuantity = product.Quantity;

                    // Update inventory
                    product.Quantity -= nextOrder.Quantity;
                    inventory[index] = product;

                    Console.WriteLine($"\nOrder processed! Updated {product.Name} quantity: {product.Quantity}");

                    // Record action for undo
                    actionHistory.Push(new InventoryAction
                    {
                        ActionType = "ProcessOrder",
                        ProductId = nextOrder.ProductId,
                        OldQuantity = oldQuantity,
                        NewQuantity = product.Quantity,
                        Description = $"Processed order {nextOrder.OrderId} for {nextOrder.Quantity} {product.Name}"
                    });
                }
                else
                {
                    Console.WriteLine("Error: Product not found in inventory!");
                }

                Console.WriteLine($"\nRemaining orders in queue: {pendingOrders.Count}");
            }
            else
            {
                Console.WriteLine("No pending orders in the queue.");

                // Add a new order for demonstration
                Console.WriteLine("\nAdding a new order to the queue for demonstration...");
                Order newOrder = new Order
                {
                    OrderId = $"O00{new Random().Next(3, 9)}",
                    ProductId = inventory[new Random().Next(0, inventory.Count)].Id,
                    Quantity = new Random().Next(1, 5)
                };

                pendingOrders.Enqueue(newOrder);
                Console.WriteLine($"New order added: {newOrder.OrderId} for product {newOrder.ProductId}");
                Console.WriteLine($"Orders in queue: {pendingOrders.Count}");
            }
        }

        // Stack demonstration
        private void UndoLastAction()
        {
            Console.WriteLine("\n=== Undo Last Action (Stack) ===");

            if (actionHistory.Count > 0)
            {
                // Pop the last action (LIFO)
                InventoryAction lastAction = actionHistory.Pop();
                Console.WriteLine($"Undoing: {lastAction.Description}");

                // Perform the undo operation based on action type
                switch (lastAction.ActionType)
                {
                    case "Add":
                        // Remove the added product
                        inventory.RemoveAll(p => p.Id == lastAction.ProductId);
                        Console.WriteLine($"Removed product {lastAction.ProductId} from inventory.");
                        break;

                    case "Update":
                    case "ProcessOrder":
                        // Restore the old quantity
                        int index = inventory.FindIndex(p => p.Id == lastAction.ProductId);
                        if (index >= 0)
                        {
                            Product product = inventory[index];
                            product.Quantity = lastAction.OldQuantity;
                            inventory[index] = product;
                            Console.WriteLine($"Restored quantity of {product.Name} to {lastAction.OldQuantity}");
                        }
                        break;
                }

                Console.WriteLine($"\nUndo successful! Remaining actions in history: {actionHistory.Count}");
            }
            else
            {
                Console.WriteLine("No actions in history to undo.");
            }
        }

        // Advanced collection operations
        private void ShowAdvancedOperations()
        {
            Console.WriteLine("\n=== Advanced Collection Operations ===");

            // LINQ operations on List
            Console.WriteLine("\n1. LINQ Operations on Inventory List:");

            // Filtering
            var electronicsProducts = inventory.Where(p => p.Category == "Electronics").ToList();
            Console.WriteLine($"- Electronics products count: {electronicsProducts.Count}");

            // Ordering
            var orderedByPrice = inventory.OrderByDescending(p => p.Price).ToList();
            Console.WriteLine("- Products ordered by price (descending):");
            foreach (var p in orderedByPrice)
            {
                Console.WriteLine($"  {p.Name}: ${p.Price}");
            }

            // Aggregation
            decimal totalInventoryValue = inventory.Sum(p => p.Price * p.Quantity);
            Console.WriteLine($"- Total inventory value: ${totalInventoryValue}");

            // Grouping
            var groupedByCategory = inventory.GroupBy(p => p.Category);
            Console.WriteLine("\n- Products grouped by category:");
            foreach (var group in groupedByCategory)
            {
                Console.WriteLine($"  {group.Key}: {group.Count()} products");
            }

            // HashSet demonstration
            Console.WriteLine("\n2. HashSet Operations (unique collections):");
            HashSet<string> categories = new HashSet<string>();

            // Add all categories from inventory
            foreach (var product in inventory)
            {
                categories.Add(product.Category);
            }

            Console.WriteLine($"- Unique categories: {string.Join(", ", categories)}");

            // Try to add a duplicate
            bool added = categories.Add("Electronics");
            Console.WriteLine($"- Added 'Electronics' again: {added} (duplicates are ignored)");

            // SortedList demonstration
            Console.WriteLine("\n3. SortedList (automatically sorted key/value pairs):");
            SortedList<string, int> productCountByCategory = new SortedList<string, int>();

            foreach (var product in inventory)
            {
                if (productCountByCategory.ContainsKey(product.Category))
                {
                    productCountByCategory[product.Category] += product.Quantity;
                }
                else
                {
                    productCountByCategory.Add(product.Category, product.Quantity);
                }
            }

            Console.WriteLine("- Product counts by category (alphabetically sorted):");
            foreach (var kvp in productCountByCategory)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value} items");
            }

            // LinkedList demonstration
            Console.WriteLine("\n4. LinkedList (efficient insertions and removals):");
            LinkedList<string> recentActions = new LinkedList<string>();

            // Add some actions
            recentActions.AddFirst("Viewed inventory");
            recentActions.AddFirst("Added new product");
            recentActions.AddFirst("Updated quantity");

            Console.WriteLine("- Recent actions (newest first):");
            foreach (var action in recentActions)
            {
                Console.WriteLine($"  {action}");
            }

            // Insert in the middle
            LinkedListNode<string>? secondNode = recentActions.First?.Next;
            if (secondNode != null)
            {
                recentActions.AddAfter(secondNode, "Processed order");
                Console.WriteLine("\n- After inserting in the middle:");
                foreach (var action in recentActions)
                {
                    Console.WriteLine($"  {action}");
                }
            }
        }
    }

    // Model classes
    public class Product
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class Supplier
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
    }

    public class Order
    {
        public string OrderId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }

    public class InventoryAction
    {
        public string ActionType { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public int OldQuantity { get; set; }
        public int NewQuantity { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}