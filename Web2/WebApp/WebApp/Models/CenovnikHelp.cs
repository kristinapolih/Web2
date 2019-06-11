using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class CenovnikHelp
    {
        public int ID { get; set; }
        public string OdDatuma { get; set; }
        public string DoDatuma { get; set; }
        public string VremenskaCena { get; set; }
        public string DnevnaCena { get; set; }
        public string MesecnaCena { get; set; }
        public string GodisnjaCena { get; set; }
        public bool Menja { get; set; }

        public CenovnikHelp() { }
    }
}