using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class StavkaRepository : Repository<Stavka, int>, IStavkaRepository
    {
        public StavkaRepository(DbContext context) : base(context)
        {
        }
    }
}