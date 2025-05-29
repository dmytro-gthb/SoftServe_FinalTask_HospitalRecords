using SoftServe_FinalTask_HospitalRecords.Models;

namespace SoftServe_FinalTask_HospitalRecords.Services
{
    public static class DoctorWriter
    {
        public static void WriteFilteredDoctors(List<Doctor> skilledDoctors)
        {
            var surgeons = skilledDoctors.OfType<Surgeon>()
                .Where(s => s.SurgeriesPerformed > 1000 && s.YearsOfExperience > 5)
                .ToList();

            var pediatricians = skilledDoctors.OfType<Pediatrician>()
                .Where(p => p.DeclaredPatients > 1000)
                .ToList();

            File.WriteAllLines("ExperiencedSurgeons.txt",
                surgeons.Select(s => $"{s.Name},{s.Age},{s.YearsOfExperience},{s.Salary},{s.SurgeriesPerformed}"));

            File.WriteAllLines("PopularPediatricians.txt",
                pediatricians.Select(p => $"{p.Name},{p.Age},{p.YearsOfExperience},{p.Salary},{p.DeclaredPatients}"));
        }
    }
}
