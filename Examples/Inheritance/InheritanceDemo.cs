using System;

namespace CSharpLearning.Examples.Inheritance
{
    public class InheritanceDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("=== Inheritance Demo ===\n");

            // Demonstrating basic inheritance
            Console.WriteLine("Basic Inheritance:");
            Animal animal = new Animal("Generic Animal", 5);
            animal.Eat();
            animal.Sleep();
            animal.MakeSound();

            Console.WriteLine();
            Dog dog = new Dog("Buddy", 3, "Golden Retriever");
            dog.Eat();
            dog.Sleep();
            dog.MakeSound();  // Overridden method
            dog.Fetch();      // Dog-specific method

            Console.WriteLine();
            Cat cat = new Cat("Whiskers", 2, 9);
            cat.Eat();
            cat.Sleep();
            cat.MakeSound();  // Overridden method
            cat.Purr();       // Cat-specific method

            // Demonstrating 'is-a' relationship with polymorphism
            Console.WriteLine("\nPolymorphism with 'is-a' relationship:");
            Animal dogAsAnimal = new Dog("Rex", 4, "German Shepherd");
            Animal catAsAnimal = new Cat("Felix", 3, 7);

            dogAsAnimal.MakeSound();  // Calls Dog's implementation
            catAsAnimal.MakeSound();  // Calls Cat's implementation

            // Type checking and casting
            Console.WriteLine("\nType checking and casting:");
            if (dogAsAnimal is Dog dogReference)
            {
                Console.WriteLine($"Successfully cast to Dog: {dogReference.Breed}");
                dogReference.Fetch();
            }

            // Method hiding vs overriding
            Console.WriteLine("\nMethod hiding vs overriding:");
            Bird bird = new Bird("Tweety", 1, 20.5);
            bird.Eat();       // Calls Bird's hidden implementation
            bird.MakeSound(); // Calls Bird's overridden implementation

            Animal birdAsAnimal = bird;
            birdAsAnimal.Eat();       // Calls Animal's implementation (hiding)
            birdAsAnimal.MakeSound(); // Calls Bird's implementation (overriding)

            // Abstract classes
            Console.WriteLine("\nAbstract classes:");
            // Cannot instantiate: Vehicle vehicle = new Vehicle("Generic", 0); // Error
            Car car = new Car("Toyota", 120, 4);
            car.StartEngine();
            car.StopEngine();
            car.DisplayInfo();

            Motorcycle motorcycle = new Motorcycle("Harley", 160, false);
            motorcycle.StartEngine();
            motorcycle.StopEngine();
            motorcycle.DisplayInfo();

            // Constructor chaining
            Console.WriteLine("\nConstructor chaining:");
            SportsCar sportsCar = new SportsCar("Ferrari", 300, 2, true);
            sportsCar.DisplayInfo();

            // Protected members
            Console.WriteLine("\nProtected members:");
            sportsCar.TestProtectedAccess();

            // Sealed classes and methods
            Console.WriteLine("\nSealed classes and methods:");
            ElectricCar electricCar = new ElectricCar("Tesla", 150, 4, 500);
            electricCar.DisplayInfo();
            // Cannot inherit from ElectricCar because it's sealed

            // Multiple inheritance through interfaces
            Console.WriteLine("\nMultiple inheritance through interfaces:");
            Amphibian amphibian = new Amphibian("AquaCar", 80, 4);
            amphibian.Drive();
            amphibian.Float();
            amphibian.DisplayInfo();

            Console.WriteLine("\n=== End of Inheritance Demo ===");
        }
    }

    // Base class
    public class Animal
    {
        // Properties
        public string Name { get; set; }
        public int Age { get; set; }

        // Constructor
        public Animal(string name, int age)
        {
            Name = name;
            Age = age;
            Console.WriteLine($"Animal constructor called for {Name}");
        }

        // Virtual method (can be overridden)
        public virtual void MakeSound()
        {
            Console.WriteLine($"{Name} makes a generic animal sound");
        }

        // Non-virtual method (can be hidden but not overridden)
        public void Eat()
        {
            Console.WriteLine($"{Name} is eating");
        }

        // Another method
        public void Sleep()
        {
            Console.WriteLine($"{Name} is sleeping");
        }

        // Protected method - accessible only to derived classes
        protected void InternalProcess()
        {
            Console.WriteLine($"Internal process for {Name}");
        }
    }

    // Derived class - inherits from Animal
    public class Dog : Animal
    {
        // Additional property specific to Dog
        public string Breed { get; set; }

        // Constructor with base constructor call
        public Dog(string name, int age, string breed) : base(name, age)
        {
            Breed = breed;
            Console.WriteLine($"Dog constructor called for {Name}, breed: {Breed}");
        }

        // Override of virtual method
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} barks loudly!");
        }

        // Dog-specific method
        public void Fetch()
        {
            Console.WriteLine($"{Name} is fetching the ball");
        }
    }

    // Another derived class - inherits from Animal
    public class Cat : Animal
    {
        // Additional property specific to Cat
        public int LivesLeft { get; set; }

        // Constructor with base constructor call
        public Cat(string name, int age, int livesLeft) : base(name, age)
        {
            LivesLeft = livesLeft;
            Console.WriteLine($"Cat constructor called for {Name}, lives left: {LivesLeft}");
        }

        // Override of virtual method
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} meows softly");
        }

        // Cat-specific method
        public void Purr()
        {
            Console.WriteLine($"{Name} is purring");
        }
    }

    // Demonstrating method hiding with 'new' keyword
    public class Bird : Animal
    {
        public double WingSpan { get; set; }

        public Bird(string name, int age, double wingSpan) : base(name, age)
        {
            WingSpan = wingSpan;
            Console.WriteLine($"Bird constructor called for {Name}, wingspan: {WingSpan}");
        }

        // Method hiding - hides the base class implementation
        public new void Eat()
        {
            Console.WriteLine($"{Name} is pecking at seeds");
        }

        // Method overriding - replaces the base class implementation
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} chirps melodiously");
        }
    }

    // Abstract class - cannot be instantiated directly
    public abstract class Vehicle
    {
        public string Model { get; set; }
        public int MaxSpeed { get; set; }

        // Constructor in abstract class
        protected Vehicle(string model, int maxSpeed)
        {
            Model = model;
            MaxSpeed = maxSpeed;
            Console.WriteLine($"Vehicle constructor called for {Model}");
        }

        // Abstract method - must be implemented by derived classes
        public abstract void StartEngine();

        // Virtual method - can be overridden
        public virtual void StopEngine()
        {
            Console.WriteLine($"{Model}'s engine stopped");
        }

        // Non-virtual method
        public void DisplayInfo()
        {
            Console.WriteLine($"Vehicle: {Model}, Max Speed: {MaxSpeed} mph");
            DisplaySpecificInfo();  // Template method pattern
        }

        // Protected virtual method for template method pattern
        protected virtual void DisplaySpecificInfo()
        {
            Console.WriteLine("No specific info available for generic vehicle");
        }
    }

    // Concrete class derived from abstract class
    public class Car : Vehicle
    {
        public int NumberOfDoors { get; set; }

        public Car(string model, int maxSpeed, int numberOfDoors) : base(model, maxSpeed)
        {
            NumberOfDoors = numberOfDoors;
            Console.WriteLine($"Car constructor called for {Model}, doors: {NumberOfDoors}");
        }

        // Implementation of abstract method
        public override void StartEngine()
        {
            Console.WriteLine($"{Model}'s car engine started with a roar");
        }

        // Override of virtual method
        protected override void DisplaySpecificInfo()
        {
            Console.WriteLine($"Car specific info: {NumberOfDoors} doors");
        }

        // Method to demonstrate protected member access
        public void TestProtectedAccess()
        {
            Console.WriteLine("Accessing protected members from derived class:");
            DisplaySpecificInfo();  // Accessing protected method from base class
        }
    }

    // Another level of inheritance
    public class SportsCar : Car
    {
        public bool HasTurbo { get; set; }

        // Constructor chaining through multiple levels
        public SportsCar(string model, int maxSpeed, int numberOfDoors, bool hasTurbo)
            : base(model, maxSpeed, numberOfDoors)
        {
            HasTurbo = hasTurbo;
            Console.WriteLine($"SportsCar constructor called for {Model}, turbo: {HasTurbo}");
        }

        // Override of virtual method
        public override void StartEngine()
        {
            Console.WriteLine($"{Model}'s sports car engine started with a VROOM!");
        }

        // Override of virtual method
        protected override void DisplaySpecificInfo()
        {
            base.DisplaySpecificInfo();  // Call base implementation first
            Console.WriteLine($"Sports car specific info: Turbo: {HasTurbo}");
        }
    }

    // Sealed class - cannot be inherited from
    public sealed class ElectricCar : Car
    {
        public int BatteryRange { get; set; }

        public ElectricCar(string model, int maxSpeed, int numberOfDoors, int batteryRange)
            : base(model, maxSpeed, numberOfDoors)
        {
            BatteryRange = batteryRange;
            Console.WriteLine($"ElectricCar constructor called for {Model}, range: {BatteryRange}");
        }

        // Sealed method - cannot be overridden in derived classes (if this wasn't a sealed class)
        public sealed override void StartEngine()
        {
            Console.WriteLine($"{Model}'s electric motor powered on silently");
        }

        protected override void DisplaySpecificInfo()
        {
            base.DisplaySpecificInfo();
            Console.WriteLine($"Electric car specific info: Battery Range: {BatteryRange} miles");
        }
    }

    // Motorcycle class derived from Vehicle
    public class Motorcycle : Vehicle
    {
        public bool HasSidecar { get; set; }

        public Motorcycle(string model, int maxSpeed, bool hasSidecar) : base(model, maxSpeed)
        {
            HasSidecar = hasSidecar;
            Console.WriteLine($"Motorcycle constructor called for {Model}, sidecar: {HasSidecar}");
        }

        public override void StartEngine()
        {
            Console.WriteLine($"{Model}'s motorcycle engine started with a rumble");
        }

        protected override void DisplaySpecificInfo()
        {
            Console.WriteLine($"Motorcycle specific info: Sidecar: {HasSidecar}");
        }
    }

    // Interfaces for multiple inheritance
    public interface IDriveable
    {
        void Drive();
    }

    public interface IFloatable
    {
        void Float();
    }

    // Class implementing multiple interfaces (multiple inheritance)
    public class Amphibian : Car, IDriveable, IFloatable
    {
        public Amphibian(string model, int maxSpeed, int numberOfDoors)
            : base(model, maxSpeed, numberOfDoors)
        {
            Console.WriteLine($"Amphibian constructor called for {Model}");
        }

        // Implementation of IDriveable interface
        public void Drive()
        {
            Console.WriteLine($"{Model} is driving on land");
        }

        // Implementation of IFloatable interface
        public void Float()
        {
            Console.WriteLine($"{Model} is floating on water");
        }

        // Override from Car
        public override void StartEngine()
        {
            Console.WriteLine($"{Model}'s amphibious engine started");
        }
    }
}