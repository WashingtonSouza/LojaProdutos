using SQFramework.Web.Mvc.Extensions;
using System.Net;
using System.Web.Mvc;

namespace LojaProduto.Presentation.Filters
{
    public class CustomErrorHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.LogException(filterContext.Exception);

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                string message = "Ocorreu um erro inesperado. Contate seu administrador de sistema.";

                if (filterContext.HttpContext.IsDebuggingEnabled)
                    message += string.Format("<br/><br/>{0}", filterContext.Exception.Message);

                filterContext.Result = new JsonResult
                {
                    Data = new { @success = false, @title = "Erro", @message = message },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                string controllerName = (string)filterContext.RouteData.Values["controller"];
                string actionName = (string)filterContext.RouteData.Values["action"];

                filterContext.Result = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(new HandleErrorInfo(filterContext.Exception, controllerName, actionName)),
                };
            }

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}