using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Karta")]
    public class KartaController : ApiController
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        IUnitOfWork unitOfWork;
        
        public KartaController(IUnitOfWork uw)
        {
            unitOfWork = uw;
        }

        [HttpGet, Route("getTipKarte")]
        //[Authorize(Roles = "Admin")]
        public IHttpActionResult GetTipKarte()
        {
            /*if (User.Identity.IsAuthenticated)
            {
                //User.IsInRole()
            }*/
            /*List<string> a = unitOfWork.AddressRepository.GetAll().ToList();
            return Ok(a);*/
            List<Stavka> a = unitOfWork.StavkaRepository.GetAll().ToList();
            return Ok(a);
        }
    }
}
