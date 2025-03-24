using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpLearning.Examples.Encapsulation
{
    // HospitalSystem class demonstrates encapsulation at the system level
    public class HospitalSystem
    {
        // Private backing fields
        private readonly string _hospitalName;
        private readonly Dictionary<string, Patient> _patients;
        private readonly Dictionary<string, MedicalRecord> _medicalRecords;

        // Public properties
        public string HospitalName => _hospitalName;

        // Read-only properties that expose counts but not the actual collections
        public int PatientCount => _patients.Count;
        public int RecordCount => _medicalRecords.Count;

        // Constructor
        public HospitalSystem(string hospitalName)
        {
            if (string.IsNullOrWhiteSpace(hospitalName))
                throw new ArgumentException("Hospital name cannot be empty");

            _hospitalName = hospitalName;
            _patients = new Dictionary<string, Patient>();
            _medicalRecords = new Dictionary<string, MedicalRecord>();

            Console.WriteLine($"Hospital system initialized for {HospitalName}");
        }

        // Methods to add data to the system
        public void AddPatient(Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));

            if (_patients.ContainsKey(patient.PatientId))
                throw new InvalidOperationException($"Patient with ID {patient.PatientId} already exists");

            _patients[patient.PatientId] = patient;
            Console.WriteLine($"Patient {patient.FullName} added to the system");
        }

        public void AddMedicalRecord(MedicalRecord record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            if (_medicalRecords.ContainsKey(record.RecordId))
                throw new InvalidOperationException($"Medical record with ID {record.RecordId} already exists");

            if (!_patients.ContainsKey(record.PatientId))
                throw new InvalidOperationException($"Patient with ID {record.PatientId} does not exist");

            _medicalRecords[record.RecordId] = record;
            Console.WriteLine($"Medical record {record.RecordId} added for patient {record.PatientId}");
        }

        // Methods to retrieve data from the system
        public Patient GetPatient(string patientId)
        {
            if (string.IsNullOrWhiteSpace(patientId))
                throw new ArgumentException("Patient ID cannot be empty");

            if (_patients.TryGetValue(patientId, out Patient patient))
                return patient;

            return null;
        }

        public MedicalRecord GetMedicalRecord(string recordId)
        {
            if (string.IsNullOrWhiteSpace(recordId))
                throw new ArgumentException("Record ID cannot be empty");

            if (_medicalRecords.TryGetValue(recordId, out MedicalRecord record))
                return record;

            return null;
        }

        // Method to get records for a specific patient
        public IEnumerable<MedicalRecord> GetPatientRecords(string patientId)
        {
            if (string.IsNullOrWhiteSpace(patientId))
                throw new ArgumentException("Patient ID cannot be empty");

            return _medicalRecords.Values.Where(r => r.PatientId == patientId);
        }
    }
}