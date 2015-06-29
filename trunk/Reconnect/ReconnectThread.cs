using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;  //DLLImport

/**
 * 断线重连线程
 * @Date 2014.7.31
 * @Author 
 * @Version 
 * @Description 断线自动重连，并弹出正在重连的提示框。重连达到指定次数(预设3次)后，弹出
 *              需要用户响应提示框，等待用户响应。然后分别间隔1分钟、2分钟、4分钟、8分钟、
 *              16分钟、32分钟......后再次尝试自动重连。用户响应后将重连次数重置为0，并
 *              进入新一轮重连机制循环。
 * @Modifications
 * */
namespace RFIDReaderMiddleware.Reconnect
{
    class ReconnectThread
    { 
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ReconnectThread));
        private Form1 form1;
        private Hashtable alertState = new Hashtable();
        private HashSet<int> reconnectCntSet = new HashSet<int>{4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384};
        private Thread reconnectThread;
        private static object lockThis = new object();

        public ReconnectThread(Form1 form)
        {
            this.form1 = form;
        }

        public void Start()
        {
            reconnectThread = new Thread(new ThreadStart(reconnect));
            reconnectThread.Start();
        }

        public void Abort()
        {
            reconnectThread.Abort();
            AlertState.Clear();
        }

        public bool IsAlive()
        {
            return reconnectThread.IsAlive;
        }

        public Hashtable AlertState
        {
            get
            {
                lock (lockThis)
                {
                    return this.alertState;
                }
            }
            set
            {
                lock (lockThis)
                {
                    alertState = value;
                }
            }
        }

        private void reconnect()
        {
            Thread.Sleep(20000);   //检测线程启动延时
            while (!form1.IsClose)
            {
                for (int i = 0; i < form1.Xtreaderlist.Count; i++)  //  XReaderInitList是所有已注册的reader
                {
                    XtiveReaderInitClass CurInitR = form1.Xtreaderlist[i];
                    RFIDInterface CurR = CurInitR.reader;

                    //Test
                    //bool flag = CurR.Check();
                    //System.Console.WriteLine(CurR.ip + " " + CurR.Check() + CurR.ReConnectCount);
                    //if (!CurR.Check() && CurR.ReConnectCount < CurInitR.errCountOfLoadTagData)
                    //if (!flag)

                    if (!CurR.Check())
                    {
                        if (CurR.ReConnectCount <= CurInitR.errCountOfLoadTagData || reconnectCntSet.Contains(CurR.ReConnectCount))
                        {
                            Thread tipThread = new Thread(new ParameterizedThreadStart(reconnectTips));
                            tipThread.IsBackground = true;
                            tipThread.Start(CurR);

                            CurR.ReConnect(CurR.ip);
                        }
                        else if (!(bool)AlertState[CurR.ip])
                        {
                            Thread connectThread = new Thread(new ParameterizedThreadStart(reconnectWait));
                            connectThread.IsBackground = true;
                            //connectThread.Priority = ThreadPriority.Lowest;
                            connectThread.Start(CurR);

                            CurR.ReConnectCount++;
                        }
                        else
                        {
                            CurR.ReConnectCount++;
                        }
                    }
                }
                Thread.Sleep(15000); //检测频率15s
            }
        }

        private void reconnectTips(object obj)
        { //提示窗口线程
            RFIDInterface creader = obj as RFIDInterface;
            Thread boxclose = new Thread(new ParameterizedThreadStart(closebox));
            boxclose.IsBackground = true;
            boxclose.Start(obj);
            MessageBox.Show(creader.ip + "断线，正在尝试重连......", creader.ip + "Tips");
            log.Info(creader.ip + "断线，正在尝试重连......");
        }

        private void closebox(object obj)
        {//关闭提示窗口线程
            RFIDInterface clreader = obj as RFIDInterface;
            Thread.Sleep(1000);
            IntPtr dlg = FindWindow(null, clreader.ip + "Tips");
            if (dlg != IntPtr.Zero)
            {
                PostMessage(dlg, VM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
        }

        private void reconnectWait(object obj)
        {  /*
            * 弹出对话框，提示用户读写器断线，并等待用户响应，重连或返回  2014.2.24
            */
            RFIDInterface rfidreader = obj as RFIDInterface;

            AlertState[rfidreader.ip] = true;

            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show(rfidreader.ip + "掉线或断电，请重新连接或上电重启，完成后按\"确定\"按钮重连", "断线重连", messButton);

            log.Info(rfidreader.ip + "掉线或断电");
            //System.Console.WriteLine("Wait");
            if (dr == DialogResult.OK)
            {
                rfidreader.ReConnect(rfidreader.ip);
            }
            //System.Console.WriteLine("Reconnect");

             AlertState[rfidreader.ip] = false;
             rfidreader.ReConnectCount = 0;
        }

        #region dll引入
        //找到窗体
        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        //发送模拟"Enter"按键
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        public const int VM_CLOSE = 0x10;
        #endregion
    }
}
