using Polling.Domain.Abstract;
using Polling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Polling.WebUI.Models
{
    public class PollListViewModel
    {
        public IEnumerable<Poll> Polls { get; set; }
        public string CurrentCategory { get; set; }
        public string SearchText { get; set; }

        public int PageSize = 2;

        public PollListViewModel(IPollRepository repository, string text, string type, int page)
        {
            Polls = repository.Polls;
            CurrentCategory = text;
        }
    }
}