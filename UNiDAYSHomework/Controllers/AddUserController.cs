using System;
using System.Web.Mvc;
using UNiDAYSHomework.Models;
using UNiDAYSHomework.Utilities;
using UNiDAYSHomework.Data;

namespace UNiDAYSHomework.Controllers
{
    public class AddUserController : Controller
    {
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
                PersistUser(newUser);

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
            UserRepository repository = new UserRepository();

            repository.CreateUser(newUser);
        }
    }
}