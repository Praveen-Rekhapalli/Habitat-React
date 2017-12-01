

namespace Sitecore.Foundation.ReactJs.Extensions
{
    using Providers;
    using React;
    using React.Exceptions;
    using React.TinyIoC;
    using Sitecore.Extensions.StringExtensions;
    using Sitecore.Mvc;
    using Sitecore.Mvc.Presentation;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.WebPages;

    public class JsxView : BuildManagerCompiledView
    {

        public string LayoutPath { get; private set; }

        public bool RunViewStartPages { get; private set; }

        public IEnumerable<string> ViewStartFileExtensions { get;private set; }

        internal IVirtualPathFactory VirtualPathFactory { get; set; }

        internal DisplayModeProvider DisplayModeProvider { get; set; }

        public JsxView(ControllerContext controllerContext, string viewPath, string layoutPath, bool runViewStartPages, IEnumerable<string> viewStartFileExtensions):
            this(controllerContext, viewPath, layoutPath , runViewStartPages, viewStartFileExtensions, (IViewPageActivator)null)
        {

        }

        public JsxView(ControllerContext controllerContext, string viewPath, string layoutPath, bool runViewStartPages, IEnumerable<string> viewStartFileExtensions, IViewPageActivator viewPageActivator):
            base(controllerContext, viewPath , viewPageActivator)
        {
            this.LayoutPath = layoutPath ?? string.Empty;
            this.RunViewStartPages = runViewStartPages;
            this.ViewStartFileExtensions = viewStartFileExtensions ?? Enumerable.Empty<string>();  
        }

        public override void Render(ViewContext viewContext, TextWriter writer)
        {
            if(viewContext == null)
            {
                throw new ArgumentNullException(nameof(viewContext));
            }

            this.RenderView(viewContext, writer, null);
        }

        protected override void RenderView(ViewContext viewContext, TextWriter writer, object instance)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            var componentName = Path.GetFileNameWithoutExtension(this.ViewPath)?.Replace("-", string.Empty);
            var props = GetProps(viewContext.ViewData.Model);

            try
            {
                IReactComponent reactComponent = this.Environment.CreateComponent(componentName, props);
                if(ReactSettingsProvider.Current.EnableClientside)
                {
                    writer.WriteLine(reactComponent.RenderHtml());
                    var tagBuilder = new TagBuilder("script")
                    {
                        InnerHtml = reactComponent.RenderJavaScript()
                    };

                    
                    writer.Write(System.Environment.NewLine);
                    writer.Write(tagBuilder.ToString());

                }
                else
                {
                    writer.WriteLine(reactComponent.RenderHtml(renderServerOnly: true));
                }
            }
            catch(React.Exceptions.ReactScriptLoadException ex)
            {
                throw new ReactScriptLoadException("ReactJs.Net script loading exception.", ex);
            }

        }

        private IReactEnvironment Environment
        {
            get
            {
                try
                {
                    return ReactEnvironment.Current;
                }
                catch(TinyIoCResolutionException ex)
                {
                    throw new ReactNotInitialisedException("ReactJs.Net is not initialized correctly.", ex);
                }
            }
        }

        internal Rendering Rendering => RenderingContext.Current.Rendering;

        protected virtual dynamic GetProps(object model)
        {
            dynamic props = new ExpandoObject();
            var propsDictionary = (IDictionary<string, object>)props;

            dynamic placeholders = new ExpandoObject();
            var placeholdersDictionary = (IDictionary<string, object>)placeholders;

            propsDictionary["placeholders"] = placeholders;
            propsDictionary["data"] = model;

            var placeholdersField = this.Rendering.RenderingItem.InnerItem["Place Holders"];
            if(string.IsNullOrWhiteSpace(placeholdersField))
            {
                return props;
            }

            var controlId = this.Rendering.Parameters["id"] ?? string.Empty;
            dynamic placeholderId = null;

            var placeholderKeys = placeholdersField.Split(Constants.Comma, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();
            foreach(var placeholderKey in placeholderKeys)
            {
                if(placeholderKey.StartsWith("$Id."))
                {
                    if(placeholderId == null)
                    {
                        placeholderId = new ExpandoObject();
                        placeholdersDictionary["$Id"] = placeholderId;
                    }

                    ((IDictionary<string, Object>)placeholderId)[placeholderKey.Mid(3)] = PageContext.Current.HtmlHelper.Sitecore().Placeholder(controlId + placeholderKey.Mid(3)).ToString();
                }
                else
                {
                    placeholdersDictionary[placeholderKey] = PageContext.Current.HtmlHelper.Sitecore().Placeholder(placeholderKey).ToString();
                }
            }

            return props;
        }
    }
}