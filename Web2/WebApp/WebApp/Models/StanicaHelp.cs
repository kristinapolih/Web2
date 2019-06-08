using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class StanicaHelp
    {
        private double x;
        private double y;
        private string naziv;
        private Adresa adresa;
        private bool isStation;

        public StanicaHelp()
        {
            Adresa = new Adresa();
        }

        public double X
        {
            get
            { return x; }
            set
            { x = value; }
        }

        public bool IsStanica
        {
            get
            {  return isStation; }
            set
            { isStation = value; }
        }

        public double Y
        {
            get
            { return y; }
            set
            {  y = value; }
        }

        public string Naziv
        {
            get
            { return naziv; }
            set
            { naziv = value; }
        }


        public Adresa Adresa
        {
            get
            {  return adresa; }
            set
            {  adresa = value; }
        }
    }
}