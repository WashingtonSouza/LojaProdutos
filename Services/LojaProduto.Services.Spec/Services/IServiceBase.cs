using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace LojaProduto.Services.Spec.Services
{
    [ServiceContract]
    public interface IServiceBase
    {
        [OperationContract]
        string GetServiceVersion();
    }
}