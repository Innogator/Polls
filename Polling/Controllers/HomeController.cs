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
            PollListViewModel viewModel = new PollListViewModel(repository, "lunch", "", 1);

            return View(viewModel);
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