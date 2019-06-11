using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Hub")]
    public class ValuesController : ApiController
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        public static IUnitOfWork unitOfWork;

        public ValuesController(IUnitOfWork uw)
        {
            unitOfWork = uw;
        }


        [HttpGet, Route("getHub")]
        public IHttpActionResult GetHub()
        {
            return Ok();
        }
        
    }
}
