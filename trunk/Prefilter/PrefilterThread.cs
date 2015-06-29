using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rfiddata;
using com.espertech.esper.client;

/**
 * RFID数据预过滤线程
 * @Date 2014.7.31
 * @Author
 * @Version
 * @Description 过滤掉指定时间窗口内（默认为5s，具体时间间隔可在配置文件中设置）
 *              UID、ReaderIP、An完全相同的标签。
 * @Modifications
 * */
namespace RFIDReaderMiddleware.Prefilter
{
    class PrefilterThread
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(PrefilterThread));

        private long _filterTimeInterval;
        private String _epl1;
        private String _epl2;
        private EPServiceProvider _epService;
        private EPStatement _statement;
        private EPRuntime _epRuntime;
        private Form1 _form;

        public PrefilterThread():this(5, null)
        {
            //调用无参构造函数时，首先调用有参构造函数
        }

        public PrefilterThread(long filterTimeInterval, Form1 form)
        {
            this._form = form;
            this._filterTimeInterval = filterTimeInterval;
            initStatementEPL();
        }

        public void start()
        {
            Configuration config = new Configuration();
            config.AddEventType(typeof(XtiveTag));
            this._epService = EPServiceProviderManager.GetDefaultProvider(config);
            this._epRuntime = this._epService.EPRuntime;
            this._epService.EPAdministrator.CreateEPL(this._epl1);
            try
            {
                this._statement = this._epService.EPAdministrator.CreateEPL(this._epl2);
            }
            catch (Exception e1)
            { }
            try
            {
                this._statement.AddEventHandlerWithReplay(new PrefilterListener(this._form).PrefilterEventHandler);
            }
            catch (Exception e1)
            { }
            
        }

        public void stop()
        {
            this._statement.Stop();
            this._statement.Dispose();
            this._epService.Dispose();
        }

        public EPRuntime getEPRuntime()
        {
            return this._epRuntime;
        }

        private void initStatementEPL()
        {
            this._epl1 = "create context precontext partition by Uid, ReaderIP, An from XtiveTag";
            this._epl2 = "context precontext select context.id as ID, Uid as UID, Rssi, BLowPower, BExcite, ReaderIP, An, DateTime from XtiveTag.win:time_batch(" + this._filterTimeInterval + " sec)";
            log.Info(this._epl1);
            log.Info(this._epl2);
        }
    }
}
