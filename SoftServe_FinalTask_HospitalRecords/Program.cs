//Визначити класи Особа, Лікар, Педіатр, Кардіолог, Невролог, Хірург і організувати їх у правильну єрархію. 
//Вони повинні містити дані про лікаря як особу, а також дані про досвід роботи і зарплату. 
//Педіатр повинен містити ані про кількість задекларованих пацієнтів, а хірург – кількість проведених операцій. 
//Зчитати перемішані дані про лікарів із файла у масив. Вивести у новий файл хірургів з кількістю операцій більше 1000 і досвідом більше 5 років. 
//В інший файл вивести дані про педіатрів з більше ніж 1000 пацієнтів. 



using SoftServe_FinalTask_HospitalRecords.Services;

namespace SoftServe_FinalTask_HospitalRecords
{
   


    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "doctors_list.txt";

            Console.WriteLine("All doctors available: ");
            Console.WriteLine("--------------------------------");

            try
            {
                var allLines = File.ReadAllLines(inputFilePath);
                foreach (var line in allLines)
                {
                    Console.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("--------------------------------\n");

            var doctors = DoctorReader.ReadDoctorsList(inputFilePath);
            DoctorWriter.WriteFilteredDoctors(doctors);

            Console.WriteLine("Doctors list was processed and filtered files were created.");

            Console.WriteLine("\nPress:");
            Console.WriteLine(" [S] - to view ExperiencedSurgeons.txt");
            Console.WriteLine(" [P] — to view PopularPediatricians.txt");
            Console.WriteLine(" or any key to close the programm.");

            var key = Console.ReadKey(intercept: true).Key;

            if (key == ConsoleKey.S)
            {
                Console.WriteLine("List of experienced surgeons:");
                Console.WriteLine("----------------------------------");
                if (File.Exists("ExperiencedSurgeons.txt"))
                    File.ReadLines("ExperiencedSurgeons.txt").ToList().ForEach(Console.WriteLine);
                else
                    Console.WriteLine("File not found.");
            }
            else if (key == ConsoleKey.P)
            {
                Console.WriteLine("\n\nList of popular pediarticians: ");
                Console.WriteLine("-----------------------------------");
                if (File.Exists("PopularPediatricians.txt"))
                    File.ReadLines("PopularPediatricians.txt").ToList().ForEach(Console.WriteLine);
                else
                    Console.WriteLine("File not found.");
            }
        }

    }
}