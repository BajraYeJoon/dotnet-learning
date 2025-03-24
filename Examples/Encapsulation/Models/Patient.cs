using System;

namespace CSharpLearning.Examples.Encapsulation
{
    // Patient class demonstrates information hiding, data protection, and validation
    public class Patient
    {
        // Private fields - information hiding
        private readonly string _patientId;  // Immutable
        private string _firstName;
        private string _lastName;
        private readonly DateTime _dateOfBirth;  // Immutable
        private readonly string _ssn;  // Sensitive data, immutable

        // Public properties with appropriate access levels

        // Read-only property (immutable)
        public string PatientId => _patientId;

        // Properties with validation
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("First name cannot be empty");
                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Last name cannot be empty");
                _lastName = value;
            }
        }

        // Read-only property (immutable)
        public DateTime DateOfBirth => _dateOfBirth;

        // Calculated property
        public int Age => CalculateAge(_dateOfBirth);

        // Full name - calculated from other properties
        public string FullName => $"{FirstName} {LastName}";

        // Data protection - only show masked version of SSN
        public string MaskedSSN => MaskSSN(_ssn);

        // Constructor with validation
        public Patient(string patientId, string firstName, string lastName, DateTime dateOfBirth, string ssn)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(patientId))
                throw new ArgumentException("Patient ID cannot be empty");

            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty");

            if (dateOfBirth > DateTime.Now)
                throw new ArgumentException("Date of birth cannot be in the future");

            if (string.IsNullOrWhiteSpace(ssn) || !IsValidSSN(ssn))
                throw new ArgumentException("Invalid SSN format");

            // Initialize fields
            _patientId = patientId;
            _firstName = firstName;
            _lastName = lastName;
            _dateOfBirth = dateOfBirth;
            _ssn = ssn;
        }

        // Private helper methods

        // Calculate age based on birth date
        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            // Adjust age if birthday hasn't occurred yet this year
            if (birthDate.Date > today.AddYears(-age))
                age--;

            return age;
        }

        // Mask SSN for privacy
        private string MaskSSN(string ssn)
        {
            // Only show last 4 digits
            if (ssn.Length >= 4)
                return $"XXX-XX-{ssn.Substring(ssn.Length - 4)}";

            return "XXX-XX-XXXX";
        }

        // Validate SSN format
        private bool IsValidSSN(string ssn)
        {
            // Simple validation - in a real system this would be more robust
            return ssn.Length >= 9;
        }
    }
}