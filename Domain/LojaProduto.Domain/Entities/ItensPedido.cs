using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQFramework.Spring.Domain;
using LojaProduto.Integration.Spec;

namespace LojaProduto.Domain.Entities
{
    public partial class ItensPedido : DomainBase<ItensPedido, IItensPedidoRepository<ItensPedido>>
    {
        protected ItensPedido()
        {
        }

        public ItensPedido(Pedido pedido, Produto produto)
        {
            this.SetPedido(pedido);
            this.SetProduto(produto);
        }

        protected int id;
        protected int quantidadeProduto;
        protected decimal totalItemPedido;

        protected Pedido pedido;
        protected Produto produto;

        public virtual int Id { get { return id; } }
        public virtual int QuantidadeProduto { get { return quantidadeProduto; } set { quantidadeProduto = value; } }
        public virtual decimal TotalItemPedido { get { return totalItemPedido; } set { totalItemPedido = value; } }

        public virtual Pedido Pedido { get { return pedido; } }
        public virtual Produto Produto { get { return produto; } }

        public virtual void SetPedido(Pedido pedido)
        {
            this.pedido = pedido;
        }

        public virtual void SetProduto(Produto produto)
        {
            this.produto = produto;
        }
    }
}