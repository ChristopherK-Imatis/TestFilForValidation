using TestFilForValidation.Data;
using TestFilForValidation.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimTest
{    
    [TestClass]
    public class CreateTests
    {
        [TestMethod]
        public void TestValidationLogic_EmptyFirstName_ShouldFail()
        {
            //Arrange
            var page = new Create();

            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);

            //Act
            page.HandleInvalidSubmit(); //Trigger the method

            //Assert
            Assert.IsTrue(consoleOut.ToString().Contains("Invalid submit"));

            //Clean up! Clean up! Clean up! Clean up!
            consoleOut.Dispose();
        }
    }
}