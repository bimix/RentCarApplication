﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarDataAccess;
using RentCarApplication.Models;
using RentCarApplication.Controllers;
using System.Web.UI.WebControls;
using System.IO;

namespace RentCarApplication.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index(int? id)
        {

            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult ListCars()
        {
            string username = User.Identity.Name;
            var cars = new List<CarDataAccess.Car>();
            using (var db = new BookCarDBEntities())
            {
                cars = db.Cars.ToList();
            }
            return View(cars);
        }


        //public ActionResult Delete(int id)
        //{
        //    try
        //    {

        //        using (BookCarDBEntities entities = new BookCarDBEntities())
        //        {
        //            var entity = entities.Cars.FirstOrDefault(e => e.Id == id);
        //            if (entity == null)
        //            {
        //                return View("not found");
        //            }
        //            else
        //            {
        //                entities.Cars.Remove(entity);
        //                entities.SaveChanges();
        //                return View("success");
        //            }
        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        return View(ex + "error");
        //    }

        //}



        public ActionResult Payment(int id)
        {

            using (BookCarDBEntities entities = new BookCarDBEntities())
            {
                var entity = entities.Cars.FirstOrDefault(c => c.Id == id);

                if (entity != null)
                {
                    return View(entity);
                }
                else
                {
                    return View("Not Found");
                }
            }
        }
    }
}