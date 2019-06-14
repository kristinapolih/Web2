using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class AddLinijeHelp
    {
        public int ID;
        public string BrojRute;
        public List<Koordinate> tacke;

        public AddLinijeHelp()
        {
            tacke = new List<Koordinate>();
        }
    }
}