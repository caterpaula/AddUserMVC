using System;
using System.Web.Mvc;
using UNiDAYSHomework.Models;
using UNiDAYSHomework.Data;
using UNiDAYSHomework.Utilities;

namespace UNiDAYSHomework.Controllers
{
    public class AddUserController : Controller
    {
        IUserRepository repository;

        public AddUserController(IUserRepository userRepository)
        {
            this.repository = userRepository;
        }

        // GET: /AddUser
        // GET: /AddUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AddUser/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            //check if User object passed in is valid - checks against data annotations in model
            if (!ModelState.IsValid) return View(user);

            var newUser = ProcessNewUser(user);

            TempData["SuccessMessage"] = "New user successfully added.";

            try
            {
                this.repository.CreateUser(newUser);
            }
            catch (Exception ex)
            {
                TempData["SuccessMessage"] = "An error occured and you were not added to the database. Please try again later.";
            }

            return RedirectToAction("Create");
            //returns model error messages if model is not valid
        }

        //use ProcessNewUser method to create db ready user object, including GUID + encrypted password
        private static User ProcessNewUser(User newUserRequest)
        {
            var newUser = new User
            {
                UserID = Guid.NewGuid(),
                EmailAddress = newUserRequest.EmailAddress,
                EncryptedPassword = EncryptionUtils.Md5Hash(newUserRequest.Password)
            };

            return newUser;
        }

    }
}