using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LojaProduto.Domain.Entities;
using LojaProduto.Integration.Spec;
using SQFramework.Data.Pagging;
using SQFramework.Spring.Data.Hibernate;
using NHibernate.Transform;

namespace LojaProduto.Integration.Impl
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository<Produto>
    {
        public PageMessage<Produto> ListarProdutos(int startIndex, int pageSize, string orderProperty, bool orderAscending)
        {
            var criteria = DetachedCriteria.For<Produto>();

            return Page<Produto>(criteria, startIndex, pageSize, orderProperty, orderAscending);
        }

        public IList<Produto> PesquisarProdutos(string pesquisa)
        {
            var criteria = DetachedCriteria.For<Produto>();

            if (!string.IsNullOrEmpty(pesquisa))
                criteria.Add(Restrictions.Like("nome", pesquisa));
            
            var result = this.List<Produto>(criteria);

            return result;
        }
    }
}