using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using WebApp.Controllers;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;


namespace WebApp.Hubs
{
    [HubName("notifications")]
    public class NotificationHub : Hub
    {
        public static ConcurrentDictionary<string, Timer> Timers = new ConcurrentDictionary<string, Timer>();
        private static readonly object balanceLock = new object();
        IUnitOfWork unitOfWork = ValuesController.unitOfWork;
        private static ApplicationDbContext dbContext = new ApplicationDbContext();
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        private static Dictionary<string, string> groupNames = new Dictionary<string, string>();
        private static Dictionary<string, List<int>> RouteBus = new Dictionary<string, List<int>>();

        public NotificationHub() { }

        public void BroadcastData(string nameOfGroup)
        {
            Groups.Add(Context.ConnectionId, nameOfGroup);
            groupNames[Context.ConnectionId] = nameOfGroup;
            List<int> list = new List<int>();
            Random r = new Random();
            int count = r.Next(1, 4);
            for (int i = 0; i < count; i++)
            {
                while (true)
                {
                    int k = r.Next(1, 6);
                    if (!list.Contains(k))
                    {
                        list.Add(k);
                        break;
                    }
                }
            }

            RouteBus[nameOfGroup] = list;
        }

        public void TimeServerUpdates()
        {
            Random random = new Random();
            Timer timer = new Timer();
            timer.Interval = random.Next(5000, 10000);
            timer.Start();
            timer.Elapsed += OnTimedEvent;
            Timers[Context.ConnectionId] = timer;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            GetTime();
        }

        public void GetTime()
        {
            //try
            //{
                foreach (string val in groupNames.Values)
                {
                    lock (balanceLock)
                    {
                        Linija linija = unitOfWork.LinijaRepository.GetAll().Where(x => x.Broj == val).FirstOrDefault();
                        List<LinijaStanica> linijastanica = unitOfWork.LinijaStanicaRepository.GetAll().Where(x => x.IDLinija == linija.ID).ToList();
                        Dictionary<int, Stanica> stanice = new Dictionary<int, Stanica>();

                        foreach (LinijaStanica s in linijastanica)
                        {
                            Stanica station = unitOfWork.StanicaRepository.Get(s.IDStanica);

                            if (station.IsStanica)
                            {
                                stanice.Add(s.BrojStanice, station);
                            }
                        }

                        List<Stanica> stationsToSend = new List<Stanica>();
                        List<int> lis = RouteBus[val];

                        foreach (int k in lis)
                        {
                            stationsToSend.Add(stanice[k]);
                        }

                        lis = lis.Select(x => x + 1).ToList();

                        Clients.Group(val).setRealTime(stationsToSend);


                        for (int i = 0; i < lis.Count; i++)
                        {
                            if (lis[i] > stanice.Count)
                            {
                                lis[i] = 1;
                            }
                        }

                        RouteBus[val] = lis;

                    }
                }
            //}
            //catch (Exception) { }
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            if (groupNames.ContainsKey(Context.ConnectionId))
            {
                Timer timer = new Timer();
                Timers.TryRemove(Context.ConnectionId, out timer);
                timer.Close();
                Groups.Remove(Context.ConnectionId, groupNames[Context.ConnectionId]);
                RouteBus.Remove(groupNames[Context.ConnectionId]);
                groupNames.Remove(Context.ConnectionId);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}