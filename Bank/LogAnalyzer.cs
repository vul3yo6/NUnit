using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class LogAnalyzer
    {
        #region Basic

        public bool WasLastFileNameValid { get; set; }

        public event EventHandler BeforeValid;

        public LogAnalyzer() { }

        public bool IsValidLogFileName(string fileName)
        {
            WasLastFileNameValid = false;

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("filename has to be provided");
            }

            // 呼叫事件
            if (BeforeValid != null)
            {
                BeforeValid(this, EventArgs.Empty);
            }

            if (fileName.EndsWith(".SLF"))
            {
                WasLastFileNameValid = true;
                return true;
            }

            return false;
        } 

        #endregion

        // --------------------------------------

        #region Advanced

        //protected IWebService service;

        //public LogAnalyzer(IWebService service)
        //{
        //    this.service = service;
        //}

        protected WebService service = new WebService();

        /// <summary>
        /// 1.透過抽取與重寫 - 建立KeyPro存根
        /// 2.透過建構式注入 - 建立WebService模擬對象
        /// </summary>
        /// <param name="message"></param>
        public void LogError(string message)
        {
            //var key = GetKeyPro();
            var key = new KeyPro();
            if (key.Check())
            {
                this.service.LogError(message);
            }
        }

        //protected virtual IKeyPro GetKeyPro()
        //{
        //    return new KeyPro();
        //} 

        #endregion
    }

    // 模擬對象, 可以截取成interface
    public class WebService //: IWebService
    {
        public void LogError(string message) { }
    }

    public interface IWebService
    {
        void LogError(string message);
    }

    // 存根, 可以截取成interface
    public class KeyPro //: IKeyPro
    {
        public bool Check()
        {
            return false;
        }
    }

    public interface IKeyPro
    {
        bool Check();
    }
}
