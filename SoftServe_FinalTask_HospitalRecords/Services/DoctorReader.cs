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
                        if (!Enum.TryParse(parts[2].Trim(), true, out Specialty specialty))
                        {
                            specialty = Specialty.Undefined;
                        }
                        int yearsOfExperience = int.Parse(parts[3].Trim());
                        decimal salary = decimal.Parse(parts[4].Trim());
                        Doctor doctor;
                        switch (specialty)
                        {
                            case Specialty.Pediatrician:
                                int declaredPatients = int.Parse(parts[5].Trim());
                                doctor = new Pediatrician(name, age, specialty, yearsOfExperience, salary, declaredPatients);
                                break;

                            case Specialty.Surgeon:
                                int surgeriesPerformed = int.Parse(parts[5].Trim());
                                doctor = new Surgeon(name, age, specialty, yearsOfExperience, salary, surgeriesPerformed);
                                break;

                            case Specialty.Cardiologist:
                                doctor = new Cardiologist(name, age, specialty, yearsOfExperience, salary);
                                break;

                            case Specialty.Neurologist:
                                doctor = new Neurologist(name, age, specialty, yearsOfExperience, salary);
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
        public static void SaveDoctorsToFile(List<Doctor> doctors, string filePath)
        {
            var lines = new List<string>();

            foreach (var doc in doctors)
            {
                string line = $"{doc.Name},{doc.Age},{doc.Specialty},{doc.YearsOfExperience},{doc.Salary}";

                if (doc is Surgeon s)
                    line += $",{s.SurgeriesPerformed}";
                else if (doc is Pediatrician p)
                    line += $",{p.DeclaredPatients}";

                lines.Add(line);
            }

            File.WriteAllLines(filePath, lines);
        }

    }

}
