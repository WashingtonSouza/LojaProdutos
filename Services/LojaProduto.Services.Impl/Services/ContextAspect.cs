using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AopAlliance.Intercept;
using Common.Logging;
using System.Threading;
using System.Globalization;

namespace LojaProduto.Services.Impl.Services
{
    public class ContextAspect : SQFramework.Spring.Services.Aspects.ContextAspect
    {
        public override void BeforeInvoke(IMethodInvocation invocation)
        {
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");

            base.BeforeInvoke(invocation);
        }

        public override void AfterInvoke(IMethodInvocation invocation)
        {
            base.AfterInvoke(invocation);
        }

        public override void RegisterException(IMethodInvocation invocation, Exception ex)
        {
            base.RegisterException(invocation, ex);

            try
            {
                LogManager.GetLogger(GetType()).Error(ex);
            }
            catch
            {
            }
        }
    }
}