using Polling.Domain.Abstract;
using Polling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Polling.WebUI.Models
{
    public class PollHomeViewModel
    {
        public IEnumerable<Poll> Polls { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string CurrentCategory { get; set; }
        public string SearchText { get; set; }

        public int PageSize = 2;

        public PollHomeViewModel(IPollRepository repository, string text, string type, int page)
        {
            Categories = repository.Categories.OrderBy(x => x.Name);
            Polls = repository.Polls;
            CurrentCategory = text;
        }
    }
}