using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polling.Domain.Entities
{
    public class User
    {
        public User()
        {
            this.Comments = new List<Comment>();
            this.Polls = new List<Poll>();
            this.Votes = new List<Vote>();
        }

        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Reputation { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Poll> Polls { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
