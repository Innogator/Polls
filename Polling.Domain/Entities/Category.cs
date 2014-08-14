using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polling.Domain.Entities
{
    public class Category
    {
        public Category()
        {
            this.Polls = new List<Poll>();
        }

        // just a simple change
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Poll> Polls { get; set; }
    }
}
