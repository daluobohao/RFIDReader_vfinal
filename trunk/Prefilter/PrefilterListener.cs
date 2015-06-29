using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rfiddata;
using RFIDReaderMiddleware.JmsMiddleWare;

using com.espertech.esper.client;


namespace RFIDReaderMiddleware.Prefilter
{
    class PrefilterListener
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(PrefilterListener));
        private JmsAdvancedBroker nosoursejmxbroker = null;
        private XMLReaderConvertor xmlreaderConvert = XMLReaderConvertor.getXMLReaderConvertor();
        private Form1 form1;
        private int sendCount = 0;

        public PrefilterListener(Form1 form)
        {
            this.form1 = form;
            JmsServerInfo jmsServerinfo = new JmsServerInfo();
            jmsServerinfo = xmlreaderConvert.getJmsServerInfo();
            nosoursejmxbroker = new JmsAdvancedBroker(jmsServerinfo);
        }

        public void PrefilterEventHandler(object o, UpdateEventArgs args)
        {
            if(args.NewEvents != null)
            {
                int lastContextID = -1;
                int newContextID;
                string strResult = "";
                string sendMessage = "";
                try
                {
                    strResult += "-----------Tags are as follows：--------------\r\n";
                    foreach (EventBean eb in args.NewEvents)
                    {
                        newContextID = Int32.Parse(eb.Get("ID").ToString());
                        if (newContextID == lastContextID)
                        {
                            continue;
                        }

                        XtiveTag tag = (new XtiveTag.Builder())
                                .SetUid(eb.Get("UID").ToString())
                                .SetRssi(Int32.Parse(eb.Get("Rssi").ToString()))
                                .SetBLowPower(Boolean.Parse(eb.Get("BLowPower").ToString()))
                                .SetBExcite(Boolean.Parse(eb.Get("BExcite").ToString()))
                                .SetReaderIP(eb.Get("ReaderIP").ToString())
                                .SetAn(eb.Get("An").ToString())
                                .SetDateTime(eb.Get("DateTime").ToString()).Build();

                        nosoursejmxbroker.addXtiveTag(tag);
                        sendCount++;
                        sendMessage = tag.Uid + "|" + tag.Rssi + "|" + tag.BLowPower + "|" + tag.BExcite + "|" + 
                                      tag.ReaderIP + "|" + tag.An + "|" + tag.DateTime;
                        log.Info(sendMessage);
                        strResult = strResult + sendMessage + "\r\n";

                        lastContextID = newContextID;
                    }
                    strResult += "-----------Over!--------------------------\r\n";

                    form1.updateMsgBoxM(strResult, sendCount.ToString()); //更新UI
                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                }
            }
        }

        /*
        public void Update(object[] newEvents)
        {
            System.Console.WriteLine("test");
            foreach(object newEvent in newEvents)
            {
                XtiveTag tag = (XtiveTag)newEvent;
                System.Console.WriteLine(tag.ToString());
            }
        }
        * */
    }
}
