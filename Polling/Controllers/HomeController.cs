using Polling.Domain.Abstract;
using Polling.Domain.Entities;
using Polling.WebUI.CustomBinding;
using Polling.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public PartialViewResult Vote(int pollID, int option)
        {
            // default userId to 1 for now
            Vote vote = new Vote()
            {
                PollID = pollID,
                ChoiceID = option,
                DateVoted = DateTime.Now,
                UserID = 2
            };
            repository.AddVote(vote);

            // get the count of all votes for the poll
            Dictionary<string, int> voteDict = new Dictionary<string, int>();
            Poll poll = repository.Poll(pollID);

            foreach (var opt in poll.Options)
            {
                int count = repository.GetVotes(pollID, opt.OptionID);
                voteDict.Add(opt.Text.Replace("'", "\\\'"), count);
            }

            string[] keys = voteDict.OrderByDescending(x => x.Value).Select(x => x.Key).ToArray();
            int[] values = voteDict.OrderByDescending(x => x.Value).Select(x => x.Value).ToArray();
            // now we have the votes for each option
            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
            .SetTitle(new DotNet.Highcharts.Options.Title { Text = poll.PollQuestion })
            .SetXAxis(new DotNet.Highcharts.Options.XAxis
                {
                    Categories = keys
                })
            .SetSeries(new DotNet.Highcharts.Options.Series
                {
                    Type = DotNet.Highcharts.Enums.ChartTypes.Bar,
                    Name = "Votes",
                    Data = new DotNet.Highcharts.Helpers.Data(Array.ConvertAll(values, x => (object)x))
                });
            //chart.SetTooltip(new DotNet.Highcharts.Options.Tooltip
            //{
            //    UseHTML = true,
            //    HeaderFormat = "<span>Those who voted</span><table>",
            //    PointFormat = "<tr><td style"
            //});
            return PartialView("_PollResults", chart);
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

        public ActionResult CreatePoll()
        {
            Poll poll = new Poll();
            IEnumerable<Category> categories = repository.Categories.ToList();
            PollViewModel viewModel = new PollViewModel()
            {
                Poll = poll,
                Categories = new SelectList(categories, "CategoryID", "Name"),
                Options = new List<Option>() 
                { 
                    new Option(),
                    new Option(),
                    new Option(),
                    new Option()
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreatePoll([ModelBinder(typeof(CreatePollBinder))] PollViewModel model)
        {
            Poll poll = model.Poll;
            // add some values not populated from the form
            poll.PubDate = DateTime.Now;

            int id = repository.AddPoll(poll);
            
            List<Option> options = new List<Option>(model.Options.ToList().Count);
            // relate the options to the poll
            foreach (Option opt in model.Options)
            {
                Option o = opt;
                o.PollID = id;
                options.Add(o);
            }
            repository.AddOptions(options);

            return RedirectToAction("Index");
        }

        public PartialViewResult BlankOptionRow()
        {
            return PartialView("_OptionEditor", new Option());
        }
    }
}