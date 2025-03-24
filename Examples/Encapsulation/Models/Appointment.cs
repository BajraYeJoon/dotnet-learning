using System;

namespace CSharpLearning.Examples.Encapsulation
{
    // Appointment class demonstrates business rules and validation
    public class Appointment
    {
        // Private backing fields
        private readonly string _patientId;
        private readonly string _doctorId;
        private readonly DateTime _startTime;
        private readonly TimeSpan _duration;
        private string _notes;

        // Public properties
        public string PatientId => _patientId;
        public string DoctorId => _doctorId;
        public DateTime StartTime => _startTime;
        public DateTime EndTime => StartTime + Duration;
        public TimeSpan Duration => _duration;

        public string Notes
        {
            get => _notes;
            set => _notes = value ?? "";
        }

        // Constructor with business rules validation
        public Appointment(
            string patientId,
            string doctorId,
            DateTime startTime,
            TimeSpan duration,
            string notes)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(patientId))
                throw new ArgumentException("Patient ID cannot be empty");

            if (string.IsNullOrWhiteSpace(doctorId))
                throw new ArgumentException("Doctor ID cannot be empty");

            if (startTime < DateTime.Now)
                throw new ArgumentException("Appointment cannot be in the past");

            if (duration <= TimeSpan.Zero || duration > TimeSpan.FromHours(4))
                throw new ArgumentException("Duration must be positive and no more than 4 hours");

            // Business rule: Appointments must be during business hours (8 AM - 6 PM)
            if (startTime.Hour < 8 || (startTime + duration).Hour > 18 || (startTime + duration).Hour == 18 && (startTime + duration).Minute > 0)
                throw new ArgumentException("Appointments must be scheduled during business hours (8 AM - 6 PM)");

            // Business rule: Appointments must be on weekdays
            if (startTime.DayOfWeek == DayOfWeek.Saturday || startTime.DayOfWeek == DayOfWeek.Sunday)
                throw new ArgumentException("Appointments cannot be scheduled on weekends");

            // Initialize fields
            _patientId = patientId;
            _doctorId = doctorId;
            _startTime = startTime;
            _duration = duration;
            _notes = notes ?? "";
        }
    }
}