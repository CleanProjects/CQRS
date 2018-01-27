using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class LoginController : BaseController
    {
        public IActionResult Index()
        {
            ViewBag.IsLoggedIn = IsLoggedIn;
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] string user, string password)
        {
            if (CredentialsValid(user, password))
            {
                LoginUser(user, password);
                return RedirectToAction("Index", "Posts"); 
            }

            return RedirectToAction("Index", "Login"); 
        }
    }
}