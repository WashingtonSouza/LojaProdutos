using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQFramework.Spring.Domain;
using LojaProduto.Integration.Spec;

namespace LojaProduto.Domain.Entities
{
    public partial class Endereco : DomainBase<Endereco, IEnderecoRepository<Endereco>>
    {
        public Endereco()
        {
        }

        protected int id;
        protected string logradouro;
        protected int numero;
        protected string complemento;
        protected string cidade;
        protected string cep;
        protected string uf;

        protected IList<Cliente> clientes;
        protected IList<Fornecedor> fornecedores;

        public virtual int Id { get { return id; } }
        public virtual string Logradouro { get { return logradouro; } set { logradouro = value; } }
        public virtual int Numero { get { return numero; } set { numero = value; } }
        public virtual string Complemento { get { return complemento; } set { complemento = value; } }
        public virtual string Cidade { get { return cidade; } set { cidade = value; } }
        public virtual string Cep { get { return cep; } set { cep = value; } }
        public virtual string Uf { get { return uf; } set { uf = value; } }

        public virtual IList<Cliente> Clientes { get { return (clientes ?? (clientes = new List<Cliente>())); } }
        public virtual IList<Fornecedor> Fornecedores { get { return (fornecedores ?? (fornecedores = new List<Fornecedor>())); } }
    }
}