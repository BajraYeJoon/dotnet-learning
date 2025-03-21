using System;

namespace CSharpLearning.Examples.ClassesAndObjects
{
    public class ClassesDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("=== Classes and Objects Demo ===\n");

            // Demonstrating object creation and constructors
            Console.WriteLine("Creating objects with different constructors:");
            Person person1 = new Person();  // Default constructor
            Person person2 = new Person("John", "Doe", 30, new DateTime(1993, 5, 15));  // Parameterized constructor
            Person person3 = new Person("Jane", "Smith");  // Constructor with chaining

            Console.WriteLine("\nAccessing and modifying properties:");
            Console.WriteLine($"Person 1: {person1.FullName}, Age: {person1.Age}");

            // Using properties to modify state
            person1.FirstName = "Alice";
            person1.LastName = "Johnson";
            person1.Age = 25;

            Console.WriteLine($"Updated Person 1: {person1.FullName}, Age: {person1.Age}");

            // Demonstrating method calls
            Console.WriteLine("\nCalling methods:");
            person1.Introduce();
            person2.Introduce("Greetings");

            // Demonstrating static members
            Console.WriteLine("\nStatic members:");
            Person.DisplayCount();
            Console.WriteLine($"Maximum age allowed: {Person.MaximumAge}");

            // Demonstrating object initialization syntax
            Console.WriteLine("\nObject initializer syntax:");
            Person person4 = new Person
            {
                FirstName = "Robert",
                LastName = "Brown",
                Age = 40
            };
            person4.Introduce();

            // Demonstrating validation in properties
            Console.WriteLine("\nProperty validation:");
            try
            {
                person1.Age = 200; // Should throw an exception
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Caught exception: {ex.Message}");
            }

            try
            {
                person1.FirstName = ""; // Should throw an exception
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Caught exception: {ex.Message}");
            }

            // Memory allocation demonstration
            Console.WriteLine("\nMemory allocation (stack vs heap):");
            DemonstrateMemoryAllocation();

            // Garbage collection demonstration
            Console.WriteLine("\nGarbage collection:");
            DemonstrateGarbageCollection();

            Console.WriteLine("\n=== End of Classes and Objects Demo ===");
        }

        private static void DemonstrateMemoryAllocation()
        {
            // Value types are stored on the stack
            int number = 42;
            bool flag = true;
            DateTime date = DateTime.Now;

            Console.WriteLine("Value types (stored on stack):");
            Console.WriteLine($"  int: {number}");
            Console.WriteLine($"  bool: {flag}");
            Console.WriteLine($"  DateTime: {date}");

            // Reference types are stored on the heap (reference on stack)
            Person person = new Person("Stack", "Heap", 30, DateTime.Now);
            string name = "This is a string"; // String is a reference type

            Console.WriteLine("\nReference types (stored on heap, reference on stack):");
            Console.WriteLine($"  Person: {person.FullName}");
            Console.WriteLine($"  string: {name}");

            // Demonstrating reference behavior
            Person person1 = new Person("Original", "Person", 25, DateTime.Now);
            Person person2 = person1; // Both variables reference the same object

            Console.WriteLine("\nReference behavior:");
            Console.WriteLine($"  person1: {person1.FullName}, Age: {person1.Age}");
            Console.WriteLine($"  person2: {person2.FullName}, Age: {person2.Age}");

            // Changing person2 also changes person1 because they reference the same object
            person2.Age = 30;
            Console.WriteLine("\nAfter changing person2.Age:");
            Console.WriteLine($"  person1: {person1.FullName}, Age: {person1.Age}");
            Console.WriteLine($"  person2: {person2.FullName}, Age: {person2.Age}");
        }

        private static void DemonstrateGarbageCollection()
        {
            Console.WriteLine("Creating objects that will go out of scope...");

            for (int i = 0; i < 3; i++)
            {
                Person tempPerson = new Person($"Temp{i}", "Person", i, DateTime.Now);
                tempPerson.Introduce();
                // tempPerson goes out of scope after each iteration
            }

            Console.WriteLine("\nObjects are now eligible for garbage collection");
            Console.WriteLine("Forcing garbage collection...");

            // Force garbage collection (normally you wouldn't do this in production code)
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("Garbage collection completed");
            Person.DisplayCount();
        }
    }
}