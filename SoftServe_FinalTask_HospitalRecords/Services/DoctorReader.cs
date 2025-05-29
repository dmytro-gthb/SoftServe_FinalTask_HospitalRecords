using SoftServe_FinalTask_HospitalRecords.Models;

namespace SoftServe_FinalTask_HospitalRecords.Services
{
    public static class DoctorReader
    {
        public static List<Doctor> ReadDoctorsList(string filePath)
        {
            var list = new List<Doctor>();
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        if (parts.Length < 5) continue;
                        string name = parts[0].Trim();
                        int age = int.Parse(parts[1].Trim());
                        string specialty = parts[2].Trim();
                        int yearsOfExperience = int.Parse(parts[3].Trim());
                        decimal salary = decimal.Parse(parts[4].Trim());
                        Doctor doctor;
                        switch (specialty.ToLower())
                        {
                            case "pediatrician":
                                int declaredPatients = int.Parse(parts[5].Trim());
                                doctor = new Pediatrician(name, age, specialty, yearsOfExperience, salary, declaredPatients);
                                break;
                            case "surgeon":
                                int surgeriesPerformed = int.Parse(parts[5].Trim());
                                doctor = new Surgeon(name, age, specialty, yearsOfExperience, salary, surgeriesPerformed);
                                break;
                            case "neurologist":
                                doctor = new Neurologist(name, age, specialty, yearsOfExperience, salary);
                                break;
                            case "cardiologist":
                                doctor = new Cardiologist(name, age, specialty, yearsOfExperience, salary);
                                break;
                            default:
                                doctor = new Doctor(name, age, specialty, yearsOfExperience, salary);
                                break;
                        }
                        list.Add(doctor);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
            return list;
        }
    }

}
