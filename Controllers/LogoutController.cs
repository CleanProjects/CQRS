using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class LogoutController : BaseController
    {
        [HttpPost]
        public IActionResult Index()
        {
            LogoutUser();
            return RedirectToAction("Idex", "Posts"); 
        }
    }
}