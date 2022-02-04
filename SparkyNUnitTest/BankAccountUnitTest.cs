﻿using Moq;
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
    public class BankAccountUnitTest
    {

        private BankAccount account;
        [SetUp]
        public void SetUp()
        {
            
        }

        //[Test]
        //public void BankDepositLogFakker_Add100_ReturnTrue()
        //{
        //    BankAccount bankAccount = new(new LogFakker());
        //    var result = bankAccount.Deposit(100);
        //    Assert.That(result, Is.True);
        //    Assert.That(bankAccount.GetBalance(), Is.EqualTo(100));

        //}

        [Test]
        public void BankDeposit_Add100_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.Message("Deposit Invoked"));

            BankAccount bankAccount = new(logMock.Object);

            var result = bankAccount.Deposit(100);
            Assert.That(result, Is.True);
            Assert.That(bankAccount.GetBalance(), Is.EqualTo(100));

        }

        [Test]
        [TestCase(200,100)]
        [TestCase(200,150)]
        public void BankWithdraw_Withdraw100With200Balance_ReturnsTrue(int balance, int withdraw)
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(u=>u.LogToDb(It.IsAny<string>())).Returns(true);  
            logMock.Setup(u=>u.LogBalanceAfterWithdrawal(It.Is<int>(x => x >0 ))).Returns(true);

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(balance);
            var result = bankAccount.Withdraw(withdraw);
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase(200, 300)]
        public void BankWithdraw_Withdraw300Whith200Balance_ReturnFalse(int balance, int withdraw)
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x => x > 0))).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x => x < 0))).Returns(false);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.IsInRange<int>(int.MinValue, -1,Moq.Range.Inclusive))).Returns(false);

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(balance);
            var result = bankAccount.Withdraw(withdraw);
            Assert.IsFalse(result);

        }        

        [Test]
        public void BankLogDummy_LogMockString_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            var desiredOutput = "hello";
            logMock.Setup(u => u.MessageWithReturnStr(It.IsAny<string>())).Returns((string str) => str.ToLower());

            Assert.That(logMock.Object.MessageWithReturnStr("Hello"),Is.EqualTo(desiredOutput));
        }


        [Test]
        public void BankLogDummy_LogMockStringOutputStr_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            var desiredOutput = "hello";
            logMock.Setup(u => u.LogWithOutputResult(It.IsAny<string>(), out desiredOutput)).Returns(true);
            string result = "";
            Assert.IsTrue(logMock.Object.LogWithOutputResult("Ben",out result));    
            Assert.That(result, Is.EqualTo(desiredOutput));
        }


        [Test]
        public void BankLogDummy_LogRefChecker_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
           Customer customer = new ();
           Customer customerNotUsed = new ();

            logMock.Setup(u => u.LogWithRefObject(ref customer)).Returns(true);

            Assert.IsTrue(logMock.Object.LogWithRefObject(ref customer));
            Assert.IsFalse(logMock.Object.LogWithRefObject(ref customerNotUsed));
        }

    }
}
