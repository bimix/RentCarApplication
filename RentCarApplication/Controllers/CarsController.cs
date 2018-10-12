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
            using(BookCarDBEntities entities = new BookCarDBEntities ())
            {
                return entities.Cars.ToList();
            }
        }



        public Car Get(int id)
        {
            using (BookCarDBEntities entities = new BookCarDBEntities())
            {
                return entities.Cars.FirstOrDefault(c => c.Id == id);
            }
        }


        public void Post([FromBody] Car car)
        {
            using (BookCarDBEntities entities = new BookCarDBEntities())
            {
                entities.Cars.Add(car);
                entities.SaveChanges();
            }
        }
    }
}
