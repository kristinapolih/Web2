using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class RedVoznje
    {
        public int iD;
        public DanUNedelji datum;

        [Key]
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public DanUNedelji Datum
        {
            get { return datum; }
            set { datum = value; }
        }

        public RedVoznje() { }
    }
}