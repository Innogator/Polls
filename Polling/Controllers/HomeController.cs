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

        public PartialViewResult Vote(int pollID, int option)
        {
            // default userId to 1 for now
            Vote vote = new Vote()
            {
                PollID = pollID,
                ChoiceID = option,
                DateVoted = DateTime.Now,
                UserID = 1
            };
            repository.AddVote(vote);

            // get the count of all votes for the poll
            Dictionary<string, int> voteDict = new Dictionary<string, int>();
            Poll poll = repository.Poll(pollID);

            foreach (var opt in poll.Options)
            {
                int count = repository.GetVotes(pollID, opt.OptionID);
                voteDict.Add(opt.Text, count);
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

        public PartialViewResult PollResults()
        {
            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart");
            chart.SetXAxis(new DotNet.Highcharts.Options.XAxis
            {
                Categories = new[] {
                        "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
                    }
            });
            chart.SetSeries(new DotNet.Highcharts.Options.Series
            {
                Data = new DotNet.Highcharts.Helpers.Data(new object[] {
                        29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4
                    })
            });
            //chart.SetTooltip(new DotNet.Highcharts.Options.Tooltip
            //{
            //    UseHTML = true,
            //    HeaderFormat = "<span>Those who voted</span><table>",
            //    PointFormat = "<tr><td style"
            //});
            return PartialView("_PollResults", chart);
        }
    }
}