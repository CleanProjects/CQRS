using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class LogoutController : BaseController
    {
        public IActionResult Index()
        {
            LogoutUser();
            return RedirectToAction("Index", "Posts"); 
        }
    }
}