using System;
using System.Collections.Generic;

namespace CSharpLearning.Examples.Encapsulation
{
    public class EncapsulationDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("=== Encapsulation in Health Records System Demo ===\n");

            // Create a hospital system
            HospitalSystem hospitalSystem = new HospitalSystem("General Hospital");

            // SECTION 1: INFORMATION HIDING & DATA PROTECTION
            Console.WriteLine("INFORMATION HIDING & DATA PROTECTION");
            Console.WriteLine("------------------------------------");

            // Create patients with protected personal information
            Patient patient1 = new Patient("P10001", "John", "Doe", new DateTime(1980, 5, 15), "123-45-6789");
            Patient patient2 = new Patient("P10002", "Jane", "Smith", new DateTime(1992, 8, 22), "987-65-4321");

            // Add patients to the system
            hospitalSystem.AddPatient(patient1);
            hospitalSystem.AddPatient(patient2);

            // Display patient information - note how SSN is masked
            Console.WriteLine("\nPatient Information (Public View):");
            DisplayPatientInfo(patient1);
            DisplayPatientInfo(patient2);

            // SECTION 2: ACCESS LEVELS
            Console.WriteLine("\nACCESS LEVELS");
            Console.WriteLine("-------------");

            // Create staff members with different access levels
            Doctor doctor = new Doctor("D5001", "Dr. Sarah Johnson", "Cardiology");
            Nurse nurse = new Nurse("N3001", "Michael Chen", "General Care");
            Administrator admin = new Administrator("A2001", "Lisa Brown", "Records");

            Console.WriteLine("\nStaff Access Demonstration:");

            // Add medical records
            MedicalRecord record1 = new MedicalRecord(
                "MR5001",
                patient1.PatientId,
                DateTime.Now.AddDays(-10),
                "Chest pain and shortness of breath",
                "Prescribed nitroglycerin and scheduled stress test",
                doctor.StaffId,
                new List<string> { "Hypertension", "High cholesterol" },
                new List<Medication> {
                    new Medication("Lisinopril", "10mg", "Once daily"),
                    new Medication("Atorvastatin", "20mg", "Once daily at bedtime")
                }
            );

            hospitalSystem.AddMedicalRecord(record1);

            // Demonstrate different access levels
            Console.WriteLine("\nDoctor Access:");
            doctor.AccessPatientRecord(hospitalSystem, record1.RecordId);

            Console.WriteLine("\nNurse Access:");
            nurse.AccessPatientRecord(hospitalSystem, record1.RecordId);

            Console.WriteLine("\nAdministrator Access:");
            admin.AccessPatientRecord(hospitalSystem, record1.RecordId);

            // SECTION 3: PROPERTY PATTERNS
            Console.WriteLine("\nPROPERTY PATTERNS");
            Console.WriteLine("----------------");

            // Demonstrate different property patterns
            Console.WriteLine("\nRead-only Properties:");
            Console.WriteLine($"Patient ID (read-only): {patient1.PatientId}");
            Console.WriteLine($"Record creation date (read-only): {record1.CreationDate}");

            Console.WriteLine("\nCalculated Properties:");
            Console.WriteLine($"Patient age: {patient1.Age} years");
            Console.WriteLine($"Record is recent: {record1.IsRecent}");

            // SECTION 4: IMMUTABILITY
            Console.WriteLine("\nIMMUTABILITY");
            Console.WriteLine("-----------");

            // Create an immutable lab result
            LabResult labResult = new LabResult(
                "LR8001",
                patient1.PatientId,
                DateTime.Now.AddDays(-5),
                "Blood Test",
                new Dictionary<string, string> {
                    { "Hemoglobin", "14.2 g/dL" },
                    { "White Blood Cells", "7.5 x10^9/L" },
                    { "Platelets", "250 x10^9/L" }
                }
            );

            Console.WriteLine("\nImmutable Lab Result:");
            Console.WriteLine($"Lab ID: {labResult.LabId}");
            Console.WriteLine($"Test Type: {labResult.TestType}");
            Console.WriteLine($"Test Date: {labResult.TestDate}");
            Console.WriteLine("Results:");
            foreach (var result in labResult.Results)
            {
                Console.WriteLine($"  {result.Key}: {result.Value}");
            }

            // Try to modify the immutable object (will not compile)
            // labResult.Results.Add("Glucose", "95 mg/dL"); // This would cause a compilation error

            // SECTION 5: VALIDATION AND BUSINESS RULES
            Console.WriteLine("\nVALIDATION AND BUSINESS RULES");
            Console.WriteLine("----------------------------");

            // Demonstrate validation in action
            Console.WriteLine("\nValidation Examples:");

            try
            {
                // Try to create a medication with invalid dosage
                Medication invalidMed = new Medication("Aspirin", "", "Twice daily");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation error: {ex.Message}");
            }

            try
            {
                // Try to create a patient with future birth date
                Patient invalidPatient = new Patient("P99999", "Invalid", "Patient", DateTime.Now.AddYears(1), "111-22-3333");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation error: {ex.Message}");
            }

            // Demonstrate business rules with appointments
            Console.WriteLine("\nAppointment Scheduling (Business Rules):");

            // Create valid appointment
            Appointment validAppointment = new Appointment(
                patient1.PatientId,
                doctor.StaffId,
                DateTime.Now.AddDays(3).Date.AddHours(14), // 2:00 PM in 3 days
                TimeSpan.FromMinutes(30),
                "Follow-up consultation"
            );

            Console.WriteLine($"Valid appointment created: {validAppointment.StartTime} with {validAppointment.DoctorId}");

            try
            {
                // Try to create appointment outside business hours
                Appointment invalidAppointment = new Appointment(
                    patient1.PatientId,
                    doctor.StaffId,
                    DateTime.Now.AddDays(3).Date.AddHours(20), // 8:00 PM in 3 days
                    TimeSpan.FromMinutes(30),
                    "After-hours appointment"
                );
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Business rule violation: {ex.Message}");
            }

            // SECTION 6: GETTER AND SETTER PATTERNS
            Console.WriteLine("\nGETTER AND SETTER PATTERNS");
            Console.WriteLine("-------------------------");

            // Create a prescription to demonstrate custom getters and setters
            Prescription prescription = new Prescription(
                "PR7001",
                patient1.PatientId,
                doctor.StaffId,
                DateTime.Now,
                new List<Medication> {
                    new Medication("Amoxicillin", "500mg", "Three times daily with food")
                },
                7 // 7-day supply
            );

            Console.WriteLine("\nPrescription Information:");
            Console.WriteLine($"ID: {prescription.PrescriptionId}");
            Console.WriteLine($"Status: {prescription.Status}");
            Console.WriteLine($"Days Supply: {prescription.DaysSupply}");
            Console.WriteLine($"Refills Remaining: {prescription.RefillsRemaining}");

            // Demonstrate custom setter with validation
            Console.WriteLine("\nUpdating prescription:");
            try
            {
                prescription.RefillsRemaining = 3; // Valid
                Console.WriteLine($"Refills updated to: {prescription.RefillsRemaining}");

                prescription.RefillsRemaining = -1; // Invalid
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation error: {ex.Message}");
            }

            // Demonstrate backing field pattern with status changes
            Console.WriteLine("\nChanging prescription status:");
            prescription.Status = PrescriptionStatus.Filled;
            Console.WriteLine($"New status: {prescription.Status}");

            // Try to set an invalid status transition
            try
            {
                prescription.Status = PrescriptionStatus.Cancelled;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Status transition error: {ex.Message}");
            }

            Console.WriteLine("\n=== End of Encapsulation Demo ===");
        }

        // Helper method to display patient information
        private static void DisplayPatientInfo(Patient patient)
        {
            Console.WriteLine($"Patient: {patient.FullName}");
            Console.WriteLine($"ID: {patient.PatientId}");
            Console.WriteLine($"DOB: {patient.DateOfBirth.ToShortDateString()} (Age: {patient.Age})");
            Console.WriteLine($"SSN: {patient.MaskedSSN}");
            Console.WriteLine();
        }
    }
}