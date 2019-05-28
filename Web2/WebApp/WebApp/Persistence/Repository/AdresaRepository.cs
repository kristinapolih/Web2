using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class AdresaRepository : Repository<Adresa, int>, IAdresaRepository
    {
        public AdresaRepository(DbContext context) : base(context)
        {
        }
    }
}