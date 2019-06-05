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

        /*[HttpGet, Route("getTipKarte")]
        //[Authorize(Roles = "Admin")]
        public IHttpActionResult GetTipKarte()
        {
            /*if (User.Identity.IsAuthenticated)
            {
                //User.IsInRole()
            }*/
            /*List<string> a = unitOfWork.AddressRepository.GetAll().ToList();
            return Ok(a);*/
            /*List<Stavka> a = unitOfWork.StavkaRepository.GetAll().ToList();
            return Ok(a);
        }*/


        [Route("getCene")]
        public IHttpActionResult GetCene()
        {
            Cenovnik cenovnik = unitOfWork.CenovnikRepository.GetAll().Where(x => DateTime.Compare(x.OD, DateTime.Now) < 0 && DateTime.Compare(x.DO, DateTime.Now) > 0).FirstOrDefault();

            List<Stavka> stavka = unitOfWork.StavkaRepository.GetAll().ToList();
            List<CenovnikStavka> cenovnikStavka = unitOfWork.CenovnikStavkaRepository.GetAll().Where(x => x.IDCenovnika == cenovnik.ID).ToList();

            string json = "[";
            int c = 0;
            foreach (CenovnikStavka p in cenovnikStavka)
            {
                json += "{\"type\": \"" + stavka.Where(i => i.ID == p.IDSt5avka).FirstOrDefault().TipKarte.ToString() + "\",";
                json += "\"price\": \"" + cenovnikStavka.Where(i => i.ID == p.ID).FirstOrDefault().Cena + "\"},";
                c++;
            }
            json = json.Remove(json.Length - 1, 1);
            json += "]";
            
            return Ok(json);
        }

        /*[Route("getCoefficient")]
        public IHttpActionResult GetCoefficient()
        {
            Coefficients coefficients = unitOfWork.CoefficientRepository.GetAll().FirstOrDefault();
            return Ok(coefficients);
        }*/
    }
}
