using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Stanica
    {
        private int iD;
        private string naziv;
        private string adresa;
        private int iDKoordinata;

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
    }
}