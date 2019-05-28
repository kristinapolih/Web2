using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class KoordinateRepository : Repository<Koordinate, int>, IKoordinateRepository
    {
        public KoordinateRepository(DbContext context) : base(context)
        {
        }
    }
}