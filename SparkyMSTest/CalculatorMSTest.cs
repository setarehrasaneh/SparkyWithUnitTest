using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sparky;

namespace SparkyMSTest
{
    [TestClass]
    public class CalculatorMSTest
    {
        [TestMethod]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            //Arrange

            Calculator calculator = new();

            //Act
            int result = calculator.AddNumbers(10, 20);
            //Assert
            Assert.AreEqual(30, result);

        }
    }
}