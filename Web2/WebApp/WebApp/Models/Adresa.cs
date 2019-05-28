using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Adresa
    {
        public int iD;
        public string ulica;
        public string broj;
        public string grad;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public string Ulica
        {
            get { return ulica; }
            set { ulica = value; }
        }
        public string Broj
        {
            get { return broj; }
            set { broj = value; }
        }
        public string Grad
        {
            get { return grad; }
            set { grad = value; }
        }
    }
}