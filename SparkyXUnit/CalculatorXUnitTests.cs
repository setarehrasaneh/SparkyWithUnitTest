using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SparkyNUnitTest
{

    public  class CalculatorXUnitTests
    {

        [Fact]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            //Arrange

            Calculator calculator = new();

            //Act
            int result = calculator.AddNumbers(10, 20);
            //Assert
            Assert.Equal(30, result);
        }

        [Fact]
        public void IsOddChecker_InputEventNumber_ReturnFalse()
        {
            //Arrange
            Calculator calculator = new();

            //Act

            bool isOdd = calculator.IsoddNumber(10);

            //Assert
            Assert.False(isOdd);
        }

        [Fact]
        public void OddRange_InputMinAndMaxRange_ReturnValidOddNumberRange()
        {
            //Arrange
            Calculator calculator = new();
            List<int> expectedOddRange = new() { 5, 7, 9 }; //5-10

            //Act
            List<int> result = calculator.GetOddRange(5, 10);

            //Assert
            //  Assert.AreEqual(expectedOddRange, result);  
            Assert.Equivalent(expectedOddRange, result);
            Assert.Contains(7, result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count);
            Assert.DoesNotContain(6, result);
            Assert.Equal(result.OrderBy( u => u), result);
        }


        [Theory]
        [InlineData(11)]  
        [InlineData(13)]  
        public void IsOddChecker_InputOddNumber_ReturnTrue(int a)
        {
            //Arrange
            Calculator calculator = new();

            //Act

            bool isOdd = calculator.IsoddNumber(a);

            //Assert
            Assert.True(isOdd);
        }


        [Theory]
        [InlineData(10, false)]
        [InlineData(11,  true)]
        public void IsOddChecker_InputNumber_ReturnTrueIfOdd(int a , bool expectedResult)
        {
          // Arrange
            Calculator calculator = new();

          //  Act

            var result =  calculator.IsoddNumber(a);
            //Assert
            Assert.Equal(expectedResult, result);

        }

        [Theory]
        [InlineData(5.4,10.5)] //15.09
        //[InlineData(5.43,10.53)] //15.96
        //[InlineData(5.49,10.59)] //16.08
        public void AddNumbers_InputTwoDouble_GetCorrectAddition(double a , double b)
        {
            //Arrange

            Calculator calculator = new();

            //Act
            double result = calculator.AddNumbersDouble(a, b);
            //Assert
            Assert.Equal(15.5, result,0);
        }

    }
}
