
namespace SoftServe_FinalTask_HospitalRecords.Models
{

    public class Cardiologist : Doctor
    {
        private Cardiologist() { }

        public Cardiologist(string name, int age, Specialty specialty, 
                            int yearsOfExperience, decimal salary) : 
                            base(name, age, specialty, yearsOfExperience, salary)
        { }
    }
}
