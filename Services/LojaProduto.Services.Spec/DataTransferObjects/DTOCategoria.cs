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
    public class DTOCategoria
    {
        [DataMember(), Key(), Required(), DisplayName("Id")]
        public int Id { get; set; }

        [DataMember(), Required(), StringLength(10), DisplayName("Código Integração")]
        public string CodigoIntegracao { get; set; }

        [DataMember(), Required(), StringLength(50), DisplayName("Nome")]
        public string Nome { get; set; }
    }
}