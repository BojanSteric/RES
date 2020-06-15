using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AMIAgregator
{
    class Program
    {
        static void Main(string[] args)
        {
            //otvori servis
            ServiceHost svc = new ServiceHost(typeof(DeviceMethods));
            svc.AddServiceEndpoint(typeof(IDevice),
            new NetTcpBinding(),
            new Uri("net.tcp://localhost:4000/DeviceMethods"));

            ServiceHost svc2 = new ServiceHost(typeof(AgregatorMethods));
            svc2.AddServiceEndpoint(typeof(IAgregator),
                new NetTcpBinding(),
                new Uri("net.tcp://localhost:5000/AgregatorMethods"));


            //otvori client
            //ChannelFactory<IAgregator> factory = new ChannelFactory<IAgregator>(
            //        new NetTcpBinding(),
            //        new EndpointAddress("net.tcp://localhost:5000/AgregatorMethods"));

            //IAgregator kanal = factory.CreateChannel();

           
            
            //otvori server
            svc.Open();
            svc2.Open();
            AgregatorMethods agm = new AgregatorMethods();

            int a = agm.formirajKljuc();

            List<DevClass> ldc = new List<DevClass>();
            foreach (var v in Baza.devices.Values)
            {
                ldc.Add(v);
            }

            Agregator ag = new Agregator(a, ldc);

            agm.dodajAgregator(a, ag);  //dodaje u baza klasu

            DeviceMethods dvcm = new DeviceMethods();
            dvcm.deleteXML();

            int i = 1;
            int j = 1;

            while (true)
            {
                Thread.Sleep(2000);
                /*if (Baza.devices.ContainsKey(1))
                {
                    Console.WriteLine("dev " + Baza.devices[1].AMIDevCode + " time " + Baza.devices[1].TimeStamp + " p " + Baza.devices[1].Power.ToString());
                    Console.WriteLine("tip "+ Baza.devices[1].ListMerenje[1].Tip + " Vrednost "+ Baza.devices[1].ListMerenje[1].Vrednost);
                }
                else {
                    break;
                }*/
                bool chk = Baza.devices.Any();
                if (chk)
                {
                    foreach (KeyValuePair<int, DevClass> kvp in Baza.devices)
                    {
                        i = kvp.Key;
                        break;
                    }
                }
                

                foreach (KeyValuePair<int, DevClass> dc in Baza.devices)
                {
                    if (!ag.devs.Contains(dc.Value))
                    {
                        ag.devs.Add(dc.Value);
                    }
                    i = dc.Key;
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("dev " + Baza.devices[i].AMIDevCode + " time " + Baza.devices[i].TimeStamp + " p " + Baza.devices[i].Power.ToString());
                    foreach (var item in Baza.devices[i].ListMerenje)
                    {
                        j = item.Key;
                        Console.WriteLine("Tip: " + Baza.devices[i].ListMerenje[j].Tip + " Vrednost: " + Baza.devices[i].ListMerenje[j].Vrednost);
                    }

                    Console.WriteLine("-------------------------------------------------");

                }

                dvcm.deleteXML();

                foreach (DevClass dc in Baza.devices.Values)
                {
                    dvcm.XMLWrite(dc);
                }

            }

            
         
        }
    }
}
