using System;
using System.Collections.Generic;

namespace CSharpLearning.Examples.Polymorphism
{
    public class PolymorphismDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("=== Polymorphism in Ride-Sharing Application Demo ===\n");

            // SECTION 1: STATIC (COMPILE-TIME) POLYMORPHISM
            Console.WriteLine("STATIC (COMPILE-TIME) POLYMORPHISM");
            Console.WriteLine("----------------------------------");

            // Method overloading example
            Console.WriteLine("\n1. Method Overloading:");
            FareCalculator calculator = new FareCalculator();

            // Different ways to calculate fare using overloaded methods
            double basicFare = calculator.CalculateFare(10.5);  // Distance only
            double timeFare = calculator.CalculateFare(10.5, 15);  // Distance and time
            double premiumFare = calculator.CalculateFare(10.5, 15, 1.5);  // With surge pricing
            double customFare = calculator.CalculateFare(10.5, 15, 1.5, 5.0);  // With additional fee

            Console.WriteLine($"Basic fare (distance only): ${basicFare}");
            Console.WriteLine($"Time-based fare: ${timeFare}");
            Console.WriteLine($"Premium fare (with surge): ${premiumFare}");
            Console.WriteLine($"Custom fare (with additional fee): ${customFare}");

            // Operator overloading example
            Console.WriteLine("\n2. Operator Overloading:");
            RideDistance distance1 = new RideDistance(5.0);
            RideDistance distance2 = new RideDistance(7.5);

            RideDistance totalDistance = distance1 + distance2;  // Using overloaded + operator
            Console.WriteLine($"Distance 1: {distance1.Miles} miles");
            Console.WriteLine($"Distance 2: {distance2.Miles} miles");
            Console.WriteLine($"Total distance: {totalDistance.Miles} miles");

            bool isLonger = distance1 > distance2;  // Using overloaded > operator
            Console.WriteLine($"Is distance 1 longer than distance 2? {isLonger}");

            // SECTION 2: DYNAMIC (RUNTIME) POLYMORPHISM
            Console.WriteLine("\nDYNAMIC (RUNTIME) POLYMORPHISM");
            Console.WriteLine("-------------------------------");

            // Creating different vehicle types
            Console.WriteLine("\n1. Method Overriding with Virtual Methods:");
            Vehicle standardCar = new StandardCar("Toyota Camry", "ABC123", 4);
            Vehicle luxuryCar = new LuxuryCar("Mercedes S-Class", "XYZ789", 4, true);
            Vehicle suv = new SUV("Ford Explorer", "DEF456", 7, true);

            // Using virtual/override methods to demonstrate runtime polymorphism
            DisplayVehicleInfo(standardCar);
            DisplayVehicleInfo(luxuryCar);
            DisplayVehicleInfo(suv);

            // Abstract method example
            Console.WriteLine("\n2. Abstract Methods:");
            // Create a list of different ride types
            List<RideBase> rides = new List<RideBase>
            {
                new EconomyRide(standardCar, 12.5, 20),
                new PremiumRide(luxuryCar, 12.5, 20),
                new FamilyRide(suv, 12.5, 20, 5)
            };

            // Process each ride using abstract methods
            foreach (var ride in rides)
            {
                Console.WriteLine($"\nRide Type: {ride.GetType().Name}");
                Console.WriteLine($"Vehicle: {ride.Vehicle.Model}");
                Console.WriteLine($"Base Fare: ${ride.CalculateBaseFare()}");
                Console.WriteLine($"Final Fare: ${ride.CalculateFinalFare()}");
                Console.WriteLine($"Ride Description: {ride.GetRideDescription()}");
            }

            // SECTION 3: INTERFACE POLYMORPHISM
            Console.WriteLine("\nINTERFACE POLYMORPHISM");
            Console.WriteLine("---------------------");

            // Create payment processors implementing the same interface
            Console.WriteLine("\n1. Interface Implementation:");
            IPaymentProcessor creditCardProcessor = new CreditCardProcessor();
            IPaymentProcessor payPalProcessor = new PayPalProcessor();
            IPaymentProcessor applePay = new ApplePayProcessor();

            // Process payments using different processors through the same interface
            ProcessPayment(creditCardProcessor, 25.50);
            ProcessPayment(payPalProcessor, 32.75);
            ProcessPayment(applePay, 15.99);

            // Multiple interface implementation
            Console.WriteLine("\n2. Multiple Interface Implementation:");
            RideManager rideManager = new RideManager();

            // Use the ride manager through different interfaces
            IBookingSystem bookingSystem = rideManager;
            INotificationSystem notificationSystem = rideManager;

            bookingSystem.BookRide("John Doe", "123 Main St", "456 Elm St");
            bookingSystem.CancelRide("R12345");
            notificationSystem.SendNotification("Driver is arriving in 2 minutes");

            // SECTION 4: TYPE COERCION AND CASTING
            Console.WriteLine("\nTYPE COERCION AND CASTING");
            Console.WriteLine("-----------------------");

            // Upcasting (implicit)
            Console.WriteLine("\n1. Upcasting (implicit):");
            Vehicle vehicle = new LuxuryCar("BMW 7 Series", "LUX777", 4, true);
            Console.WriteLine($"Upcasted vehicle type: {vehicle.GetType().Name}");
            Console.WriteLine($"Vehicle description: {vehicle.GetDescription()}");

            // Downcasting (explicit)
            Console.WriteLine("\n2. Downcasting (explicit):");
            if (vehicle is LuxuryCar luxuryVehicle)
            {
                Console.WriteLine("Successfully downcasted to LuxuryCar");
                Console.WriteLine($"Has minibar: {luxuryVehicle.HasMinibar}");
                luxuryVehicle.ActivateMinibar();
            }

            // Type checking with 'is' and 'as' operators
            Console.WriteLine("\n3. Type checking with 'is' and 'as':");

            // Using 'is' operator
            if (vehicle is LuxuryCar)
            {
                Console.WriteLine("Vehicle is a LuxuryCar");
            }

            // Using 'as' operator
            SUV? suvVehicle = vehicle as SUV;
            if (suvVehicle != null)
            {
                Console.WriteLine("Vehicle is an SUV");
            }
            else
            {
                Console.WriteLine("Vehicle is not an SUV");
            }

            Console.WriteLine("\n=== End of Polymorphism Demo ===");
        }

        // Helper method to demonstrate polymorphic behavior
        private static void DisplayVehicleInfo(Vehicle vehicle)
        {
            Console.WriteLine($"\nVehicle: {vehicle.Model} ({vehicle.GetType().Name})");
            Console.WriteLine($"License: {vehicle.LicensePlate}");
            Console.WriteLine($"Capacity: {vehicle.Capacity} passengers");
            Console.WriteLine($"Description: {vehicle.GetDescription()}");
            vehicle.StartRide();
            vehicle.EndRide();
        }

        // Helper method to demonstrate interface polymorphism
        private static void ProcessPayment(IPaymentProcessor processor, double amount)
        {
            Console.WriteLine($"\nProcessing ${amount} payment with {processor.GetType().Name}:");
            bool success = processor.ProcessPayment(amount);
            Console.WriteLine($"Payment {(success ? "successful" : "failed")}");

            // Get receipt using the interface method
            string receipt = processor.GenerateReceipt();
            Console.WriteLine($"Receipt: {receipt}");
        }
    }

    #region Static Polymorphism Examples

    // Example of method overloading (static polymorphism)
    public class FareCalculator
    {
        private const double BaseRatePerMile = 2.0;
        private const double BaseRatePerMinute = 0.3;

        // Overloaded methods with different parameters

        // Calculate fare based on distance only
        public double CalculateFare(double distanceInMiles)
        {
            return Math.Round(distanceInMiles * BaseRatePerMile, 2);
        }

        // Calculate fare based on distance and time
        public double CalculateFare(double distanceInMiles, int timeInMinutes)
        {
            return Math.Round((distanceInMiles * BaseRatePerMile) + (timeInMinutes * BaseRatePerMinute), 2);
        }

        // Calculate fare with surge pricing
        public double CalculateFare(double distanceInMiles, int timeInMinutes, double surgeFactor)
        {
            double baseFare = (distanceInMiles * BaseRatePerMile) + (timeInMinutes * BaseRatePerMinute);
            return Math.Round(baseFare * surgeFactor, 2);
        }

        // Calculate fare with additional fee
        public double CalculateFare(double distanceInMiles, int timeInMinutes, double surgeFactor, double additionalFee)
        {
            double baseFare = (distanceInMiles * BaseRatePerMile) + (timeInMinutes * BaseRatePerMinute);
            return Math.Round((baseFare * surgeFactor) + additionalFee, 2);
        }
    }

    // Example of operator overloading (static polymorphism)
    public class RideDistance
    {
        public double Miles { get; }

        public RideDistance(double miles)
        {
            Miles = miles;
        }

        // Overload the + operator
        public static RideDistance operator +(RideDistance a, RideDistance b)
        {
            return new RideDistance(a.Miles + b.Miles);
        }

        // Overload the > operator
        public static bool operator >(RideDistance a, RideDistance b)
        {
            return a.Miles > b.Miles;
        }

        // Overload the < operator
        public static bool operator <(RideDistance a, RideDistance b)
        {
            return a.Miles < b.Miles;
        }
    }

    #endregion

    #region Dynamic Polymorphism Examples

    // Base class with virtual methods
    public class Vehicle
    {
        public string Model { get; }
        public string LicensePlate { get; }
        public int Capacity { get; }

        public Vehicle(string model, string licensePlate, int capacity)
        {
            Model = model;
            LicensePlate = licensePlate;
            Capacity = capacity;
        }

        // Virtual method that can be overridden
        public virtual string GetDescription()
        {
            return $"Standard vehicle: {Model} with capacity for {Capacity} passengers";
        }

        // Virtual method for starting a ride
        public virtual void StartRide()
        {
            Console.WriteLine($"Starting ride with {Model}");
        }

        // Virtual method for ending a ride
        public virtual void EndRide()
        {
            Console.WriteLine($"Ending ride with {Model}");
        }
    }

    // Derived class overriding methods
    public class StandardCar : Vehicle
    {
        public StandardCar(string model, string licensePlate, int capacity)
            : base(model, licensePlate, capacity)
        {
        }

        // Override the base class method
        public override string GetDescription()
        {
            return $"Economy car: {Model} with comfortable seating for {Capacity} passengers";
        }

        public override void StartRide()
        {
            Console.WriteLine($"Starting economy ride with {Model}");
        }
    }

    // Another derived class with additional features
    public class LuxuryCar : Vehicle
    {
        public bool HasMinibar { get; }

        public LuxuryCar(string model, string licensePlate, int capacity, bool hasMinibar)
            : base(model, licensePlate, capacity)
        {
            HasMinibar = hasMinibar;
        }

        // Override with additional information
        public override string GetDescription()
        {
            string baseDescription = $"Luxury car: {Model} with premium seating for {Capacity} passengers";
            return HasMinibar ? $"{baseDescription}, includes complimentary minibar" : baseDescription;
        }

        public override void StartRide()
        {
            Console.WriteLine($"Starting luxury ride with {Model} - enjoy your premium experience!");
        }

        public override void EndRide()
        {
            Console.WriteLine($"Ending luxury ride with {Model} - we hope you enjoyed the premium service!");
        }

        // Additional method specific to LuxuryCar
        public void ActivateMinibar()
        {
            if (HasMinibar)
            {
                Console.WriteLine("Minibar activated - enjoy complimentary drinks!");
            }
            else
            {
                Console.WriteLine("This vehicle doesn't have a minibar.");
            }
        }
    }

    // Another derived class
    public class SUV : Vehicle
    {
        public bool HasThirdRow { get; }

        public SUV(string model, string licensePlate, int capacity, bool hasThirdRow)
            : base(model, licensePlate, capacity)
        {
            HasThirdRow = hasThirdRow;
        }

        public override string GetDescription()
        {
            string baseDescription = $"SUV: {Model} with spacious seating for {Capacity} passengers";
            return HasThirdRow ? $"{baseDescription}, includes third-row seating" : baseDescription;
        }

        public override void StartRide()
        {
            Console.WriteLine($"Starting family-friendly ride with {Model} SUV");
        }

        // Additional method specific to SUV
        public void FoldThirdRow()
        {
            if (HasThirdRow)
            {
                Console.WriteLine("Third row folded for extra cargo space");
            }
            else
            {
                Console.WriteLine("This SUV doesn't have a third row");
            }
        }
    }

    // Abstract base class example
    public abstract class RideBase
    {
        public Vehicle Vehicle { get; }
        public double Distance { get; }
        public int Duration { get; }

        protected RideBase(Vehicle vehicle, double distance, int duration)
        {
            Vehicle = vehicle;
            Distance = distance;
            Duration = duration;
        }

        // Abstract method that must be implemented by derived classes
        public abstract double CalculateBaseFare();

        // Abstract method for final fare calculation
        public abstract double CalculateFinalFare();

        // Abstract method for ride description
        public abstract string GetRideDescription();
    }

    // Concrete implementation of abstract class
    public class EconomyRide : RideBase
    {
        private const double RatePerMile = 1.5;
        private const double RatePerMinute = 0.2;

        public EconomyRide(Vehicle vehicle, double distance, int duration)
            : base(vehicle, distance, duration)
        {
        }

        public override double CalculateBaseFare()
        {
            return Math.Round((Distance * RatePerMile) + (Duration * RatePerMinute), 2);
        }

        public override double CalculateFinalFare()
        {
            // Economy rides have no additional fees
            return CalculateBaseFare();
        }

        public override string GetRideDescription()
        {
            return $"Economy ride with {Vehicle.Model} - affordable and reliable";
        }
    }

    // Another concrete implementation
    public class PremiumRide : RideBase
    {
        private const double RatePerMile = 2.5;
        private const double RatePerMinute = 0.4;
        private const double LuxuryFee = 5.0;

        public PremiumRide(Vehicle vehicle, double distance, int duration)
            : base(vehicle, distance, duration)
        {
        }

        public override double CalculateBaseFare()
        {
            return Math.Round((Distance * RatePerMile) + (Duration * RatePerMinute), 2);
        }

        public override double CalculateFinalFare()
        {
            // Premium rides have a luxury fee
            return Math.Round(CalculateBaseFare() + LuxuryFee, 2);
        }

        public override string GetRideDescription()
        {
            return $"Premium ride with {Vehicle.Model} - luxury travel experience";
        }
    }

    // Another concrete implementation with additional parameters
    public class FamilyRide : RideBase
    {
        private const double RatePerMile = 2.0;
        private const double RatePerMinute = 0.3;
        private const double ChildSeatFee = 2.0;

        public int NumberOfChildSeats { get; }

        public FamilyRide(Vehicle vehicle, double distance, int duration, int numberOfChildSeats)
            : base(vehicle, distance, duration)
        {
            NumberOfChildSeats = numberOfChildSeats;
        }

        public override double CalculateBaseFare()
        {
            return Math.Round((Distance * RatePerMile) + (Duration * RatePerMinute), 2);
        }

        public override double CalculateFinalFare()
        {
            // Family rides have child seat fees
            return Math.Round(CalculateBaseFare() + (NumberOfChildSeats * ChildSeatFee), 2);
        }

        public override string GetRideDescription()
        {
            return $"Family ride with {Vehicle.Model} - spacious and comfortable with {NumberOfChildSeats} child seats";
        }
    }

    #endregion

    #region Interface Polymorphism Examples

    // Payment processor interface
    public interface IPaymentProcessor
    {
        bool ProcessPayment(double amount);
        string GenerateReceipt();
    }

    // Concrete implementation of payment processor
    public class CreditCardProcessor : IPaymentProcessor
    {
        public bool ProcessPayment(double amount)
        {
            // Simulate credit card processing
            Console.WriteLine("Processing credit card payment...");
            return true;
        }

        public string GenerateReceipt()
        {
            return "Credit Card Payment Receipt #" + GenerateReceiptNumber();
        }

        private string GenerateReceiptNumber()
        {
            return "CC-" + DateTime.Now.Ticks.ToString().Substring(10);
        }
    }

    // Another implementation of the same interface
    public class PayPalProcessor : IPaymentProcessor
    {
        public bool ProcessPayment(double amount)
        {
            // Simulate PayPal processing
            Console.WriteLine("Processing PayPal payment...");
            return true;
        }

        public string GenerateReceipt()
        {
            return "PayPal Payment Receipt #" + GenerateReceiptNumber();
        }

        private string GenerateReceiptNumber()
        {
            return "PP-" + DateTime.Now.Ticks.ToString().Substring(10);
        }
    }

    // Third implementation of the same interface
    public class ApplePayProcessor : IPaymentProcessor
    {
        public bool ProcessPayment(double amount)
        {
            // Simulate Apple Pay processing
            Console.WriteLine("Processing Apple Pay payment...");
            return true;
        }

        public string GenerateReceipt()
        {
            return "Apple Pay Receipt #" + GenerateReceiptNumber();
        }

        private string GenerateReceiptNumber()
        {
            return "AP-" + DateTime.Now.Ticks.ToString().Substring(10);
        }
    }

    // Multiple interface definitions
    public interface IBookingSystem
    {
        void BookRide(string customerName, string pickup, string destination);
        void CancelRide(string rideId);
    }

    public interface INotificationSystem
    {
        void SendNotification(string message);
    }

    // Class implementing multiple interfaces
    public class RideManager : IBookingSystem, INotificationSystem
    {
        // Implementation of IBookingSystem
        public void BookRide(string customerName, string pickup, string destination)
        {
            Console.WriteLine($"Booking ride for {customerName} from {pickup} to {destination}");
        }

        public void CancelRide(string rideId)
        {
            Console.WriteLine($"Cancelling ride {rideId}");
        }

        // Implementation of INotificationSystem
        public void SendNotification(string message)
        {
            Console.WriteLine($"Sending notification: {message}");
        }
    }

    #endregion
}