using SoftServe_FinalTask_HospitalRecords.Models;
using log4net;
using System.Diagnostics.Metrics;

namespace SoftServe_FinalTask_HospitalRecords.Services
{
    internal class DoctorManager
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DoctorManager));
        public static void ShowAllDoctors(List<Doctor> doctors)
        {
            if (doctors == null || !doctors.Any())
            {
                Console.WriteLine("No doctors available.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("─────────────────────────────" +
                              "─────────────────────────────────");
            Console.WriteLine("                  List of Registered Doctors");
            Console.WriteLine("──────────────────────────────" +
                              "────────────────────────────────");
            Console.ResetColor();

            foreach (var doctor in doctors)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($" Name: {doctor.Name}");
                Console.ResetColor();

                Console.WriteLine($"    Age: {doctor.Age}");
                Console.WriteLine($"    Specialty: {doctor.Specialty}");
                Console.WriteLine($"    Experience: {doctor.YearsOfExperience} years");
                Console.WriteLine($"    Salary: {doctor.Salary}");

                if (doctor is Surgeon surgeon)
                {
                    Console.WriteLine($"    Surgeries Performed: " +
                                      $"{surgeon.SurgeriesPerformed}");
                }
                else if (doctor is Pediatrician pediatrician)
                {
                    Console.WriteLine($"     Declared Patients: " +
                                      $"{pediatrician.DeclaredPatients}");
                }

                Console.WriteLine("──────────────────────────────" +
                                  "────────────────────────────────");
            }
        }


        public static void CreateDoctor(List<Doctor> doctors)
        {
            log.Info("Adding a new doctor.");

            Console.WriteLine("Adding new doctor:");

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                log.Error("Name cannot be empty.");
                Console.WriteLine("Name cannot be empty. " +
                                  "Please enter a valid name.");
                return;
            }

            Console.Write("Enter Age: ");
            if (!int.TryParse(Console.ReadLine(), out int age) || age < 18)
            {
                log.Error("Age cannot be lower than legal working age.");
                Console.WriteLine("Age cannot be lower than legal working age. " +
                                  "Please enter a valid number.");
                return;
            }

            Console.Write("Enter Specialty (Surgeon, Pediatrician, " +
                          "Cardiologist, Neurologist): ");
            string input = Console.ReadLine();
            if (!Enum.TryParse(input, true, 
                                out Specialty specialty))
            {
                log.Warn($"Invalid specialty input: {input}. " +
                         $"Defaulting to Undefined.");
                Console.WriteLine("Invalid specialty. " +
                                  "Setting to Undefined.");
                specialty = Specialty.Undefined;
            }

            Console.Write("Enter Years of Experience: ");
            if (!int.TryParse(Console.ReadLine(), out int yearsOfExperience) || yearsOfExperience < 0)
            {
                log.Error("Years of experience cannot be negative.");
                Console.WriteLine("Working experience cannot be negative." +
                                  "Please enter a valid number.");
                return;
            }

            Console.Write("Enter Salary: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal salary) || salary < 0)
            {
                log.Error("Salary cannot be negative.");
                Console.WriteLine("Salary cannot be negative. " +
                                  "Please enter a valid number.");
                return;
            }

            Doctor newDoctor;

            switch (specialty)
            {
                case Specialty.Pediatrician:
                    Console.Write("Enter Declared Patients: ");
                    if (!int.TryParse(Console.ReadLine(), out int declaredPatients) 
                                        || declaredPatients < 0)
                    {
                        log.Error("Declared patients cannot be negative.");
                        Console.WriteLine("Declared patients cannot be negative. " +
                                          "Please enter a valid number.");
                        return;
                    }
                    newDoctor = new Pediatrician(name, age, specialty, 
                        yearsOfExperience, salary, declaredPatients);
                    break;
                case Specialty.Surgeon:
                    Console.Write("Enter Surgeries Performed: ");
                    if (!int.TryParse(Console.ReadLine(), out int surgeriesPerformed) 
                        || surgeriesPerformed < 0)
                    {
                        log.Error("Surgeries performed cannot be negative.");
                        Console.WriteLine("Surgeries performed cannot be negative. " +
                                          "Please enter a valid number.");
                        return;
                    }
                    newDoctor = new Surgeon(name, age, specialty, 
                                            yearsOfExperience, salary, surgeriesPerformed);
                    break;
                case Specialty.Neurologist:
                case Specialty.Cardiologist:
                default:
                    newDoctor = new Doctor(name, age, specialty, 
                                            yearsOfExperience, salary);
                    break;
            }
            doctors.Add(newDoctor);
            JsonDoctorService.SerializeDoctors(doctors);
            log.Info($"Doctor '{name}' added successfully as {specialty}.");
            Console.WriteLine("Doctor added successfully.");
        }

        public static void DeleteDoctor(List<Doctor> doctors)
        {
            Console.Write("Enter the name of the doctor to delete: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty. Please enter a valid name.");
                return;
            }
            var doctorToRemove = doctors.FirstOrDefault(d => 
                                d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (doctorToRemove != null)
            {
                doctors.Remove(doctorToRemove);
                Console.WriteLine($"Doctor {name} has been removed.");
            }
            else
            {
                Console.WriteLine($"No doctor found with the name {name}.");
            }
            JsonDoctorService.SerializeDoctors(doctors);
        }

        public static void UpdateDoctor(List<Doctor> doctors)
        {
            log.Info("Updating a doctor.");
            Console.WriteLine("Updating a doctor:");

            Console.Write("Enter the name of the doctor to update: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                log.Error("Name cannot be empty.");
                Console.WriteLine("Name cannot be empty. " +
                                  "Please enter a valid name.");
                return;
            }

            var doctorToUpdate = doctors.FirstOrDefault(d => 
                                d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (doctorToUpdate == null)
            {
                log.Warn($"No doctor found with the name {name}.");
                Console.WriteLine($"No doctor found with the name {name}.");
                return;
            }

            Console.Write("Enter new Age (or press Enter to keep current): ");
            string ageInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(ageInput) && int.TryParse(ageInput, out int age) && age >= 18)
            {
                if (age < 18)
                {
                    log.Error("Age cannot be lower than legal working age.");
                    Console.WriteLine("Age cannot be lower than legal working age. " +
                                      "Please enter a valid number.");
                    return;
                }
                doctorToUpdate.Age = age;
            }
            else if (!string.IsNullOrWhiteSpace(ageInput))
            {
                log.Error("Invalid age input.");
                Console.WriteLine("Invalid age. Keeping current value.");
            }

            Console.Write("Enter new Specialty (or press Enter to keep current): ");
            string specialtyInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(specialtyInput))
            {
                if (Enum.TryParse(specialtyInput, true, out Specialty newSpecialty))
                {
                    doctorToUpdate.Specialty = newSpecialty;
                }
                else
                {
                    log.Warn($"Invalid specialty input: {specialtyInput}. Keeping current value.");
                    Console.WriteLine("Specialty cannot be empty. Keeping current value.");
                }

                Console.Write("Enter new Years of Experience " +
                              "(or press Enter to keep current): ");
                string yearsInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(yearsInput) && int.TryParse
                        (yearsInput, out int yearsOfExperience) && yearsOfExperience >= 0)
                {
                    doctorToUpdate.YearsOfExperience = yearsOfExperience;
                }
                else if (!string.IsNullOrWhiteSpace(yearsInput))
                {
                    log.Error("Invalid years of experience input.");
                    Console.WriteLine("Invalid years of experience. " +
                                      "Keeping current value.");
                }

                Console.Write("Enter new Salary (or press Enter to keep current): ");
                string salaryInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(salaryInput) && 
                    decimal.TryParse(salaryInput, out decimal salary) && salary >= 0)
                {
                    doctorToUpdate.Salary = salary;
                }
                else if (!string.IsNullOrWhiteSpace(salaryInput))
                {
                    log.Error("Invalid salary input.");
                    Console.WriteLine("Invalid salary. Keeping current value.");
                }

                if (doctorToUpdate is Surgeon surgeon)
                {
                    Console.Write("Enter new Surgeries Performed " +
                                  "(or press Enter to keep current): ");
                    string surgeriesInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(surgeriesInput) &&
                        int.TryParse(surgeriesInput, out int surgeriesPerformed) && surgeriesPerformed >= 0)
                    {
                        surgeon.SurgeriesPerformed = surgeriesPerformed;
                    }
                    else if (!string.IsNullOrWhiteSpace(surgeriesInput))
                    {
                        log.Error("Invalid surgeries performed input.");
                        Console.WriteLine("Invalid surgeries performed. Keeping current value.");
                    }
                }
                else if (doctorToUpdate is Pediatrician pediatrician)
                {
                    Console.Write("Enter new Declared Patients (or press Enter to keep current): ");
                    string patientsInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(patientsInput) && 
                        int.TryParse(patientsInput, out int declaredPatients) && declaredPatients >= 0)
                    {
                        pediatrician.DeclaredPatients = declaredPatients;
                    }
                    else if (!string.IsNullOrWhiteSpace(patientsInput))
                    {
                        log.Error("Invalid declared patients input.");
                        Console.WriteLine("Invalid declared patients. Keeping current value.");
                    }
                }
                log.Info($"Doctor '{name}' updated successfully.");
                JsonDoctorService.SerializeDoctors(doctors);
                Console.WriteLine("Doctor updated successfully.");
            }
        }
    }
}
