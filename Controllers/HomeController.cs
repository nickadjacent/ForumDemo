using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ForumDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace ForumDemo.Controllers
{
    public class HomeController : Controller
    {

        private ForumContext db;

        // controller constructor overload
        public HomeController(ForumContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId != null)
            {
                return RedirectToAction("All", "Posts");
            }
            return View();
        }

        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "is taken");
                }
            }

            // will be false if custom error messages have been added
            if (ModelState.IsValid == false)
            {
                // to display error messages
                return View("Index");
            }

            // hash pw
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            newUser.Password = hasher.HashPassword(newUser, newUser.Password);

            newUser.CreatedAt = DateTime.Now;
            newUser.UpdatedAt = DateTime.Now;
            db.Users.Add(newUser);
            db.SaveChanges();

            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            return RedirectToAction("All", "Posts");
        }

        public IActionResult Login(LoginUser loginUser)
        {
            string genericErrMsg = "Invalid Email or Password";

            if (ModelState.IsValid == false)
            {
                // display validation error messages
                return View("Index");
            }

            User dbUser = db.Users.FirstOrDefault(user => user.Email == loginUser.LoginEmail);

            if (dbUser == null)
            {
                ModelState.AddModelError("LoginEmail", genericErrMsg);
            }
            // user found in db
            else
            {
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                PasswordVerificationResult result = hasher.VerifyHashedPassword(loginUser, dbUser.Password, loginUser.LoginPassword);

                if (result == 0)
                {
                    ModelState.AddModelError("LoginEmail", genericErrMsg);
                }
            }
            // this is true if any of our above manually added model errors happened: ModelState.AddModelError
            if (ModelState.IsValid == false)
            {
                // display custom validation error messages
                return View("Index");
            }

            // if none of the returns happened, good to go
            HttpContext.Session.SetInt32("UserId", dbUser.UserId);

            return RedirectToAction("All", "Posts");
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
