using SQFramework.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Xml;

namespace LojaProduto.Presentation.Helpers
{
    public enum IconType
    {
        Voltar,
        Salvar,
        Limpar,
        Novo,
        Pesquisar
    }

    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString DropDownListCompositeFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string optionLabel, object htmlAttributes)
        {
            string name = ExpressionHelper.GetExpressionText(expression).Split('.').First();

            var selectList = (IEnumerable<SelectListItem>)htmlHelper.ViewData[name + "List"];

            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) ?? new RouteValueDictionary();

            var keys = expression.ReturnType.GetProperties().Where(p => p.GetAttribute<KeyAttribute>() != null).Select(p => p.Name);

            attributes.Add("data-composite-fields", string.Join(",", keys));

            return htmlHelper.DropDownListFor(expression, selectList, optionLabel, attributes);
        }

        public static MvcHtmlString DropDownListFor<TModel, TProperty>(
           this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string optionLabel, object htmlAttributes)
        {
            string name = ExpressionHelper.GetExpressionText(expression).Split('.').First();

            var selectList = (IEnumerable<SelectListItem>)htmlHelper.ViewData[name + "List"];

            var result = htmlHelper.DropDownListFor(expression, selectList, optionLabel, htmlAttributes);

            var attributes = htmlHelper.GetUnobtrusiveValidationAttributes(name);

            var doc = new XmlDocument();
            doc.LoadXml(result.ToString());

            foreach (var attribute in attributes)
                doc.DocumentElement.Attributes[attribute.Key].Value = attribute.Value.ToString();

            return new MvcHtmlString(doc.OuterXml);
        }

        public static MvcHtmlString Button(this HtmlHelper helper, string id, IconType iconType, string text = null, bool isSubmit = false)
        {
            var i = new TagBuilder("i");
            i.MergeAttribute("aria-hidden", "true");
            i.MergeAttribute("class", GetCssClass(iconType));

            var button = new TagBuilder("button");
            button.MergeAttribute("id", id);
            button.MergeAttribute("class", "btn btn-default btn-antt");

            if (isSubmit)
                button.MergeAttribute("type", "submit");

            button.InnerHtml = string.Format("{0}&nbsp;{1}", i, text ?? iconType.ToString());

            return MvcHtmlString.Create(button.ToString());
        }

        public static MvcHtmlString ConfirmationLink(this HtmlHelper helper, string linkText,
            string actionName, string controllerName, string title, string message, object htmlAttributes = null)
        {
            var link = new TagBuilder("a");

            if (htmlAttributes != null)
                link.MergeAttributes<string, object>(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            link.MergeAttribute("href", helper.GetUrl(actionName, controllerName));
            link.MergeAttribute("onclick", string.Format("showConfirm('{0}','{1}');return false;", title, message));

            link.InnerHtml = linkText;

            return MvcHtmlString.Create(link.ToString());
        }

        private static string GetUrl(this HtmlHelper htmlHelper, string actionName, string controllerName = null)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            return urlHelper.Action(actionName, controllerName);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper helper, string url, IconType iconType)
        {
            var i = new TagBuilder("i");
            i.MergeAttribute("aria-hidden", "true");
            i.MergeAttribute("class", GetCssClass(iconType));

            var link = new TagBuilder("a");
            link.MergeAttribute("class", "btn btn-default btn-antt");
            link.MergeAttribute("href", url);

            link.InnerHtml = string.Format("{0}&nbsp;{1}", i, iconType);

            return MvcHtmlString.Create(link.ToString());
        }

        private static string GetCssClass(IconType type)
        {
            switch (type)
            {
                case IconType.Salvar:
                    return "glyphicon glyphicon-floppy-disk";
                case IconType.Voltar:
                    return "glyphicon glyphicon-arrow-left";
                case IconType.Novo:
                    return "glyphicon glyphicon-file";
                case IconType.Pesquisar:
                    return "glyphicon glyphicon-search";
                case IconType.Limpar:
                    return "glyphicon glyphicon-erase";
                default:
                    return string.Empty;
            }
        }
    }
}