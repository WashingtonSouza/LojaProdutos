using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LojaProduto.Services.UnitTest.Common;

namespace LojaProduto.Services.UnitTest.Produto
{
    [TestClass]
    public class ObterProdutoUnitTest : TestBase
    {
        [TestCategory("Produto")]
        [TestMethod]
        public void ObterProduto()
        {
            var obterProduto = GetCadastroService().ObterProduto(1);

            Assert.IsNotNull(obterProduto);
        }
    }
}
