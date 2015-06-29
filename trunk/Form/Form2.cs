using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using rfiddata;

/**
 * 写标签信息线程
 * @Date
 * @Author
 * @Version
 * @Description 通过支持写操作的RFID读写器往特定标签里写标签信息
 * @Modifications
 * 
 * */
namespace RFIDReaderMiddleware
{
    public partial class Form2 : Form
    {
        #region DataBlock
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Form2));
        private XMLReaderConvertor xmlreaderConvert = XMLReaderConvertor.getXMLReaderConvertor();

        private List<RFIDReader> rfidWriterList = new List<RFIDReader>();
        private RFIDInterface reader;

        private XtiveTag[] xtaglist = new XtiveTag[100];
        private int recordcount = 0;

        //flags
        private bool openState = false;
        private bool readState = false;

        private delegate void myInvoke(string strRe);

        Form frm3;   //锁定解锁窗口

        #endregion

        public Form2()
        {
            InitializeComponent();
            bankTypeCB.SelectedIndex = 1;
            startCB.SelectedIndex = 1;
            lengthCB.SelectedIndex = 5;

            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);

            rfidWriterList = xmlreaderConvert.getRFIDReaderList("writer");

            //Reader打开之前不可用
            this.btnWrite.Enabled = false;
            this.btnReadM4.Enabled = false;
            this.lockBTN.Enabled = false;
        }

        /// <summary>
        /// 打开RFID读写器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenReader_Click(object sender, EventArgs e)
        {
            openState = (openState ^ true);
            this.btnOpenReader.Text = openState ? "Close Reader" : "Open Reader";
            if (openState)
            {
                try
                {
                    int count = rfidWriterList.Count;
                    if (count <= 0 && rfidWriterList == null)
                    {
                        throw new Exception("从数据库中获取数据失败");
                    }
                    else
                    {
                        //根据不同类型的Reader,ReaderFactory实例化不通过的Reader
                        ReaderFactory readerFact = new ReaderFactory();

                        reader = readerFact.MakeReader(rfidWriterList[0].RFidreaderType);
                        reader.Open(rfidWriterList[0].RFidreaderIP);

                        this.readerLB.Items.Add(Convert.ToString(rfidWriterList[0].RFidreaderID + " " + rfidWriterList[0].RFidreaderIP + " " + rfidWriterList[0].RFidreaderType));
                    }

                    //Reader打开之前不可用
                    this.btnWrite.Enabled = true;
                    this.btnReadM4.Enabled = true;
                    this.lockBTN.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("打开读写器失败：" + ex.Message);
                }
            }
            else
            {
                this.readerLB.Items.Clear();
                this.reader.CleanBuffer();
                this.reader.Close();

                //Reader关闭之后不可用
                this.btnWrite.Enabled = false;
                this.btnReadM4.Enabled = false;
                this.lockBTN.Enabled = false;
            }
        }

        private void btnReadM4_Click(object sender, EventArgs e)
        {
            this.btnWrite.Enabled = false;
            this.lockBTN.Enabled = false;

            readState = (readState ^ true);
            this.btnReadM4.Text = readState ? "Stop" : "Read Data";

            if (readState)
            {
                //清除上次缓存标签信息
                this.reader.CleanBuffer();

                if (!backgroundWorker1.IsBusy)
                {
                    this.backgroundWorker1.RunWorkerAsync();
                }
            }
            else
            {
                if (this.backgroundWorker1.WorkerSupportsCancellation)
                {
                    this.backgroundWorker1.CancelAsync();
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            bool flg = true;

            while (flg)
            {
                this.reader.LoadTagData(ref xtaglist, ref recordcount);

                for (int i = 0; i < recordcount; i++)
                {
                    flg = false;
                    string tag = xtaglist[i].Uid + " An:" + xtaglist[i].An;

                    myInvoke mi = new myInvoke(showTags);
                    IAsyncResult aResult;
                    object CallResult;
                    aResult = this.msgLB.BeginInvoke(mi, tag);
                    aResult.AsyncWaitHandle.WaitOne();
                    CallResult = this.msgLB.EndInvoke(aResult);
                }

                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
            }
        }

        private void showTags(string strR)
        {
            this.msgLB.Items.Add(strR);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {//后台线程出错
                this.msgLB.Items.Add("Error:" + e.Error.Message);
            }
            else if (e.Cancelled)
            {//取消后台操作
                this.msgLB.Items.Add("No Tag Detect");
            }
            else
            {//正常结束
                readState = (readState ^ true);
                this.btnReadM4.Text = readState ? "Stop" : "Read Data";
            }

            this.btnWrite.Enabled = true;
            this.lockBTN.Enabled = true;
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            this.btnReadM4.Enabled = false;

            string writeData = this.tbWriteValue.Text.Trim();
            int bankType = this.bankTypeCB.SelectedIndex;

            if (this.reader.WriteTagID(writeData, bankType))
            {
                this.msgLB.Items.Add("Success!");
            }
            else if(this.reader.ReaderErrorCode == 154)
            {
                this.msgLB.Items.Add("No tag found.");
            }

            this.btnReadM4.Enabled = true;
        }

        private void clearBTN_Click(object sender, EventArgs e)
        {
            msgLB.Items.Clear();
        }

        private void tbWriteValue_TextChanged(object sender, EventArgs e)
        {
            this.dataLength.Text = "" + this.tbWriteValue.TextLength;
        }

        private void lockBTN_Click(object sender, EventArgs e)
        {
            this.msgLB.Items.Add("This button is unavailable now.");
        }
    }
}
