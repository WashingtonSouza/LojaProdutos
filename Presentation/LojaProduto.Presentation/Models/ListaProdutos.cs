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
    }
}