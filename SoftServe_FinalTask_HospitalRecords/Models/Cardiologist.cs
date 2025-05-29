
namespace SoftServe_FinalTask_HospitalRecords.Models
{

    public class Cardiologist : Doctor
    {
        public Cardiologist() { }

        public Cardiologist(string name, int age, string specialty, int yearsOfExperience, decimal salary) : base(name, age, specialty, yearsOfExperience, salary)
        { }
    }
}
