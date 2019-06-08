using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Helper
{
    public static class HelperReader
    {
        static ApplicationDbContext dbContext = new ApplicationDbContext();
        static IUnitOfWork unitOfWork;

        public static void Reader(IUnitOfWork uw, string routeName)
        {
            unitOfWork = uw;
            bool state = true;
            string line;
            int idRoute = unitOfWork.LinijaRepository.GetAll().Where(x => x.Broj == routeName).FirstOrDefault().ID;
            System.IO.StreamReader file = new System.IO.StreamReader(@"E:\WEB2\Web2\Web2\WebApp\Scripts\" + routeName + ".txt");
            while ((line = file.ReadLine()) != null)
            {
                if (line == "-")
                {
                    state = false;
                    continue;
                }

                if (state) // dots
                {
                    HelperReader.DoDot(line, idRoute);
                }
                else // stations
                {
                    HelperReader.DoStation(line, idRoute);
                }
            }

            file.Close();
        }

        private static void DoDot(string dot, int idRoute)
        {
            string[] split = dot.Split(',');
            double X = Convert.ToDouble(split[0]);
            double Y = Convert.ToDouble(split[1]);
            int idStation;

            Stanica s = unitOfWork.StanicaRepository.GetAll().Where(x => x.X == X && x.Y == Y).FirstOrDefault();

            if (s == null)
            {
                // dodati Stanica
                Stanica stanica = new Stanica() { X = X, Y = Y, Naziv = "", IsStanica = false, IDAdresa = -1 };
                unitOfWork.StanicaRepository.Add(stanica);
                unitOfWork.Complete();
                idStation = unitOfWork.StanicaRepository.GetAll().Where(x => x.X == X && x.Y == Y).FirstOrDefault().ID;
            }
            else
            {
                idStation = s.ID;
            }

            // dodati LinijaStanica
            LinijaStanica linijaStanica = new LinijaStanica() { IDLinija = idRoute, IDStanica = idStation };
            unitOfWork.LinijaStanicaRepository.Add(linijaStanica);
            unitOfWork.Complete();
        }

        private static void DoStation(string station, int idRoute)
        {
            string[] split = station.Split('|');
            double Y = Convert.ToDouble(split[0]);
            double X = Convert.ToDouble(split[1]);
            string Name = split[2];

            int idStation;

            Stanica s = unitOfWork.StanicaRepository.GetAll().Where(x => x.X == X && x.Y == Y).FirstOrDefault();

            if (s == null)
            {
                Adresa a = new Adresa() { Grad = split[3], Ulica = split[4], Broj = split[5] };
                unitOfWork.AdresaRepository.Add(a);
                unitOfWork.Complete();
                int idAddress = unitOfWork.AdresaRepository.GetAll().Where(x => x.Grad == split[3] && x.Ulica == split[4] && x.Broj == split[5]).FirstOrDefault().ID;

                // dodati Stanica                                                               
                Stanica stationA = new Stanica() { X = X, Y = Y, Naziv = Name, IsStanica = true, IDAdresa = idAddress };
                unitOfWork.StanicaRepository.Add(stationA);
                unitOfWork.Complete();
                idStation = unitOfWork.StanicaRepository.GetAll().Where(x => x.X == X && x.Y == Y).FirstOrDefault().ID;
            }
            else
            {
                idStation = s.ID;
            }

            // dodati LinijaStanica
            LinijaStanica ls = new LinijaStanica() { IDLinija = idRoute, IDStanica = idStation };
            unitOfWork.LinijaStanicaRepository.Add(ls);
            unitOfWork.Complete();
        }
    }
}