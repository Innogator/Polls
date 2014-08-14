using Polling.Domain.Abstract;
using Polling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Polling.Controllers
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
            List<Poll> polls = repository.Polls.ToList();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}