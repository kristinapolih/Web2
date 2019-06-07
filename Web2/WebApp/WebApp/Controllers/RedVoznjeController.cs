using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;
using WebApp.Models;
using Newtonsoft.Json.Linq;
using WebApp.Helper;

namespace WebApp.Controllers
{
    [RoutePrefix("api/RedVoznje")]
    public class RedVoznjeController : ApiController
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        IUnitOfWork unitOfWork;

        public RedVoznjeController(IUnitOfWork uw)
        {
            /*HelperReader.Reader(uw, "1A");
            HelperReader.Reader(uw, "1B");
            HelperReader.Reader(uw, "4A");
            HelperReader.Reader(uw, "4B");
            //HelperReader.Reader(uw, "11A");
            //HelperReader.Reader(uw, "11B");
            HelperReader.Reader(uw, "12A");
            HelperReader.Reader(uw, "12B");
            HelperReader.Reader(uw, "13A");
            HelperReader.Reader(uw, "13B");
            HelperReader.Reader(uw, "16A");
            HelperReader.Reader(uw, "16B");
            HelperReader.Reader(uw, "22A");
            HelperReader.Reader(uw, "22B");
            HelperReader.Reader(uw, "32A");
            HelperReader.Reader(uw, "32B");
            HelperReader.Reader(uw, "41A");
            HelperReader.Reader(uw, "41B");
            HelperReader.Reader(uw, "51BA");
            HelperReader.Reader(uw, "51BB");*/
            unitOfWork = uw;
        }

        [HttpGet, Route("getDanasnjiDatum")]
        public IHttpActionResult GetDanasnjiDatum()
        {
            var datum = DateTime.Today.ToString("dddd, dd-MMMM-yyyy");
            return Ok(datum);
        }

        [HttpGet, Route("getTipRedVoznje")]
        public IHttpActionResult GetTipRedVoznje()
        {
            List<string> lista = new List<string>();
            lista.Add(TipVoznje.Gradski.ToString());
            lista.Add(TipVoznje.Prigradski.ToString());
            return Ok(lista);
        }

        [HttpGet, Route("getTipDana")]
        public IHttpActionResult GetTipDana()
        {
            List<string> lista = new List<string>();
            lista.Add(DanUNedelji.RadniDan.ToString());
            lista.Add(DanUNedelji.Subota.ToString());
            lista.Add(DanUNedelji.Nedelja.ToString());
            return Ok(lista);
        }

        [HttpGet, Route("getLinijeGradskeRadniDan")]
        public IHttpActionResult GetLinijeGradskeRadniDan()
        {
            List<string> lista = unitOfWork.LinijaRepository.GetAll().Where(x => x.TipVoznje == TipVoznje.Gradski && x.Datum == DanUNedelji.RadniDan).Select(x => x.Naziv).ToList();
            return Ok(lista);
        }

        [HttpGet, Route("getLinijeGradskeSubota")]
        public IHttpActionResult GetLinijeGradskeSubota()
        {
            List<string> lista = unitOfWork.LinijaRepository.GetAll().Where(x => x.TipVoznje == TipVoznje.Gradski && x.Datum == DanUNedelji.Subota).Select(x => x.Naziv).ToList();
            return Ok(lista);
        }

        [HttpGet, Route("getLinijeGradskeNedelja")]
        public IHttpActionResult GetLinijeGradskeNedelja()
        {
            List<string> lista = unitOfWork.LinijaRepository.GetAll().Where(x => x.TipVoznje == TipVoznje.Gradski && x.Datum == DanUNedelji.Nedelja).Select(x => x.Naziv).ToList();
            return Ok(lista);
        }

        [HttpGet, Route("getLinijePrigradskeRadniDan")]
        public IHttpActionResult GetLinijePrigradskeRadniDan()
        {
            List<string> lista = unitOfWork.LinijaRepository.GetAll().Where(x => x.TipVoznje == TipVoznje.Prigradski && x.Datum == DanUNedelji.RadniDan).Select(x => x.Naziv).ToList();
            return Ok(lista);
        }

        [HttpGet, Route("getLinijePrigradskeSubota")]
        public IHttpActionResult GetLinijePrigradskeSubota()
        {
            List<string> lista = unitOfWork.LinijaRepository.GetAll().Where(x => x.TipVoznje == TipVoznje.Prigradski && x.Datum == DanUNedelji.Subota).Select(x => x.Naziv).ToList();
            return Ok(lista);
        }

        [HttpGet, Route("getLinijePrigradskeNedelja")]
        public IHttpActionResult GetLinijePrigradskeNedelja()
        {
            List<string> lista = unitOfWork.LinijaRepository.GetAll().Where(x => x.TipVoznje == TipVoznje.Prigradski && x.Datum == DanUNedelji.RadniDan).Select(x => x.Naziv).ToList();
            return Ok(lista);
        }

        [HttpPost, Route("redVoznjeParametri")]
        public IHttpActionResult RedVoznjeParametri(object par)
        {
            JObject jObject = JObject.FromObject(par);
            JToken jUser = jObject;
            string redvoznje = (string)jUser["redvoznje"];
            string dan = (string)jUser["dan"];
            string linija = (string)jUser["linija"];

            DanUNedelji d = DanUNedelji.RadniDan;
            if (String.Compare(dan, d.ToString()) == 0)
                d = DanUNedelji.RadniDan;
            else if (String.Compare(dan, DanUNedelji.Subota.ToString()) == 0)
                d = DanUNedelji.Subota;
            else
                d = DanUNedelji.Nedelja;

            List<string> polasci = unitOfWork.LinijaRepository.Find(x => x.Naziv == linija && x.Datum == d).Select(x => x.Polasci).ToList();
            string[] vremena = polasci[0].Split('.');

            List<string> lista = new List<string>();
            foreach(var s in vremena)
            {
                lista.Add(s);
            }
            return Ok(lista);
        }

        [Route("getLinije")]
        public IHttpActionResult GetLinije()
        {
            List<Linije> routes = new List<Linije>();

            List<Linija> routesDb = unitOfWork.LinijaRepository.GetAll().ToList();

            foreach (Linija l in routesDb)
            {
                if(!routes.Exists(x => x.BrojRute == l.Broj))
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
                    stanice.Add(new StanicaHelp { X = stanica.X, Y = stanica.Y, Name = stanica.Naziv, IsStation = stanica.IsStanica, Adresa = unitOfWork.AdresaRepository.Get(stanica.IDAdresa) });
            }
            Linije linije = new Linije() { ID = l.ID, BrojRute = l.Naziv, TipRute = l.TipVoznje };
            linije.Stanice = stanice;
            return Ok(linije);
        }
    }
}
