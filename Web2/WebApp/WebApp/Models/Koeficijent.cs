using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Koeficijent
    {
        public int iD;
        public string ime;
        public float vrednost;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }
        public float Vrednost
        {
            get { return vrednost; }
            set { vrednost = value; }
        }
    }
}