using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using LojaProduto.Common;

namespace LojaProduto.Services.Spec.DataTransferObjects
{
    [DataContract()]
    [Serializable()]
    public class DTOPedido
    {
        [DataMember(), Key(), Required()]
        public int Id { get; set; }

        [DataMember(), Required(), StringLength(10)]
        public string Codigo { get; set; }

        [DataMember(), Required()]
        public DateTime DataElaboracao { get; set; }

        [DataMember(), Required()]
        public decimal ValorTotalPedido { get; set; }

        [DataMember()] //, Required()
        public int statusPedido { get; set; }

        [DataMember(), Required()]
        public int TipoVenda { get; set; }

        [DataMember(), Required()]
        public decimal TotalParcelas { get; set; }

        [DataMember(), Required()]
        public DTOCliente Cliente { get; set; }

        public List<DTOItensPedido> ItensPedidos = new List<DTOItensPedido>(); 
    }
}