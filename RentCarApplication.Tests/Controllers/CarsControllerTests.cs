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

            Assert.AreEqual(6, cars.Count());
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

            var response = controller.GetbyId(5);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var car = response.Content.ReadAsAsync<Car>().Result;
            Assert.AreEqual(5, car.Id);
        }


        [TestMethod()]
        public void GettingaCarWithUnknownID() //This one is failing 
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

            var response = controller.GetbyId(19);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            var car = response.Content.ReadAsAsync<Car>().Result;
            Assert.AreNotEqual(19, car.Id);
        }

        

        
        [TestMethod]
        public void DeleteMethodTest()
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
                Request = new HttpRequestMessage(HttpMethod.Delete, "http://localhost:6664/api/cars")
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
            

            var response = controller.Delete(22);
            //var response2 = controller.GetbyId(2);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode); // if it finds a car with that id then it deletes and compares status code which is 
                                                                     //OK with the returned status code which should be OK since it has deleted it succesfully
            
            //var car2 = response.Content.ReadAsAsync<Car>().Result;
            //var car = response2.Content.ReadAsAsync<Car>().Result;
            //Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            //Assert.AreEqual(HttpStatusCode.NotFound , response2.StatusCode);

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
                Id = 15,
                model = "trhd",
                make = "rtd",
                location = "dddlocation",
                price = "4400",
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
            HttpResponseMessage actionResult = controller.GetbyId(57);

            //Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

            Assert.AreEqual(HttpStatusCode.NotFound, actionResult.StatusCode);
        }




        [TestMethod] //Another way of testing the NotFound exceptions
        [ExpectedException(typeof(HttpResponseException))]
        public void Controller_throws()
        {
            try
            {
                var sut = new CarsController();
                sut.GetbyId(19);
            }
            catch(HttpResponseException ex)
            {
                Assert.AreEqual(ex.Response.StatusCode,
                    HttpStatusCode.BadRequest);
                throw;
            }
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