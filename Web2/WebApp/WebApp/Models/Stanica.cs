using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Stanica
    {
        public int iD;
        public string naziv;
        public string adresa;
        public int iDKoordinata;
        public List<int> iDLinija;

        [Key]
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }
        public string Adresa
        {
            get { return adresa; }
            set { adresa = value; }
        }
        public int IDKoordinata
        {
            get { return iDKoordinata; }
            set { iDKoordinata = value; }
        }
        public List<int> IDLinija
        {
            get { return iDLinija; }
            set { iDLinija = value; }
        }
    }
}