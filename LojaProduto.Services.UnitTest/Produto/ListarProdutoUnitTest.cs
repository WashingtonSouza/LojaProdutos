using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LojaProduto.Services.UnitTest.Common;

namespace LojaProduto.Services.UnitTest.Produto
{
    [TestClass]
    public class ListarProdutoUnitTest : TestBase
    {
        [TestCategory("Produto")]
        [TestMethod]
        public void ListarProduto()
        {
            var listaProduto = GetCadastroService().ListarProdutos();

            Assert.IsNotNull(listaProduto);
        }
    }
}
