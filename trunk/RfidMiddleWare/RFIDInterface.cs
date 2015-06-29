using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Diagnostics;
using System.Runtime.InteropServices;

using Org.LLRP.LTK.LLRPV1;
using Org.LLRP.LTK.LLRPV1.DataType;
using Org.LLRP.LTK.LLRPV1.Impinj;
using rfiddata;


namespace RFIDReaderMiddleware
{
    public interface RFIDInterface
    {
        void Open(string ReaderIP);
        void Close();
        void CleanBuffer();
        bool LoadTagData(ref XtiveTag[] tagData, ref int RecordCount);
        bool Check();
        bool WriteTagID(string TagData, int type);
        #region 关于WriteTagID函数形参的说明
        /*
         *(1)type
        数据写入格式
         */
        #endregion    

        bool IsConnected
        {
            get;
            set;
        }

        string ip
        {
            get;
            set;
        }

        int ReaderErrorCode
        {
            get;
            set;
        }
        string ReaderErrorSpecified
        {
            get;
            set;
        }

        /**
         * 2014.2.17
         * */
        void ReConnect(object arg);  
        int ReConnectCount   //重连次数  2014.2.17
        {
            get;
            set;
        }
        /**
         * 
         * */
    }

  
    /*public struct XtiveTag 
    {
        public string UID; // 标签ID
        public int RSSI;//信号强度值
        public bool bLowPower;//电源强度
        public bool bExcite;//是否处于活动状态
        public string ReaderIP;//读写器IP
        public string An;//天线端口号（读写器端口）
        public string datetime;//时间
        
    }*/

    public class ReaderFactory
    {
        public RFIDInterface MakeReader(string ReaderType)
        {
            switch (ReaderType)
            {
                case "无源I型":
                    return new Alien9800();

                case "无源II型":
                    return new ImpinjRev();

                case "有源I型":
                    return new Fenghuo();
                default:
                    return null;

            }
        }
    }

    public class XtiveReaderInitClass
    {
        public XtiveReaderInitClass(RFIDInterface reader, bool initStatus)
        {
            this.reader = reader;
            this.bInitStatus = initStatus;
        }

        public RFIDInterface reader;
        public bool bInitStatus;    // reader初始连接状态
        public int errCountOfLoadTagData = 3;   // 断线重连的容忍次数，连续3次就不容忍了
    }
  
}
