using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

using vk6miniprojekti.Models;


namespace vk7miniprojekti.Hubs
{
    public class ChatHub : Hub
    {

        public async Task SendMessage(string user, string message)
        {
            //messagen talletus db:hen tähän
            AcademyChatContext db = new AcademyChatContext();
            DateTime tempTime = DateTime.Now;
            await Clients.All.SendAsync("ReceiveMessage", user, message, tempTime);

            var sender = (from s in db.Person
                         where s.NickName == user
                         select s).FirstOrDefault();

            Message m = new Message();
            m.FromPerson = sender;
            m.Subject = "testisubject";
            m.MessageText = message;
            m.SendTime = tempTime;
            m.PrivateMessage = false;
            db.Message.Add(m);
            db.SaveChanges();

        }


    }
}
