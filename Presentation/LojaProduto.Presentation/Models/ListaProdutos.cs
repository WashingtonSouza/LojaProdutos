using LojaProduto.Services.Spec.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LojaProduto.Presentation.Models
{
    public class ListaProdutos
    {
        public List<DTOProduto> Produtos { get; set; }

        public string TextoDePesquisa { get; set; }

        public List<DTOProduto> ListaPedido { get; set; }

        public string Nome { get; set; }

        public string Categoria { get; set; }

        public string PrecoProduto { get; set; }

        public int Quantidade { get; set; }

        public string NomeImagem { get; set; }

        public int IdProduto { get; set; }

    }
}