using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Linija
    {
        public int iD;
        public string redniBroj;
        public TipVoznje tipVoznje;
        public DanUNedelji datum;
        public List<int> iDStanica;
        public string polasci;

        [Key]
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public string Naziv
        {
            get { return redniBroj; }
            set { redniBroj = value; }
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
        public List<int> IDStanica
        {
            get { return iDStanica; }
            set { iDStanica = value; }
        }
        public string Polasci
        {
            get { return polasci; }
            set { polasci = value; }
        }
    }
}