﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class CenovnikStavkaRepository : Repository<CenovnikStavka, int>, ICenovnikStavkaRepository
    {
        public CenovnikStavkaRepository(DbContext context) : base(context)
        {
        }
    }
}