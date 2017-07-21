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
    public class DTOFornecedor
    {
        [DataMember(), Key(), Required(), DisplayName("Id")]
        public int Id { get; set; }

        [DataMember(), Required(), StringLength(50), DisplayName("Nome")]
        public string Nome { get; set; }

        [DataMember(), Required(), StringLength(20), DisplayName("Codigo")]
        public string Codigo { get; set; }

        [DataMember(), Required(), DisplayName("Telefone")]
        public int Telefone { get; set; }

        [DataMember(), Required(), StringLength(100), DisplayName("NomeFantasia")]
        public string NomeFantasia { get; set; }

        [DataMember(), Required(), StringLength(11), DisplayName("Cnpj")]
        public string Cnpj { get; set; }

        [DataMember(), Required(), DisplayName("Endereco")]
        public DTOEndereco Endereco { get; set; }
    }
}