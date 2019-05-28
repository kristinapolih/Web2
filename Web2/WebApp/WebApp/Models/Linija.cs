using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Linija
    {
        public int iD;
        public int redniBroj;
        public TipVoznje tipVoznje;
        public List<int> iDStanica;
        public string polasci;

        [Key]
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
        public TipVoznje TipVoznje
        {
            get { return tipVoznje; }
            set { tipVoznje = value; }
        }
        public List<int> IDStanica
        {
            get { return iDStanica; }
            set { iDStanica = value; }
        }
        public string Polasci
        {
            get { return polasci; }
            set { polasci = value; }
        }
    }
}