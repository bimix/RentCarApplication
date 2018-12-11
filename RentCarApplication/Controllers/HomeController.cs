using System;
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





        public ActionResult Delete(int id, Car car)
        {
            try
            {
                using (BookCarDBEntities db = new BookCarDBEntities())
                {

                    var carToDelete = db.Cars.FirstOrDefault(c => c.Id == id);
                    var book = CreateNewBooking(carToDelete);
                    db.Bookings.Add(book);



                    db.Cars.Remove(carToDelete);

                    db.SaveChanges();

                    return View(book);
                }

            }
            catch (Exception ex)
            {
                return View(ex + "error");
            }
        }

        private Booking CreateNewBooking(Car car)
        {
            var bookingCreated = new Booking
            {
                id = car.Id,
                model = car.model,
                make = car.make,
                price = car.price,
                location = car.location
            };

            return bookingCreated;
        }

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