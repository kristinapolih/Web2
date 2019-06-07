using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class LinijaStanicaRepository : Repository<LinijaStanica, int>, ILinijaStanicaRepository
    {
        public LinijaStanicaRepository(DbContext context) : base(context)
        {
        }
    }
}