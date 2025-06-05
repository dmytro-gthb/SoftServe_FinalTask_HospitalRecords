using System.Text.Json;
using System.Text.Json.Serialization;
using SoftServe_FinalTask_HospitalRecords.Models;

namespace SoftServe_FinalTask_HospitalRecords.Serialization 
{
    public class DoctorJsonConverter : JsonConverter<Doctor>
    {
        public override Doctor Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;

                if (!root.TryGetProperty("Specialty", out var specialtyProp))
                    throw new JsonException("Specialty not found");

                var specialty = Enum.Parse<Specialty>(specialtyProp.GetString());

                return specialty switch
                {
                    Specialty.Surgeon => JsonSerializer.Deserialize<Surgeon>
                        (root.GetRawText(), options),
                    Specialty.Pediatrician => JsonSerializer.Deserialize<Pediatrician>
                        (root.GetRawText(), options),
                    Specialty.Cardiologist => JsonSerializer.Deserialize<Cardiologist>
                        (root.GetRawText(), options),
                    Specialty.Neurologist => JsonSerializer.Deserialize<Neurologist>
                        (root.GetRawText(), options),
                    _ => JsonSerializer.Deserialize<Doctor>(root.GetRawText(), options),
                };
            }
        }

        public override void Write(Utf8JsonWriter writer, Doctor value, JsonSerializerOptions options)
        {
            var actualType = value.GetType();
            JsonSerializer.Serialize(writer, value, actualType, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
    }
}
