using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class KupljenaKarta
    {
        private TipPutnika tipPutnika;
        private TipKarte tipKarte;
        private float cena;

        public KupljenaKarta() { }

        public TipPutnika TipPutnika
        {
            get { return tipPutnika; }
            set { tipPutnika = value; }
        }
        public TipKarte TipKarte
        {
            get { return tipKarte; }
            set { tipKarte = value; }
        }
        public float Cena
        {
            get { return cena; }
            set { cena = value; }
        }
    }
}