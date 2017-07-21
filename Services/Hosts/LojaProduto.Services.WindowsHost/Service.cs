using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using Common.Logging;
using SQFramework.Spring;

namespace LojaProduto.Services.WindowsHost
{
    public partial class Service : ServiceBase
    {
        private readonly ILog logger;
        private Thread workerThread;

        public Service()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            logger = LogManager.GetLogger(GetType());

            InitializeComponent();
        }

        protected void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;

            if (ex != null && logger != null)
                logger.Error(ex.Message, ex);
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                workerThread = new Thread(() =>
                {
                    try
                    {
                        ObjectFactory.Initialize();
                    }
                    catch (Exception ex)
                    {
                        if (logger != null)
                            logger.Error(ex.Message, ex);

                        Stop();
                    }
                });

                workerThread.Start();
            }
            catch (Exception ex)
            {
                if (logger != null)
                    logger.Error(ex.Message, ex);

                Stop();
            }
        }

        protected override void OnStop()
        {
            try
            {
                ObjectFactory.Finalize();
            }
            catch (Exception ex)
            {
                if (logger != null)
                    logger.Error(ex.Message, ex);
            }
        }
    }
}