using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polling.Domain.Entities
{
    public class PollTag
    {
        [Key, Column(Order = 0)]
        public int PollID { get; set; }
        [Key, Column(Order = 1)]
        public int TagID { get; set; }

        public virtual Poll Poll { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
