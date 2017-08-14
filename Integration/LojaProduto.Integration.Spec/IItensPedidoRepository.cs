using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQFramework.Spring;
using SQFramework.Spring.Domain;
using SQFramework.Data;
using SQFramework.Data.Pagging;
using LojaProduto.Services.Spec.DataTransferObjects;

namespace LojaProduto.Integration.Spec
{
    [ObjectMap("ItensPedidoRepository", true)]
    public interface IItensPedidoRepository<T> : IRepositoryBase<T>
    {
        PageMessage<T> ListarItensPedidos(int startIndex, int pageSize, string orderProperty, bool orderAscending);
    }
}