using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Stavka
    {
        public int iD;
        public TipKarte tipKarte;

        [Key]
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public TipKarte TipKarte
        {
            get { return tipKarte; }
            set { tipKarte = value; }
        }
    }
}