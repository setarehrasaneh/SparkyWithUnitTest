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
    public class CustomerNUnitTests
    {
        private Customer customer;

        [SetUp]
        public void SetUp()
        {
            customer = new Customer();
        }


        [Test] 
        public void CombineName_InputFirstAndLastName_ReturnFullName()
        {
            //Arrange
            //Act
            customer.GreetAndCombineNames("Ben", "Spark");
            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(customer.GreetMessage, "Hello, Ben Spark");
                Assert.That(customer.GreetMessage, Is.EqualTo("Hello, Ben Spark"));
                Assert.That(customer.GreetMessage, Does.Contain("ben Spark").IgnoreCase);
                Assert.That(customer.GreetMessage, Does.StartWith("Hello,"));
                Assert.That(customer.GreetMessage, Does.EndWith("Spark"));
                Assert.That(customer.GreetMessage, Does.Match("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]"));
            });

        }

        [Test]
        public void GreetMessage_NotGreeted_ReturnsNull()
        {
            //Arrange
            //Act
            customer.GreetAndCombineNames("Ben", "Spark");
            //Assert
            Assert.IsNotNull(customer.GreetMessage);
        }

        [Test]
        public void DiscountCheck_DefaultCustomer_ReturnDiscountInRange()
        {
            int result = customer.Discount;
            Assert.That(result, Is.InRange(10, 25));
        }

        [Test]
        public void GreetMessage_GreetWithoutLastName_ReturnNotNull()
        {
            customer.GreetAndCombineNames("ben","");

            Assert.IsNotNull(customer.GreetMessage);

            Assert.IsFalse(string.IsNullOrEmpty(customer.GreetMessage));
        }

        [Test]
        public void GreetChecker_EmptyFirstName_ThrowsException()
        {
            var exceptionDetails = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Spark"));
            Assert.AreEqual("First Name Is Empty", exceptionDetails.Message);
            Assert.That(() => customer.GreetAndCombineNames("", "Spark"), 
                Throws.ArgumentException.With.Message.EqualTo("First Name Is Empty"));

            Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Spark"));
            Assert.That(() => customer.GreetAndCombineNames("", "Spark"),
              Throws.ArgumentException);
        }

        [Test]
        public void CustomerType_CreateCustomerWithLessThan100Order_ReturnBasicCustomer()
        {
            customer.OrderTotal = 10;
            var result = customer.GetCustomerDetail();
            Assert.That(result, Is.TypeOf<Basicustomer>());
        }

        [Test]
        public void CustomerType_CreateCustomerWithMoreThan100Order_ReturnPlaniumCustomer()
        {
            customer.OrderTotal = 110;
            var result = customer.GetCustomerDetail();
            Assert.That(result, Is.TypeOf<PlaniumCustomer>());
        }

    }
}
