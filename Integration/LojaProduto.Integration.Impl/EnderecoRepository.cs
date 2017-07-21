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
    public class EnderecoRepository : RepositoryBase<Endereco>, IEnderecoRepository<Endereco>
    {
        public PageMessage<Endereco> ListarEnderecos(int startIndex, int pageSize, string orderProperty, bool orderAscending)
        {
            var criteria = DetachedCriteria.For<Endereco>();

            return Page<Endereco>(criteria, startIndex, pageSize, orderProperty, orderAscending);
        }
    }
}