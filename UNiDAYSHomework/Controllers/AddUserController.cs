using System;
using System.Web.Mvc;
using UNiDAYSHomework.Models;
using UNiDAYSHomework.Utilities;
using UNiDAYSHomework.Data;
using System.Web.Configuration;

namespace UNiDAYSHomework.Controllers
{
    public class AddUserController : Controller
    {
        private Gateway gateway;
        private UserRepository repository;

        public AddUserController()
        {
            this.gateway = new Gateway(WebConfigurationManager.ConnectionStrings["UNiDAYSDB"].ConnectionString);

            this.repository = new UserRepository(gateway);
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
            if (ModelState.IsValid)
            {
                var newUser = ProcessNewUser(user);
                this.repository.CreateUser(newUser);

                TempData["SuccessMessage"] = "New user successfully added.";

                return RedirectToAction("Create");
            }
            else
            {
                //returns model error messages if model is not valid
                return View(user);
            }
        }

        //use ProcessNewUser method to create db ready user object, including GUID + encrypted password
        public User ProcessNewUser(User newUserRequest)
        {
            User newUser = new User();
            newUser.UserID = Guid.NewGuid();
            newUser.EmailAddress = newUserRequest.EmailAddress;
            newUser.EncryptedPassword = EncryptionUtils.Md5Hash(newUserRequest.Password);

            return newUser;
        }


        public void PersistUser(User newUser)
        {          
            repository.CreateUser(newUser);
        }
    }
}