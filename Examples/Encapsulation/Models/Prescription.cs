using System;
using System.Collections.Generic;

namespace CSharpLearning.Examples.Encapsulation
{
    // Enum for prescription status
    public enum PrescriptionStatus
    {
        Created,
        Filled,
        Refilled,
        Completed,
        Cancelled
    }

    // Prescription class demonstrates getter/setter patterns and business rules
    public class Prescription
    {
        // Private backing fields
        private readonly string _prescriptionId;
        private readonly string _patientId;
        private readonly string _doctorId;
        private readonly DateTime _issueDate;
        private readonly List<Medication> _medications;
        private int _daysSupply;
        private int _refillsRemaining;
        private PrescriptionStatus _status;

        // Read-only properties
        public string PrescriptionId => _prescriptionId;
        public string PatientId => _patientId;
        public string DoctorId => _doctorId;
        public DateTime IssueDate => _issueDate;

        // Read-only collection
        public IReadOnlyList<Medication> Medications => _medications.AsReadOnly();

        // Property with validation
        public int DaysSupply
        {
            get => _daysSupply;
            set
            {
                if (value <= 0 || value > 90)
                    throw new ArgumentException("Days supply must be between 1 and 90");
                _daysSupply = value;
            }
        }

        // Property with validation
        public int RefillsRemaining
        {
            get => _refillsRemaining;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Refills cannot be negative");
                _refillsRemaining = value;
            }
        }

        // Property with business rules in setter
        public PrescriptionStatus Status
        {
            get => _status;
            set
            {
                // Implement business rules for status transitions
                if (_status == PrescriptionStatus.Cancelled && value != PrescriptionStatus.Cancelled)
                    throw new InvalidOperationException("Cannot change status of a cancelled prescription");

                if (_status == PrescriptionStatus.Completed && value != PrescriptionStatus.Completed)
                    throw new InvalidOperationException("Cannot change status of a completed prescription");

                // Additional rules could be implemented here

                _status = value;

                // Side effects of status change
                if (value == PrescriptionStatus.Refilled)
                {
                    RefillsRemaining--;
                    if (RefillsRemaining <= 0)
                    {
                        _status = PrescriptionStatus.Completed;
                    }
                }
            }
        }

        // Constructor
        public Prescription(
            string prescriptionId,
            string patientId,
            string doctorId,
            DateTime issueDate,
            List<Medication> medications,
            int daysSupply,
            int refillsRemaining = 0)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(prescriptionId))
                throw new ArgumentException("Prescription ID cannot be empty");

            if (string.IsNullOrWhiteSpace(patientId))
                throw new ArgumentException("Patient ID cannot be empty");

            if (string.IsNullOrWhiteSpace(doctorId))
                throw new ArgumentException("Doctor ID cannot be empty");

            if (issueDate > DateTime.Now)
                throw new ArgumentException("Issue date cannot be in the future");

            if (medications == null || medications.Count == 0)
                throw new ArgumentException("Prescription must include at least one medication");

            if (daysSupply <= 0 || daysSupply > 90)
                throw new ArgumentException("Days supply must be between 1 and 90");

            if (refillsRemaining < 0)
                throw new ArgumentException("Refills cannot be negative");

            // Initialize fields
            _prescriptionId = prescriptionId;
            _patientId = patientId;
            _doctorId = doctorId;
            _issueDate = issueDate;
            _medications = new List<Medication>(medications);  // Defensive copy
            _daysSupply = daysSupply;
            _refillsRemaining = refillsRemaining;
            _status = PrescriptionStatus.Created;
        }
    }
}