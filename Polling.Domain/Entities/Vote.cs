using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polling.Domain.Entities
{
    public class Vote
    {
        public int VoteID { get; set; }
        public int PollID { get; set; }
        public int ChoiceID { get; set; }
        public int UserID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMM dd, yyyy}")]
        public System.DateTime DateVoted { get; set; }

        public virtual Option Option { get; set; }
        public virtual Poll Poll { get; set; }
        public virtual User User { get; set; }
    }
}
