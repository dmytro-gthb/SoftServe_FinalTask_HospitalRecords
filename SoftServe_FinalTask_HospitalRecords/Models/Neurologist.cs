
namespace SoftServe_FinalTask_HospitalRecords.Models
{
    public class Neurologist : Doctor
    {
        public Neurologist() { }

        public Neurologist(string name, int age, string specialty, int yearsOfExperience, decimal salary) : base(name, age, specialty, yearsOfExperience, salary)
        { }
    }
}
