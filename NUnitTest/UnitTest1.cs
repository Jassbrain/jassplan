using System;
using NUnit.Framework;
using JassWeb.Models;

namespace NUnitTest
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {

            var class1 = new Class1();
            var result = class1.foo();
            Assert.AreEqual(result, "Hi");
        }
    }
}
