﻿using NUnit.Framework;
using Sparky;
using System.Collections.Generic;

namespace SparkyNUnitTest
{
    [TestFixture]
    public class FiboNUnitTest
    {
        [Test]
        public void FiboChecker_Input1_ReturnFiboSeries()
        {
            List<int> expectedRange = new() { 0 };
            Fibo fibo = new();
            fibo.Range = 1;

            List<int> result = fibo.GetFiboSeries();

            Assert.That(result, Is.Not.Empty);  
            Assert.That(result, Is.Ordered);  
            Assert.That(result, Is.EquivalentTo(expectedRange));  
        }

        [Test]
        public void FiboChecker_Input6_ReturnFiboSeries()
        {
            List<int> expectedRange = new() { 0, 1, 1, 2 ,3 ,5 };
            Fibo fibo = new();
            fibo.Range = 6;

            List<int> result = fibo.GetFiboSeries();

            Assert.That(result, Does.Contain(3));
            Assert.That(result, Has.No.Member(4));
            Assert.That(result.Count, Is.EqualTo(6));
            Assert.That(result, Is.EquivalentTo(expectedRange));
        }
    }
}
