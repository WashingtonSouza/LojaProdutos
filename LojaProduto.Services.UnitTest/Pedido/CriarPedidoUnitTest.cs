using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LojaProduto.Services.UnitTest.Common;

namespace LojaProduto.Services.UnitTest.Pedido
{
    [TestClass]
    public class CriarPedidoUnitTest : TestBase
    {
        [TestCategory("Pedido")]
        [TestMethod]
        public void CriarPedido()
        {
            var pedido = GetCadastroService().ObtemPedidoEmAberto(CodigoCliente);
            if(pedido == null)
            {
                pedido = GetCadastroService().CriaPedido(CodigoCliente);
                Assert.IsNotNull(pedido);
            }

            Assert.IsNotNull(pedido);
        }
    }
}
