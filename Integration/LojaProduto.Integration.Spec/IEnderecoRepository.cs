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
    [ObjectMap("EnderecoRepository", true)]
    public interface IEnderecoRepository<T> : IRepositoryBase<T>
    {
        PageMessage<T> ListarEnderecos(int startIndex, int pageSize, string orderProperty, bool orderAscending);
    }
}