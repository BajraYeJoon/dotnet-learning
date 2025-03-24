using System;

namespace CSharpLearning.Examples.Encapsulation
{
    // Base class for all staff members
    public abstract class Staff
    {
        // Private backing fields
        private readonly string _staffId;
        private string _name;
        private readonly string _department;

        // Public properties
        public string StaffId => _staffId;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty");
                _name = value;
            }
        }

        public string Department => _department;

        // Constructor
        protected Staff(string staffId, string name, string department)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(staffId))
                throw new ArgumentException("Staff ID cannot be empty");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty");

            if (string.IsNullOrWhiteSpace(department))
                throw new ArgumentException("Department cannot be empty");

            // Initialize fields
            _staffId = staffId;
            _name = name;
            _department = department;
        }

        // Abstract method to be implemented by derived classes
        public abstract void AccessPatientRecord(HospitalSystem system, string recordId);
    }

    // Doctor class with specific access rights
    public class Doctor : Staff
    {
        public Doctor(string staffId, string name, string department)
            : base(staffId, name, department)
        {
        }

        // Doctors have full access to medical records
        public override void AccessPatientRecord(HospitalSystem system, string recordId)
        {
            var record = system.GetMedicalRecord(recordId);
            if (record != null)
            {
                Console.WriteLine($"Doctor {Name} accessing full medical record {recordId}");
                Console.WriteLine($"Patient ID: {record.PatientId}");
                Console.WriteLine($"Symptoms: {record.Symptoms}");
                Console.WriteLine($"Diagnoses: {string.Join(", ", record.Diagnoses)}");
                Console.WriteLine($"Treatment: {record.Treatment}");
                Console.WriteLine("Medications:");
                foreach (var medication in record.Medications)
                {
                    Console.WriteLine($"  {medication}");
                }
            }
            else
            {
                Console.WriteLine($"Record {recordId} not found");
            }
        }
    }

    // Nurse class with limited access rights
    public class Nurse : Staff
    {
        public Nurse(string staffId, string name, string department)
            : base(staffId, name, department)
        {
        }

        // Nurses have limited access to medical records
        public override void AccessPatientRecord(HospitalSystem system, string recordId)
        {
            var record = system.GetMedicalRecord(recordId);
            if (record != null)
            {
                Console.WriteLine($"Nurse {Name} accessing limited medical record {recordId}");
                Console.WriteLine($"Patient ID: {record.PatientId}");
                Console.WriteLine($"Symptoms: {record.Symptoms}");
                // Nurses can see medications but not diagnoses
                Console.WriteLine("Medications:");
                foreach (var medication in record.Medications)
                {
                    Console.WriteLine($"  {medication}");
                }
            }
            else
            {
                Console.WriteLine($"Record {recordId} not found");
            }
        }
    }

    // Administrator class with minimal access rights
    public class Administrator : Staff
    {
        public Administrator(string staffId, string name, string department)
            : base(staffId, name, department)
        {
        }

        // Administrators have minimal access to medical records
        public override void AccessPatientRecord(HospitalSystem system, string recordId)
        {
            var record = system.GetMedicalRecord(recordId);
            if (record != null)
            {
                Console.WriteLine($"Administrator {Name} accessing minimal medical record {recordId}");
                Console.WriteLine($"Patient ID: {record.PatientId}");
                Console.WriteLine($"Record Date: {record.CreationDate}");
                Console.WriteLine($"Doctor ID: {record.DoctorId}");
                // Administrators cannot see clinical details
            }
            else
            {
                Console.WriteLine($"Record {recordId} not found");
            }
        }
    }
}