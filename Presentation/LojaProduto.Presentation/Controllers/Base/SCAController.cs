using SCA.Servicos.Spec.Servicos;
using SCA.WebControls;
using SQFramework.Core;
using SQFramework.Web.Mvc;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace LojaProduto.Presentation.Controllers.Base
{
    public abstract class SCAController : BaseController
    {
        #region [ Methods ]

        public SCAController()
        {
            ObterUsuarioLogado();
        }

        public abstract ISCAService GetSCAService();

        private void ObterUsuarioLogado()
        {
            try
            {
                var context = System.Web.HttpContext.Current;

                if (context != null && context.User != null && context.User.Identity != null && context.User.Identity.IsAuthenticated)
                {
                    var identity = context.User.Identity as FormsIdentity;
                    Guid identificadorAutenticacao = (identity != null && identity.Ticket != null ? identity.Ticket.UserData : null).ToGuid();

                    if (identificadorAutenticacao != Guid.Empty)
                    {
                        var usuario = SCAApplicationContext.Usuario;

                        if (usuario != null && usuario.IdentificadorAutenticacao == identificadorAutenticacao)
                            return;

                        var retorno = GetSCAService().ObterInformacoesUsuarioLogado(identificadorAutenticacao.ToString());

                        if (retorno.Usuario != null)
                        {
                            SCAApplicationContext.Usuario = retorno.Usuario;
                            SCAApplicationContext.Permissoes = retorno.Permissoes;
                            SCAApplicationContext.AdicionarUsuarioLogado();

                            return;
                        }
                    }
                }

                DeslogarUsuario();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        protected void DeslogarUsuario()
        {
            var context = System.Web.HttpContext.Current;

            context.Response.Cookies.Clear();
            context.User = null;
            FormsAuthentication.SignOut();
            context.Session.Abandon();
        }

        public bool UsuarioPossuiPermissao()
        {
            try
            {
                var usuario = SCAApplicationContext.Usuario;

                if (usuario == null)
                    return false;

                if (usuario.Master)
                    return true;

                var permissoes = SCAApplicationContext.Permissoes;

                if (permissoes != null && permissoes.PermissoesFuncionalidades != null && permissoes.PermissoesFuncionalidades.Count > 0)
                {
                    var controller = string.Format("/{0}", ControllerContext.RequestContext.RouteData.Values["controller"]);
                    var action = string.Format("{0}/{1}", controller, ControllerContext.RequestContext.RouteData.Values["action"]);

                    return permissoes.PermissoesFuncionalidades
                        .Any(p => p.Value && (p.Key.EqualsIgnoreCase(controller) || p.Key.EqualsIgnoreCase(action)));
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            return false;
        }

        #endregion
    }
}