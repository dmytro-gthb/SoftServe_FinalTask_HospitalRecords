
namespace SoftServe_FinalTask_HospitalRecords.Models
{
    public class Surgeon : Doctor
    {
        public int SurgeriesPerformed { get; set; }
        private Surgeon() { }
        public Surgeon(string name, int age, Specialty specialty, int yearsOfExperience, decimal salary, int surgeriesPerformed)
            : base(name, age, specialty, yearsOfExperience, salary)
        {
            SurgeriesPerformed = surgeriesPerformed;
        }
    }
}
