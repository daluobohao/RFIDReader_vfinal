using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace RFIDReaderMiddleware
{
    class JmsBroker
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(JmsBroker));

        private IConnectionFactory _factory;
        private IConnection _connection;
        private ISession _session;
        private IMessageProducer _prodcer;
        /// <summary>
        /// 创建中间件的接口
        /// </summary>
        public JmsBroker(String url,String queuename)
        {
            try
            {
                if (_factory == null)
                {
                    _factory = new ConnectionFactory(url);
                }

                if (_connection == null)
                {
                    _connection = _factory.CreateConnection();
                }
                if (_session == null)
                {
                    _session = _connection.CreateSession();
                }
                if (_prodcer == null)
                {
                    _prodcer = _session.CreateProducer(
                                 new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(queuename));
                }
            }
            catch(Exception msg)
            {
                log.Error(msg.Message);
            }
        }


        public JmsBroker(JmsServerInfo serverinfo)//Jms中间件
        {
            try
            {
                if (_factory == null)
                {
                    Console.WriteLine("url:" + serverinfo.Url + ";" + serverinfo.QueueName);
                    _factory = new ConnectionFactory("tcp://192.168.1.117:61616");
                }
                if (_connection == null)
                {
                    _connection = _factory.CreateConnection();
                }
                if (_session == null)
                {
                    _session = _connection.CreateSession(AcknowledgementMode.DupsOkAcknowledge);
                }
                if (_prodcer == null)
                {
                    _prodcer = _session.CreateProducer(
                                 new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(serverinfo.QueueName));
                }
            }
            catch (Exception msg)
            {
                log.Error(msg.Message);
            }
        }

         ~JmsBroker()
        {
            
            if (_session != null)
            {
                _session.Close();
               
            }
            if (_connection != null)
            {
                _connection.Close();
            }
        }



        public void sendMessage(string message)
        {
            try
            {

                ITextMessage msg = this._prodcer.CreateTextMessage();
                msg.Text = message;
                this._prodcer.Send(msg, Apache.NMS.MsgDeliveryMode.NonPersistent, Apache.NMS.MsgPriority.Normal, TimeSpan.MinValue);

            }

            catch (System.Exception e)
            {
                log.Error(e.Message);
            }
        }
    }
}
