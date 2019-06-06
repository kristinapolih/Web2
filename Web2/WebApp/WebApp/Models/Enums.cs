using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public enum TipVoznje
    {
        Gradski,
        Prigradski
    }

    public enum TipKarte
    {
        Vremenska,
        Dnevna,
        Mesecna,
        Godisnja
    }

    public enum TipPutnika
    {
        Regularni,
        Djak,
        Penzioner
    }

    public enum ProcesVerifikacije
    {
        Procesira,
        Prihvacen,
        Odbijen
    }

    public enum DanUNedelji
    {
        RadniDan,
        Subota,
        Nedelja
    }
}