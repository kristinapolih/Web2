using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class RedVoznje
    {
        public int iD;
        public DateTime datum;
        public int linija;
        public TipVoznje tipVoznje;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }
        public int Linija
        {
            get { return linija; }
            set { linija = value; }
        }
        public TipVoznje TipVoznje
        {
            get { return tipVoznje; }
            set { tipVoznje = value; }
        }

        public RedVoznje() { }
    }
}