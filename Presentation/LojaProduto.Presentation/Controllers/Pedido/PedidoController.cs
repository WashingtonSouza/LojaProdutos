using LojaProduto.Presentation.Controllers.Base;
using LojaProduto.Services.Spec.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LojaProduto.Presentation.Controllers.Pedido
{
    public class PedidoController : CustomController
    {
        // GET: Pedido
        public ActionResult Pedido(int idProduto = 0, int quantidadeProduto = 0)
        {

            GetCadastroService().TemPedido(idProduto, quantidadeProduto);

            return Json(JsonRequestBehavior.DenyGet);
        }

        public ActionResult ListaPedidos(string query)
        {
            //ListaPedidos model = new ListaPedidos();

            return View();
        }
    }
}