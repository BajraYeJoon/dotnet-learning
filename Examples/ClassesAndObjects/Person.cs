using System;

namespace CSharpLearning.Examples.ClassesAndObjects
{
    // A basic class definition with various components
    public class Person
    {
        // Fields (instance variables)
        private string _firstName;
        private string _lastName;
        private int _age;
        private readonly DateTime _dateOfBirth;  // readonly field can only be set in constructor

        // Static (class) variable - shared across all instances
        private static int _totalPersonCount = 0;

        // Constants - implicitly static
        public const int MaximumAge = 150;

        // Properties - provide controlled access to fields
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("First name cannot be empty");
                _firstName = value;
            }
        }

        // Auto-implemented property (C# creates the backing field automatically)
        public string LastName { get; set; }

        // Property with validation
        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 0 || value > MaximumAge)
                    throw new ArgumentOutOfRangeException(nameof(value), $"Age must be between 0 and {MaximumAge}");
                _age = value;
            }
        }

        // Read-only property (calculated from other fields)
        public string FullName => $"{FirstName} {LastName}";

        // Read-only property based on readonly field
        public DateTime DateOfBirth => _dateOfBirth;

        // Static property
        public static int TotalPersonCount => _totalPersonCount;

        // Default constructor
        public Person()
        {
            _firstName = "Unknown";
            LastName = "Unknown";
            _age = 0;
            _dateOfBirth = DateTime.Now;
            _totalPersonCount++;

            Console.WriteLine("Default constructor called");
        }

        // Parameterized constructor
        public Person(string firstName, string lastName, int age, DateTime dateOfBirth)
        {
            _firstName = firstName;
            LastName = lastName;
            _age = age;
            _dateOfBirth = dateOfBirth;
            _totalPersonCount++;

            Console.WriteLine("Parameterized constructor called");
        }

        // Constructor overloading with chaining (calls another constructor)
        public Person(string firstName, string lastName) : this(firstName, lastName, 0, DateTime.Now)
        {
            Console.WriteLine("Constructor with two parameters called");
        }

        // Static constructor - called once before the first instance is created
        static Person()
        {
            Console.WriteLine("Static constructor called - initializing the Person class");
            // Initialize static members here
        }

        // Destructor/Finalizer - called by garbage collector
        ~Person()
        {
            // Cleanup code (rarely needed in C# due to garbage collection)
            Console.WriteLine($"Finalizer called for {FullName}");
            _totalPersonCount--;
        }

        // Instance method
        public void Introduce()
        {
            Console.WriteLine($"Hello, my name is {FullName} and I am {Age} years old.");
        }

        // Method overloading - same name, different parameters
        public void Introduce(string greeting)
        {
            Console.WriteLine($"{greeting}, my name is {FullName} and I am {Age} years old.");
        }

        // Static method
        public static void DisplayCount()
        {
            Console.WriteLine($"There are {TotalPersonCount} Person instances.");
        }
    }
}