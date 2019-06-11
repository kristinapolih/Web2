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


        [HttpGet, Route("getCene")]
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

        [HttpGet, Route("getKoeficijente")]
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
                k.ODDatum = DateTime.Now;
                k.DoDatum = DateTime.Now;
                k.Cena = karta.Cena;

                if (karta.TipKarte == TipKarte.Vremenska)
                {
                    k.DoDatum = k.DoDatum.AddHours(1);
                    if (k.DoDatum.Day != k.ODDatum.Day)
                    {
                        string ss = k.ODDatum.Date.ToString();
                        string[] niz = ss.Split(' ');
                        niz[1] = "11:59:59 PM";
                        string time = niz[0] + " " + niz[1];
                        k.DoDatum = Convert.ToDateTime(time);
                    }
                    k.TipKarte = TipKarte.Vremenska;
                }
                else if (karta.TipKarte == TipKarte.Dnevna)
                {
                    k.DoDatum = k.DoDatum.AddDays(1);
                    if (k.DoDatum.Day != k.ODDatum.Day)
                    {
                        string ss = k.ODDatum.Date.ToString();
                        string[] niz = ss.Split(' ');
                        niz[1] = "11:59:59 PM";
                        string time = niz[0] + " " + niz[1];
                        k.DoDatum = Convert.ToDateTime(time);
                    }
                    k.TipKarte = TipKarte.Dnevna;
                }
                else if (karta.TipKarte == TipKarte.Mesecna)
                {
                    k.DoDatum = k.DoDatum.AddMonths(1);
                    if (k.DoDatum.Month != k.ODDatum.Month)
                    {
                        string ss = k.ODDatum.ToString();
                        string[] niz = ss.Split(' ');
                        string lastDay = GetLastDay(k.ODDatum.Month).ToString();
                        niz[1] = "11:59:59 PM";
                        string[] nizniz = niz[0].Split('-');
                        nizniz[0] = lastDay;
                        string wholeDate = nizniz[0] + "-" + nizniz[1] + "-" + nizniz[2] + " " + niz[1];
                        k.DoDatum = Convert.ToDateTime(wholeDate);
                    }
                    k.TipKarte = TipKarte.Mesecna;
                }
                else if (karta.TipKarte == TipKarte.Godisnja)
                {
                    k.DoDatum = k.DoDatum.AddYears(1);
                    if (k.ODDatum.Year != k.DoDatum.Year)
                    {
                        string ss = k.ODDatum.ToString();
                        string[] niz = ss.Split(' ');
                        string[] nizniz = niz[0].Split('-');
                        nizniz[0] = "31";
                        nizniz[1] = "Dec";
                        niz[1] = "11:59:59 PM";
                        string wholeDate = nizniz[0] + "-" + nizniz[1] + "-" + nizniz[2] + " " + niz[1];
                        k.DoDatum = Convert.ToDateTime(wholeDate);
                    }
                    k.TipKarte = TipKarte.Godisnja;
                }

                string s = User.Identity.GetUserId();
                int passenger = unitOfWork.KorisnikRepository.GetAll().Where(x => x.IDUser == s).FirstOrDefault().ID;
                k.IDKorisnik = passenger;

                unitOfWork.KartaRepository.Add(k);
                unitOfWork.Complete();
                return Ok("Uspesno ste kupili kartu...");
            }
            return Ok("Niste autentifikovani....");
        }

        [HttpGet, Route("getKarte")]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult GetKarte()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<KartaHelp> ticketHelps = new List<KartaHelp>();
                int passenger_id = unitOfWork.KorisnikRepository.GetAll().Where(x => x.IDUser == User.Identity.GetUserId()).FirstOrDefault().ID;
                List<Karta> tickets = unitOfWork.KartaRepository.GetAll().Where(x => x.IDKorisnik == passenger_id && !x.Obrisana).ToList();

                foreach (Karta k in tickets)
                {
                    if (k.TipKarte == TipKarte.Vremenska)
                    {
                        string s1 = k.ODDatum.ToString();
                        string[] niz = s1.Split(' ');
                        string s2 = k.DoDatum.ToString();
                        string[] niz1 = s2.Split(' ');
                        string time = niz[1] + " - " + niz1[1];
                        ticketHelps.Add(new KartaHelp { Id = k.ID, Price = k.Cena.ToString(), Type = k.TipKarte.ToString(), Date = time });
                    }
                    else
                    {
                        string time = k.ODDatum.Date.ToString("dd/MMMM/yyyy").Split(' ')[0] + " - " + k.DoDatum.Date.ToString("dd/MMMM/yyyy").Split(' ')[0];
                        ticketHelps.Add(new KartaHelp { Id = k.ID, Price = k.Cena.ToString(), Type = k.TipKarte.ToString(), Date = time });
                    }
                }

                return Ok(ticketHelps);
            }
            
            return Ok("Niste autentifikovani....");
        }

        [HttpPost, Route("obrisiKartu")]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult ObrisiKartu(KartaHelp ticketHelp)
        {
            if (User.Identity.IsAuthenticated)
            {
                Karta k = unitOfWork.KartaRepository.Get(ticketHelp.Id);
                k.Obrisana = true;
                unitOfWork.KartaRepository.Update(k);
                unitOfWork.Complete();

                return Ok();
            }
            return Ok("Niste autentifikovani....");
        }

        [HttpGet, Route("getKartu")]
        [Authorize(Roles = "Controller")]
        public IHttpActionResult GetKartu(int id)
        {
            List<string> poruka = new List<string>();
            if (User.Identity.IsAuthenticated)
            {
                Karta k = unitOfWork.KartaRepository.Find(x => x.ID == id).FirstOrDefault();
                if (k == null)
                {
                    poruka.Add("Karta sa ovim id-jem ne postoji....");
                    return Ok(poruka);
                }

                int result = DateTime.Compare(k.DoDatum, DateTime.Now);
                if (result > 0)
                {
                    poruka.Add("Karta je VALIDNA.");
                    poruka.Add("Od: " + k.ODDatum.ToString());
                    poruka.Add("Do: " + k.DoDatum.ToString());
                    return Ok(poruka);
                }
                else
                {
                    poruka.Add("Karta NIJE VALIDNA.");
                    return Ok(poruka);
                }
            }

            return Ok();
        }


        private int GetLastDay(int month)
        {
            int res;

            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                res = 31;
            }
            else if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                res = 30;
            }
            else
            {
                int year = DateTime.Now.Year;
                if (year % 4 == 0)
                {
                    res = 29;
                }
                else
                {
                    res = 28;
                }
            }

            return res;
        }
    }
}
