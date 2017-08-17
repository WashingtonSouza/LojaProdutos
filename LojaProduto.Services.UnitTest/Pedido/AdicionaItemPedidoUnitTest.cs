using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LojaProduto.Services.UnitTest.Common;

namespace LojaProduto.Services.UnitTest.Pedido
{
    [TestClass]
    public class AdicionaItemPedidoUnitTest :TestBase
    {
        [TestCategory("Pedido")]
        [TestMethod]
        public void AdicionaItemPedido()
        {
            var pedido = GetCadastroService().ObtemPedidoEmAberto(CodigoCliente);
            var quantidadeProduto = 10;
            var idProduto = 4;

            GetCadastroService().AdicionaItemPedido(pedido.Id, quantidadeProduto, idProduto);

            foreach (var itemPedido in pedido.ItensPedidos)
            {
                //Assert.AreEqual();
            }
        }
    }
}
