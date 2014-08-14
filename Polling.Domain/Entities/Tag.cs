using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polling.Domain.Entities
{
    public class Tag
    {
        public int TagID { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PollTag> PollTags { get; set; }
    }
}
