using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarDataAccess;

namespace RentCarApplication.Controllers
{
    public class CarsController : ApiController
    {
        public IEnumerable<Car> Get()
        {
            using (BookCarDBEntities entities = new BookCarDBEntities())
            {
                return entities.Cars.ToList();
            }
        }

        

        public HttpResponseMessage GetbyId(int id)
        {
            using (BookCarDBEntities entities = new BookCarDBEntities())
            {
                var entity = entities.Cars.FirstOrDefault(c => c.Id == id);

                if (entities != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Car with id = " + id.ToString() + "was not found");
                }
            }
        }


        public HttpResponseMessage Post([FromBody] Car car)
        {
            try
            {

                using (BookCarDBEntities entities = new BookCarDBEntities())
                {
                    entities.Cars.Add(car);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, car);
                    message.Headers.Location = new Uri(Request.RequestUri + car.Id.ToString());
                    return message;
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {

                using (BookCarDBEntities entities = new BookCarDBEntities())
                {
                    var entity = entities.Cars.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Car with id = " + id.ToString() + "doesnt exists");
                    }
                    else
                    {
                        entities.Cars.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Car with id" + id + "has been deleted succesfully");
                    }
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        public HttpResponseMessage Put(int id, [FromBody] Car car)
        {
            using (BookCarDBEntities entities = new BookCarDBEntities())
            {
                try
                {
                    var entity = entities.Cars.FirstOrDefault(e => e.Id == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Car with id =" + id.ToString() + "was not found!");

                    }
                    else
                    {
                        entity.model = car.model;
                        entity.make = car.make;
                        entity.location = car.location;
                        entity.avaStart = car.avaStart;
                        entity.avaEnd = car.avaEnd;
                        entity.price = car.price;

                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }




            }

        }
    }
}
