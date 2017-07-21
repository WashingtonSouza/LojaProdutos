using System.Web;
using System.Web.Mvc;
using LojaProduto.Presentation.Filters;

namespace LojaProduto.Presentation
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomErrorHandlerAttribute());
            filters.Add(new SCAAuthorizeAttribute());
        }
    }
}