
namespace SoftServe_FinalTask_HospitalRecords.Models
{
    public class Pediatrician : Doctor
    {
        public int DeclaredPatients { get; set; }
        private Pediatrician() { }
        public Pediatrician(string name, int age, Specialty specialty,
                int yearsOfExperience, decimal salary, int declaredPatients)
            : base(name, age, specialty, yearsOfExperience, salary)
        {
            DeclaredPatients = declaredPatients;
        }
    }
}
