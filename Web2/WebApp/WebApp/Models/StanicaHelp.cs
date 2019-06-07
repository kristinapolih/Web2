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
        private string ime;
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

        public bool IsStation
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

        public string Name
        {
            get
            { return ime; }
            set
            { ime = value; }
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