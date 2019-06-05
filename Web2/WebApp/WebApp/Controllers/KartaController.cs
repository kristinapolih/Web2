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
    [RoutePrefix("api/Karta")]
    public class KartaController : ApiController
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        IUnitOfWork unitOfWork;
        
        public KartaController(IUnitOfWork uw)
        {
            unitOfWork = uw;
        }


        [Route("getCene")]
        public IHttpActionResult GetCene()
        {
            Cenovnik cenovnik = unitOfWork.CenovnikRepository.GetAll().Where(x => DateTime.Compare(x.OD, DateTime.Now) < 0 && DateTime.Compare(x.DO, DateTime.Now) > 0).FirstOrDefault();

            List<Stavka> stavka = unitOfWork.StavkaRepository.GetAll().ToList();
            List<CenovnikStavka> cenovnikStavka = unitOfWork.CenovnikStavkaRepository.GetAll().Where(x => x.IDCenovnika == cenovnik.ID).ToList();

            List<string> list = new List<string>();
            foreach (CenovnikStavka p in cenovnikStavka)
            {
                list.Add(cenovnikStavka.Where(i => i.ID == p.ID).FirstOrDefault().Cena);
            }
            
            return Ok(list);
        }

        [Route("getKoeficijente")]
        public IHttpActionResult GetKoeficijente()
        {
            Koeficijent k = unitOfWork.KoeficijentRepository.GetAll().FirstOrDefault();
            return Ok(k);
        }


        [HttpPost, Route("kupiKartu")]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult KupiKartu(KupljenaKarta karta)
        {
            var userStore = new UserStore<ApplicationUser>(dbContext);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (User.Identity.IsAuthenticated)
            {
                Karta k = new Karta();
                k.odDatum = DateTime.Now;
                k.DoDatum = DateTime.Now;
                k.Cena = karta.Cena;

                if (karta.TipKarte == TipKarte.Vremenska)
                {
                    k.DoDatum = k.DoDatum.AddHours(1);
                    k.TipKarte = TipKarte.Vremenska;
                }
                else if (karta.TipKarte == TipKarte.Dnevna)
                {
                    k.DoDatum = k.DoDatum.AddDays(1);
                    k.TipKarte = TipKarte.Dnevna;
                }
                else if (karta.TipKarte == TipKarte.Mesecna)
                {
                    k.DoDatum = k.DoDatum.AddMonths(1);
                    k.TipKarte = TipKarte.Mesecna;
                }
                else if (karta.TipKarte == TipKarte.Godisnja)
                {
                    k.DoDatum = k.DoDatum.AddYears(1);
                    k.TipKarte = TipKarte.Godisnja;
                }

                string s = User.Identity.GetUserId();
                int passenger = unitOfWork.KorisnikRepository.GetAll().Where(x => x.IDUser == s).FirstOrDefault().ID;
                k.IDKorisnik = passenger;

                unitOfWork.KartaRepository.Add(k);
                unitOfWork.Complete();
                return Ok();
            }
            return Ok();
        }
    }
}
