using Polling.Domain.Abstract;
using Polling.Domain.Entities;
using Polling.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Polling.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IPollRepository repository;

        public HomeController(IPollRepository pollRepository)
        {
            repository = pollRepository;
        }

        public ActionResult Index()
        {
            PollHomeViewModel viewModel = new PollHomeViewModel(repository, "lunch", "", 1);

            return View(viewModel);
        }

        // given a selected category, return a list of
        // all the questions for the category
        public PartialViewResult GetCategoryPolls(int categoryId = 0)
        {
            IEnumerable<Poll> polls = repository.Polls
                .Where(x => x.CategoryID == categoryId)
                .OrderBy(x => x.PubDate);

            return PartialView("_CategoryPolls", polls);
        }

        // given a selected poll, return the
        // details of the poll
        public PartialViewResult GetPoll(int pollId)
        {
            Poll poll = repository.Poll(pollId);

            return PartialView("_PollDetails", poll);
        }
    }
}