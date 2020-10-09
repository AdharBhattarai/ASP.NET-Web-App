using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResturantWebApp.Models;

namespace ResturantWebApp.Controllers
{
    public class UserController : Controller
    {

        [HttpGet]
        public IActionResult Registration()
        {
            User roleUser = DAL.GetUserForCookie(Request.Cookies["user"]);
            if (roleUser != null)
            {
                TempData["Error"] = "You are already logged in.";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Reference Taken From Professor Holmes for password hashing and salt.
        public IActionResult Registration([Bind("FirstName, LastName, Email, Password, Phone")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Salt = Hash.GenerateSalt();
                user.Password = Hash.HashIt(user.Password, user.Salt);
                DAL.UserAdd(user);

                return RedirectToAction("Index", "Home");
            }

            else
            {
                NotFound();
            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            User usr = DAL.UserGet(userName, password);
            if (usr != null)
            {
                // success
                Response.Cookies.Append("user", DAL.GetCookie(usr));

                TempData["Success"] = "Successfuly logged in.";

            }
            else
            {
                // failed
            }

            return RedirectToAction("Index", "Menu");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("user");
            return RedirectToAction("Index", "Home");
        }
    }
}
