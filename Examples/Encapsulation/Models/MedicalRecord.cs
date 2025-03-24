using System;
using System.Collections.Generic;

namespace CSharpLearning.Examples.Encapsulation
{
    // Medical record class demonstrates immutability and access control
    public class MedicalRecord
    {
        // Private backing fields
        private readonly string _recordId;
        private readonly string _patientId;
        private readonly DateTime _creationDate;
        private readonly string _symptoms;
        private readonly string _treatment;
        private readonly string _doctorId;
        private readonly List<string> _diagnoses;
        private readonly List<Medication> _medications;

        // Public read-only properties (immutable)
        public string RecordId => _recordId;
        public string PatientId => _patientId;
        public DateTime CreationDate => _creationDate;

        // Properties that should be accessible to medical staff only
        // In a real system, these would have more sophisticated access control
        public string Symptoms => _symptoms;
        public string Treatment => _treatment;
        public string DoctorId => _doctorId;

        // Read-only collections - return copies to prevent modification
        public IReadOnlyList<string> Diagnoses => _diagnoses.AsReadOnly();
        public IReadOnlyList<Medication> Medications => _medications.AsReadOnly();

        // Calculated property
        public bool IsRecent => (DateTime.Now - CreationDate).TotalDays < 30;

        // Constructor
        public MedicalRecord(
            string recordId,
            string patientId,
            DateTime creationDate,
            string symptoms,
            string treatment,
            string doctorId,
            List<string> diagnoses,
            List<Medication> medications)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(recordId))
                throw new ArgumentException("Record ID cannot be empty");

            if (string.IsNullOrWhiteSpace(patientId))
                throw new ArgumentException("Patient ID cannot be empty");

            if (creationDate > DateTime.Now)
                throw new ArgumentException("Creation date cannot be in the future");

            if (string.IsNullOrWhiteSpace(doctorId))
                throw new ArgumentException("Doctor ID cannot be empty");

            // Initialize fields
            _recordId = recordId;
            _patientId = patientId;
            _creationDate = creationDate;
            _symptoms = symptoms ?? "";
            _treatment = treatment ?? "";
            _doctorId = doctorId;

            // Create defensive copies of collections
            _diagnoses = diagnoses != null ? new List<string>(diagnoses) : new List<string>();
            _medications = medications != null ? new List<Medication>(medications) : new List<Medication>();
        }
    }
}