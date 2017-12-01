namespace Sitecore.Foundation.ReactJs.Models
{
    public interface IReactSettings
    {
        string BundleName { get; set; }
        bool  EnableClientside { get; set; }
        bool UseDebugReactScript { get; set; }
    }
}