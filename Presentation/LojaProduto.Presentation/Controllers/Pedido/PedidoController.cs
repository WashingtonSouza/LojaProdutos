using LojaProduto.Presentation.Controllers.Base;
using LojaProduto.Services.Spec.DataTransferObjects;
using LojaProduto.Presentation.Models;
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
            var pedido = GetCadastroService().ObtemPedidoEmAberto(CodigoCliente);     
            
            if (pedido == null)
            {
                pedido = GetCadastroService().CriaPedido(CodigoCliente);
            }

            GetCadastroService().AdicionaItemPedido(pedido.Id, quantidadeProduto, idProduto);

            return Json(JsonRequestBehavior.DenyGet);
        }

        public ActionResult Pedidos()
        {
            Pedidos model = new Pedidos();

            var resultado = GetCadastroService().ListarItensPedidos();
            model.ItensPedidos = resultado.ToList();            

            return View(model);
        }
    }
}