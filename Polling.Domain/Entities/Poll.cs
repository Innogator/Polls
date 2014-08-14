using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Polling.Domain.Entities
{
    public class Poll
    {
        public Poll()
        {
            this.Options = new List<Option>();
            this.Comments = new List<Comment>();
            this.Votes = new List<Vote>();
            this.Author = new User();
        }

        [HiddenInput(DisplayValue = false)]
        public int PollID { get; set; }

        [StringLength(200, ErrorMessage = "Question cannot exceed 200 characters")]
        [Required]
        public string PollQuestion { get; set; }

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        [Required]
        public string Description { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMM dd, yyyy}")]
        public System.DateTime PubDate { get; set; }

        [StringLength(200, ErrorMessage = "UrlSlug cannot exceed 200 characters")]
        public string UrlSlug { get; set; }

        [StringLength(50, ErrorMessage = "Meta cannot exceed 200 characters")]
        public string Meta { get; set; }

        public Nullable<int> CategoryID { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Option> Options { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual User Author { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<PollTag> PollTags { get; set; }
    }
}
