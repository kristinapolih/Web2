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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cenovnik != null)
            {
                try
                {

                    Cenovnik c = new Cenovnik();
                    c.OD = DateTime.Parse(cenovnik.OdDatuma);
                    c.DO = DateTime.Parse(cenovnik.DoDatuma);
                    c.Stamp = DateTime.Now;

                    unitOfWork.CenovnikRepository.Add(c);
                    unitOfWork.Complete();

                    c = unitOfWork.CenovnikRepository.Find(x => DateTime.Compare(x.OD, c.OD) == 0 && DateTime.Compare(x.DO, c.DO) == 0).ToList()[0];

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
                catch (Exception)
                {
                    return NotFound();
                }
            }
            else
            {
                ModelState.AddModelError("", "Cenovnik not found!");
                return BadRequest(ModelState);
            }
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

        [HttpGet, Route("getCenovnikIzmena")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetCenovnikIzmena(int id)
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
            ch.Stamp = c.Stamp.ToString();

            if (DateTime.Compare(c.DO, DateTime.Now) >= 0)
                ch.Menja = true;

            return Ok(ch);
        }

        [HttpPost, Route("izmeniCenovnik")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult IzmeniCenovnik(CenovnikHelp cenovnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cenovnik != null)
            {
                try
                {
                    Cenovnik c = unitOfWork.CenovnikRepository.Get(cenovnik.ID);

                    if (String.Compare(cenovnik.Stamp, c.Stamp.ToString()) == 0)
                    {
                        if (DateTime.Compare(c.DO, DateTime.Parse(cenovnik.DoDatuma)) < 0)
                        {
                            c.DO = DateTime.Parse(cenovnik.DoDatuma);
                            c.Stamp = DateTime.Now;

                            unitOfWork.CenovnikRepository.Update(c);
                            unitOfWork.Complete();

                            return Ok("Uspešno ste izmenili cenovnik....");
                        }
                        else
                        {
                            return Ok("Ne možete staviti manji datum do kojeg cenovnik važi....");
                        }
                    }
                    else
                    {
                        return Ok("Drugi admin je vec menjao ovaj cenovnik, osvežite stranicu....");
                    }
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            else
            {
                ModelState.AddModelError("", "Cenovnik not found!");
                return BadRequest(ModelState);
            }
        }
    }
}
