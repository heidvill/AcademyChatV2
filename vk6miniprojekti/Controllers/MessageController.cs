using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vk6miniprojekti.Models;

namespace vk6miniprojekti.Controllers
{
    public class MessageController : Controller
    {
        AcademyChatContext db = new AcademyChatContext();
        public IActionResult Index()
        {

            var msg = (from m in db.Message
                       where m.PrivateMessage ==false
                       orderby m.SendTime
                       select m).ToList();
            foreach (var item in msg)
            {
                db.Entry(item).Reference(p => p.FromPerson).Load();
            }
            return View(msg);
        }
        public IActionResult Messages()
        {
            return View();
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Message m)
        {
            m.SendTime = DateTime.Now;
            db.Message.Add(m);
            db.SaveChanges();

            var msg = (from mes in db.Message
                       where mes.PrivateMessage == false
                       orderby mes.SendTime descending
                       select mes).ToList();
            return View("index", msg);
        }

        public IActionResult Search(int id = 3)
        {

            var haeviestit = from v in db.Message
                             where v.FromPersonId == id
                             select v;

            var person = (from n in db.Person
                        where n.PersonId == id
                        select n).FirstOrDefault();

            ViewBag.Nimi = person.NickName;

            return View(haeviestit);
        }
    }
}