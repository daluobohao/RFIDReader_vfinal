using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using RFIDReaderMiddleware.JmsMiddleWare;
using RFIDReaderMiddleware.Crypto;
using rfiddata;
using com.espertech.esper.client;

//using System.Diagnostics;
/**
 * 主UI线程
 * @Date
 * @Author
 * @Version
 * @Description 应用主界面，进行应用的初始化(启动预过滤线程、断线重连)，并响应用户操作(包括打开读写器、关闭读写器、监听数据等)。
 *              打开读写器，从这些读写器中读取标签信息并发送到预过滤处理引擎。读写器列表显示，发送的标签信息实时显示。       
 * @Modifications
 * */

namespace RFIDReaderMiddleware
{
    public partial class Form1 : Form
    {
        #region 数据区
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Form1));
        private XMLReaderConvertor xmlreaderConvert = XMLReaderConvertor.getXMLReaderConvertor();

        private List<XtiveReaderInitClass> xtreaderlist = new List<XtiveReaderInitClass>();
        private List<RFIDReader> rfidreaderlist = new List<RFIDReader>();

        private XtiveTag[] xtaglist = new XtiveTag[200];


        private bool ismonitor = true ;//监控标志位
        private bool isclose = false;
        private int recordcount = 0;

        private Prefilter.PrefilterThread prefilter = null; //预处理线程
        private int readFre; //读标签频率

        private Reconnect.ReconnectThread reconnectThread;

        private delegate void updateMsgBoxDelegate(string strRe, string sendcount);
        private static object lockThis = new object();
        #endregion
        
        public Form1()
        {
            InitializeComponent();
            
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);

            //初始化rfidreaderlist
            rfidreaderlist = xmlreaderConvert.getRFIDReaderList("reader");
            readFre = xmlreaderConvert.getTagsReadFreq();

            //预过滤
            long prefilterTimeInterval = xmlreaderConvert.getFilterInterval();
            prefilter = new Prefilter.PrefilterThread(prefilterTimeInterval,this);
            prefilter.start();

            //断线重连
            reconnectThread = new Reconnect.ReconnectThread(this);
        }

        private void openreaderBTN_Click(object sender, EventArgs e)
        {
            try
            {
                openreaderBTN.Enabled = false;

                initReaderList();

                monitorrfidBTN.Enabled = true; //等待读写器列表初始化后检测数据按钮才可以操作 2014.2.20
                closereaderBTN.Enabled = true;

                reconnectThread.Start();
            }
            catch (Exception msg)
            {
                log.Error("打开列表读写器出现异常，异常信息为：" + msg.Message);
            }
            finally
            {
                //
            }
        }

        private void monitorrfidBTN_Click(object sender, EventArgs e)
        {
            ismonitor = (ismonitor ^ true);
            this.monitorrfidBTN.Text = ismonitor ? "停止监测数据" : "开始监测数据";
            if (ismonitor) 
            {
                closereaderBTN.Enabled = false;

                if (!backgroundWorker1.IsBusy)
                {
                    this.backgroundWorker1.RunWorkerAsync();
                }
                log.Info("开始监测数据");
            }
            else
            {
                closereaderBTN.Enabled = true;
                log.Info("停止监测数据");
            }
        }

        public void initReaderList()
        {
          //  List<RFIDReader> rfidreaderlist = new List<RFIDReader>();
          //  //连接数据库，获取所有reader的基本信息
          ////  RFIDReaderList createrredaerlist = new RFIDReaderList();
          //  XMLReaderConvertor xmlreaderlist = new XMLReaderConvertor();
          //  rfidreaderlist = xmlreaderlist.getRFIDReaderList();
           
            //rfidreaderlist的个数
            int count = rfidreaderlist.Count;
            if (count <= 0 && rfidreaderlist == null)
            {
                throw new Exception("从数据库中获取数据失败");
            }
            else
            {
                //根据不同类型的Reader,ReaderFactory实例化不通过的Reader
                ReaderFactory readerFact = new ReaderFactory();
                for (int i = 0; i < count; i++)
                {
                    //实例化reader
                    RFIDInterface reader = readerFact.MakeReader(rfidreaderlist[i].RFidreaderType);
                    //打开reader
                    //reader.ip = rfidreaderlist[i].RFidreaderIP;  //2014.2.17:设置IP
                    //System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
                    //stopwatch.Start();
                    reader.Open(rfidreaderlist[i].RFidreaderIP);
                    //stopwatch.Stop();
                    
                    reconnectThread.AlertState.Add(rfidreaderlist[i].RFidreaderIP,false);  //2014.2.24,对话框状态

                    this.readerListBox.Items.Add(Convert.ToString(rfidreaderlist[i].RFidreaderID+" " + rfidreaderlist[i].RFidreaderIP +" "+ rfidreaderlist[i].RFidreaderType));
                    
                    this.Xtreaderlist.Add(new XtiveReaderInitClass(reader, false));

                    //TimeSpan timespan = stopwatch.Elapsed;
                    //System.Console.WriteLine(rfidreaderlist[i].RFidreaderIP+" "+timespan.TotalMilliseconds);
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
           EPRuntime epRuntime = prefilter.getEPRuntime();
           while (ismonitor)
           {
               if (this.Xtreaderlist.Count > 0)
               {
                   foreach (XtiveReaderInitClass curInitR in Xtreaderlist)  //  XReaderInitList是所有已注册的reader
                   {
                       RFIDInterface curR = curInitR.reader;

                       if ((bool)reconnectThread.AlertState[curR.ip] || !curR.Check())
                       {
                           continue;        //检测跳过
                       }

                       bool bResult = curR.LoadTagData(ref xtaglist, ref recordcount);

                       if (recordcount > 0)
                       {
                           for (int i = 0; i < recordcount; i++)
                           {
                               rfiddata.XtiveTag tag = xtaglist[i];
                               epRuntime.SendEvent(tag); //发送事件到预过滤处理引擎
                           }                        
                       }
                       else
                       {
                           //strResult = "No Tag! \r\n";
                       }
                   }
               }
               Thread.Sleep(this.readFre);  //读取数据频率
           }
        }
                                 

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void closereaderBTN_Click(object sender, EventArgs e)    
        {
            isclose = true;
            if (reconnectThread != null)
                reconnectThread.Abort();

            closereaderBTN.Enabled = false;
            monitorrfidBTN.Enabled = false;

            clearReaderList();

            openreaderBTN.Enabled = true;
            
            log.Info("关闭所有读写器");
        }
        
        //关闭连接时的操作
        private bool clearReaderList()
        {
            int count = this.Xtreaderlist.Count;

            this.readerListBox.Items.Clear();

            for(int i = 0 ; i < count ; i++)
            {
                this.Xtreaderlist[i].reader.CleanBuffer();
                this.Xtreaderlist[i].reader.Close();
            }

            Xtreaderlist.Clear();
            return true;
        }

        //设置关闭窗体时动作
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            //关闭窗体前执行以下动作
            ismonitor = false;   //停止后台异步操作
            
            isclose = true;
            if (reconnectThread != null)
                reconnectThread.Abort();
            this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Close();

            log.Info("退出程序");
        }

        /// <summary>
        /// 打开写标签窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
        }

        /*
        private void button2_Click(object sender, EventArgs e)
        {
            JmsServerInfo jmsServerInfo = new JmsServerInfo();
            jmsServerInfo = xmlreaderConvert.getJmsServerInfo();
            JmsAdvancedBroker nosoursejmsbroker = new JmsAdvancedBroker(jmsServerInfo);
            nosoursejmsbroker.setShouldCrypto(false);

            DateTime startTime = DateTime.Now;

            Console.WriteLine("Start Time:" + startTime);
            //string sendmessage = "";
            for (int i = 0; i < 1; i++)
            {
                rfiddata.XtiveTag tag;

                if (i % 4 == 0)
                {
                    //sendmessage = "360000000000006010"+String.Format("{0:D6}", i)+"|-88|0|0|192.168.0.102|1|2014-5-20 20:30:20";
                    tag = (new rfiddata.XtiveTag.Builder()).SetUid("36000000000000601010" + String.Format("{0:D4}", i)).SetRssi(-88).SetBLowPower(false).SetBExcite(false).SetReaderIP("192.168.0.102").SetAn("0").SetDateTime("2014-7-10 20:30:20").Build();
                }
                else if (i % 4 == 1)
                {
                    tag = (new rfiddata.XtiveTag.Builder()).SetUid("36000000000000601010" + String.Format("{0:D4}", i)).SetRssi(-88).SetBLowPower(false).SetBExcite(false).SetReaderIP("192.168.0.102").SetAn("1").SetDateTime("2014-7-10 20:30:20").Build();
                }
                else if (i % 4 == 2)
                {
                    tag = (new rfiddata.XtiveTag.Builder()).SetUid("36000000000000601010" + String.Format("{0:D4}", i)).SetRssi(-88).SetBLowPower(false).SetBExcite(false).SetReaderIP("192.168.0.102").SetAn("2").SetDateTime("2014-7-10 20:30:20").Build();
                }
                else
                {
                    tag = (new rfiddata.XtiveTag.Builder()).SetUid("36000000000000601010" + String.Format("{0:D4}", i)).SetRssi(-88).SetBLowPower(false).SetBExcite(false).SetReaderIP("192.168.0.102").SetAn("3").SetDateTime("2014-7-10 20:30:20").Build();
                }

                nosoursejmsbroker.addXtiveTag(tag);

                //       this.sendCountLable.Text = sendcount.ToString();
                //System.Console.WriteLine(sendmessage);

                
            }
            DateTime endTime = DateTime.Now;
            TimeSpan ts = endTime - startTime;
            Console.WriteLine("End Time:" + endTime);
            Console.WriteLine("Time:" + ts.TotalMilliseconds);

        }
        */   
        public void updateMsgBoxM(string str, string sendcount)
        {
            if (this.InvokeRequired)
            {
                updateMsgBoxDelegate updateMsgBox = new updateMsgBoxDelegate(updateMsgBoxM);
                this.BeginInvoke(updateMsgBox, new object[] { str, sendcount });
            }
            else
            {
                this.contentrfidTB.Text = str;
                this.sendCountLable.Text = sendcount;
                this.contentrfidTB.ScrollToCaret();
            }
        }

        public bool IsClose
        {
            get { return this.isclose; }
        }

        public List<XtiveReaderInitClass> Xtreaderlist
        {
            get
            {
                lock (lockThis)
                {
                    return this.xtreaderlist;
                }
            }
            set
            {
                lock (lockThis)
                {
                    this.xtreaderlist = value;
                }
            }
        }
    }
}