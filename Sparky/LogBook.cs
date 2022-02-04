using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public interface ILogBook
    {
        void Message(string message);
        bool LogToDb(string message);

        bool LogBalanceAfterWithdrawal(int balanceAfterWithdrawal);
        string MessageWithReturnStr(string message);
        bool LogWithOutputResult(string str, out string OutputStr);

        bool LogWithRefObject(ref Customer customer);
    }
    public class LogBook : ILogBook
    {
        public bool LogBalanceAfterWithdrawal(int balanceAfterWithdrawal)
        {
            if(balanceAfterWithdrawal >= 0)
            {
                return true;
            }
            Console.WriteLine("failure");
            return false;
        }

        public bool LogToDb(string message)
        {
            Console.WriteLine(message);
            return true;
        }

        public bool LogWithOutputResult(string str, out string OutputStr)
        {
            OutputStr = "Hello " + str;
            return true;
        }

        public bool LogWithRefObject(ref Customer customer)
        {
            return true;
        }

        public void Message(string message)
        {
            Console.WriteLine(message);
        }

        public string MessageWithReturnStr(string message)
        {
            Console.WriteLine(message);
            return message.ToLower();
        }
    }

    //public class LogFakker : ILogBook
    //{
    //    public void Message(string message)
    //    {
    //        Console.WriteLine(message);
    //    }
    //}
}
