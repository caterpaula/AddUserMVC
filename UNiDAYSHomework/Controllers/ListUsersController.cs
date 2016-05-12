using System.Collections.Generic;
using System.Web.Mvc;
using UNiDAYSHomework.Data;
using UNiDAYSHomework.Models;

namespace UNiDAYSHomework.Controllers
{
    public sealed class ListUsersController : Controller
    {

        IUserRepository repository;

        public ListUsersController(IUserRepository userRepository)
        {
            this.repository = userRepository;
        }

        // GET: ListUsers
        public ActionResult Index()
        {

            var queryResult = this.repository.ListAllUsers<List<User>>();

            if (queryResult.WasSuccessful)
            {

                if (queryResult.Results.Count > 0)
                {
                    return View(queryResult.Results);
                }

                TempData["Feedback"] = "No users currently listed.";
                return RedirectToAction("NoUsers");
            }

            TempData["Feedback"] = "Database cannot be reached at this time. Please try again later.";
            return RedirectToAction("NoUsers");
        }

        public ActionResult NoUsers()
        {
            return View();
        }
    }
}