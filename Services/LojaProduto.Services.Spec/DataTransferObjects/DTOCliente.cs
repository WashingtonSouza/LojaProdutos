using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LojaProduto.Services.Spec.DataTransferObjects
{
    [DataContract()]
    [Serializable()]
    public class DTOCliente
    {
        [DataMember(), Key(), Required()]
        public int Id { get; set; }

        [DataMember(), Required(), StringLength(50)]
        public string Nome { get; set; }

        [DataMember(), Required(), StringLength(20)]
        public string Codigo { get; set; }

        [DataMember(), Required()]
        public int Telefone { get; set; }

        [DataMember(), Required(), StringLength(150)]
        public string Filiacao { get; set; }

        [DataMember(), Required(), StringLength(11)]
        public string Cpf { get; set; }

        [DataMember(), Required()]
        public decimal LimiteCredito { get; set; }

        [DataMember(), Required(), StringLength(1)]
        public string TipoStatus { get; set; }

        [DataMember(), Required()]
        public DTOEndereco Endereco { get; set; }
    }
}