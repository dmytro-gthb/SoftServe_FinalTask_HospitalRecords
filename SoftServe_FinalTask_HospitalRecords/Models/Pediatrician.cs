
namespace SoftServe_FinalTask_HospitalRecords.Models
{
    public class Pediatrician : Doctor
    {
        public int DeclaredPatients { get; set; }
        public Pediatrician() { }
        public Pediatrician(string name, int age, string specialty, int yearsOfExperience, decimal salary, int declaredPatients)
            : base(name, age, specialty, yearsOfExperience, salary)
        {
            DeclaredPatients = declaredPatients;
        }
    }
}
