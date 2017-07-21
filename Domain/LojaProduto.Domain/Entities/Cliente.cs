using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQFramework.Spring.Domain;
using LojaProduto.Integration.Spec;

namespace LojaProduto.Domain.Entities
{
    public partial class Cliente : DomainBase<Cliente, IClienteRepository<Cliente>>
    {
        protected Cliente()
        {
        }

        public Cliente(Endereco endereco)
        {
            this.SetEndereco(endereco);
        }

        protected int id;
        protected string nome;
        protected string codigo;
        protected int telefone;
        protected string filiacao;
        protected string cpf;
        protected decimal limiteCredito;
        protected string tipoStatus;

        protected Endereco endereco;

        protected IList<Pedido> pedidos;

        public virtual int Id { get { return id; } }
        public virtual string Nome { get { return nome; } set { nome = value; } }
        public virtual string Codigo { get { return codigo; } set { codigo = value; } }
        public virtual int Telefone { get { return telefone; } set { telefone = value; } }
        public virtual string Filiacao { get { return filiacao; } set { filiacao = value; } }
        public virtual string Cpf { get { return cpf; } set { cpf = value; } }
        public virtual decimal LimiteCredito { get { return limiteCredito; } set { limiteCredito = value; } }
        public virtual string TipoStatus { get { return tipoStatus; } set { tipoStatus = value; } }

        public virtual Endereco Endereco { get { return endereco; } }

        public virtual IList<Pedido> Pedidos { get { return (pedidos ?? (pedidos = new List<Pedido>())); } }

        public virtual void SetEndereco(Endereco endereco)
        {
            this.endereco = endereco;
        }
    }
}