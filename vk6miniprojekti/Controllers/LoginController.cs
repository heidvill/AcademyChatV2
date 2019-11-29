using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vk6miniprojekti.Models;

namespace vk6miniprojekti.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        //tämän metodin pitää palauttaa message view + tuo viewBagissa ViewBag.NickName:n
        public IActionResult Index(LoginDetails log)
        {
            try
            {
                AcademyChatContext db = new AcademyChatContext();

                //var hashedPw = log.Password.GetHashCode(); //hashia ei käytetty loginiin vielä koska db:n hash ei ole sama kuin GetHashCode()
                Person user;
                if (ModelState.IsValid)
                {
                    //hakee rivin db:stä jossa nickname ja password on samat kuin käyttäjän syöttämät ja palauttaa null jos ei matchaa
                    user = (from u in db.Person
                            where u.NickName == log.Name && u.Password == log.Password
                            select u).FirstOrDefault();

                    if (user != null)
                    {
                        ViewBag.NickName = user.NickName;
                        HttpContext.Session.SetString("Nickname", user.NickName);
                        return RedirectToAction("Index", "Message");
                    }
                }
                return Content("username of password incorrect");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Content("login error");
            }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Nickname");
            return RedirectToAction("index", "message");
        }
    }

}