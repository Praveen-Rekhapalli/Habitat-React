
namespace Sitecore.Feature.FAQ.Repositories
{
    using Sitecore.Data.Items;
    using Sitecore.Feature.FAQ.Models;

    public interface IFaqRepo
    {
        FaqItems GetFaqAccordion([NotNull] Item item);
    }
}