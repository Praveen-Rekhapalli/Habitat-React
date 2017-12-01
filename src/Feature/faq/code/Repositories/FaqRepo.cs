

namespace Sitecore.Feature.FAQ.Repositories
{
    using Models;
    using Sitecore.Data.Items;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Web.UI.WebControls;

    public class FaqRepo : IFaqRepo
    {
        public FaqItems GetFaqAccordion([NotNull] Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            return new FaqItems
            {
                Items = this.GetFaqs(item)
            };
        }

        protected virtual IList<FaqItem> GetFaqs([NotNull] Item renderingItem)
        {
            if (renderingItem == null)
            {
                throw new ArgumentNullException(nameof(renderingItem));
            }

            var faqItems = new List<FaqItem>();
            var items = renderingItem.GetMultiListValueItems(Templates.FaqGroup.Fields.GroupMember).Where(i => i.IsDerived(Templates.Faq.ID));
            foreach (var item in items)
            {
                faqItems.Add(new FaqItem
                {
                    Id = item.ID.ToShortID().ToString(),
                    Question = FieldRenderer.Render(item, Templates.Faq.Fields.Question.ToString()),
                    Answer = FieldRenderer.Render(item, Templates.Faq.Fields.Answer.ToString())
                });
            }

            return faqItems;
        }
    }
}