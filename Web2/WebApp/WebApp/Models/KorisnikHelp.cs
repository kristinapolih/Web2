using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class KorisnikHelp
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string DatumRodjenja { get; set; }
        public string TipKorisnika { get; set; }
        public string Stanje { get; set; }

        public KorisnikHelp() {}
    }
}