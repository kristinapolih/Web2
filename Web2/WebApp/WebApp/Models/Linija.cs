using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Linija
    {
        private int iD;
        private string broj;
        private string naziv;
        private TipVoznje tipVoznje;
        private DanUNedelji datum;
        private string polasci;
        private bool obrisana = false;

        [Key]
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public string Broj
        {
            get { return broj; }
            set { broj = value; }
        }
        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }
        public TipVoznje TipVoznje
        {
            get { return tipVoznje; }
            set { tipVoznje = value; }
        }
        public DanUNedelji Datum
        {
            get { return datum; }
            set { datum = value; }
        }
        public string Polasci
        {
            get { return polasci; }
            set { polasci = value; }
        }
        public bool Obrisana
        {
            get { return obrisana; }
            set { obrisana = value; }
        }
    }
}