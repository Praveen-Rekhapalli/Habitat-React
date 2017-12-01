

namespace Sitecore.Foundation.ReactJs.Extensions
{
    using System.Web.Mvc;
    public class JsxViewEngine : BuildManagerViewEngine
    {
        public JsxViewEngine() : this(null)
        {

        }

        public JsxViewEngine(IViewPageActivator viewPageActivator) : base(viewPageActivator)
        {
            this.AreaViewLocationFormats = new[]
            {
                    "~/Areas/{2}/Views/{1}/{0}.jsx",
                    "~/Areas/{2}/Views/Shared/{0}.jsx"
            };

            this.AreaMasterLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{0}.jsx",
                "~/Areas/{2}/Views/Shared/{0}.jsx"
            };

            this.AreaPartialViewLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{0}.jsx",
                "~/Areas/{2}/Views/Shared/{0}.jsx"
            };

            this.ViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.jsx",
                "~/Views/Shared/{0}.jsx"
            };

            this.MasterLocationFormats = new[]
            {
                "~/Views/{1}/{0}.jsx",
                "~/Views/Shared/{0}.jsx"
            };

            this.PartialViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.jsx",
                "~/Views/Shared/{0}.jsx"
            };

            this.FileExtensions = new[]
            {
                "jsx","js"
            };
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return new JsxView(controllerContext, partialPath, null, false, this.FileExtensions, this.ViewPageActivator)
            {
                DisplayModeProvider = this.DisplayModeProvider
            };
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new JsxView(controllerContext, viewPath, masterPath, true, this.FileExtensions, this.ViewPageActivator)
            {
                DisplayModeProvider = this.DisplayModeProvider
            };

        }
    }
}