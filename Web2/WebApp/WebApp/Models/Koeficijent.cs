using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Koeficijent
    {
        public int iD;
        public float djak;
        public float pensioner;

        [Key]
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public float Djak
        {
            get { return djak; }
            set { djak = value; }
        }
        public float Pensioner
        {
            get { return pensioner; }
            set { pensioner = value; }
        }
    }
}