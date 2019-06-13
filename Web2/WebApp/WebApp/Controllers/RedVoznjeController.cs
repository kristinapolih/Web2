using System;
using System.Collections.Generic;
using System.Linq;
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
            HelperReader.Reader(uw, "12A");
            HelperReader.Reader(uw, "12B");
            HelperReader.Reader(uw, "13A");
            HelperReader.Reader(uw, "13B");
            HelperReader.Reader(uw, "16A");
            HelperReader.Reader(uw, "16B");
            HelperReader.Reader(uw, "22A");
            HelperReader.Reader(uw, "22B");
            HelperReader.Reader(uw, "32B");
            HelperReader.Reader(uw, "41A");
            HelperReader.Reader(uw, "41B");
            HelperReader.Reader(uw, "51BA");*/
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
            List<string> lista = unitOfWork.LinijaRepository.GetAll().Where(x => x.TipVoznje == TipVoznje.Gradski && x.Datum == DanUNedelji.RadniDan && !x.Obrisana).Select(x => x.Naziv).ToList();
            return Ok(lista);
        }

        [HttpGet, Route("getLinijeGradskeSubota")]
        public IHttpActionResult GetLinijeGradskeSubota()
        {
            List<string> lista = unitOfWork.LinijaRepository.GetAll().Where(x => x.TipVoznje == TipVoznje.Gradski && x.Datum == DanUNedelji.Subota && !x.Obrisana).Select(x => x.Naziv).ToList();
            return Ok(lista);
        }

        [HttpGet, Route("getLinijeGradskeNedelja")]
        public IHttpActionResult GetLinijeGradskeNedelja()
        {
            List<string> lista = unitOfWork.LinijaRepository.GetAll().Where(x => x.TipVoznje == TipVoznje.Gradski && x.Datum == DanUNedelji.Nedelja && !x.Obrisana).Select(x => x.Naziv).ToList();
            return Ok(lista);
        }

        [HttpGet, Route("getLinijePrigradskeRadniDan")]
        public IHttpActionResult GetLinijePrigradskeRadniDan()
        {
            List<string> lista = unitOfWork.LinijaRepository.GetAll().Where(x => x.TipVoznje == TipVoznje.Prigradski && x.Datum == DanUNedelji.RadniDan && !x.Obrisana).Select(x => x.Naziv).ToList();
            return Ok(lista);
        }

        [HttpGet, Route("getLinijePrigradskeSubota")]
        public IHttpActionResult GetLinijePrigradskeSubota()
        {
            List<string> lista = unitOfWork.LinijaRepository.GetAll().Where(x => x.TipVoznje == TipVoznje.Prigradski && x.Datum == DanUNedelji.Subota && !x.Obrisana).Select(x => x.Naziv).ToList();
            return Ok(lista);
        }

        [HttpGet, Route("getLinijePrigradskeNedelja")]
        public IHttpActionResult GetLinijePrigradskeNedelja()
        {
            List<string> lista = unitOfWork.LinijaRepository.GetAll().Where(x => x.TipVoznje == TipVoznje.Prigradski && x.Datum == DanUNedelji.RadniDan && !x.Obrisana).Select(x => x.Naziv).ToList();
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

        

        //TODO atomske operacije

        [HttpGet, Route("getLinijeAdmin")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetLinijeAdmin()
        {
            List<Linije> routes = new List<Linije>();

            List<Linija> routesDb = unitOfWork.LinijaRepository.GetAll().Where(x => !x.Obrisana).ToList();

            foreach (Linija l in routesDb)
            {
                routes.Add(new Linije { ID = l.ID, ImeRute = l.Naziv, TipVoznje = l.TipVoznje.ToString(), Dan = l.Datum.ToString() });
            }

            routes = routes.OrderBy(x => x.TipVoznje) .ToList();

            return Ok(routes);
        }

        [HttpGet, Route("getLinijuAdmin")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetLinijuAdmin(int id)
        {
            try
            {
                Linija l = unitOfWork.LinijaRepository.Get(id);
                string polasci = l.Polasci;
                string[] vremena = polasci.Split('.');
                string ret = "";
                ret += l.Stamp.ToString() + '*';
                foreach (var s in vremena)
                {
                    ret += s + '\n';
                }
                return Ok(ret);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet, Route("GetLinijuListAdmin")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetLinijuListAdmin(int id)
        {
            try
            {
                Linija l = unitOfWork.LinijaRepository.Get(id);
                string[] vremena = l.Polasci.Split('.');

                List<string> lista = new List<string>();
                foreach (var s in vremena)
                {
                    lista.Add(s);
                }
                return Ok(lista);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpPost, Route("izmeniPolaskeAdmin")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult IzmeniPolaskeAdmin(PolasciHelp Polasci)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Polasci != null)
            {
                try
                {
                    Linija l = unitOfWork.LinijaRepository.Get(Polasci.ID);

                    if (String.Compare(l.Stamp.ToString(), Polasci.Stamp) == 0)
                    {
                        string[] vremena = Polasci.Polasci.Split('\n');
                        string s = "";
                        foreach (var ss in vremena)
                        {
                            s += ss + '.';
                        }
                        l.Polasci = s;
                        l.Stamp = DateTime.Now;

                        unitOfWork.LinijaRepository.Update(l);
                        unitOfWork.Complete();

                        return Ok("Polasci uspešno izmenjeni...");
                    }
                    else
                    {
                        return Ok("Drugi admin je vec menjao ovu liniju, osvežite stranicu....");
                    }
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            else
            {
                ModelState.AddModelError("", "Not found!");
                return BadRequest(ModelState);
            }
        }

        [HttpPost, Route("izmeniImeLinijeAdmin")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult IzmeniImeLinijeAdmin(PolasciHelp Polasci)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Polasci != null)
            {
                try
                {
                    Linija l = unitOfWork.LinijaRepository.Get(Polasci.ID);

                    if (String.Compare(l.Stamp.ToString(), Polasci.Stamp) == 0)
                    {
                        l.Naziv = Polasci.ImeRute;
                        l.Stamp = DateTime.Now;

                        unitOfWork.LinijaRepository.Update(l);
                        unitOfWork.Complete();

                        return Ok("Ime linije uspešno izmenjeno...");
                    }
                    else
                    {
                        return Ok("Drugi admin je vec menjao ovu liniju, osvežite stranicu....");
                    }
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            else
            {
                ModelState.AddModelError("", "Not found!");
                return BadRequest(ModelState);
            }
        }


        [HttpPost, Route("izmeniDanLinijeAdmin")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult IzmeniDanLinijeAdmin(PolasciHelp Polasci)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Polasci != null)
            {
                try
                {
                    Linija l = unitOfWork.LinijaRepository.Get(Polasci.ID);

                    if (String.Compare(l.Stamp.ToString(), Polasci.Stamp) == 0)
                    {

                        if (String.Compare(Polasci.Dan, DanUNedelji.RadniDan.ToString()) == 0)
                            l.Datum = DanUNedelji.RadniDan;
                        else if (String.Compare(Polasci.Dan, DanUNedelji.Subota.ToString()) == 0)
                            l.Datum = DanUNedelji.Subota;
                        else if (String.Compare(Polasci.Dan, DanUNedelji.Nedelja.ToString()) == 0)
                            l.Datum = DanUNedelji.Nedelja;

                        l.Stamp = DateTime.Now;

                        unitOfWork.LinijaRepository.Update(l);
                        unitOfWork.Complete();

                        return Ok("Dan linije uspešno izmenjen...");
                    }
                    else
                    {
                        return Ok("Drugi admin je vec menjao ovu liniju, osvežite stranicu....");
                    }
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            else
            {
                ModelState.AddModelError("", "Not found!");
                return BadRequest(ModelState);
            }
        }


        [HttpGet, Route("obrisiLinijuAdmin")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult ObrisiLinijuAdmin(int id)
        {
            try
            {
                Linija l = unitOfWork.LinijaRepository.Get(id);
                l.Obrisana = true;

                unitOfWork.LinijaRepository.Update(l);
                unitOfWork.Complete();

                return Ok("Linija uspesno obrisana...");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    


        [HttpPost, Route("dodajNovuLinijuAdmin")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DodajNovuLinijuAdmin(PolasciHelp Polasci)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Polasci != null)
            {
                try
                {
                    Linija l = new Linija();
                    l.Obrisana = false;
                    l.Naziv = Polasci.ImeRute;
                    l.Polasci = Polasci.Polasci;

                    if (String.Compare(Polasci.TipVoznje, TipVoznje.Gradski.ToString()) == 0)
                        l.TipVoznje = TipVoznje.Gradski;
                    else
                        l.TipVoznje = TipVoznje.Prigradski;

                    if (String.Compare(Polasci.Dan, DanUNedelji.RadniDan.ToString()) == 0)
                        l.Datum = DanUNedelji.RadniDan;
                    else if (String.Compare(Polasci.Dan, DanUNedelji.Subota.ToString()) == 0)
                        l.Datum = DanUNedelji.Subota;
                    else if (String.Compare(Polasci.Dan, DanUNedelji.Nedelja.ToString()) == 0)
                        l.Datum = DanUNedelji.Nedelja;

                    string[] pom = Polasci.ImeRute.Split(' ');
                    l.Broj = pom[0];

                    unitOfWork.LinijaRepository.Add(l);
                    unitOfWork.Complete();

                    return Ok("Nova linija je uspešno dodata...");
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            else
            {
                ModelState.AddModelError("", "Not found!");
                return BadRequest(ModelState);
            }
        }
    }
}
