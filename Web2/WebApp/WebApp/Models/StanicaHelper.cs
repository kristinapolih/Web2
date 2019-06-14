using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class StanicaHelper
    {
        private int iD;
        private double x;
        private double y;
        private string naziv;
        private string adresa;
        private string brojRute;
        private bool isStation;
        public List<int> IDRute;
        public List<string> BrojeviRuta;
        public List<int> BrojeviStanica;
        private int brojURuti;

        public StanicaHelper()
        {
            IDRute = new List<int>();
            BrojeviRuta = new List<string>();
            BrojeviStanica = new List<int>();
        }

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public int BrojURuti
        {
            get { return brojURuti; }
            set { brojURuti = value; }
        }
        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public string BrojRute
        {
            get { return brojRute; }
            set { brojRute = value; }
        }
        public bool IsStanica
        {
            get { return isStation; }
            set { isStation = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
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
    }
}