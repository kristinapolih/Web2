using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;
using WebApp.Models;
using Newtonsoft.Json.Linq;


namespace WebApp.Controllers
{
    [RoutePrefix("api/MrezaLinija")]
    public class MrezaLinijaController : ApiController
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        IUnitOfWork unitOfWork;

        public MrezaLinijaController(IUnitOfWork uw)
        {
            unitOfWork = uw;
        }

        [HttpGet, Route("getLinijeGradske")]
        public IHttpActionResult GetLinijeGradske()
        {
            List<Linije> routes = new List<Linije>();

            List<Linija> routesDb = unitOfWork.LinijaRepository.GetAll().Where(x => x.TipVoznje == TipVoznje.Gradski && !x.Obrisana).ToList();

            foreach (Linija l in routesDb)
            {
                if (!routes.Exists(x => x.BrojRute == l.Broj))
                    routes.Add(new Linije { ID = l.ID, BrojRute = l.Broj, ImeRute = l.Naziv, TipRute = l.TipVoznje });
            }

            return Ok(routes);
        }

        [HttpGet, Route("getLinijePrigradske")]
        public IHttpActionResult GetLinijePrigradske()
        {
            List<Linije> routes = new List<Linije>();

            List<Linija> routesDb = unitOfWork.LinijaRepository.GetAll().Where(x => x.TipVoznje == TipVoznje.Prigradski && !x.Obrisana).ToList();

            foreach (Linija l in routesDb)
            {
                if (!routes.Exists(x => x.BrojRute == l.Broj))
                    routes.Add(new Linije { ID = l.ID, BrojRute = l.Broj, ImeRute = l.Naziv, TipRute = l.TipVoznje });
            }

            return Ok(routes);
        }

        [HttpGet, Route("getLiniju")]
        public IHttpActionResult GetLiniju(int id)
        {
            Linija l = unitOfWork.LinijaRepository.Get(id);

            List<LinijaStanica> linijaStanica = unitOfWork.LinijaStanicaRepository.GetAll().Where(x => x.IDLinija == l.ID).ToList();
            List<StanicaHelp> stanice = new List<StanicaHelp>();

            foreach (LinijaStanica ls in linijaStanica)
            {
                Stanica stanica = unitOfWork.StanicaRepository.Get(ls.IDStanica);
                if (stanica != null)
                    stanice.Add(new StanicaHelp { X = stanica.X, Y = stanica.Y, Naziv = stanica.Naziv, IsStanica = stanica.IsStanica, Adresa = unitOfWork.AdresaRepository.Get(stanica.IDAdresa) });
            }
            Linije linije = new Linije() { ID = l.ID, BrojRute = l.Naziv, TipRute = l.TipVoznje };
            linije.Stanice = stanice;
            return Ok(linije);
        }
    }
}
