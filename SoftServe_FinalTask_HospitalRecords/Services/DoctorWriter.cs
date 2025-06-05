using SoftServe_FinalTask_HospitalRecords.Models;

namespace SoftServe_FinalTask_HospitalRecords.Services
{
    public static class DoctorWriter
    {
        public static void WriteExperiencedSurgeons(List<Doctor> skilledSurgeons)
        {
            var surgeons = skilledSurgeons.OfType<Surgeon>()
                .Where(s => s.SurgeriesPerformed > 1000 && s.YearsOfExperience > 5)
                .ToList();

            string fileName = "ExperiencedSurgeons.txt";

            File.WriteAllLines(fileName,
                surgeons.Select(s => $"{s.Name},{s.Age},{s.YearsOfExperience}," +
                $"{s.Salary},{s.SurgeriesPerformed}"));

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nExperienced surgeons with 1000 or more surgeries: ");
            Console.ResetColor();

            foreach (var line in File.ReadAllLines(fileName))
            {
                Console.WriteLine(line);
            }
        }


        public static void WritePopularPediatricians(List<Doctor> skilledPediatricians)
        {
            var pediatricians = skilledPediatricians.OfType<Pediatrician>()
            .Where(p => p.DeclaredPatients > 1000)
            .ToList();

            string fileName = "PopularPediatricians.txt";

            File.WriteAllLines("PopularPediatricians.txt",
                pediatricians.Select(p => $"{p.Name},{p.Age},{p.YearsOfExperience}," +
                $"{p.Salary},{p.DeclaredPatients}"));

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nPopular pediatritians with 1000 or more patients: ");
            Console.ResetColor();

            foreach (var line in File.ReadAllLines(fileName))
            {
                Console.WriteLine(line);
            }
        }
    }
}