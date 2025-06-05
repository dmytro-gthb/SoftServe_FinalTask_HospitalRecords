using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftServe_FinalTask_HospitalRecords.Models;
using SoftServe_FinalTask_HospitalRecords.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SoftServe_FinalTask_HospitalRecords.Tests
{
    [TestClass]
    public class DoctorWriterTests
    {
        [TestMethod]
        public void WriteExperiencedSurgeons_WritesExpectedContent()
        {
            var doctors = new List<Doctor>
            {
                new Surgeon("Ivan", 50, "Surgeon", 10, 30000, 1500),
                new Surgeon("Olga", 40, "Surgeon", 3, 25000, 800)
            };

            string testFilePath = "Test_ExperiencedSurgeons.txt";

            File.WriteAllLines(testFilePath,
                doctors.OfType<Surgeon>()
                    .Where(s => s.SurgeriesPerformed > 1000 && s.YearsOfExperience > 5)
                    .Select(s => $"{s.Name},{s.Age},{s.YearsOfExperience}," +
                    $"{s.Salary},{s.SurgeriesPerformed}")
            );

            var resultLines = File.ReadAllLines(testFilePath);

            Assert.AreEqual(1, resultLines.Length);
            Assert.IsTrue(resultLines[0].Contains("Ivan"));

            File.Delete(testFilePath);
        }
    }
}
