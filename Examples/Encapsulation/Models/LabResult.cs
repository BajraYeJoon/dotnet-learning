using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CSharpLearning.Examples.Encapsulation
{
    // LabResult class demonstrates true immutability
    public class LabResult
    {
        // All fields are readonly
        private readonly string _labId;
        private readonly string _patientId;
        private readonly DateTime _testDate;
        private readonly string _testType;
        private readonly ReadOnlyDictionary<string, string> _results;

        // Public read-only properties
        public string LabId => _labId;
        public string PatientId => _patientId;
        public DateTime TestDate => _testDate;
        public string TestType => _testType;

        // Truly immutable dictionary of results
        public IReadOnlyDictionary<string, string> Results => _results;

        // Constructor
        public LabResult(
            string labId,
            string patientId,
            DateTime testDate,
            string testType,
            Dictionary<string, string> results)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(labId))
                throw new ArgumentException("Lab ID cannot be empty");

            if (string.IsNullOrWhiteSpace(patientId))
                throw new ArgumentException("Patient ID cannot be empty");

            if (testDate > DateTime.Now)
                throw new ArgumentException("Test date cannot be in the future");

            if (string.IsNullOrWhiteSpace(testType))
                throw new ArgumentException("Test type cannot be empty");

            if (results == null)
                throw new ArgumentNullException(nameof(results), "Results cannot be null");

            // Initialize fields
            _labId = labId;
            _patientId = patientId;
            _testDate = testDate;
            _testType = testType;

            // Create a truly immutable dictionary
            _results = new ReadOnlyDictionary<string, string>(
                new Dictionary<string, string>(results)
            );
        }
    }
}