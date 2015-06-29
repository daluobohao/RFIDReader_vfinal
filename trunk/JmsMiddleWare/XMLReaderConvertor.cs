using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

namespace RFIDReaderMiddleware
{
    class XMLReaderConvertor
    {
        #region data
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(XMLReaderConvertor));
         /// <summary>
        /// 获取rfidreader 的集合
        /// </summary>
        private List<RFIDReader> _readerlist;

        /// <summary>
        /// 消息中间件服务器信息
        /// </summary>
        private JmsServerInfo _jmsserverinfo;

        /// <summary>
        /// 标签读写频率
        /// </summary>
        private int _tagsreadfre = -1;

        /// <summary>
        /// 标签预过滤频率
        /// </summary>
        private long _filterInterval = -1;

        private static XMLReaderConvertor _xmlReaderConvertor;
        private XmlDocument _xmldocument;
        private static Object lockThis = new Object();   //线程锁
        #endregion

        public static XMLReaderConvertor getXMLReaderConvertor()
        {
            if (_xmlReaderConvertor == null)
            {
                lock (lockThis)
                {
                    _xmlReaderConvertor = new XMLReaderConvertor();
                }
            }
            return _xmlReaderConvertor;
        }

        private XMLReaderConvertor()
        {
            try
            {
                _xmldocument = new XmlDocument();
                _xmldocument.Load("appplication_config.xml");
            }
            catch (Exception e)
            {
                log.Error("读取配置文件失败，异常信息为：" + e.Message);
            }
        }

        /// <summary>
        /// 获取消息中间件连接信息
        /// </summary>
        /// <returns></returns>
        public JmsServerInfo getJmsServerInfo()
        {
            _jmsserverinfo = new JmsServerInfo();
            convertJmsServerInfoXML();
            return this._jmsserverinfo;
        }


        /// <summary>
        /// 获取RFIDReaderList
        /// </summary>
        /// <returns>RFIDReader的list集合</returns>
        public List<RFIDReader> getRFIDReaderList(string type)
        {
            //从数据库中获取值
            this._readerlist = new List<RFIDReader>();
                        
            if (type.Equals("reader"))
            {
                convertReaderXML();
            }
            else if (type.Equals("writer"))
            {
                convertWriterXML();
            }

            return this._readerlist;
        }

        public int getTagsReadFreq()
        {
            if (this._tagsreadfre == -1)
            {
                convertFrequencyXML();
            }
            return this._tagsreadfre;
        }

        public long getFilterInterval()
        {
            if (this._filterInterval == -1)
            {
                convertFrequencyXML();
            }
            return this._filterInterval;
        }

        private void convertReaderXML()
        {
            try
            {
                XmlNode reader = _xmldocument.SelectSingleNode("/root/reader");
                if (reader == null)
                {
                    return;
                }
                if (reader.HasChildNodes == true)
                {
                    XmlNodeList readerlist = reader.ChildNodes;
                    foreach (XmlNode node in readerlist)
                    {

                        if (node.Name.Equals("node"))
                        {
                            string ip = node.Attributes["ip"].Value.ToString();
                            string type = node.Attributes["type"].Value.ToString();
                            string position = node.Attributes["position"].Value.ToString();
                            RFIDReader rfid = new RFIDReader();
                            rfid.RFidreaderID = int.Parse(node.Attributes["id"].Value.ToString());
                            rfid.RFidreaderIP = ip;
                            rfid.RFidreaderPosition = position;
                            rfid.RFidreaderType = type;
                            _readerlist.Add(rfid);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("读取读写器列表时出现异常，异常信息为：" + e.Message);
            }
           
        }

        private void convertWriterXML()
        {
            try
            {
                XmlNode reader = _xmldocument.SelectSingleNode("/root/writer");
                if (reader == null)
                {
                    return;
                }
                if (reader.HasChildNodes == true)
                {
                    XmlNodeList readerlist = reader.ChildNodes;
                    foreach (XmlNode node in readerlist)
                    {

                        if (node.Name.Equals("node"))
                        {
                            string ip = node.Attributes["ip"].Value.ToString();
                            string type = node.Attributes["type"].Value.ToString();
                            string position = node.Attributes["position"].Value.ToString();
                            RFIDReader rfid = new RFIDReader();
                            rfid.RFidreaderID = int.Parse(node.Attributes["id"].Value.ToString());
                            rfid.RFidreaderIP = ip;
                            rfid.RFidreaderPosition = position;
                            rfid.RFidreaderType = type;
                            _readerlist.Add(rfid);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("读取写读写器信息时出现异常，异常信息为：" + e.Message);
            }
        }

        private void convertJmsServerInfoXML()
        {
            try
            {
                XmlNode reader = _xmldocument.SelectSingleNode("/root/producer");
                if (reader.HasChildNodes == true)
                {
                    XmlNodeList readerlist = reader.ChildNodes;
                    foreach (XmlNode node in readerlist)
                    {

                        if (node.Name.Equals("serverurl"))
                        {
                            this._jmsserverinfo.Url = node.InnerText;

                        }
                        else if (node.Name.Equals("queuename"))
                        {
                            this._jmsserverinfo.QueueName = node.InnerText;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("读取消息中间件连接参数时出现异常，异常信息为：" + e.Message);
            }
        }

        private void convertFrequencyXML()
        {
            try
            {
                XmlNode freq = _xmldocument.SelectSingleNode("/root/frequency");
                if (freq.HasChildNodes)
                {
                    XmlNodeList freqList = freq.ChildNodes;
                    foreach (XmlNode node in freqList)
                    {
                        if (node.Name.Equals("tagsreadfre"))
                        {
                            this._tagsreadfre = int.Parse(node.InnerText) * 1000;
                        }
                        else if (node.Name.Equals("filterTimeInterval"))
                        {
                            this._filterInterval = long.Parse(node.InnerText);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("读取标签读取过滤频率参数时出现异常，异常信息为：" + e.Message);
            }
        }
    }
}
