using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Ajax;
using System.Web.Mvc;
using System.ComponentModel;

namespace AppProj.Web.Helpers
{
    public static class ActionLinkHelper
    {
        #region Ajax Action Link

        public static IHtmlString ImageActionLink(this AjaxHelper helper, string imageUrl, string altText, string actionName, string controlerName, object routeValues, AjaxOptions ajaxOptions, object imageHtmlAttribute, object linkHtmlAttribute)
        {
            return ImageActionLinkBase(helper, imageUrl, altText, actionName, controlerName, routeValues, ajaxOptions, imageHtmlAttribute, linkHtmlAttribute);
        }

        public static IHtmlString IconActionLink(this AjaxHelper helper, string iconClass, string linkText, string actionName, string controlerName, object routeValues, AjaxOptions ajaxOptions, object linkHtmlAttribute)
        {
            return IconActionLinkBase(helper, iconClass, linkText, actionName, controlerName, routeValues, ajaxOptions, linkHtmlAttribute);
        }

        #endregion

        #region Html Action Link

        public static IHtmlString ImageActionLink(this HtmlHelper helper, string imageUrl, string altText, string actionName, string controlerName, object routeValues, object imageHtmlAttribute, object linkHtmlAttribute)
        {
            return ImageActionLinkBase(helper, imageUrl, altText, actionName, controlerName, routeValues, imageHtmlAttribute, linkHtmlAttribute);
        }

        public static IHtmlString IconActionLink(this HtmlHelper helper, string iconClass, string linkText, string actionName, string controlerName, object routeValues, object linkHtmlAttribute)
        {
            return IconActionLinkBase(helper, iconClass, linkText, actionName, controlerName, routeValues, linkHtmlAttribute);
        }

        #endregion

        #region Private methods

        private static IHtmlString ImageActionLinkBase(this AjaxHelper helper, string imageUrl, string altText, string actionName, string controlerName, object routeValues, AjaxOptions ajaxOptions, object imageHtmlAttribute, object linkHtmlAttribute)
        {
            string link = "";

            controlerName = controlerName == "" ? null : controlerName;

            link = helper.ActionLink("[replaceme]", actionName, controlerName, routeValues, ajaxOptions).ToHtmlString();

            if (controlerName == null)
            {
                link = helper.ActionLink("[replaceme]", actionName, routeValues, ajaxOptions, linkHtmlAttribute).ToHtmlString();
            }
            else
            {
                link = helper.ActionLink("[replaceme]", actionName, controlerName, routeValues, ajaxOptions, linkHtmlAttribute).ToHtmlString();
            }

            var imgHtml = ImageBuilder(imageUrl, altText, imageHtmlAttribute);

            return MvcHtmlString.Create(link.Replace("[replaceme]", imgHtml));
        }

        private static IHtmlString ImageActionLinkBase(this HtmlHelper helper, string imageUrl, string altText, string actionName, string controlerName, object routeValues, object imageHtmlAttribute, object linkHtmlAttribute)
        {
            controlerName = controlerName == "" ? null : controlerName;

            var imgHtml = ImageBuilder(imageUrl, altText, imageHtmlAttribute);
            var url = new UrlHelper(helper.ViewContext.RequestContext);

            var anchorBuilder = new TagBuilder("a");

            if (controlerName == null)
            {
                anchorBuilder.MergeAttribute("href", url.Action(actionName, routeValues));
            }
            else
            {
                anchorBuilder.MergeAttribute("href", url.Action(actionName, controlerName, routeValues));
            }

            anchorBuilder.MergeAttributes(AnonymousObjectToKeyValue(linkHtmlAttribute), true);

            anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }

        private static IHtmlString IconActionLinkBase(this AjaxHelper helper, string iconClass, string linkText, string actionName, string controlerName, object routeValues, AjaxOptions ajaxOptions, object linkHtmlAttribute)
        {
            string link = "";

            controlerName = controlerName == "" ? null : controlerName;

            link = helper.ActionLink("[replaceme]", actionName, controlerName, routeValues, ajaxOptions).ToHtmlString();

            if (controlerName == null)
            {
                link = helper.ActionLink("[replaceme]", actionName, routeValues, ajaxOptions, linkHtmlAttribute).ToHtmlString();
            }
            else
            {
                link = helper.ActionLink("[replaceme]", actionName, controlerName, routeValues, ajaxOptions, linkHtmlAttribute).ToHtmlString();
            }

            var iconHtml = IconBuilder(iconClass) + linkText;

            return MvcHtmlString.Create(link.Replace("[replaceme]", iconHtml));
        }

        private static IHtmlString IconActionLinkBase(this HtmlHelper helper, string iconClass, string linkText, string actionName, string controlerName, object routeValues, object linkHtmlAttribute)
        {
            controlerName = controlerName == "" ? null : controlerName;

            var iconHtml = IconBuilder(iconClass);
            var url = new UrlHelper(helper.ViewContext.RequestContext);

            var anchorBuilder = new TagBuilder("a");

            if (controlerName == null)
            {
                anchorBuilder.MergeAttribute("href", url.Action(actionName, routeValues));
            }
            else
            {
                anchorBuilder.MergeAttribute("href", url.Action(actionName, controlerName, routeValues));
            }

            anchorBuilder.MergeAttributes(AnonymousObjectToKeyValue(linkHtmlAttribute), true);

            anchorBuilder.InnerHtml = iconHtml + linkText;
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }

        private static string ImageBuilder(string imageUrl, string altText, object htmlAttribute)
        {
            altText = altText == null ? "" : altText;

            var imageBuilder = new TagBuilder("img");
            imageBuilder.MergeAttribute("src", imageUrl);
            imageBuilder.MergeAttribute("alt", altText);

            imageBuilder.MergeAttributes(AnonymousObjectToKeyValue(htmlAttribute), true);

            return imageBuilder.ToString();
        }

        private static string IconBuilder(string iconClass)
        {
            var iconBuilder = new TagBuilder("i");
            iconBuilder.AddCssClass(iconClass);
            return iconBuilder.ToString();
        }

        private static Dictionary<string, object> AnonymousObjectToKeyValue(object anonymousObject)
        {
            var dictionary = new Dictionary<string, object>();

            if (anonymousObject != null)
            {
                foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(anonymousObject))
                {
                    dictionary.Add(pd.Name, pd.GetValue(anonymousObject));
                }
            }

            return dictionary;
        }

        #endregion

        public static MvcHtmlString If(this MvcHtmlString value, bool isEnable, MvcHtmlString falseValue = default(MvcHtmlString))
        {
            return isEnable ? value : falseValue;
        }
    }
}