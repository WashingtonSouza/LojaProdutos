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
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository<Pedido>
    {
        public PageMessage<Pedido> ListarPedidos(int startIndex, int pageSize, string orderProperty, bool orderAscending)
        {
            var criteria = DetachedCriteria.For<Pedido>();

            return Page<Pedido>(criteria, startIndex, pageSize, orderProperty, orderAscending);
        }

        public Pedido VerificaSePossuiPedidoAberto(int idCliente)
        {
            //var criteria = DetachedCriteria.For<Pedido>();


            //var sistemas = SQFramework.Spring.ObjectFactory.GetSingleton<eCarta.Integration.Spec.Services.ISCARepository>()
            //    .ListarSistemas<VOSistema>().Where(x => x.Diretoria == area.Sigla);


            //var criteria = DetachedCriteria.For<Pedido>()
            //  .Add(Restrictions.Conjunction().Add(
            //  Restrictions.Eq("cliente.id", Convert.ToInt32(idCliente)))
            //  .Add(Restrictions.Eq("statusPedido", 1)));

            //return Get<Pedido>(criteria);




            var criteria = DetachedCriteria.For<Pedido>();

            criteria.Add(Restrictions.And(Expression.Eq("cliente.id", Convert.ToInt32(idCliente)), Expression.Eq("statusPedido", 1)));

            return this.List<Pedido>(criteria).FirstOrDefault();
        }
    }
}