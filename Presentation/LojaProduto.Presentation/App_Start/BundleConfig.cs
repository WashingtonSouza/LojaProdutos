using SQFramework.Web.Mvc.Bundle;
using System.Web.Optimization;

namespace LojaProduto.Presentation
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            var bundle = new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/globalize/globalize.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/jquery.validate.globalize.js",
                        "~/Scripts/globalize/cultures/globalize.culture.pt-BR.js");

            bundle.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(bundle);

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                       "~/Scripts/jquery.unobtrusive-ajax.custom.js",
                       "~/Scripts/jquery.unobtrusive-ajax*"));

            bundles.Add(new StyleBundle("~/Theme/css").Include(
                      "~/App_Themes/Default/css/bootstrap.min.css",
                      "~/App_Themes/Default/css/bootstrap-theme.min.css",
                      "~/App_Themes/Default/css/site.css",
                      "~/Content/jquery-chosen/chosen.min.css"));

            bundles.Add(new StyleBundle("~/Content/jqueryui").Include(
                     "~/Content/jquery-ui/jquery-ui.min.css",
                     "~/Content/jquery-ui/jquery-ui.structure.min.css",
                     "~/Content/jquery-ui/jquery-ui.theme.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                     "~/Scripts/jquery-ui/jquery-ui.min.js",
                     "~/Scripts/jquery-ui/datepicker-pt-BR.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryplugin").Include(
                     "~/Scripts/jquery.mask.js",
                     "~/Scripts/jquery.columns.paginator.js",
                     "~/Scripts/jquery.columns.js",
                     "~/Scripts/mustache.js",
                     "~/Scripts/mustache-wax.js",
                     "~/Scripts/jquery-chosen/chosen.jquery.min.js",
                     "~/Scripts/bootbox.js",
                     "~/Scripts/maskedinput-binder.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-js-utils").Include(
                     "~/Scripts/util.js",
                     "~/Scripts/ajax.utils.js"));
        }
    }
}