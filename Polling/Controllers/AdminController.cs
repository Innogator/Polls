using Polling.Domain.Abstract;
using Polling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Polling.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IPollRepository repository;

        public AdminController(IPollRepository pollRepository)
        {
            repository = pollRepository;
        }
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            repository.AddCategory(category);
            return View();
        }
	}
}