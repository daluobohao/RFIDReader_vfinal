using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using rfiddata;

namespace RFIDReaderMiddleware
{
    public class Fenghuo : RFIDInterface
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Fenghuo));

        public void Open(string ReaderIP)
        {
            try
            {
                _ip = ReaderIP;
                if (IntPtr.Zero == (m_handle = OpenReader(ReaderIP)))
                {
                    _IsConnected = false;
                    _ReaderErrorCode = 12;
                    _ReaderErrorSpecified = "读写器Open函数连接不上：" + ReaderIP.ToString();
                    log.Info(_ReaderErrorSpecified);
                    return;
                }
                else
                {
                    //_ip = ReaderIP;
                    _IsConnected = true;
                    _ReaderErrorCode = 0;
                    _ReaderErrorSpecified = "";
                }
            }
            catch (Exception e)
            {
                _IsConnected = false;
                _ReaderErrorCode = 12;
                _ReaderErrorSpecified = "读写器Open函数故障：" + ReaderIP.ToString() + "  " + e.Message.ToString();
                log.Error(_ReaderErrorSpecified);
                return;
            }
            finally
            { }
        }

        public void Close()
        {
            try
            {
             //   Debug.WriteLine("into Close: " + m_handle.ToString());
                if (m_handle != IntPtr.Zero)
                {
                    CloseReader(m_handle);
                    m_handle = IntPtr.Zero;
                }

                _IsConnected = false;        // set reader.IsConnected = false manually
                _ReaderErrorCode = 0;
                _ReaderErrorSpecified = "";
            }
            catch (Exception e)
            {
                _ReaderErrorCode = 22;
                _ReaderErrorSpecified = "读写器Close函数故障：" + m_handle.ToString() + "  " + e.Message.ToString();
                log.Error(_ReaderErrorSpecified);
                return;
            }
            finally
            {
            }
        }

        public void CleanBuffer()
        {
            try
            {
                while (ReaderTag(m_handle, ref btTagType, ref TagID, ref bExcite, ref bLowPower, ref  ExciteID, ref RID, ref fRSSI))
                {
                    ;
                }
                _ReaderErrorCode = 0;
                _ReaderErrorSpecified = "";
            }
            catch (Exception e)
            {
                _ReaderErrorCode = 32;
                _ReaderErrorSpecified = "读写器CleanBuffer函数故障：" + m_handle.ToString() + "  " + e.Message.ToString();
                log.Error(_ReaderErrorSpecified);
                return;
            }
            finally { }
        }

        public bool LoadTagData(ref XtiveTag[] tagData, ref int RecordCount)
        {
            if (m_handle == IntPtr.Zero)
                return false;

            RecordCount = 0;
            try
            {
                while (RecordCount < 100)
                {
                    if (!ReaderTag(m_handle, ref btTagType, ref TagID, ref bExcite, ref bLowPower, ref  ExciteID, ref RID, ref fRSSI))  // get one tag once, FIFO
                    {
                   //     Debug.WriteLine("failed to ReaderTag");
                        break;
                    }
                    else
                    {
                  //     Debug.WriteLine(TagID.ToString());
                        /*tagData[RecordCount].RSSI = Convert.ToInt32(fRSSI);
                        tagData[RecordCount].UID = TagID.ToString();
                        //    tagData[RecordCount].RID = RID;
                        tagData[RecordCount].bLowPower = bLowPower;
                        tagData[RecordCount].bExcite = bExcite;
                        tagData[RecordCount].An = "0";
                        tagData[RecordCount].ReaderIP = ip;
                        tagData[RecordCount].datetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");*/
                        XtiveTag tag = (new XtiveTag.Builder())
                                   .SetUid(TagID.ToString())
                                   .SetRssi(Convert.ToInt32(fRSSI))
                                   .SetBLowPower(bLowPower)
                                   .SetBExcite(bExcite)
                                   .SetReaderIP(ip)
                                   .SetAn("0")
                                   .SetDateTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Build();
                        tagData[RecordCount] = tag;
                        RecordCount++;
                    }
                }
                _ReaderErrorCode = 0;
                _ReaderErrorSpecified = "";

            }
            catch (Exception e)
            {
                _ReaderErrorCode = 42;
                _ReaderErrorSpecified = "读写器LoadTagData函数故障：" + m_handle.ToString() + "  " + e.Message.ToString();
                log.Error(_ReaderErrorSpecified);
                return false;
            }
            finally
            {

            }
            if (RecordCount == 0)
                return false;
            else
                return true;

        }

        public bool Check()
        {
            try
            {
                _ReaderErrorCode = 0;
                _ReaderErrorSpecified = "";
                return CheckReader(m_handle);
            }
            catch (Exception e)
            {
                _ReaderErrorCode = 52;
                _ReaderErrorSpecified = "读写器LoadTagData函数故障：" + m_handle.ToString() + "  " + e.Message.ToString();
                log.Error(_ReaderErrorSpecified);
                return false;
            }
            finally
            {
            }
        }

        public bool WriteTagID(string TagData, int type)
        {
            return false;
        }

        /**  
         * 2014.2.17 
         * */
        public void ReConnect(object arg)
        {
            ReConnectCount++;
            string ReaderIP = (string)arg;

            Open(ReaderIP);
            Thread.Sleep(3000);  //等待重连
        }

        private object lockK = new object();
        private int _ReConnectCount = 0;
        public int ReConnectCount
        {
            get
            {
                lock (lockK)
                {
                    return _ReConnectCount;
                }
            }
            set
            {
                lock (lockK)
                {
                    _ReConnectCount = value;
                }
            }
        }
        /**
         * */


        #region  参数定义

        private bool _IsConnected;
        public bool IsConnected
        {
            get
            {
                return _IsConnected;
            }
            set
            {
                _IsConnected = value;
            }

        }

        private string _ip;
        public string ip
        {
            get
            {
                return _ip;
            }
            set
            {
                _ip = value;
            }
        }

        private int _ReaderErrorCode = 0;
        public int ReaderErrorCode
        {
            get
            {
                return _ReaderErrorCode;
            }
            set
            {
                _ReaderErrorCode = value;
            }
        }

        private string _ReaderErrorSpecified = null;
        public string ReaderErrorSpecified
        {
            get
            {
                return _ReaderErrorSpecified;
            }
            set
            {
                _ReaderErrorSpecified = value;
            }
        }
        private IntPtr m_handle = IntPtr.Zero;

        ////////////////////////
        //btTagType     标签类型
        //TagID         标签ID
        //bExcite       是否激励 
        //bLowPower     是否低电
        //ExciteID      激励器ID
        //RID           读写器 ID
        //fRSSI         信号强度

        private byte btTagType = 0;
        private UInt32 TagID = 0;
        private bool bExcite = false;
        private bool bLowPower = false;
        private UInt16 ExciteID;
        private UInt16 RID;
        private float fRSSI;

        #endregion

        #region dll引入
        //初始化读写器或者无线收发器
        [DllImport("SP5X0.dll")]
        public static extern IntPtr OpenReader(string ip);
        //关闭读写器
        [DllImport("SP5X0.dll")]
        public static extern void CloseReader(IntPtr hCom);
        //读取标签
        [DllImport("SP5X0.dll")]
        public static extern bool ReaderTag(IntPtr hCom, ref byte TagType, ref UInt32 TagID, ref bool bExcite, ref bool bLowPower, ref UInt16 ExciteID, ref UInt16 RID, ref float fRSSI);

        //读取标签
        [DllImport("SP5X0.dll")]
        public static extern bool CheckReader(IntPtr hCom);

      
        #endregion

    }
}
