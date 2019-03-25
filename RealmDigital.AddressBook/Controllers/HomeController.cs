using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealmDigital.AddressBook.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //// As Login screen is not considering, creating default session with first UserId. 
            Infrastructure.ProjectSession.UserID = 1;
            return RedirectToAction("index","contact");
        }
    }
}