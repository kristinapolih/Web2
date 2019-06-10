using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class LinijaStanica
    {
        private int iD;
        private int iDLinija;
        private int iDStanica;
        private int brojStanice;

        public LinijaStanica()
        {

        }

        [Key]
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public int IDLinija
        {
            get { return iDLinija; }
            set { iDLinija = value; }
        }
        public int IDStanica
        {
            get { return iDStanica; }
            set { iDStanica = value; }
        }
        public int BrojStanice
        {
            get { return brojStanice; }
            set { brojStanice = value; }
        }
    }
}