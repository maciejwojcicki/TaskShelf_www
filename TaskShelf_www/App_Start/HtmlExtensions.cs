using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace TaskShelf_www.App_Start
{
    public static class HtmlExtensions
    {
        //Umozliwia uzywanie skryptow w PartialViews / Actions itp
        public static MvcHtmlString AddScript(this HtmlHelper htmlHelper, string path)
        {
            htmlHelper.ViewContext.HttpContext.Items["_scriptpath_" + Guid.NewGuid()] = path;
            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString AddCss(this HtmlHelper htmlHelper, string path)
        {
            htmlHelper.ViewContext.HttpContext.Items["_csspath_" + Guid.NewGuid()] = path;
            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString Translate(this HtmlHelper htmlHelper, string key)
        {
            var text = key;
            var cache = htmlHelper.ViewContext.HttpContext.Cache;
            if (cache["_language_" + key] != null)
            {
                text = cache["_language_" + key].ToString();
            }
            return new MvcHtmlString(text);
        }

        public static String Translate(string key)
        {
            var text = key;
            var cache = HttpContext.Current.Cache;
            if (cache["_language_" + key] != null)
            {
                text = cache["_language_" + key].ToString();
            }
            return text;
        }

        public static MvcHtmlString Translate(this HtmlHelper htmlHelper, string key, IDictionary<string, string> replaces)
        {
            var text = key;
            var cache = htmlHelper.ViewContext.HttpContext.Cache;
            if (cache["_language_" + key] != null)
            {
                text = cache["_language_" + key].ToString();
            }
            foreach (var item in replaces.Keys)
            {
                text = text.Replace("@@" + item + "@@", replaces[item]);
            }
            return new MvcHtmlString(text);
        }

        //wykonuje skrypt po zaladowaniu plikow js
        public static MvcHtmlString ExecuteScript(this HtmlHelper htmlHelper, string script)
        {
            htmlHelper.ViewContext.HttpContext.Items["_scripttoexecute_" + Guid.NewGuid()] = script;
            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString RenderScripts(this HtmlHelper htmlHelper)
        {
            var paths = new List<string>();
            var scripts = new List<string>();
            foreach (object key in htmlHelper.ViewContext.HttpContext.Items.Keys)
            {
                if (key.ToString().StartsWith("_scriptpath_"))
                {
                    var path = (string)htmlHelper.ViewContext.HttpContext.Items[key];
                    paths.Add(path);
                }
                if (key.ToString().StartsWith("_scripttoexecute_"))
                {
                    var script = (string)htmlHelper.ViewContext.HttpContext.Items[key];
                    scripts.Add(script);
                }
            }
            var sb = new StringBuilder();
            sb.Append(Scripts.Render(paths.ToArray()).ToHtmlString());
            scripts.ForEach(e =>
            {
                var tag = new TagBuilder("script");
                tag.Attributes.Add("type", "text/javascript");
                tag.InnerHtml = e;
                sb.Append(tag.ToString());
            });
            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString RenderCss(this HtmlHelper htmlHelper)
        {
            var paths = new List<string>();
            foreach (object key in htmlHelper.ViewContext.HttpContext.Items.Keys)
            {
                if (key.ToString().StartsWith("_csspath_"))
                {
                    var path = (string)htmlHelper.ViewContext.HttpContext.Items[key];
                    paths.Add(path);
                }
            }
            var sb = new StringBuilder();
            sb.Append(Styles.Render(paths.ToArray()).ToHtmlString());
            return new MvcHtmlString(sb.ToString());
        }
    }
}