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
        private int iDAdresa;
        private double x;
        private double y;
        private bool isStanica;
        private DateTime stamp;
        private bool obrisana;

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
        public int IDAdresa
        {
            get { return iDAdresa; }
            set { iDAdresa = value; }
        }
        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public double Y
        {
            get  { return y; }
            set { y = value; }
        }
        public bool IsStanica
        {
            get { return isStanica; }
            set { isStanica = value; }
        }
        public bool Obrisana
        {
            get { return obrisana; }
            set { obrisana = value; }
        }
        public DateTime Stamp
        {
            get { return stamp; }
            set { stamp = value; }
        }
    }
}