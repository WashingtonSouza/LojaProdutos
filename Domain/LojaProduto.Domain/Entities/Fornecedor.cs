using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQFramework.Spring.Domain;
using LojaProduto.Integration.Spec;

namespace LojaProduto.Domain.Entities
{
    public partial class Fornecedor : DomainBase<Fornecedor, IFornecedorRepository<Fornecedor>>
    {
        protected Fornecedor()
        {
        }

        public Fornecedor(Endereco endereco)
        {
            this.SetEndereco(endereco);
        }

        protected int id;
        protected string nome;
        protected string codigo;
        protected int telefone;
        protected string nomeFantasia;
        protected string cnpj;

        protected Endereco endereco;

        protected IList<Produto> produtos;

        public virtual int Id { get { return id; } }
        public virtual string Nome { get { return nome; } set { nome = value; } }
        public virtual string Codigo { get { return codigo; } set { codigo = value; } }
        public virtual int Telefone { get { return telefone; } set { telefone = value; } }
        public virtual string NomeFantasia { get { return nomeFantasia; } set { nomeFantasia = value; } }
        public virtual string Cnpj { get { return cnpj; } set { cnpj = value; } }

        public virtual Endereco Endereco { get { return endereco; } }

        public virtual IList<Produto> Produtos { get { return (produtos ?? (produtos = new List<Produto>())); } }

        public virtual void SetEndereco(Endereco endereco)
        {
            this.endereco = endereco;
        }
    }
}