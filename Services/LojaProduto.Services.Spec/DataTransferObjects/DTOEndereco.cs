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
    public class DTOEndereco
    {
        [DataMember(), Key(), Required()]
        public int Id { get; set; }

        [DataMember(), StringLength(100)]
        public string Logradouro { get; set; }

        [DataMember(), Required()]
        public int Numero { get; set; }

        [DataMember(), StringLength(100)]
        public string Complemento { get; set; }

        [DataMember(), StringLength(100)]
        public string Cidade { get; set; }

        [DataMember(), Required(), StringLength(8)]
        public string Cep { get; set; }

        [DataMember(), StringLength(2)]
        public string Uf { get; set; }
    }
}