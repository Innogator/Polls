using Polling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Polling.WebUI.Models
{
    public class PollViewModel
    {
        public Poll Poll { get; set; }
        public IEnumerable<Option> Options { get; set; }
        public int SelectedCategoryId { get; set; } 
        public SelectList Categories { get; set; }            
    }
}