using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Karta
    {
        private int iD;
        private DateTime odDatum;
        private DateTime doDatum;
        private int iDCenovnikStavka;
        private int iDKorisnik;
        private float cena;
        private TipKarte tipKarte;
        private bool obrisana;

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

        public bool Obrisana
        {
            get { return obrisana; }
            set { obrisana = value; }
        }
    }
}