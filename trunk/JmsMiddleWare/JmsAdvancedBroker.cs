using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using rfiddata;
using System.Threading;
using RFIDReaderMiddleware.Crypto;
using System.IO;
using System.IO.Compression;

namespace RFIDReaderMiddleware.JmsMiddleWare
{
    class JmsAdvancedBroker
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(JmsAdvancedBroker));

        protected IConnectionFactory _factory;
        protected IConnection _connection;
        protected ISession _session;
        protected IMessageProducer _producer;

        private object _lockObject = new object();
        protected XtiveTags.Builder _tagsBuilder;

        private static int _tagMaxCount = 500;
        private static int _maxTimeInterval = 1000;

        protected DateTime _lastDateTime=DateTime.Now;

        protected Boolean _shouldCrypto;
        protected KeyAgreement _keyAgreement;

        private JmsServerInfo _serverinfo;

        public JmsAdvancedBroker(JmsServerInfo serverinfo)
        {
            this._serverinfo = serverinfo;
            Thread jmsconnectThread = new Thread(new ThreadStart(jmsInitial));
            jmsconnectThread.IsBackground = true;
            jmsconnectThread.Start();
        }

        private void jmsInitial()
        {
            try
            {
                //Console.WriteLine("url:" + serverinfo.Url + ";" + serverinfo.QueueName);
                _factory = new ConnectionFactory(this._serverinfo.Url);
                //_factory.UseCompression = true;
                _connection = _factory.CreateConnection();
                _session = _connection.CreateSession(AcknowledgementMode.DupsOkAcknowledge);
                _producer = _session.CreateProducer(
                                 new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(this._serverinfo.QueueName));

                _tagsBuilder = new XtiveTags.Builder();
                Thread thread = new Thread(new ThreadStart(checkTags));
                thread.IsBackground = true;
                thread.Start();
            }
            catch (Exception msg)
            {
                //出错信息
                log.Error(msg.Message);
            }
        }

        ~JmsAdvancedBroker()
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

        public void addXtiveTag(rfiddata.XtiveTag tag)
        {
            
            lock (_lockObject)
            {
                _tagsBuilder.AddTags(tag);
                if (_tagsBuilder.TagsCount >= _tagMaxCount)
                {
                    this.sendTags();
                }
            }
        }

        protected void sendTags()
        {
            try
            {
                IBytesMessage msg = _producer.CreateBytesMessage();
                //msg.Properties.SetBytes("aa", new byte[5]);
                MemoryStream stream = new MemoryStream();
                GZipStream gZipStream = new GZipStream(stream, CompressionMode.Compress);
                byte[] bytes=_tagsBuilder.Build().ToByteArray();
                gZipStream.Write(bytes, 0, bytes.Length);
                gZipStream.Close();
                byte[] compressedBytes = stream.ToArray();
                if (_shouldCrypto)
                {
                    _keyAgreement.writeMessage(msg, compressedBytes);
                }
                else
                {
                    msg.WriteBytes(compressedBytes);
                }

                this._producer.Send(msg, Apache.NMS.MsgDeliveryMode.NonPersistent, Apache.NMS.MsgPriority.Normal, TimeSpan.MinValue);
                _lastDateTime = DateTime.Now;
                _tagsBuilder = new XtiveTags.Builder();
            }

            catch (System.Exception e)
            {
                log.Error(e.Message);
            }
        }

        protected void checkTags()
        {
            while (true)
            {
                DateTime now = DateTime.Now;
                if ((now - _lastDateTime).TotalMilliseconds > _maxTimeInterval)
                {
                    lock (_lockObject)
                    {
                        if (_tagsBuilder.TagsCount > 0)
                        {
                            Console.WriteLine("will send");
                            this.sendTags();
                        }
                    }
                }
                Thread.Sleep(_maxTimeInterval);
            }
            
        }

        public void setShouldCrypto(Boolean shouldCrypto)
        {
            _shouldCrypto = shouldCrypto;
            if (_shouldCrypto)
            {
                _keyAgreement = new KeyAgreement();
                _keyAgreement.start();
            }
        }
    }
}
