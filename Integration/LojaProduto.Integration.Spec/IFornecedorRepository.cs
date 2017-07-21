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
    [ObjectMap("FornecedorRepository", true)]
    public interface IFornecedorRepository<T> : IRepositoryBase<T>
    {
        PageMessage<T> ListarFornecedores(int startIndex, int pageSize, string orderProperty, bool orderAscending);
    }
}