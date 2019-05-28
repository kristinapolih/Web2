using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class KoeficijentRepository : Repository<Koeficijent, int>, IKoeficijentRepository
    {
        public KoeficijentRepository(DbContext context) : base(context)
        {
        }
    }
}