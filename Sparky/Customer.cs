using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{

    public interface ICustomer
    {
        int Discount { get; set; }
        string GreetMessage { get; set; }

        int OrderTotal { get; set; }

        bool IsPlatinum { get; set; }

        string GreetAndCombineNames(string firstName, string lastName);
        public CustomerType GetCustomerDetail();
    }
    public class Customer : ICustomer
    {
        public int Discount { get; set; }
        public string GreetMessage { get; set; }

        public int OrderTotal { get; set; }

        public bool IsPlatinum { get; set; }

        public Customer()
        {
            IsPlatinum = false;
            Discount = 15;
        }

        public string GreetAndCombineNames(string firsName, string lastName)
        {

            if (string.IsNullOrWhiteSpace(firsName))
            {
                throw new ArgumentException("First Name Is Empty");
            }
            GreetMessage = $"Hello, {firsName} {lastName}";
            Discount = 20;
            return GreetMessage;
        }

        public CustomerType GetCustomerDetail()
        {
            if (OrderTotal < 100)
            {
                return new Basicustomer();
            };
            return new PlaniumCustomer();
        }
    }

        public class CustomerType { }
        public class Basicustomer : CustomerType { }

        public class PlaniumCustomer : CustomerType { }
    }