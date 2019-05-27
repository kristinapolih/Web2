using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Linija
    {
        public int iD;
        public int redniBroj;
        public List<int> iDStanica;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public int Naziv
        {
            get { return redniBroj; }
            set { redniBroj = value; }
        }
        public List<int> IDStanica
        {
            get { return iDStanica; }
            set { iDStanica = value; }
        }
    }
}