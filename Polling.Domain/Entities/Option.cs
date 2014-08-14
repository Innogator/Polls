using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polling.Domain.Entities
{
    public class Option
    {
        public Option()
        {
            this.Votes = new List<Vote>();
        }

        public int OptionID { get; set; }

        [StringLength(50, ErrorMessage = "Option cannot exceed 200 characters")]
        public string Text { get; set; }

        public int PollID { get; set; }
        public virtual Poll Poll { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
