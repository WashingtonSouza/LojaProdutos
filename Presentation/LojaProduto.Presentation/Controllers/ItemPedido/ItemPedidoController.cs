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
        public ActionResult ItemPedido()
        {
            DTOItensPedido itemPedido = new DTOItensPedido();
            //itemPedido.Produto = GetCadastroService().ObterProduto(idProduto);

            //GetCadastroService().AdicionaItemPedido(itemPedido, quantidadeProduto);
            //return Json(itemPedido, JsonRequestBehavior.DenyGet);
            return View();
        }
    }
}