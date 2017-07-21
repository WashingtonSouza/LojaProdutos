using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQFramework.Spring.Domain;
using LojaProduto.Integration.Spec;

namespace LojaProduto.Domain.Entities
{
    public partial class Pedido : DomainBase<Pedido, IPedidoRepository<Pedido>>
    {
        protected Pedido()
        {
        }

        public Pedido(Cliente cliente)
        {
            this.SetCliente(cliente);
        }

        protected int id;
        protected string codigo;
        protected DateTime dataElaboracao;
        protected decimal valorTotalPedido;
        protected int statusPedido;
        protected int tipoVenda;
        protected decimal totalParcelas;

        protected Cliente cliente;

        protected IList<ItensPedido> itensPedidos;

        public virtual int Id { get { return id; } }
        public virtual string Codigo { get { return codigo; } set { codigo = value; } }
        public virtual DateTime DataElaboracao { get { return dataElaboracao; } set { dataElaboracao = value; } }
        public virtual decimal ValorTotalPedido { get { return valorTotalPedido; } set { valorTotalPedido = value; } }
        public virtual int StatusPedido { get { return statusPedido; } set { statusPedido = value; } }
        public virtual int TipoVenda { get { return tipoVenda; } set { tipoVenda = value; } }
        public virtual decimal TotalParcelas { get { return totalParcelas; } set { totalParcelas = value; } }

        public virtual Cliente Cliente { get { return cliente; } }

        public virtual IList<ItensPedido> ItensPedidos { get { return (itensPedidos ?? (itensPedidos = new List<ItensPedido>())); } }

        public virtual void SetCliente(Cliente cliente)
        {
            this.cliente = cliente;
        }

        public override void Save()
        {
            base.Save();
        }
    }
}