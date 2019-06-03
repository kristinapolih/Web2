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

namespace WebApp.Controllers
{
    [RoutePrefix("api/RedVoznje")]
    public class RedVoznjeController : ApiController
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        IUnitOfWork unitOfWork;

        public RedVoznjeController(IUnitOfWork uw)
        {
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

        [HttpGet, Route("getLinije")]
        public IHttpActionResult GetLinije()
        {
            List<string> lista = unitOfWork.LinijaRepository.GetAll().Select(x => x.Naziv).ToList();
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

            //TODO
            List<string> lista = unitOfWork.LinijaRepository.GetAll().Select(x => x.Naziv).ToList();
            return Ok(lista);
        }
    }
}
