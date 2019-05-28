using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IStanicaRepository StanicaRepository { get; set; }
        IRedVoznjeRepository RedVoznjeRepository { get; set; }
        ILinijaRepository LinijaRepository { get; set; }
        IKorisnikRepository KorisnikRepository { get; set; }
        IKoordinateRepository KoordinateRepository { get; set; }
        IKoeficijentRepository KoeficijentRepository { get; set; }
        IKartaRepository KartaRepository { get; set; }
        ICenovnikRepository CenovnikRepository { get; set; }
        ICenovnikStavkaRepository CenovnikStavkaRepository { get; set; }
        IAdresaRepository AdresaRepository { get; set; }
        int Complete();
    }
}
