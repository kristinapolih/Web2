using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Persistence;

namespace WebApp.Controllers
{
    public class StaniceController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Stanice
        public IQueryable<Stanica> GetStanicas()
        {
            return db.Stanicas;
        }

        // GET: api/Stanice/5
        [ResponseType(typeof(Stanica))]
        public IHttpActionResult GetStanica(int id)
        {
            Stanica stanica = db.Stanicas.Find(id);
            if (stanica == null)
            {
                return NotFound();
            }

            return Ok(stanica);
        }

        // PUT: api/Stanice/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStanica(int id, Stanica stanica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stanica.ID)
            {
                return BadRequest();
            }

            db.Entry(stanica).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StanicaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Stanice
        [ResponseType(typeof(Stanica))]
        public IHttpActionResult PostStanica(Stanica stanica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stanicas.Add(stanica);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stanica.ID }, stanica);
        }

        // DELETE: api/Stanice/5
        [ResponseType(typeof(Stanica))]
        public IHttpActionResult DeleteStanica(int id)
        {
            Stanica stanica = db.Stanicas.Find(id);
            if (stanica == null)
            {
                return NotFound();
            }

            db.Stanicas.Remove(stanica);
            db.SaveChanges();

            return Ok(stanica);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StanicaExists(int id)
        {
            return db.Stanicas.Count(e => e.ID == id) > 0;
        }
    }
}