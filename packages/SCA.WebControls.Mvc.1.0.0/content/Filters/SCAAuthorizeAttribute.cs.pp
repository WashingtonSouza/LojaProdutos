using $rootnamespace$.Controllers.Base;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;

namespace $rootnamespace$.Filters
{
    public class SCAAuthorizeAttribute : AuthorizeAttribute
    {
        #region [ Methods ]

        public override void OnAuthorization(AuthorizationContext context)
        {
            var controller = context.Controller as SCAController;

            if (controller != null)
            {
                if (!controller.UsuarioPossuiPermissao())
                {
                    var request = context.HttpContext.Request;

                    if (request.IsAjaxRequest())
                    {
                        var response = context.HttpContext.Response;

                        response.SuppressFormsAuthenticationRedirect = true;
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        response.End();
                    }
                    else
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "AccessDenied" }));
                }
            }
            else
                base.OnAuthorization(context);
        }

        #endregion
    }
}