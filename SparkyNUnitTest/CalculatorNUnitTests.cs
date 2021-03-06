using NUnit.Framework;
using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkyNUnitTest
{
    [TestFixture]
    public  class CalculatorNUnitTests
    {

        [Test]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            //Arrange

            Calculator calculator = new();

            //Act
            int result = calculator.AddNumbers(10, 20);
            //Assert
            Assert.AreEqual(30, result);
        }

        [Test]
        public void IsOddChecker_InputEventNumber_ReturnFalse()
        {
            //Arrange
            Calculator calculator = new();

            //Act

            bool isOdd = calculator.IsoddNumber(10);

            //Assert
            Assert.That(isOdd, Is.EqualTo(false));   
           // Assert.IsFalse(isOdd);
        }

        [Test]
        public void OddRange_InputMinAndMaxRange_ReturnValidOddNumberRange()
        {
            //Arrange
            Calculator calculator = new();
            List<int> expectedOddRange = new() { 5, 7, 9 }; //5-10

            //Act
            List<int> result = calculator.GetOddRange(5, 10);

            //Assert
            //  Assert.AreEqual(expectedOddRange, result);  
            Assert.That(result, Is.EquivalentTo(expectedOddRange));
            Assert.That(result, Does.Contain(7));
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result, Has.No.Member(6));
            Assert.That(result, Is.Ordered);
            Assert.That(result, Is.Unique);
        }

        [Test]
        [TestCase(11)]  
        [TestCase(13)]  
        public void IsOddChecker_InputOddNumber_ReturnTrue(int a)
        {
            //Arrange
            Calculator calculator = new();

            //Act

            bool isOdd = calculator.IsoddNumber(a);

            //Assert
            Assert.That(isOdd, Is.EqualTo(true));
            Assert.IsTrue(isOdd);
        }


        [Test]
        [TestCase(10, ExpectedResult =false)]
        [TestCase(11, ExpectedResult =true)]
        public bool IsOddChecker_InputNumber_ReturnTrueIfOdd(int a)
        {
            //Arrange
            Calculator calculator = new();

            //Act

            return calculator.IsoddNumber(a);

        }

        [Test]
        [TestCase(5.4,10.5)]
        [TestCase(5.43,10.53)]
        [TestCase(5.49,10.59)]
        public void AddNumbers_InputTwoDouble_GetCorrectAddition(double a , double b)
        {
            //Arrange

            Calculator calculator = new();

            //Act
            double result = calculator.AddNumbersDouble(a, b);
            //Assert
            Assert.AreEqual(15.5, result,1);
        }

    }
}
