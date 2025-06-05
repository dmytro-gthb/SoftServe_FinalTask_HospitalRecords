using SoftServe_FinalTask_HospitalRecords.Models;
using SoftServe_FinalTask_HospitalRecords.Serialization;
using System.Text.Json;

using System.Text.Json.Serialization;
using System.IO;

namespace SoftServe_FinalTask_HospitalRecords.Services
{
    public static class JsonDoctorService
    {
        private const string JsonFilePath = "doctors.json";

        public static void SerializeDoctors(List<Doctor> doctors)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new DoctorJsonConverter() },
                ReferenceHandler = System.Text.Json.Serialization.
                                    ReferenceHandler.IgnoreCycles
            };


            string json = JsonSerializer.Serialize(doctors, options);
            File.WriteAllText(JsonFilePath, json);
            Console.WriteLine("Doctors saved to JSON.");
        }

        public static List<Doctor> DeserializeDoctors()
        {
            if (!File.Exists(JsonFilePath))
            {
                Console.WriteLine("JSON file not found.");
                return new List<Doctor>();
            }

            string json = File.ReadAllText(JsonFilePath);

            var options = new JsonSerializerOptions
            {
                Converters = { new DoctorJsonConverter() }
            };

            return JsonSerializer.Deserialize<List<Doctor>>(json, options) ?? 
                   new List<Doctor>();
        }
    }
}