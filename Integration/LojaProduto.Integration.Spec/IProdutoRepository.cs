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
    [ObjectMap("ProdutoRepository", true)]
    public interface IProdutoRepository<T> : IRepositoryBase<T>
    {
        PageMessage<T> ListarProdutos(int startIndex, int pageSize, string orderProperty, bool orderAscending);

        IList<T> PesquisarProdutos(string pesquisa);
    }
}