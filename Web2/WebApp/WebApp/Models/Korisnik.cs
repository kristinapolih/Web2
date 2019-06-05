using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Korisnik
    {
        public int iD;
        public string iDUser;
        public string email;
        public string lozinka;
        public string ime;
        public string prezime;
        public string adresa;
        public DateTime datumRodjenja;
        public TipPutnika tipKorisnika;
        public string slika;
        public ProcesVerifikacije stanje;

        #region Props
        [Key]
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public string IDUser
        {
            get { return iDUser; }
            set { iDUser = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Lozinka
        {
            get { return lozinka; }
            set { lozinka = value; }
        }
        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }
        public string Prezime
        {
            get { return prezime; }
            set { prezime = value; }
        }
        public string Adresa
        {
            get { return adresa; }
            set { adresa = value; }
        }
        public DateTime DatumRodjenja
        {
            get { return datumRodjenja; }
            set { datumRodjenja = value; }
        }
        public TipPutnika TipKorisnika
        {
            get { return tipKorisnika; }
            set { tipKorisnika = value; }
        }
        public string Slika
        {
            get { return slika; }
            set { slika = value; }
        }
        public ProcesVerifikacije Stanje
        {
            get { return stanje; }
            set { stanje = value; }
        }
#endregion

        public Korisnik()
        {
            Stanje = ProcesVerifikacije.Procesira;
        }
        
    }
}