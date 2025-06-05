//Визначити класи Особа, Лікар, Педіатр, Кардіолог, Невролог, Хірург і організувати їх у правильну єрархію. 
//Вони повинні містити дані про лікаря як особу, а також дані про досвід роботи і зарплату. 
//Педіатр повинен містити ані про кількість задекларованих пацієнтів, а хірург – кількість проведених операцій. 
//Зчитати перемішані дані про лікарів із файла у масив. Вивести у новий файл хірургів з кількістю операцій більше 1000 і досвідом більше 5 років. 
//В інший файл вивести дані про педіатрів з більше ніж 1000 пацієнтів. 

using SoftServe_FinalTask_HospitalRecords.Models;
using SoftServe_FinalTask_HospitalRecords.Services;
using log4net;

namespace SoftServe_FinalTask_HospitalRecords
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DoctorManager));
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
            ILog log = LogManager.GetLogger(typeof(Program));
            log.Info("Program started");
            string filePath = "doctors_list.txt";
            List<Doctor> doctors = DoctorReader.ReadDoctorsList(filePath);
            if (doctors == null || !doctors.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No doctors available or file not found.");
                Console.ResetColor();
                return;
            }

            bool running = true;

            while (running)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. View all doctors");
                Console.WriteLine("2. Add doctor");
                Console.WriteLine("3. Update doctor info");
                Console.WriteLine("4. Delete doctor");
                Console.WriteLine("5. Save changes");
                Console.WriteLine("6. View experienced surgeons");
                Console.WriteLine("7. View popular pediatricians");
                Console.WriteLine("0. Exit");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter your choice (0-7): ");
                Console.ResetColor();
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        DoctorManager.ShowAllDoctors(doctors);
                        break;
                    case 2:
                        DoctorManager.CreateDoctor(doctors);
                        break;
                    case 3:
                        DoctorManager.UpdateDoctor(doctors);
                        break;
                    case 4:
                        DoctorManager.DeleteDoctor(doctors);
                        break;
                    case 5:
                        DoctorReader.SaveDoctorsToFile(doctors, filePath);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Changes saved successfully.");
                        Console.ResetColor();
                        break;
                    case 6:
                        DoctorWriter.WriteExperiencedSurgeons(doctors);
                        break;
                    case 7:
                        DoctorWriter.WritePopularPediatricians(doctors);
                        break;
                    case 0:
                        running = false;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Exiting the program.");
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}