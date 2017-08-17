//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using LojaProduto.Services.Spec.Services;
using SQFramework.Net.Services;
using SCA.WebControls;
using System.IO;
using SCA.Servicos.Spec.Servicos;
using SQFramework.Web.Mvc;

namespace LojaProduto.Services.UnitTest.Common
{
    public abstract class TestBase 
    {
        private ServiceLocator serviceLocator = new ServiceLocator();

        public ICadastroService GetCadastroService()
        {
            return serviceLocator.GetService<ICadastroService>("lojaproduto.servicos/cadastrosService");
        }

        public int CodigoCliente { get { return 5; } }


    }
}
