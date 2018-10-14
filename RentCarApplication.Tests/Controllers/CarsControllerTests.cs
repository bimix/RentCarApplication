using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentCarApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Web.Http;
using CarDataAccess;
using System.Web.Http.Hosting;
using System.Net;
using System.Web.Http.Results;
using System.Web.Http.Routing;

namespace RentCarApplication.Controllers.Tests
{
    [TestClass()]
    public class CarsControllerTests
    {
        //[TestMethod()]
        //public void GetTest()
        //{
        //    var controller = new CarsController();
        //    controller.Request = new HttpRequestMessage();
        //    controller.Configuration = new HttpConfiguration();

        //    var response = controller.Get(5);

        //    Car car;
        //    Assert.IsTrue(response.TryGetContentValue<Car>(out car));
        //    Assert.AreEqual("5", car.Id);
        //}



        [TestMethod()]
        public void GetAllCarsTest()
        {
            var controller = new CarsController();

            var cars = controller.Get();

            Assert.AreEqual(8, cars.Count());
        }




        [TestMethod()]
        public void GettingaCarWithKnownID()
        {
            var controller = new CarsController
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                }
            };

            var response = controller.Get(2);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var car = response.Content.ReadAsAsync<Car>().Result;
            Assert.AreEqual(2, car.Id);
        }


        [TestMethod()]
        public void GettingaCarWithUnknownID()
        {
            var controller = new CarsController()
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { {
                            HttpPropertyKeys.HttpConfigurationKey,
                            new HttpConfiguration() } }

                }
            };

            var response = controller.Get(111);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        }




        [TestMethod]
        public void PostTest()
        {
            var httpconfiguration = new HttpConfiguration();
            WebApiConfig.Register(httpconfiguration);
            var httpRouteData = new HttpRouteData(httpconfiguration.Routes["DefaultApi"],
                new HttpRouteValueDictionary
                {
                    { "controller", "Cars" }
                });
            var controller = new CarsController()
            {
                Request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:6664/api/cars/")
                {
                    Properties =
                    {
                        {
                            HttpPropertyKeys.HttpConfigurationKey,httpconfiguration
                        },
                        {
                            HttpPropertyKeys.HttpRouteDataKey, httpRouteData
                        },

                    }
                }
            };
            var response = controller.Post(new Car()
            {
                Id = 223,
                model = "Toyosdt",
                make = "calidfgba",
                location = "anylocation",
                price = "4000",
                avaEnd = null,
                avaStart = null
            });
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var addedCar = response.Content.ReadAsAsync<Car>().Result;

            Assert.AreEqual(string.Format("http://localhost:6664/api/cars/{0}",
                addedCar.Id),
                response.Headers.Location.ToString());

        }




        [TestMethod]
        public void GetCarNotFound()
        {
            var controller = new CarsController();
            HttpResponseMessage actionResult = controller.Get(2);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }



        [TestMethod]
        public void PutTest()
        {
            var controller = new CarsController()
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }

                }
            };

            var car = new Car() { Id = 1, model = "new model", make = "new make" };
            var response = controller.Put(car.Id, car);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var newCar = response.Content.ReadAsAsync<Car>().Result;
            Assert.AreEqual("new model", newCar.model);
            Assert.AreEqual("new make", newCar.make);

        }
    }
}