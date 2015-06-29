using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RFIDReaderMiddleware
{
    class JmsServerInfo
    {
        /// <summary>
        /// 连接消息中间件的地址url
        /// </summary>
        private String url;
        /// <summary>
        /// 消息中间中的队列名称
        /// </summary>
        private String queuename;


        public String Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
            }
        }

        public String QueueName
        {
            get
            {
                return queuename;
            }
            set
            {
                queuename = value;
            }
        }

    }
}
