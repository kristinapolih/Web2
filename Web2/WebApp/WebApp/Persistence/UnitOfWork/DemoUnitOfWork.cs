using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public class DemoUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
      
        public DemoUnitOfWork(DbContext context)
        {
            _context = context;
        }

        [Dependency]
        public IStanicaRepository StanicaRepository { get; set; }
        [Dependency]
        public ILinijaRepository LinijaRepository { get; set; }
        [Dependency]
        public IKorisnikRepository KorisnikRepository { get; set; }
        [Dependency]
        public IKoordinateRepository KoordinateRepository { get; set; }
        [Dependency]
        public IKoeficijentRepository KoeficijentRepository { get; set; }
        [Dependency]
        public IKartaRepository KartaRepository { get; set; }
        [Dependency]
        public ICenovnikRepository CenovnikRepository { get; set; }
        [Dependency]
        public ICenovnikStavkaRepository CenovnikStavkaRepository { get; set; }
        [Dependency]
        public IAdresaRepository AdresaRepository { get; set; }
        [Dependency]
        public IStavkaRepository StavkaRepository { get; set; }
        [Dependency]
        public ILinijaStanicaRepository LinijaStanicaRepository { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}