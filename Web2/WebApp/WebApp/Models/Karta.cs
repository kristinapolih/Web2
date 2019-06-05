using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Karta
    {
        public int iD;
        public DateTime odDatum;
        public DateTime doDatum;
        public int iDCenovnikStavka;
        public int iDKorisnik;
        public float cena;
        private TipKarte tipKarte;

        [Key]
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public DateTime ODDatum
        {
            get { return odDatum; }
            set { odDatum = value; }
        }
        public DateTime DoDatum
        {
            get { return doDatum; }
            set { doDatum = value; }
        }
        public int IDCenovnikStavka
        {
            get { return iDCenovnikStavka; }
            set { iDCenovnikStavka = value; }
        }
        public int IDKorisnik
        {
            get { return iDKorisnik; }
            set { iDKorisnik = value; }
        }
        public float Cena
        {
            get { return cena; }
            set { cena = value; }
        }
        public TipKarte TipKarte
        {
            get { return tipKarte; }
            set { tipKarte = value; }
        }
    }
}