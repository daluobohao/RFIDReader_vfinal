using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
//using nsAlienRFID;
using nsAlienRFID2;
using rfiddata;

namespace RFIDReaderMiddleware
{
    public class Alien9800 : RFIDInterface
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Alien9800));

        public void Open(string ReaderIP)
        {

            try
            {

                string strResult = null;
                string Usrname = "alien";
                string Password = "password";

                _ip = ReaderIP;

                mReader.ConnectAndLogin(ReaderIP, 23, Usrname, Password);
                if (mReader.IsConnected)
                {

                    //_ip = ReaderIP;
                    _IsConnected = true;

                    #region "Setting the reader"....
                    // Keep the reader working
                    //mReader.AutoMode = "Off";
                    mReader.AutoMode = "On";
                    mReader.KeepNetworkConnectionAlive = true;
                    mReader.AntennaSequence = "0 1";
                    string receive = null;
                    string strset = "set TagListCustomFormat = IP:${IP};Tag:${TAGID};RSSI:${RSSI};Antenna:%A";
                    //strset = "Get TagList";
                    receive = mReader.SendReceive(strset, false);
                    // strset=
                    //mReader.NetworkTimeout = "6000";

                    // Setting for notifications
                    //mReader.NotifyAddress = msIP + ":4000";
                    mReader.NotifyFormat = "XML";                      // the format of notifications;
                    mReader.NotifyTrigger = "TrueFalse";                     // when notifications will be sent;

                    // Setting for taglists
                    mReader.TagListFormat = "custom";                     // the format of taglists;              
                    mReader.PersistTime = "5";                          // the span that a piece of tag-info will be hold in the taglist;
                    mReader.ProgAttempts = 7;
                    string s = mReader.TagList;

                    //mReader.AutoMode = "Off";
                    strResult += "配置读写器成功！";
                    #endregion "Setting the reader"
                    _ReaderErrorCode = 0;
                    _ReaderErrorSpecified = "";

                    //}
                    //}
                }
                else
                {
                    _ReaderErrorCode = 11;
                    _ReaderErrorSpecified = "读写器Open函数连接不上：" + ReaderIP.ToString();
                    log.Info(_ReaderErrorSpecified);
                    return;
                }
            }
            catch (Exception e)
            {
                _ReaderErrorCode = 11;
                _ReaderErrorSpecified = "读写器Open函数故障：" + ReaderIP.ToString() + "  " + e.Message.ToString();
                log.Error(_ReaderErrorSpecified);
                return;
            }
            finally
            {

            }

        }

        public void Close()
        {
            try
            {
                if (mReader.IsConnected)
                {
                    mReader.NotifyMode = "Off";
                    mReader.AutoMode = "Off";
                    mReader.Disconnect();
                }
                _IsConnected = false;
                _ReaderErrorCode = 0;
                _ReaderErrorSpecified = "";
            }
            catch (Exception e)
            {
                _ReaderErrorCode = 21;
                if (ip != null)
                {
                    _ReaderErrorSpecified = "读写器Close函数故障：" + ip.ToString() + "  " + e.Message.ToString();
                    log.Error(_ReaderErrorSpecified);
                }
                else
                {
                    _ReaderErrorSpecified = "读写器Close函数故障：实例已丢失" + "  " + e.Message.ToString();
                    log.Error(_ReaderErrorSpecified);
                }
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
                mReader.ClearTagList();
                _ReaderErrorCode = 0;
                _ReaderErrorSpecified = "";
            }
            catch (Exception e)
            {
                _ReaderErrorCode = 31;
                if (ip != null)
                {
                    _ReaderErrorSpecified = "读写器CleanBuffer函数故障：" + ip.ToString() + "  " + e.Message.ToString();
                    log.Error(_ReaderErrorSpecified);
                }
                else
                {
                    _ReaderErrorSpecified = "读写器CleanBuffer函数故障：实例已丢失" + "  " + e.Message.ToString();
                    log.Error(_ReaderErrorSpecified);
                }
                return;
            }
        }

        public bool LoadTagData(ref XtiveTag[] tagData, ref int RecordCount)
        {
            //int cnt = 0;
            string strResult = null;
            //string strTagIDWithoutBlanks = null;
            RecordCount = 0;
            //string strReceive = null;

            try
            {
                strResult = mReader.TagList;
                Console.WriteLine("rfid string:" + strResult);
                if ((strResult == null || strResult.Equals("") || strResult.Equals("(No Tags)")) == false)
                {
                    char split = '\n';
                    string a = strResult.Replace("\r", "");
                    string[] ListResult = a.Split(split);

                    if (ListResult.Length > 0)
                    {
                        RecordCount = ListResult.Length;
                        if (ListResult[0][0] == 'I')
                        {
                            for (int i = 0; i < ListResult.Length; i++)
                            {
                                char splitchar = ';';
                                string[] taglist = ListResult[i].Split(splitchar);
                                char splitchartemp = ':';
                                string[] readerip = taglist[0].Split(splitchartemp);
                                
                                //tagData[i].ReaderIP = readerip[1];
                                string[] tagID = taglist[1].Split(splitchartemp);
                                //tagData[i].UID = tagID[1];
                                string[] RSSI = taglist[2].Split(splitchartemp);

                                //tagData[i].RSSI = Convert.ToInt32(Convert.ToDouble(RSSI[1].ToString()));
                                //tagData[i].bExcite = true;
                                //tagData[i].bLowPower = false;
                                string[] an = taglist[3].Split(splitchartemp);
                                //tagData[i].An = an[1];


                                //后面加入代码，用于测试，2013-3-28 16:14 罗元剑
                                //tagData[i].datetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                //  Random random = new Random();
                                /////  tagData[i].port = random.Next(100);
                                //  tagData[i].x_position = random.Next(10)+12936200;
                                //  tagData[i].y_position = random.Next(10)+4861943;

                                XtiveTag tag = (new XtiveTag.Builder())
                                   .SetUid(tagID[1])
                                   .SetRssi(Convert.ToInt32(Convert.ToDouble(RSSI[1].ToString())))
                                   .SetBLowPower(false)
                                   .SetBExcite(true)
                                   .SetReaderIP(readerip[1])
                                   .SetAn(an[1])
                                   .SetDateTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Build();
                                tagData[i] = tag;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < ListResult.Length; i++)
                            {
                                char splitchar = ',';
                                string[] taglist = ListResult[i].Split(splitchar);
                                char splitchartemp = ':';
                                string[] tagID = taglist[0].Split(splitchartemp);
                                //tagData[i].UID = tagID[1].Replace(" ","");
                                //tagData[i].An = taglist[4].Split(splitchartemp)[1];
                                XtiveTag tag = (new rfiddata.XtiveTag.Builder())
                                  .SetUid(tagID[1].Replace(" ", ""))
                                  .SetAn(taglist[4].Split(splitchartemp)[1]).Build();
                                tagData[i] = tag;
                            }
                        }
                    }

                    mReader.ClearTagList();
                    //Thread.Sleep(100);
                    //strReceive= mReader.SendReceive("set TagListCustomFormat = IP:${IP};Tag:${TAGID};RSSI:${RSSI};Antenna:${Antenna}",true);

                    _ReaderErrorCode = 0;
                    _ReaderErrorSpecified = "";
                }
            }
            catch (Exception e)
            {
                _ReaderErrorCode = 41;
                if (ip != null)
                {
                    _ReaderErrorSpecified = "读写器LoadTagData函数故障：" + ip.ToString() + "  " + e.Source.ToString() + "\r\n  " + e.Message.ToString();
                    log.Error(_ReaderErrorSpecified);
                }
                else
                {
                    _ReaderErrorSpecified = "读写器LoadTagData函数故障：实例已丢失" + "  " + e.Message.ToString();
                    log.Error(_ReaderErrorSpecified);
                }
                return false;
            }
            /*
                if (strResult == null || (!strResult.Contains("TagID")))
                {
                    return false;
                }

                TagInfo[] aTags = null;	// if no header we'll get tag list only
            try
            {
                TagInfo[] bTags = null;
                cnt = AlienUtils.ParseTagList(strResult, out bTags);
                if (cnt > 0)
                {
                    aTags = bTags.Distinct().ToArray();
                    
                    RecordCount = aTags.Count();
                }
                else
                {
                    RecordCount = cnt;
                }
                
            }
             catch (Exception ex)
            {
               // MessageBox.Show("该消息不符合规格：" + strData + "\n" + ex.Message);
            }
                #region "Show tags found"...
            //if (cnt > 0 && cnt < 100)
            //{
            //    for (int i = 0; i < cnt; i++)
            //    {
            //        tagData[i].UID = aTags[i].TagID;
            //aTags[i].Antenna
            //        tagData[i].RSSI = -60;
            //        tagData[i].bExcite = true;
            //        tagData[i].bLowPower = false;

            //    }

            //}
            if (cnt > 0)
            {
                int i = 0;
                strTagIDWithoutBlanks = aTags[0].TagID.Replace(" ", "");
                tagData[0].UID = strTagIDWithoutBlanks;
                tagData[0].RSSI = "-60";
                tagData[0].bExcite = true;
                tagData[0].bLowPower = false;

                if (cnt > 1)
                {
                    for (i = 1; i < cnt; i++)
                    {
                        if (aTags[i - 1].TagID != aTags[i].TagID)
                        {
                            strTagIDWithoutBlanks = aTags[i].TagID.Replace(" ", "");
                            tagData[i].UID = strTagIDWithoutBlanks;
                            tagData[i].RSSI = "-60";
                            tagData[i].bExcite = true;
                            tagData[i].bLowPower = false;
                        }
                        if (i >= 99 ) break;
                    }
                }
            }
            else
            {
                return false;
            }
                #endregion
*/
            return true;
        }

        public bool Check()
        {
            try
            {
                int heartbeatCount = mReader.HeartbeatCount;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return false;
            }
            return true;
            /*
             * * /
            bool strResult = false;
            try
            {
                strResult = mReader.IsConnected;
                
                _ReaderErrorCode = 0;
                _ReaderErrorSpecified = "";
            }
            catch (Exception e)
            {
                strResult = false;
                _ReaderErrorCode = 51;
                if (ip != null)
                {

                    _ReaderErrorSpecified = "读写器Check函数故障：" + ip.ToString() + "  " + e.Message.ToString();
                }
                else
                {
                    _ReaderErrorSpecified = "读写器Check函数故障：实例已丢失" + "  " + e.Message.ToString();
                }
                return false;
            }
            return strResult;
            /*
             */ 
        }

        public bool WriteTagID(string TagData, int type)
        {
            bool bResult = false;
            string strBack = null;
            string strOriginalData = AddBlank(TagData,4);
            TagData = AddBlank(TagData, 2);

            #region EPC Bank
            // Bank 0: RESERVED  
            // Bank 1: EPC
            // Bank 2: TID
            // Bank 3: USER
            #endregion

            if (type == 1)
            {
                //strBack =mReader.ProgramTag(TagData);
                try
                {
                    //strBack = mReader.ProgramTag(TagData);
                    strBack = mReader.ProgramEPC(TagData);

                    System.Console.WriteLine(strBack);

                    mReader.ClearTagList();
                }
                catch (Exception e)
                {    
                    _ReaderErrorSpecified = e.Message.ToString();
                    log.Error(_ReaderErrorSpecified);
                    if (_ReaderErrorSpecified.Split(' ')[1].Equals("154:"))
                    {
                        _ReaderErrorCode = 154;
                    }

                    return bResult = false;
                }
                finally
                {
                }

                if (strBack.Equals(strOriginalData))
                {
                    bResult = true;
                }
            }
            else if (type == 3)
            {
                try
                {
                    strBack = mReader.ProgramUser(TagData);
                    //strBack = mReader.SendReceive("G2Write = 3,0," + (TagData.Length / 2).ToString() + TagData, false);
                }
                catch (Exception e)
                {
                    _ReaderErrorCode = 61;
                    _ReaderErrorSpecified = e.Message.ToString();
                    log.Error(_ReaderErrorSpecified);
                    return bResult = false;
                }
                finally
                {
                }
                if (strBack.Equals(strOriginalData))
                {
                    bResult = true;
                }
            }
            return bResult;
        }

        /**
        * 2014.2.17
        * */
        public void ReConnect(object arg)
        {
            ReConnectCount++;
            string ReaderIP = (string)arg;

            Open(ReaderIP);
            //System.Console.WriteLine("reconnect");
            Thread.Sleep(5000);  //等待重连
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

        private clsReader mReader = new clsReader();
        private string AddBlank(string Data, int type)
        {
            string strResult = null;
            string strResultTemp = null;
            int i = 0;
            int ipadnumber = 0;
            if (type == 2)
            {
                ipadnumber = 2 - Data.Length % 2;
                strResultTemp = Data.PadRight(ipadnumber, '0');
                strResult = strResultTemp.Substring(0, 2);
                for (i = 1; i < strResultTemp.Length / 2; i++)
                {
                    strResult += " " + strResultTemp.Substring(2 * i, 2);
                }
            }
            else if (type == 4)
            {
                ipadnumber = 4 - Data.Length % 4;
                strResultTemp = Data.PadRight(ipadnumber, '0');
                strResult = strResultTemp.Substring(0, 4);
                for (i = 1; i < strResultTemp.Length / 4; i++)
                {
                    strResult += " " + strResultTemp.Substring(4 * i, 4);
                }
            }
            return strResult;
        }
        //private ComInterface meReaderInterface = ComInterface.enumTCPIP;

        private bool _IsConnected = false;
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
