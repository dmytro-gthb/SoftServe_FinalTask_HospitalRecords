
namespace SoftServe_FinalTask_HospitalRecords.Models
{
    public class Doctor : Person
    {
        public Specialty Specialty { get; set; }
        public int YearsOfExperience { get; set; }
        public decimal Salary { get; set; }
        public Doctor() { }
        public Doctor(string name, int age, Specialty specialty, 
                        int yearsOfExperience, decimal salary) : base(name, age)
        {
            Specialty = specialty;
            YearsOfExperience = yearsOfExperience;
            Salary = salary;
        }
    }
}
