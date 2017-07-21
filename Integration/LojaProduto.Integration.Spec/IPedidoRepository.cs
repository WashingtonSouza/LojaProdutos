using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQFramework.Spring;
using SQFramework.Spring.Domain;
using SQFramework.Data;
using SQFramework.Data.Pagging;

namespace LojaProduto.Integration.Spec
{
    [ObjectMap("PedidoRepository", true)]
    public interface IPedidoRepository<T> : IRepositoryBase<T>
    {
        PageMessage<T> ListarPedidos(int startIndex, int pageSize, string orderProperty, bool orderAscending);
    }
}