using SCA.Servicos.Spec.Servicos;
using SCA.WebControls;
using LojaProduto.Services.Spec.Services;

namespace LojaProduto.Presentation.Controllers.Base
{
    public abstract class CustomController : SCAController
    {
        public override ISCAService GetSCAService()
        {
            return serviceLocator.GetService<ISCAService>("antt.servicos/SCAService", SCAApplicationContext.ObterParametrosServico(), "antt");
        }

        public ICadastroService GetCadastroService()
        {
            return serviceLocator.GetService<ICadastroService>("antt.servicos/CadastroService", SCAApplicationContext.ObterParametrosServico(), "antt");
        }

        public int CodigoCliente { get { return 5; } }
    }
}