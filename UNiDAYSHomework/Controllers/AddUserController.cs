using System;
using System.Web.Mvc;
using System.Security.Cryptography;
using UNiDAYSHomework.Models;
using System.Text;
using System.Data.SqlClient;
using System.Web.Configuration;

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
                var newUser = GenerateNewUser(user);
                InsertUser(newUser);

                TempData["SuccessMessage"] = "New user successfully added.";

                return RedirectToAction("Create");
            } else {
                //returns model error messages if model is not valid
                return View(user);
            }
        }

        //use GenerateNewUser method to create db ready user object, including GUID + encrypted password
        public User GenerateNewUser(User newUserRequest)
        {
            User newUser = new User();
            newUser.UserID = Guid.NewGuid();
            newUser.EmailAddress = newUserRequest.EmailAddress;
            newUser.Password = Encrypt(newUserRequest.Password);

            return newUser;
        }

        //general MD5 encryption 
        public static string Encrypt(string stringToEncrypt)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = Encoding.UTF8.GetBytes(stringToEncrypt);
            bs = x.ComputeHash(bs);
            StringBuilder s = new StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }

        public void InsertUser(User newUser)
        {
            string query = "insert into Users (UserID, EmailAddress, Password) values (@UserID, @EmailAddress, @Password)";
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["UNiDAYSDB"].ConnectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = query;
            
            //parameterize values to prevent SQL injections
            cmd.Parameters.AddWithValue("@UserID", newUser.UserID);
            cmd.Parameters.AddWithValue("@EmailAddress", newUser.EmailAddress);
            cmd.Parameters.AddWithValue("@Password", newUser.Password);

            int res = cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}