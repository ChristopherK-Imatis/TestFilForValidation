using TestFilForValidation.Data;
using TestFilForValidation.Pages;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bunit;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace SimTest
{    
    [TestClass]
    public class CreateTests
    {
        [TestMethod]
        public void NextPatientAfter_5_ShouldBe_6()
        {
            //Arrange
            var sut = new Create();
            var testData = new Data();
            testData.Employees = new List<Employee>
            {
                    new Employee { EmployeeID = 1 },
                    new Employee { EmployeeID = 2 },
                    new Employee { EmployeeID = 3 },
                    new Employee { EmployeeID = 4 },
                    new Employee { EmployeeID = 5 }
            };

            sut.data = testData;
            sut.newEmployee= new Employee();

            //Act
            sut.SetEmployeeId();

            //Assert
            Assert.AreEqual(6, sut.newEmployee.EmployeeID);
        }

        [TestMethod]
        public void TestValidationLogic_EmptyFields_ShouldFail()
        {
            using var ctx = new Bunit.TestContext();

            // Redirect the console output to a StringWriter
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var cut = ctx.RenderComponent<Create>();

            // Act: Submit the form without filling in any fields
            cut.Find("form").Submit();

            // Assert: Capture and check the console output
            var consoleOutput = sw.ToString();
            Assert.IsTrue(consoleOutput.Contains("Invalid submit"));
        }

        [TestMethod]
        public async Task TestIfDataIsLoadingCorrectly()
        {
            // Arrange
            var jsonContent = JsonSerializer.Serialize(new Data());
            File.WriteAllText("testdata.json", jsonContent);

            using var ctx = new Bunit.TestContext();
            var cut = ctx.RenderComponent<Create>();

            // Act
            // Call the public method to simulate component initialization
            await cut.Instance.InitializeDataForTestingAsync();

            // Assert
            var newPatient = cut.Instance.newEmployee;
            Assert.IsNotNull(newPatient);

            // Clean up
            File.Delete("testdata.json");
        }
    }
}