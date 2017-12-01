
namespace Sitecore.Foundation.ReactJs.Pipelines.GetPageRendering
{
    using React;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Foundation.ReactJs.Repositories;
    using Sitecore.Mvc.Pipelines.Response.GetPageRendering;
    using Sitecore.Mvc.Presentation;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Optimization;
    using System.Web.Optimization.React;
    using System;
    using Providers;

    public class RegisterJsxFiles : GetPageRenderingProcessor
    {
        public override void Process(GetPageRenderingArgs args)
        {
            this.AddRenderingAssets(args.PageContext.PageDefinition.Renderings);

            var bundleName = ReactSettingsProvider.Current.BundleName;

            if(string.IsNullOrWhiteSpace(bundleName))
            {
                bundleName = "~/bundles/react";
            }

            var bundle = new BabelBundle(bundleName);

            foreach(var jsxFile in JsxRepository.Current.Items)
            {
                bundle.Include(jsxFile);

                if(!ReactSiteConfiguration.Configuration.Scripts.Any(s=>s.Equals(jsxFile)))
                {
                    ReactSiteConfiguration.Configuration.AddScript(jsxFile);
                }
            }

            BundleTable.Bundles.Add(bundle);
        }

        private void AddRenderingAssets(IEnumerable<Rendering> renderings)
        {
            foreach(var rendering in renderings)
            {
                var renderingItem = this.GetRenderingItem(rendering);
                if(renderingItem == null)
                {
                    return;
                }
                if(renderingItem.TemplateID == Templates.JsxRendering.ID)
                {
                    this.AddScriptAssetsFromRendering(renderingItem);
                }

                if(renderingItem.TemplateID == Templates.JsxViewRendering.ID)
                {
                    this.AddScriptAssetsFromViewRendering(renderingItem);
                }
              
            }
        }

        private void AddScriptAssetsFromViewRendering(Item renderingItem)
        {
            var jsxFile = renderingItem[Templates.JsxViewRendering.Fields.Path];
            if (!string.IsNullOrWhiteSpace(jsxFile))
            {
                JsxRepository.Current.AddScript(jsxFile, renderingItem.ID);
            }
        }

        private void AddScriptAssetsFromRendering(Item renderingItem)
        {
            var jsxFile = renderingItem[Templates.JsxRendering.Fields.JsxFile];
            if(!string.IsNullOrWhiteSpace(jsxFile))
            {
                JsxRepository.Current.AddScript(jsxFile, renderingItem.ID);
            }
        }

        private Item GetRenderingItem(Rendering rendering)
        {
            if (rendering.RenderingItem == null)
            {
                Log.Warn($"rendering.RenderingItem is null for {rendering.RenderingItemPath}", this);                
                return null;
            }

            return rendering.RenderingItem.InnerItem;
        }
    }
}