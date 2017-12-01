using System;

namespace Sitecore.Foundation.ReactJs.Models
{
    public class ReactSettings : IReactSettings
    {
        public string BundleName { get; set; }

        public bool EnableClientside { get; set; }

        public bool UseDebugReactScript{ get; set; }

    }
}