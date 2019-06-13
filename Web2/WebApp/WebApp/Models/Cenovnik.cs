using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Cenovnik
    {
        private int iD;
        private DateTime oD;
        private DateTime dO;
        private DateTime stamp;

        [Key]
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public DateTime DO
        {
            get { return dO; }
            set { dO = value; }
        }
        public DateTime OD
        {
            get { return oD; }
            set { oD = value; }
        }
        public DateTime Stamp
        {
            get { return stamp; }
            set { stamp = value; }
        }
    }
}