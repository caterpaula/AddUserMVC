using System;
using System.Web.Mvc;
using UNiDAYSHomework.Models;
using UNiDAYSHomework.Utilities;
using System.Collections.Generic;

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
                InsertUser(newUser);

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


        public void InsertUser(User newUser)
        {
            string query =
                "insert into Users (UserID, EmailAddress, Password) values (@UserID, @EmailAddress, @Password)";

            //create a dictionary of paramers and their values to pass to ExecuteDbQuery method
            Dictionary<string, object> queryParameters = new Dictionary<string, object>()
            {
                { "@UserID", newUser.UserID},
                { "@EmailAddress", newUser.EmailAddress },
                { "@Password", newUser.EncryptedPassword }
            };

            DataAccessUtils.ExecuteDbQuery(query, queryParameters);
        }
    }
}