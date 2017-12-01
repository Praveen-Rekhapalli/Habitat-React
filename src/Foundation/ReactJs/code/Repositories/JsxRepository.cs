
namespace Sitecore.Foundation.ReactJs.Repositories
{
    using Sitecore.Data;
    using System.Collections.Generic;
    public class JsxRepository
    {
        private JsxRepository()
        {
                
        }

        private static JsxRepository _current;

        private readonly List<string> items = new List<string>();

        private readonly List<ID> seenRenderings = new List<ID>();

        public static JsxRepository Current => _current ?? (_current = new JsxRepository());

        internal IEnumerable<string> Items => this.items;

        public void AddScript(string file, ID renderingId)
        {
            lock(this.items)
            {
                if(!this.seenRenderings.Contains(renderingId))
                {
                    this.seenRenderings.Add(renderingId);
                    this.items.Add(file);
                }
            }
        }


    }
}