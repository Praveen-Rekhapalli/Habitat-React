using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.FAQ.Controllers
{
    using System.Web.Mvc;
    using Sitecore.Foundation.Alerts;
    using Sitecore.Foundation.Alerts.Extensions;
    using Sitecore.Foundation.Alerts.Models;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Mvc.Presentation;
    using Repositories;
    using Foundation.ReactJs.Extensions;

    public class FaqController : Controller
    {
        private readonly IFaqRepo _faqRepository;

        public FaqController() : this(new FaqRepo())
        {
        }

        public FaqController(IFaqRepo faqRepository)
        {
            this._faqRepository = faqRepository;
        }

        public ActionResult FaqAccordionReact()
        {
            var renderingItem = RenderingContext.Current.Rendering.Item;

            if (!renderingItem?.IsDerived(Templates.FaqGroup.ID) ?? true)
            {
                return Context.PageMode.IsExperienceEditor ? this.InfoMessage(new InfoMessage(AlertTexts.InvalidDataSourceTemplateFriendlyMessage, InfoMessage.MessageType.Warning)) : null;
            }

            var model = this._faqRepository.GetFaqAccordion(renderingItem);

            return this.React("FaqAccordionReact", model);
        }
    }
}