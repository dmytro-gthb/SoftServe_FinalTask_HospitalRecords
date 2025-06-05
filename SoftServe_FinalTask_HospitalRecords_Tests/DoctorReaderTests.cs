using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftServe_FinalTask_HospitalRecords.Models;
using SoftServe_FinalTask_HospitalRecords.Services;
using System.Collections.Generic;
using System.IO;

namespace SoftServe_FinalTask_HospitalRecords.Tests
{
    [TestClass]
    public class DoctorReaderTests
    {
        [TestMethod]
        public void ReadDoctorsList_ShouldReadCorrectTypes()
        {
            string testFile = "test_doctors.txt";
            File.WriteAllLines(testFile, new[]
            {
                "Ivan Petrenko,45,Surgeon,10,25000,1200",
                "Olena Ivanova,38,Pediatrician,12,18000,1500"
            });

            var doctors = DoctorReader.ReadDoctorsList(testFile);

            Assert.AreEqual(2, doctors.Count);
            Assert.IsInstanceOfType(doctors[0], typeof(Surgeon));
            Assert.IsInstanceOfType(doctors[1], typeof(Pediatrician));

            File.Delete(testFile);
        }
    }
}
