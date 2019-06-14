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
            try
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
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpGet, Route("getStanice")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetStanice()
        {
            List<StanicaHelper> stationHelper = new List<StanicaHelper>();

            List<Stanica> stations = unitOfWork.StanicaRepository.GetAll().ToList();

            foreach (Stanica s in stations)
            {
                if (s.IsStanica)
                {
                    Adresa a = unitOfWork.AdresaRepository.Get(s.IDAdresa);
                    List<LinijaStanica> routeStations = unitOfWork.LinijaStanicaRepository.GetAll().Where(x => x.IDStanica == s.ID).ToList();

                    List<int> routesId = new List<int>();
                    List<string> routesName = new List<string>();
                    List<int> stationNumbers = new List<int>();
                    string routesNamee = "";

                    foreach (LinijaStanica ls in routeStations)
                    {
                        Linija l = unitOfWork.LinijaRepository.Get(ls.IDLinija);
                        routesName.Add(l.Broj);
                        routesId.Add(l.ID);
                        stationNumbers.Add(ls.BrojStanice);
                        routesNamee += "[" + l.Broj + "] ";
                    }

                    stationHelper.Add(new StanicaHelper()
                    {
                        X = s.X,
                        Y = s.Y,
                        Naziv = s.Naziv,
                        ID = s.ID,
                        IsStanica = s.IsStanica,
                        Adresa = a.Grad + ", " + a.Ulica + " " + a.Broj,
                        IDRute = routesId,
                        BrojRute = routesNamee,
                        BrojeviRuta = routesName,
                        BrojeviStanica = stationNumbers
                    });
                }
            }

            return Ok(stationHelper);
        }

        [HttpGet, Route("getRoutesAddStation")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult getRoutesAddStation()
        {
            List<Linije> routes = new List<Linije>();
            List<Linija> r = unitOfWork.LinijaRepository.GetAll().Where(x => x.Datum == DanUNedelji.RadniDan).ToList();

            foreach (Linija rr in r)
            {
                routes.Add(new Linije() { ID = rr.ID, BrojRute = rr.Broj });
            }
            return Ok(routes);
        }

        [HttpGet, Route("getNewRoutes")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult getNewRoutes()
        {
            List<Linije> routes = new List<Linije>();
            List<Linija> r = unitOfWork.LinijaRepository.GetAll().Where(x => x.Datum == DanUNedelji.RadniDan).ToList();

            foreach (Linija rr in r)
            {
                List<LinijaStanica> rs = unitOfWork.LinijaStanicaRepository.GetAll().Where(x => x.IDLinija == rr.ID).ToList();
                if (rs.Count == 0)
                {
                    routes.Add(new Linije() { ID = rr.ID,  BrojRute = rr.Broj });
                }
            }
            return Ok(routes);
        }


        [Route("saveStationChanges")]
        [HttpPost, Authorize(Roles = "Admin")]
        public IHttpActionResult saveStationChanges(StanicaHelper sh)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    Stanica station = unitOfWork.StanicaRepository.Find(u => u.ID == sh.ID).FirstOrDefault();
                    station.Naziv = sh.Naziv;
                    unitOfWork.StanicaRepository.Update(station);
                    unitOfWork.Complete();

                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("deleteStationFromRoute")]
        [HttpPost, Authorize(Roles = "Admin")]
        public IHttpActionResult deleteStationFromRoute(StanicaHelper sh)
        {
            int IdRoute = Int32.Parse(sh.BrojRute);
            LinijaStanica rs = unitOfWork.LinijaStanicaRepository.GetAll().Where(x => x.IDLinija == IdRoute && x.IDStanica == sh.ID).FirstOrDefault();
            unitOfWork.LinijaStanicaRepository.Remove(rs);
            unitOfWork.Complete();

            List<LinijaStanica> routeStations = unitOfWork.LinijaStanicaRepository.GetAll().Where(x => x.IDStanica == sh.ID).ToList();

            if (routeStations.Count == 0)
            {
                Stanica station = unitOfWork.StanicaRepository.Get(sh.ID);
                station.IsStanica = false;
                unitOfWork.StanicaRepository.Update(station);
                unitOfWork.Complete();
            }

            return Ok();
        }

        [Route("addStation")]
        [HttpPost, Authorize(Roles = "Admin")]
        public IHttpActionResult addStation(StanicaHelper sh)
        {
            string[] split = sh.Adresa.Split(',');
            Adresa address = new Adresa() { Grad = split[0], Ulica = split[1], Broj = split[2] };
            unitOfWork.AdresaRepository.Add(address);
            unitOfWork.Complete();

            address = unitOfWork.AdresaRepository.GetAll().Where(x => x.Grad == split[0] && x.Ulica == split[1] && x.Broj == split[2]).FirstOrDefault();
            Stanica station = new Stanica() { Naziv = sh.Naziv, X = sh.X, Y = sh.Y, IsStanica = true, IDAdresa = address.ID };
            unitOfWork.StanicaRepository.Add(station);
            unitOfWork.Complete();
            station = unitOfWork.StanicaRepository.GetAll().Where(x => x.IDAdresa == address.ID && x.X == sh.X && x.Y == sh.Y).FirstOrDefault();

            LinijaStanica routeStation = new LinijaStanica()
            {
                IDLinija = sh.IDRute[0],
                IDStanica = station.ID,
                BrojStanice = sh.BrojURuti
            };

            List<LinijaStanica> routeStations = unitOfWork.LinijaStanicaRepository.GetAll().Where(x => x.IDLinija == sh.IDRute[0] && x.BrojStanice > 0).ToList();
            routeStations = routeStations.OrderBy(x => x.BrojStanice).ToList();
            List<LinijaStanica> list = routeStations.Where(x => x.BrojStanice == routeStation.BrojStanice).ToList();

            int count = routeStation.BrojStanice;
            if (list.Count != 0)
            {
                foreach (LinijaStanica rs in routeStations)
                {
                    if (count == rs.BrojStanice)
                    {
                        LinijaStanica pom = unitOfWork.LinijaStanicaRepository.Get(rs.ID);
                        pom.BrojStanice++;
                        count++;
                        unitOfWork.LinijaStanicaRepository.Update(pom);
                        unitOfWork.Complete();
                    }
                }
            }

            unitOfWork.LinijaStanicaRepository.Add(routeStation);
            unitOfWork.Complete();
            return Ok();
        }

        [Route("addLines")]
        [HttpPost, Authorize(Roles = "Admin")]
        public IHttpActionResult addLines(AddLinijeHelp alh)
        {
            foreach (Koordinate dot in alh.tacke)
            {
                Stanica station = new Stanica() { IDAdresa = -1, X = dot.X, Y = dot.Y, Naziv = "", IsStanica = false };
                unitOfWork.StanicaRepository.Add(station);
                unitOfWork.Complete();
                station = unitOfWork.StanicaRepository.GetAll().Where(x => x.X == dot.X && x.Y == dot.Y).FirstOrDefault();
                unitOfWork.LinijaStanicaRepository.Add(new LinijaStanica() { IDStanica = station.ID, IDLinija = alh.ID, BrojStanice = 0 });
                unitOfWork.Complete();
            }
            return Ok();
        }

    }
}
