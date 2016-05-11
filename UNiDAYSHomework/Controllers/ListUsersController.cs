using System.Collections.Generic;
using System.Web.Mvc;
using UNiDAYSHomework.Data;
using UNiDAYSHomework.Models;

namespace UNiDAYSHomework.Controllers
{
    public class ListUsersController : Controller
    {

        IUserRepository repository;

        public ListUsersController(IUserRepository userRepository)
        {
            this.repository = userRepository;
        }

        // GET: ListUsers
        public ActionResult Index()
        {
            var users = this.repository.ListAllUsers();

            return View(users);
        }
    }
}