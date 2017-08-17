using LojaProduto.Services.Spec.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LojaProduto.Presentation.Models
{
    public class ListaPedidos
    {
        public int Id { get; set; }

        public string Codigo { get; set; }

        public DateTime DataElaboracao { get; set; }

        public decimal ValorTotalPedido { get; set; }

        public int statusPedido { get; set; }

        public int TipoVenda { get; set; }

        public decimal TotalParcelas { get; set; }

        public DTOCliente Cliente { get; set; }

        public List<DTOItensPedido> ItensPedidos = new List<DTOItensPedido>();

        public string NomeImagemPedido { get; set; }
    }
}