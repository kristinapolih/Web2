using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Linije
    {
        public int ID { get; set; }
        public string ImeRute { get; set; }
        public string BrojRute { get; set; }
        public TipVoznje TipRute { get; set; }
        public List<StanicaHelp> Stanice { get; set; }

        public Linije()
        {
            Stanice = new List<StanicaHelp>();
        }
    }
}