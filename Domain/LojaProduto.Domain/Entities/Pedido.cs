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

        public virtual void CriarPedido()
        {
            Codigo = "PE" + Id;
            DataElaboracao = DateTime.Now;
            statusPedido = 1;
            TipoVenda = 1;
            TotalParcelas = 1;
            ValorTotalPedido = 0;
            Save();
        }
        public virtual void AdicionarProduto(Produto produto, int quantidade)
        {
            if (quantidade <= 0)
                throw new Exception("Quantidade deve ser superior a zero");

            if (!produto.EstoqueSuficiente(quantidade))
                throw new Exception("Estoque insuficiente");

            if (statusPedido == 3 && statusPedido == 4)
                throw new Exception("Pedido encontra-se faturado ou Cancelado");

            var itemPedido = new ItensPedido(this, produto);

            statusPedido = 2;
            itemPedido.QuantidadeProduto = quantidade;
            itemPedido.CalculaTotalItemPedido();
            RetiraProdutoEstoque(produto, quantidade);

            this.ItensPedidos.Add(itemPedido);
            this.Save();
        }

        private void RetiraProdutoEstoque(Produto produto, int quantidade)
        {
            produto.QuantidadeEmEstoque -= quantidade;
        }

        public virtual void ReduzQuantidadeItemPedido(ItensPedido itemPedido, int quantidade, Produto produto)
        {
            if (itemPedido.QuantidadeProduto == 0)
            {
                ItensPedidos.Remove(itemPedido);
            }
            else
                itemPedido.QuantidadeProduto -= quantidade;

            CalcularValorTotalPedido();
            produto.QuantidadeEmEstoque += quantidade;

            this.Save();
        }

        public virtual void AumentaQuantidadeProduto(ItensPedido itemPedido, int quantidade)
        {
            if (quantidade == itemPedido.QuantidadeProduto)
                throw new ArgumentException("Quantidade inalterada");
            else
            {
                if (itemPedido.Produto.EstoqueSuficiente(quantidade))
                {
                    itemPedido.QuantidadeProduto += quantidade;
                    itemPedido.Produto.QuantidadeEmEstoque -= quantidade;
                }
                else
                    throw new ArgumentException("Quantidade indisponivel no estoque");
            }

            CalcularValorTotalPedido();
            Save();
        }

        //public virtual void AtualizaQuantidadeEstoque(ItensPedido itemPedido, int quantidade)
        //{
        //    if (quantidade > itemPedido.QuantidadeProduto)
        //    {
        //        itemPedido.QuantidadeProduto += ();
        //    }
        //    else if (itemPedido.QuantidadeProduto > quantidade)
        //    {

        //    }
        //    else
        //        itemPedido.Pedido.ItensPedidos.Remove(itemPedido);
        //}

        private void ValidarParcelaPagamento()
        {
            if (TotalParcelas < 0)
                throw new System.ArgumentException("Parcela com valor menor ou igual a Zero");

            if (TotalParcelas > 1)
            {
                TipoVenda = 2;
            }
            else
                TipoVenda = 1;
        }

        public virtual void CalcularValorTotalPedido()
        {
            foreach (var item in ItensPedidos)
            {
                ValorTotalPedido += item.TotalItemPedido;
            }
        }

        public virtual void FaturarPedido()
        {
            ValidarParcelaPagamento();
            TotalParcelas = (valorTotalPedido / TotalParcelas);
            StatusPedido = 3;

            this.Save();
        }

        public virtual void EstornaPedido(ItensPedido itemPedido)
        {
            itemPedido.Produto.QuantidadeEmEstoque += itemPedido.Produto.QuantidadeEmEstoque;
            itemPedido.Pedido.statusPedido = 3;

            this.Save();
        }
    }
}