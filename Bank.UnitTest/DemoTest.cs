using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UnitTest
{
    [TestFixture]
    public class DemoTest
    {
        [Test]
        public void Add_Input_First_1_Second_2_Return_3()
        {
            //arrange
            Account source = new Account();
            decimal deposit = 300m;
            decimal withdraw = 100m;
            decimal expected = 200m;

            //act
            decimal actual;
            source.Deposit(deposit);
            source.Withdraw(withdraw);
            actual = source.Balance;

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Assert_AreEqual()
        {
            Assert.AreEqual(2, 1+1, "Math is broken");

        }

        [Test]
        public void Assert_AreSame()
        {
            Assert.AreSame(int.Parse("1"), int.Parse("1"), "this test should fail");
        }

        [Test]
        [Category("DemoTest")]
        public void Assert_That()
        {
            Assert.That("Kent", Is.StringContaining("ent"));
        }

        [TestCase("Kent")]
        [TestCase("Kenny")]
        public void Assert_AreEqual_TestCase(string name)
        {
            bool result = name.Contains("Ken");
            Assert.True(result);
        }
    }
}
