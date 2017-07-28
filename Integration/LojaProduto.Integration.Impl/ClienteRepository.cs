using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LojaProduto.Domain.Entities;
using LojaProduto.Integration.Spec;
using SQFramework.Data.Pagging;
using SQFramework.Spring.Data.Hibernate;
using SQFramework.Core;
using LojaProduto.Services.Spec.DataTransferObjects;

namespace LojaProduto.Integration.Impl
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository<Cliente>
    {
        public PageMessage<Cliente> ListarClientes(int startIndex, int pageSize, string orderProperty, bool orderAscending)
        {
            var criteria = DetachedCriteria.For<Cliente>();

            return Page<Cliente>(criteria, startIndex, pageSize, orderProperty, orderAscending);
        }

        public Cliente PesquisaCliente(string cpf, string codigo)
        {
            var criteria = DetachedCriteria.For<Cliente>();

            if (!string.IsNullOrEmpty(cpf))
            {
                var cpfPreparado = cpf + "%";
                criteria.Add(Restrictions.Like("cpf", cpfPreparado));
            }
            else if (!String.IsNullOrEmpty(codigo))
            {
                string codigoPreparado = "%" + codigo + "%";
                criteria.Add(Restrictions.Like("codigo", codigoPreparado));
            }
            
            return this.List<Cliente>(criteria).FirstOrDefault();
        }
    }
}