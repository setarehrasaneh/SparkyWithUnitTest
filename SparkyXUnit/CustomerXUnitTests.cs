
using Sparky;
using Xunit;

namespace SparkyNUnitTest
{
    public class CustomerXUnitTests
    {
        private Customer customer;

        public CustomerXUnitTests()
        {
            customer = new Customer();
        }


        [Fact]
        public void CombineName_InputFirstAndLastName_ReturnFullName()
        {
            //Arrange
            //Act
            customer.GreetAndCombineNames("Ben", "Spark");
            //Assert

                Assert.Equal("Hello, Ben Spark", customer.GreetMessage);
                Assert.Equal("Hello, Ben Spark", customer.GreetMessage);
                Assert.Contains("ben Spark", customer.GreetMessage.ToLower());
                Assert.StartsWith("Hello,", customer.GreetMessage);
                Assert.EndsWith("Spark", customer.GreetMessage);
                Assert.Matches("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]", customer.GreetMessage);

        }

        [Fact]
        public void GreetMessage_NotGreeted_ReturnsNull()
        {
            //Arrange
            //Act
            customer.GreetAndCombineNames("Ben", "Spark");
            //Assert
            Assert. NotNull(customer.GreetMessage);
        }


        [Fact]
        public void DiscountCheck_DefaultCustomer_ReturnDiscountInRange()
        {
            int result = customer.Discount;
            Assert.InRange(result, 10, 25);
        }

        [Fact]
        public void GreetMessage_GreetWithoutLastName_ReturnNotNull()
        {
            customer.GreetAndCombineNames("ben", "");

            Assert.NotNull(customer.GreetMessage);

            Assert.False(string.IsNullOrEmpty(customer.GreetMessage));
        }

        [Fact]
        public void GreetChecker_EmptyFirstName_ThrowsException()
        {
            var exceptionDetails = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Spark"));
            Assert.Equal("First Name Is Empty", exceptionDetails.Message);

            Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Spark"));
        }

        [Fact]
        public void CustomerType_CreateCustomerWithLessThan100Order_ReturnBasicCustomer()
        {
            customer.OrderTotal = 10;
            var result = customer.GetCustomerDetail();
            Assert.IsType<Basicustomer>(result);
        }

        [Fact]
        public void CustomerType_CreateCustomerWithMoreThan100Order_ReturnPlaniumCustomer()
        {
            customer.OrderTotal = 110;
            var result = customer.GetCustomerDetail();
            Assert.IsType<PlaniumCustomer>(result);
        }

    }
}
