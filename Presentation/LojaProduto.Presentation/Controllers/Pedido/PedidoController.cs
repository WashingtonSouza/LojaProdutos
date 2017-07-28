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
        public ActionResult Pedido()
        {
            DTOPedido pedido = new DTOPedido();           

            pedido.Cliente = GetCadastroService().PesquisaCliente("", "cl25639");
            pedido.Codigo = "PE25987";
            pedido.DataElaboracao = DateTime.Now;                 

            //pedido.ItensPedidos.Add(itensPedido);

            //GetCadastroService().SalvarPedido(pedido);


            return View();
        }

        public ActionResult ListaPedidos(string query)
        {
            //ListaPedidos model = new ListaPedidos();

            return View();
        }
    }
}