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

    public class ImpinjRev : RFIDInterface
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ImpinjRev));

        LLRPClient reader = new LLRPClient();
        MSG_ERROR_MESSAGE msg_err;
        List<string> RecordTag = new List<string>(1000);
        List<int> RecordRssi = new List<int>(1000);
        List<string> AntennaID = new List<string>(1000);
        int InitNumWord = 0;
        bool writeOp = false;
        
        //2014.2.19
        private static uint HeartPericodic = 2000;
        private System.Timers.Timer timer;

        public void Open(string ReaderIP)
        {
            _ip = ReaderIP;
            Subscription();
            ENUM_ConnectionAttemptStatusType status;
            bool ret = reader.Open(ReaderIP, 5000, out status);

            if (!ret || status != ENUM_ConnectionAttemptStatusType.Success) return;

            if (Set_Reader_Config())
                IsConnected = true;

            //subscribe to reader event notification and ro access report
            //reader.OnRoAccessReportReceived += new delegateRoAccessReport(reader_OnRoAccessReportReceived);
            // reader.OnRoAccessReportReceived += new delegateRoAccessReport(reader_OnRoAccessReportReceived);
            //reader.OnKeepAlive += new delegateKeepAlive(reader_OnKeepaliveACK);
           
        }
        public void Close()
        {
            // reader.OnReaderEventNotification -= new delegateReaderEventNotification(reader_OnReaderEventNotification);
            reader.OnRoAccessReportReceived -= new delegateRoAccessReport(reader_OnRoAccessReportReceived);
            //reader.OnKeepAlive += new delegateKeepAlive(reader_OnKeepaliveACK);

            reader.Close();
        }

        public void CleanBuffer()
        {
            RecordTag.Clear();
        }

        public bool Check()
        {//2014.2.19
            return IsConnected;
            /**
             * * /
            if (ReaderErrorCode == 14)
                return false;
            else
                return true;
            /**
             * */
        }

        /**
         * 2014.2.18
         * */
        public void ReConnect(object arg)
        {
            ReConnectCount++;
            string ReaderIP = (string)arg;

            Open(ReaderIP);
            //System.Console.WriteLine("reconnect");
            Thread.Sleep(7000);  //等待初始化
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


        public bool LoadTagData(ref XtiveTag[] tagData, ref int RecordCount)
        {
            bool bResult = true;
            try
            {
                if (bResult != false)
                {
                    bResult = Stop_RoSpec();
                }
                if (bResult != false)
                {
                    bResult = Delete_RoSpec();
                }
                if (bResult != false)
                {
                    bResult = DELETE_ACCESSSPEC();
                }
                if (bResult != false)
                {
                    bResult = Add_RoSpec();
                };
                if (bResult != false)
                {
                    bResult = Enable_RoSpec();
                }
                if (bResult != false)
                {
                    bResult = Start_RoSpec();
                }
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
                return false;
            }

            System.Threading.Thread.Sleep(100);
            try
            {
                // 读取标签
                int i = 0;
                RecordCount = RecordTag.Count;
                for (i = 0; i < RecordCount; i++)
                {
                    /*tagData[i].UID = RecordTag[i];
                    tagData[i].RSSI = RecordRssi[i];
                    tagData[i].An = AntennaID[i];
                    tagData[i].bLowPower = true;
                    tagData[i].bExcite = true;
                    tagData[i].ReaderIP = ip;
                    tagData[i].datetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");*/

                    XtiveTag tag = (new XtiveTag.Builder())
                                   .SetUid(RecordTag[i])
                                   .SetRssi(RecordRssi[i])
                                   .SetBLowPower(true)
                                   .SetBExcite(true)
                                   .SetReaderIP(ip)
                                   .SetAn(AntennaID[i])
                                   .SetDateTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Build();
                    tagData[i] = tag;

                }
                RecordTag.Clear();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return false;
            }



            return true;


        }


        public bool WriteTagID(string TagData, int type)
        {
            bool bResult = false;
            InitNumWord = TagData.Length / 4;
            try
            {

                bResult = Stop_RoSpec();
                if (bResult != false)
                {
                    bResult = Delete_RoSpec();
                }
                if (bResult != false)
                {
                    bResult = DELETE_ACCESSSPEC();
                }
                if (bResult != false)
                {
                    bResult = Add_RoSpec();
                };
                if (bResult != false)
                {
                    bResult = ADD_ACCESSSPEC(TagData, type);
                }
                if (bResult != false)
                {
                    bResult = ENABLE_ACCESSSPEC();
                }
                if (bResult != false)
                {
                    bResult = Enable_RoSpec();
                }
                if (bResult != false)
                {
                    bResult = Start_RoSpec();
                }
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
                return false;
            }
            System.Threading.Thread.Sleep(400);

            return writeOp;
        }

     
        public void Subscription()
        {
            reader.OnRoAccessReportReceived += new delegateRoAccessReport(reader_OnRoAccessReportReceived);
            //reader.OnRoAccessReportReceived += new delegateRoAccessReport(reader_OnRoAccessReportReceived);
            reader.OnReaderEventNotification += new delegateReaderEventNotification(reader_OnReaderEventReceived);

            reader.OnKeepAlive += new delegateKeepAlive(reader_OnKeepAlive);  //2014.2.19
        }


        private void reader_OnRoAccessReportReceived(MSG_RO_ACCESS_REPORT msg)
        {
            if (msg.TagReportData == null || msg.TagReportData.Length < 1) return;
            
            try
            {
                for (int i = 0; i < msg.TagReportData.Length; i++)
                {
                   
                    if (msg.TagReportData[i].EPCParameter.Count > 0)
                    {
                        string epc;
                        string an;
                        // reports come in two flavors.  Get the right flavor
                        if (msg.TagReportData[i].EPCParameter[0].GetType() == typeof(PARAM_EPC_96))
                        {
                            epc = ((PARAM_EPC_96)(msg.TagReportData[i].EPCParameter[0])).EPC.ToHexString();
                        }
                        else
                        {
                            epc = ((PARAM_EPCData)(msg.TagReportData[i].EPCParameter[0])).EPC.ToHexString();

                        }
                       
                            an = ((ushort)(msg.TagReportData[i].AntennaID.AntennaID)).ToString();//天线号码//
                        
                        
                      
                        if (!RecordTag.Contains(epc))
                        {
                            AntennaID.Add(an);
                            //AntennaID.Add(msg.TagReportData[i].AntennaID.AntennaID.ToString());
                            RecordTag.Add(epc);
                            RecordRssi.Add(Convert.ToInt32(msg.TagReportData[i].PeakRSSI.PeakRSSI.ToString()));
                            
                            

                        }
                    }
                    if (msg.TagReportData[i].AccessCommandOpSpecResult != null && msg.TagReportData[i].AccessCommandOpSpecResult.Length > 0)
                    {
                        PARAM_C1G2WriteOpSpecResult resultWrite = (PARAM_C1G2WriteOpSpecResult)msg.TagReportData[i].AccessCommandOpSpecResult[0];
                        if (resultWrite.NumWordsWritten == InitNumWord)
                        {
                            DELETE_ACCESSSPEC();
                            writeOp = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }


        private void reader_OnReaderEventReceived(MSG_READER_EVENT_NOTIFICATION msg)
        {          
            if (msg.ReaderEventNotificationData.AntennaEvent != null)
            {
                ReaderErrorCode=12;
                ReaderErrorSpecified=msg.ReaderEventNotificationData.AntennaEvent.AntennaID.ToString()+"  "+msg.ReaderEventNotificationData.AntennaEvent.EventType.ToString();

            }
            if (msg.ReaderEventNotificationData.ReaderExceptionEvent != null)
            {
                ReaderErrorCode = 13;
                ReaderErrorSpecified = msg.ReaderEventNotificationData.ReaderExceptionEvent.Message.ToString();
            }
            if (msg.ReaderEventNotificationData.ConnectionCloseEvent != null)
            {
                ReaderErrorCode = 14;
                ReaderErrorSpecified = "连接断开";
            }
            
        }

        /**
         * 2014.2.19
         * */
        private void reader_OnKeepAlive(MSG_KEEPALIVE msg)
        {

            IsConnected = true;

            if(timer != null)
                timer.Dispose();
            timer = new System.Timers.Timer();
            timer.Interval = 3*HeartPericodic;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(time_EventArgs);
            timer.Start();
        }

        private void time_EventArgs(object sender, System.Timers.ElapsedEventArgs e)
        {
            IsConnected = false;
        }
        /*
         */ 

        private bool Set_Reader_Config()
        {
            MSG_SET_READER_CONFIG msg = new MSG_SET_READER_CONFIG();

            msg.ReaderEventNotificationSpec = new PARAM_ReaderEventNotificationSpec();
            msg.ReaderEventNotificationSpec.EventNotificationState = new PARAM_EventNotificationState[5];
            msg.ReaderEventNotificationSpec.EventNotificationState[0] = new PARAM_EventNotificationState();
            msg.ReaderEventNotificationSpec.EventNotificationState[0].EventType = ENUM_NotificationEventType.AISpec_Event;
            msg.ReaderEventNotificationSpec.EventNotificationState[0].NotificationState = false;

            msg.ReaderEventNotificationSpec.EventNotificationState[1] = new PARAM_EventNotificationState();
            msg.ReaderEventNotificationSpec.EventNotificationState[1].EventType = ENUM_NotificationEventType.Antenna_Event;
            msg.ReaderEventNotificationSpec.EventNotificationState[1].NotificationState = true;

            msg.ReaderEventNotificationSpec.EventNotificationState[2] = new PARAM_EventNotificationState();
            msg.ReaderEventNotificationSpec.EventNotificationState[2].EventType = ENUM_NotificationEventType.GPI_Event;
            msg.ReaderEventNotificationSpec.EventNotificationState[2].NotificationState = false;

            msg.ReaderEventNotificationSpec.EventNotificationState[3] = new PARAM_EventNotificationState();
            msg.ReaderEventNotificationSpec.EventNotificationState[3].EventType = ENUM_NotificationEventType.Reader_Exception_Event;
            msg.ReaderEventNotificationSpec.EventNotificationState[3].NotificationState = true;

            msg.ReaderEventNotificationSpec.EventNotificationState[4] = new PARAM_EventNotificationState();
            msg.ReaderEventNotificationSpec.EventNotificationState[4].EventType = ENUM_NotificationEventType.RFSurvey_Event;
            msg.ReaderEventNotificationSpec.EventNotificationState[4].NotificationState = false;

            /**
             * 2014.2.19
             * */
            msg.KeepaliveSpec = new PARAM_KeepaliveSpec();
            msg.KeepaliveSpec.KeepaliveTriggerType = ENUM_KeepaliveTriggerType.Periodic;
            msg.KeepaliveSpec.PeriodicTriggerValue = HeartPericodic;
            /*
             * */
            
            msg.ResetToFactoryDefault = false;

            MSG_SET_READER_CONFIG_RESPONSE rsp = reader.SET_READER_CONFIG(msg, out msg_err, 12000);

            if (rsp != null)
            {
                //return rsp.ToString();
                return true;
            }
            else if (msg_err != null)
            {
                //return msg_err.ToString();
                return false;
            }
            else
                //return "Command time out!";
                return false;

        }
        private bool Delete_RoSpec()
        {
            MSG_DELETE_ROSPEC msg = new MSG_DELETE_ROSPEC();
            msg.ROSpecID = 0;

            MSG_DELETE_ROSPEC_RESPONSE rsp = reader.DELETE_ROSPEC(msg, out msg_err, 12000);
            if (rsp != null)
            {
                //return rsp.ToString();
                return true;
            }
            else if (msg_err != null)
            {
                //return msg_err.ToString();
                return false;
            }
            else
                //return "Command time out!";
                return false;
        }

        private bool DELETE_ACCESSSPEC()
        {
            MSG_DELETE_ACCESSSPEC msg = new MSG_DELETE_ACCESSSPEC();
            msg.AccessSpecID = 1001;

            MSG_DELETE_ACCESSSPEC_RESPONSE rsp = reader.DELETE_ACCESSSPEC(msg, out msg_err, 12000);
            if (rsp != null)
            {
                //return rsp.ToString();
                return true;
            }
            else if (msg_err != null)
            {
                //return msg_err.ToString();
                return false;
            }
            else
                //return "Command time out!";
                return false;
        }

        private bool Add_RoSpec()
        {
            MSG_ADD_ROSPEC msg = new MSG_ADD_ROSPEC();
            msg.ROSpec = new PARAM_ROSpec();
            msg.ROSpec.CurrentState = ENUM_ROSpecState.Disabled;
            msg.ROSpec.Priority = 0x00;
            msg.ROSpec.ROSpecID = 123;

            msg.ROSpec.ROBoundarySpec = new PARAM_ROBoundarySpec();
            msg.ROSpec.ROBoundarySpec.ROSpecStartTrigger = new PARAM_ROSpecStartTrigger();
            msg.ROSpec.ROBoundarySpec.ROSpecStartTrigger.ROSpecStartTriggerType = ENUM_ROSpecStartTriggerType.Null;


            //msg.ROSpec.ROBoundarySpec.ROSpecStartTrigger.GPITriggerValue = new PARAM_GPITriggerValue();
            //msg.ROSpec.ROBoundarySpec.ROSpecStartTrigger.GPITriggerValue.GPIPortNum = 1;
            //msg.ROSpec.ROBoundarySpec.ROSpecStartTrigger.GPITriggerValue.Timeout = 10;
            //msg.ROSpec.ROBoundarySpec.ROSpecStartTrigger.GPITriggerValue.GPIEvent = true;





            msg.ROSpec.ROBoundarySpec.ROSpecStopTrigger = new PARAM_ROSpecStopTrigger();
            msg.ROSpec.ROBoundarySpec.ROSpecStopTrigger.ROSpecStopTriggerType = ENUM_ROSpecStopTriggerType.Duration;
            //msg.ROSpec.ROBoundarySpec.ROSpecStopTrigger.GPITriggerValue = new PARAM_GPITriggerValue();
            //msg.ROSpec.ROBoundarySpec.ROSpecStopTrigger.GPITriggerValue.GPIEvent = false;
            //msg.ROSpec.ROBoundarySpec.ROSpecStopTrigger.GPITriggerValue.GPIPortNum = 1;
            //msg.ROSpec.ROBoundarySpec.ROSpecStopTrigger.GPITriggerValue.Timeout = 2000;
            msg.ROSpec.ROBoundarySpec.ROSpecStopTrigger.DurationTriggerValue = 2000;

            msg.ROSpec.ROReportSpec = new PARAM_ROReportSpec();
            msg.ROSpec.ROReportSpec.ROReportTrigger = ENUM_ROReportTriggerType.Upon_N_Tags_Or_End_Of_AISpec;
            msg.ROSpec.ROReportSpec.N = 1;


            msg.ROSpec.ROReportSpec.TagReportContentSelector = new PARAM_TagReportContentSelector();
            msg.ROSpec.ROReportSpec.TagReportContentSelector.EnableAccessSpecID = false;
            msg.ROSpec.ROReportSpec.TagReportContentSelector.EnableAntennaID = true;
            msg.ROSpec.ROReportSpec.TagReportContentSelector.EnableChannelIndex = false;
            msg.ROSpec.ROReportSpec.TagReportContentSelector.EnableFirstSeenTimestamp = false;
            msg.ROSpec.ROReportSpec.TagReportContentSelector.EnableInventoryParameterSpecID = false;
            msg.ROSpec.ROReportSpec.TagReportContentSelector.EnableLastSeenTimestamp = false;
            msg.ROSpec.ROReportSpec.TagReportContentSelector.EnablePeakRSSI = true;
            msg.ROSpec.ROReportSpec.TagReportContentSelector.EnableROSpecID = false;
            msg.ROSpec.ROReportSpec.TagReportContentSelector.EnableSpecIndex = false;
            msg.ROSpec.ROReportSpec.TagReportContentSelector.EnableTagSeenCount = false;


            msg.ROSpec.SpecParameter = new UNION_SpecParameter();
            PARAM_AISpec aiSpec = new PARAM_AISpec();
            aiSpec.AntennaIDs = new UInt16Array();
            aiSpec.AntennaIDs.Add(0);
            //aiSpec.AntennaIDs.Add(2);
            aiSpec.AISpecStopTrigger = new PARAM_AISpecStopTrigger();
            aiSpec.AISpecStopTrigger.AISpecStopTriggerType = ENUM_AISpecStopTriggerType.Null;
            //aiSpec.AISpecStopTrigger.DurationTrigger = 4000;
            //aiSpec.AISpecStopTrigger.GPITriggerValue = new PARAM_GPITriggerValue();
            //aiSpec.AISpecStopTrigger.GPITriggerValue.GPIEvent = false;
            //aiSpec.AISpecStopTrigger.GPITriggerValue.GPIPortNum = 1;
            //aiSpec.AISpecStopTrigger.GPITriggerValue.Timeout = 0;


            aiSpec.InventoryParameterSpec = new PARAM_InventoryParameterSpec[1];
            aiSpec.InventoryParameterSpec[0] = new PARAM_InventoryParameterSpec();
            aiSpec.InventoryParameterSpec[0].InventoryParameterSpecID = 1234;
            aiSpec.InventoryParameterSpec[0].ProtocolID = ENUM_AirProtocols.EPCGlobalClass1Gen2;

            msg.ROSpec.SpecParameter.Add(aiSpec);

            //PARAM_ImpinjLoopSpec testloopspec = new PARAM_ImpinjLoopSpec();
            //testloopspec.LoopCount = 0;
            //msg.ROSpec.SpecParameter.AddCustomParameter(testloopspec);

            MSG_ADD_ROSPEC_RESPONSE rsp = reader.ADD_ROSPEC(msg, out msg_err, 12000);
            if (rsp != null)
            {
                //return rsp.ToString();
                return true;
            }
            else if (msg_err != null)
            {
                //return msg_err.ToString();
                return false;
            }
            else
                //return "Command time out!";
                return false;

        }

        private bool ADD_ACCESSSPEC(string tagdata,int type)
        {
            MSG_ADD_ACCESSSPEC msg = new MSG_ADD_ACCESSSPEC();
            msg.AccessSpec = new PARAM_AccessSpec();

            msg.AccessSpec.AccessSpecID = 1001;
            msg.AccessSpec.AntennaID = 1;
            msg.AccessSpec.ProtocolID = ENUM_AirProtocols.EPCGlobalClass1Gen2;
            msg.AccessSpec.CurrentState = ENUM_AccessSpecState.Disabled;
            msg.AccessSpec.ROSpecID = 123;

            //define trigger
            msg.AccessSpec.AccessSpecStopTrigger = new PARAM_AccessSpecStopTrigger();
            msg.AccessSpec.AccessSpecStopTrigger.AccessSpecStopTrigger = ENUM_AccessSpecStopTriggerType.Operation_Count;
            msg.AccessSpec.AccessSpecStopTrigger.OperationCountValue = 100;

            //define access command

            //define air protocol spec
            msg.AccessSpec.AccessCommand = new PARAM_AccessCommand();
            msg.AccessSpec.AccessCommand.AirProtocolTagSpec = new UNION_AirProtocolTagSpec();

            PARAM_C1G2TagSpec tagSpec = new PARAM_C1G2TagSpec();
            tagSpec.C1G2TargetTag = new PARAM_C1G2TargetTag[1];
            tagSpec.C1G2TargetTag[0] = new PARAM_C1G2TargetTag();
            tagSpec.C1G2TargetTag[0].Match = false; //change to "true" if you want to the following parameters take effect.
            tagSpec.C1G2TargetTag[0].MB = new TwoBits(1);
            tagSpec.C1G2TargetTag[0].Pointer = 0x20;
            tagSpec.C1G2TargetTag[0].TagData = LLRPBitArray.FromString("1111");
            tagSpec.C1G2TargetTag[0].TagMask = LLRPBitArray.FromBinString("1111111111111111");

            msg.AccessSpec.AccessCommand.AirProtocolTagSpec.Add(tagSpec);

            //define access spec
            msg.AccessSpec.AccessCommand.AccessCommandOpSpec = new UNION_AccessCommandOpSpec();

            PARAM_C1G2Write wr = new PARAM_C1G2Write();
            wr.AccessPassword = 0;
            wr.MB = new TwoBits(1);
            wr.OpSpecID = 111;
            wr.WordPointer = 2;
            //Data to be written.
            wr.WriteData = UInt16Array.FromHexString(tagdata);
            msg.AccessSpec.AccessCommand.AccessCommandOpSpec.Add(wr);

            msg.AccessSpec.AccessReportSpec = new PARAM_AccessReportSpec();
            msg.AccessSpec.AccessReportSpec.AccessReportTrigger = ENUM_AccessReportTriggerType.End_Of_AccessSpec;

            MSG_ADD_ACCESSSPEC_RESPONSE rsp = reader.ADD_ACCESSSPEC(msg, out msg_err, 12000);
            if (rsp != null)
            {
                //return rsp.ToString();
                return true;
            }
            else if (msg_err != null)
            {
                //return msg_err.ToString();
                return false;
            }
            else
                //return "Command time out!";
                return false;
        }

        private bool ENABLE_ACCESSSPEC()
        {
            MSG_ENABLE_ACCESSSPEC msg = new MSG_ENABLE_ACCESSSPEC();
            msg.AccessSpecID = 0;

            MSG_ENABLE_ACCESSSPEC_RESPONSE rsp = reader.ENABLE_ACCESSSPEC(msg, out msg_err, 12000);

            if (rsp != null)
            {
                //return rsp.ToString();
                return true;
            }
            else if (msg_err != null)
            {
                //return msg_err.ToString();
                return false;
            }
            else
                //return "Command time out!";
                return false;
        }

        private bool Enable_RoSpec()
        {
            MSG_ENABLE_ROSPEC msg = new MSG_ENABLE_ROSPEC();
            msg.ROSpecID = 123;
            MSG_ENABLE_ROSPEC_RESPONSE rsp = reader.ENABLE_ROSPEC(msg, out msg_err, 12000);
            if (rsp != null)
            {
                //return rsp.ToString();
                return true;
            }
            else if (msg_err != null)
            {
                //return msg_err.ToString();
                return false;
            }
            else
                //return "Command time out!";
                return false;

        }

        private bool Start_RoSpec()
        {
            MSG_START_ROSPEC msg = new MSG_START_ROSPEC();
            msg.ROSpecID = 123;
            MSG_START_ROSPEC_RESPONSE rsp = reader.START_ROSPEC(msg, out msg_err, 12000);
            if (rsp != null)
            {
                //return rsp.ToString();
                return true;
            }
            else if (msg_err != null)
            {
                //return msg_err.ToString();
                return false;
            }
            else
                //return "Command time out!";
                return false;

        }

        private bool Stop_RoSpec()
        {
            MSG_STOP_ROSPEC msg = new MSG_STOP_ROSPEC();
            msg.ROSpecID = 123;
            MSG_STOP_ROSPEC_RESPONSE rsp = reader.STOP_ROSPEC(msg, out msg_err, 12000);
            if (rsp != null)
            {
                //return rsp.ToString();
                return true;
            }
            else if (msg_err != null)
            {
                //return msg_err.ToString();
                return false;
            }
            else
                //return "Command time out!";
                return false;

        }

        private bool _IsConnected = false;
        public bool IsConnected
        {
            get
            {
                lock (lockK)
                {
                    return _IsConnected;
                }
            }
            set
            {
                lock (lockK)
                {
                    _IsConnected = value;
                }
            }

        }
        private string _ip = null;
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

    }
  
}
