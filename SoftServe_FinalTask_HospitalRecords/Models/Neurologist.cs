
namespace SoftServe_FinalTask_HospitalRecords.Models
{
    public class Neurologist : Doctor
    {
        private Neurologist() { }

        public Neurologist(string name, int age, Specialty specialty, 
                            int yearsOfExperience, decimal salary) : 
                            base(name, age, specialty, yearsOfExperience, salary)
        { }
    }
}
