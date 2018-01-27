using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BlogApp.Controllers
{
    public class BaseController : Controller
    {
        private string adminUser = "gringo";
        private string adminPassword = "superpassword";

        public bool IsLoggedIn 
        { 
            get 
            {
                if (HttpContext.Session.GetString("auth") == EncodeToBase64($"{adminUser}{adminPassword}"))
                {
                    return true;
                }
                return false;
            }
        }

        public void LoginUser(string user, string password)
        {
            var encoded = EncodeToBase64($"{adminUser}{adminPassword}");
            HttpContext.Session.SetString("auth", encoded);
        }

        public void LogoutUser()
        {
            HttpContext.Session.Clear();
        }

        public bool CredentialsValid(string user, string password)
        {
            if (adminPassword == password && adminUser == user)
            {
                return true;
            }
            return false;
        }

        public static string EncodeToBase64(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

    }
}