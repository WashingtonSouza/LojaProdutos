using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQFramework.Spring.Domain;
using LojaProduto.Integration.Spec;

namespace LojaProduto.Domain.Entities
{
    public partial class Produto : DomainBase<Produto, IProdutoRepository<Produto>>
    {
        protected Produto()
        {
        }

        public Produto(Categoria categoria, Fornecedor fornecedor)
        {
            this.SetCategoria(categoria);
            this.SetFornecedor(fornecedor);
        }

        protected int id;
        protected string codigoProduto;
        protected string nome;
        protected DateTime dataFabricacao;
        protected DateTime dataVencimento;
        protected decimal precoProduto;
        protected int quantidadeEmEstoque;

        protected Categoria categoria;
        protected Fornecedor fornecedor;

        protected IList<ItensPedido> itensPedidos;

        public virtual int Id { get { return id; } }
        public virtual string CodigoProduto { get { return codigoProduto; } set { codigoProduto = value; } }
        public virtual string Nome { get { return nome; } set { nome = value; } }
        public virtual DateTime DataFabricacao { get { return dataFabricacao; } set { dataFabricacao = value; } }
        public virtual DateTime DataVencimento { get { return dataVencimento; } set { dataVencimento = value; } }
        public virtual decimal PrecoProduto { get { return precoProduto; } set { precoProduto = value; } }
        public virtual int QuantidadeEmEstoque { get { return quantidadeEmEstoque; } set { quantidadeEmEstoque = value; } }

        public virtual Categoria Categoria { get { return categoria; } }
        public virtual Fornecedor Fornecedor { get { return fornecedor; } }

        public virtual IList<ItensPedido> ItensPedidos { get { return (itensPedidos ?? (itensPedidos = new List<ItensPedido>())); } }

        public virtual void SetCategoria(Categoria categoria)
        {
            this.categoria = categoria;
        }

        public virtual void SetFornecedor(Fornecedor fornecedor)
        {
            this.fornecedor = fornecedor;
        }

        public virtual bool EstoqueSuficiente(int quantidadeProduto)
        {
            if (quantidadeProduto <= 0)
            {
                throw new System.ArgumentException("Produto com quantidade Zero ou Negativo");
            }
            else if (quantidadeProduto > QuantidadeEmEstoque)
            {
                throw new System.ArgumentException("Quantidade de produto maior que a disponível em estoque");
            }
            else
                return true;
        }

        public virtual bool ValidaPrecoProduto()
        {
            if (PrecoProduto <= 0)
            {
                return false;
                throw new System.ArgumentException("Preço Zero ou negativo");
            }
            else
                return true;
        }

    }
}