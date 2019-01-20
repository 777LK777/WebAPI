using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SendController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SomeResult()
        {
            MsgData messageData = new MsgData();
            return View(messageData);
        }

        
        [HttpPost]
        public ActionResult SomeResult([FromForm] MsgData messageData)
        {           

            using (var client = new WebClient())
            {
                var parameters = new NameValueCollection
                {
                    { "token", messageData.AppToken },
                    { "user", messageData.UserKey },
                    { "message", messageData.Message }
                };

                client.UploadValues("https://api.pushover.net/1/messages.json", parameters);
            }           

            return Redirect("~/");            
        }               
    }
}