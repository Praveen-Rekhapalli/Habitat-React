

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Sitecore.Foundation.ReactJs.ReactConfig), "Configure")]

namespace Sitecore.Foundation.ReactJs
{
    using Models;
    using Providers;
    using React;
    using Sitecore.Foundation.ReactJs.Extensions;
    using System.Web.Mvc;

    public static class ReactConfig
	{
		public static void Configure()
		{
            ViewEngines.Engines.Add(new JsxViewEngine());
            ReactSiteConfiguration.Configuration.SetReuseJavaScriptEngines(true);

            ReactSiteConfiguration.Configuration.SetUseDebugReact(ReactSettingsProvider.Current.UseDebugReactScript).SetLoadBabel(true);
		}
	}
}