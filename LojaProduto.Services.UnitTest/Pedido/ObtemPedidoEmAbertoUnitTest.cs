using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LojaProduto.Services.UnitTest.Common;

namespace LojaProduto.Services.UnitTest.Pedido
{
    [TestClass]
    public class ObtemPedidoEmAbertoUnitTest : TestBase
    {
        [TestCategory("Pedido")]
        [TestMethod]
        public void ObtemPedidoEmAberto()
        {
            var pedido = GetCadastroService().ObtemPedidoEmAberto(5);

            Assert.IsNotNull(pedido);
        }

        [TestCategory("Pedido")]
        [TestMethod]
        public void VerificarSeClienteNovoPossuiPedido()
        {
            var pedido = GetCadastroService().ObtemPedidoEmAberto(1);

            Assert.IsNull(pedido);
        }

        [TestCategory("Pedido")]
        [TestMethod]
        public void lista()
        {
            var lista1 = GetCadastroService().ListarPedidos();

            Assert.IsNull(lista1);
        }


    }
}
