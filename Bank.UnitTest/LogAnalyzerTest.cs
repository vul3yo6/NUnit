using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UnitTest
{
    [TestFixture]
    public class LogAnalyzerTest
    {
        #region Basic

        [TestCase("filewithgoodextension.SLF")]
        [TestCase("filewithgoodextension.slf")]
        public void IsValidLogFileName_ValidExtensions_ReturnTrue(string fileName)
        {
            //arrange
            LogAnalyzer log = new LogAnalyzer();
            bool expected = true;

            //act
            bool result = log.IsValidLogFileName(fileName);

            //assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "filename has to be provided")]
        public void IsValidLogFileName_EmptyFileName_ThrowException()
        {
            //arrange
            LogAnalyzer log = new LogAnalyzer();

            //act
            bool result = log.IsValidLogFileName("");
        }

        [Test]
        public void IsValidLogFileName_EmptyFileName_Throw()
        {
            //arrange
            LogAnalyzer log = new LogAnalyzer();
            string expected = "filename has to be provided";

            //act
            var ex = Assert.Catch<ArgumentException>(() => log.IsValidLogFileName(""));

            //assert
            //Assert.AreEqual(expected, ex.Message);
            StringAssert.Contains(expected, ex.Message);
        }

        [Test]
        public void IsValidLogFileName_WhenCalled_ChangeWasLastFileNameValid()
        {
            //arrange
            LogAnalyzer log = new LogAnalyzer();
            bool expected = false;

            //act
            log.IsValidLogFileName("badname.foo");

            //assert
            Assert.AreEqual(expected, log.WasLastFileNameValid);
            //Assert.False(log.WasLastFileNameValid);
        }

        [Test]
        public void IsValidLogFileName_WhenCalled_BeforeValid()
        {
            //arrange
            LogAnalyzer log = new LogAnalyzer();
            bool wasCalled = false;

            //act
            log.BeforeValid += (o, e) => wasCalled = true;
            //log.BeforeValid += delegate(o, e){ wasCalled = true;}
            log.IsValidLogFileName("badname.foo");

            //assert
            Assert.True(wasCalled);
        }

        #endregion

        #region Advanced

        [Test]
        public void LogError_WhenCalled_CallWebService()
        {
            ////arrange
            //FakeWebService service = new FakeWebService();
            //FakeLogAnalyzer log = new FakeLogAnalyzer(service);
            //string expected = "service write some error";

            ////act
            //log.LogError(expected);

            ////assert
            //StringAssert.Contains(expected, service.LastError);
        }

        // 建構式注入
        public class FakeWebService : IWebService
        {
            public string LastError { get; set; }

            public void LogError(string message)
            {
                LastError = message;
            }
        }

        // 抽取與重寫
        public class FakeLogAnalyzer : LogAnalyzer
        {
            //public FakeLogAnalyzer(IWebService service) : base(service) { }

            //protected override IKeyPro GetKeyPro()
            //{
            //    return new FakeKeyPro();
            //}
        }

        public class FakeKeyPro : IKeyPro
        {
            public bool Check()
            {
                return true;
            }
        }

        #endregion
    }
}
