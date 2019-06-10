using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    [RoutePrefix("api/Cenovnik")]
    public class CenovnikController : ApiController
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        IUnitOfWork unitOfWork;

        public CenovnikController(IUnitOfWork uw)
        {
            unitOfWork = uw;
        }

        [HttpPost, Route("dodajCenovnik")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DodajCenovnik(CenovnikHelp cenovnik)
        {
            Cenovnik c = new Cenovnik();
            c.OD = DateTime.Parse( cenovnik.OdDatuma);
            c.DO = DateTime.Parse(cenovnik.DoDatuma);

            unitOfWork.CenovnikRepository.Add(c);
            unitOfWork.Complete();

            c = unitOfWork.CenovnikRepository.Find(x => x.OD.ToString("dd-MMMM-yyyy") == cenovnik.OdDatuma && x.DO.ToString("dd-MMMM-yyyy") == cenovnik.DoDatuma).ToList()[0];

            CenovnikStavka cs = new CenovnikStavka();
            cs.IDCenovnika = c.ID;
            cs.IDKoeficijent = unitOfWork.KoeficijentRepository.Get(1).ID;
            cs.IDSt5avka = 1;
            cs.Cena = cenovnik.VremenskaCena;
            unitOfWork.CenovnikStavkaRepository.Add(cs);
            unitOfWork.Complete();

            cs = new CenovnikStavka();
            cs.IDCenovnika = c.ID;
            cs.IDKoeficijent = unitOfWork.KoeficijentRepository.Get(1).ID;
            cs.IDSt5avka = 2;
            cs.Cena = cenovnik.DnevnaCena;
            unitOfWork.CenovnikStavkaRepository.Add(cs);
            unitOfWork.Complete();

            cs = new CenovnikStavka();
            cs.IDCenovnika = c.ID;
            cs.IDKoeficijent = unitOfWork.KoeficijentRepository.Get(1).ID;
            cs.IDSt5avka = 3;
            cs.Cena = cenovnik.MesecnaCena;
            unitOfWork.CenovnikStavkaRepository.Add(cs);
            unitOfWork.Complete();

            cs = new CenovnikStavka();
            cs.IDCenovnika = c.ID;
            cs.IDKoeficijent = unitOfWork.KoeficijentRepository.Get(1).ID;
            cs.IDSt5avka = 4;
            cs.Cena = cenovnik.GodisnjaCena;
            unitOfWork.CenovnikStavkaRepository.Add(cs);
            unitOfWork.Complete();

            return Ok("Uspešno ste dodali nov cenovnik....");
        }

        [HttpGet, Route("getCenovnike")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetCenovnike()
        {
            List<Cenovnik> cenovnici = unitOfWork.CenovnikRepository.GetAll().ToList();

            List<CenovnikHelp> ret = new List<CenovnikHelp>(); 
            foreach (Cenovnik c in cenovnici)
            {
                ret.Add(new CenovnikHelp() { ID = c.ID, OdDatuma = c.OD.ToString("yyyy-MM-dd"), DoDatuma = c.DO.ToString("yyyy-MM-dd") });
            }

            return Ok(ret);
        }

        [HttpGet, Route("getCenovnik")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetCenovnik(int id)
        {
            Cenovnik c = unitOfWork.CenovnikRepository.Get(id);
            List<CenovnikStavka> listaCenovnika = unitOfWork.CenovnikStavkaRepository.Find(x => x.IDCenovnika == id).ToList();

            CenovnikHelp ch = new CenovnikHelp();
            ch.OdDatuma = c.OD.ToString("yyyy-MM-dd");
            ch.DoDatuma = c.DO.ToString("yyyy-MM-dd");
            ch.VremenskaCena = listaCenovnika.Where(x => x.IDSt5avka == 1 && x.IDCenovnika == c.ID).FirstOrDefault().Cena;
            ch.DnevnaCena = listaCenovnika.Where(x => x.IDSt5avka == 2 && x.IDCenovnika == c.ID).FirstOrDefault().Cena;
            ch.MesecnaCena = listaCenovnika.Where(x => x.IDSt5avka == 3 && x.IDCenovnika == c.ID).FirstOrDefault().Cena;
            ch.GodisnjaCena = listaCenovnika.Where(x => x.IDSt5avka == 4 && x.IDCenovnika == c.ID).FirstOrDefault().Cena;
            
            return Ok(ch);
        }
    }
}
