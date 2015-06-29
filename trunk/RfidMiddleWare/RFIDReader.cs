using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RFIDReaderMiddleware
{
    /// <summary>
    /// 根据数据库中的对rfidreader的定义
    /// 实例化rfid
    /// </summary>
    class RFIDReader
    {
        /// <summary>
        /// rfidreader的id号
        /// </summary>
        private int _rfidreaderid;
        /// <summary>
        /// rfidreader对应的ip
        /// </summary>
        private string _rfidreaderip;
        /// <summary>
        /// rfidreader所放置的位置
        /// </summary>
        private string _rfidreaderpositon;
        /// <summary>
        /// rfidreader的类型
        /// </summary>
        private string _rfidreadertype;

        public int RFidreaderID
        {
            get
            {
                return this._rfidreaderid;
            }
            set
            {
                this._rfidreaderid = value;
            }
        }

        public string RFidreaderIP
        {
            get
            {
                return this._rfidreaderip;
            }
            set
            {
                this._rfidreaderip = value;
            }
        }

        public string RFidreaderPosition
        {
            get
            {
                return this._rfidreaderpositon;
            }
            set
            {
                this._rfidreaderpositon = value;
            }
        }

        public string RFidreaderType
        {
            get
            {
                return this._rfidreadertype;
            }
            set
            {
                this._rfidreadertype = value;
            }
        }
    }
}
