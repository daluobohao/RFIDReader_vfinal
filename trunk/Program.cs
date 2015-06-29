using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using com.espertech.esper.client;

namespace RFIDReaderMiddleware
{
    static class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program)); 
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread] 
        static void Main()
        {
            log.Info("设备中间件程序启动");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
