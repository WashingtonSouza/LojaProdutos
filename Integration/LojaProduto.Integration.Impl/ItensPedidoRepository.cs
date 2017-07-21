using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LojaProduto.Domain.Entities;
using LojaProduto.Integration.Spec;
using SQFramework.Data.Pagging;
using SQFramework.Spring.Data.Hibernate;

namespace LojaProduto.Integration.Impl
{
    public class ItensPedidoRepository : RepositoryBase<ItensPedido>, IItensPedidoRepository<ItensPedido>
    {
        public PageMessage<ItensPedido> ListarItensPedidos(int startIndex, int pageSize, string orderProperty, bool orderAscending)
        {
            var criteria = DetachedCriteria.For<ItensPedido>();

            return Page<ItensPedido>(criteria, startIndex, pageSize, orderProperty, orderAscending);
        }
    }
}