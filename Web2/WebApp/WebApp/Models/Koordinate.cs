﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Koordinate
    {
        private int iD;
        private double x;
        private double y;

        [Key]
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        
    }
}