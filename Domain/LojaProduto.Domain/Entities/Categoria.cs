using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQFramework.Spring.Domain;
using LojaProduto.Integration.Spec;

namespace LojaProduto.Domain.Entities
{
    public partial class Categoria : DomainBase<Categoria, ICategoriaRepository<Categoria>>
    {
        public Categoria()
        {
        }

        protected int id;
        protected string codigoIntegracao;
        protected string nome;

        protected IList<Produto> produtos;

        public virtual int Id { get { return id; } }
        public virtual string CodigoIntegracao { get { return codigoIntegracao; } set { codigoIntegracao = value; } }
        public virtual string Nome { get { return nome; } set { nome = value; } }

        public virtual IList<Produto> Produtos { get { return (produtos ?? (produtos = new List<Produto>())); } }
    }
}