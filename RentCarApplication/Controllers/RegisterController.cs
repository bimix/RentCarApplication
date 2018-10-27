using RentCarApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentCarApplication.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(System_Users obj)

        {
            if (ModelState.IsValid)
            {
                RegistrationEntities db = new RegistrationEntities();
                db.System_Users.Add(obj);
                db.SaveChanges();
            }
            return View(obj);
        }
    }
}