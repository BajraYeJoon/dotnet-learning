using System;

namespace CSharpLearning.Examples.Encapsulation
{
    // Medication class demonstrates validation and immutability
    public class Medication
    {
        // Private backing fields
        private readonly string _name;
        private readonly string _dosage;
        private readonly string _instructions;

        // Public read-only properties
        public string Name => _name;
        public string Dosage => _dosage;
        public string Instructions => _instructions;

        // Constructor with validation
        public Medication(string name, string dosage, string instructions)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Medication name cannot be empty");

            if (string.IsNullOrWhiteSpace(dosage))
                throw new ArgumentException("Dosage cannot be empty");

            // Instructions can be empty, but not null

            // Initialize fields
            _name = name;
            _dosage = dosage;
            _instructions = instructions ?? "";
        }

        // Override ToString for better display
        public override string ToString()
        {
            return $"{Name} {Dosage}" + (string.IsNullOrEmpty(Instructions) ? "" : $" - {Instructions}");
        }
    }
}