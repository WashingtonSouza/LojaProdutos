using LojaProduto.Presentation.Controllers.Base;
using System.Web.Mvc;
using System.Web.Security;

namespace LojaProduto.Presentation.Controllers
{
    public class HomeController : CustomController
    {
        #region [ Actions ]

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logoff()
        {
            DeslogarUsuario();

            return CreateMessage(true, Url.Action("Index"));
        }

        [HttpPost]
        public ActionResult PortalSistemas()
        {
            string[] enderecoPortal = FormsAuthentication.LoginUrl.Split(new char[] { '/' });

            return CreateMessage(true, string.Format("/{0}/Site/PortalSistemas.aspx", enderecoPortal[1]));
        }

        #endregion
    }
}