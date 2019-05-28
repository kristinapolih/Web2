using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class CenovnikStavka
    {
        public int iD;
        public int iDCenovnika;
        public int iDStavka;
        public int iDKoeficijent;
        public string cena;

        [Key]
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public int IDCenovnika
        {
            get { return iDCenovnika; }
            set { iDCenovnika = value; }
        }
        public int IDSt5avka
        {
            get { return iDStavka; }
            set { iDStavka = value; }
        }
        public int IDKoeficijent
        {
            get { return iDKoeficijent; }
            set { iDKoeficijent = value; }
        }
        public string Cena
        {
            get { return cena; }
            set { cena = value; }
        }
    }
}