using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Adresa
    {
        private int iD;
        private string ulica;
        private string broj;
        private string grad;

        [Key]
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