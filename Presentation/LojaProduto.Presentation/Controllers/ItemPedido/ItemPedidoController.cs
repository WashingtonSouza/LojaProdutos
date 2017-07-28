using LojaProduto.Services.Spec.DataTransferObjects;
using LojaProduto.Presentation.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaProduto.Presentation.Controllers.ItemPedido
{
    public class ItemPedidoController : CustomController
    {
        // GET: ItemPedido
        public ActionResult ItemPedido(int idProduto = 0, int quantidadeProduto = 0)
        {
            DTOItensPedido itensPedido = new DTOItensPedido();
            
            itensPedido.Produto = GetCadastroService().ObterProduto(idProduto);            
            itensPedido.QuantidadeProduto = quantidadeProduto;
            itensPedido



            return Json(itensPedido, JsonRequestBehavior.DenyGet);
        }
    }
}