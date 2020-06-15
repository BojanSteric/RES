using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AMIDevice
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IDevice> factory = new ChannelFactory<IDevice>(
                    new NetTcpBinding(),
                    new EndpointAddress("net.tcp://localhost:4000/DeviceMethods"));

            IDevice kanal = factory.CreateChannel();
            
            
            int type = kanal.randomType();
            int value = kanal.randomValue();

            
            List<Merenje> merenje = new List<Merenje>();
            
            //Baza.devices.Add(d.AMIDevCode, d);
            Dictionary<int, Merenje> z = new Dictionary<int, Merenje>();

            for (int j = 1; j < 4; j++)
            {
                type = kanal.randomType();
                value = kanal.randomValue();

                Merenje m = new Merenje(j, (measurmentType)type, value);
                merenje.Add(m);
                //Baza.devices.Add(d.AMIDevCode, d);


                z.Add(m.Id, m);
                Console.WriteLine(" tip " + m.Tip + " val " + m.Vrednost);
                Thread.Sleep(1000);
            }


            int i = kanal.formirajKljuc();

            DevClass d = new DevClass(i, z);

            kanal.DodajDevice(d);
            Console.WriteLine("Uspesno dodat dev");
            Console.WriteLine("dev " + d.AMIDevCode + " time " + d.TimeStamp + " p " + d.Power.ToString());

            kanal.DodajMerenjaDevice(merenje, d);
            Console.WriteLine("uspesno dodato merenje");


            if (kanal.dobaviDevice(i, out DevClass dc))
            {
                Console.WriteLine("dev " + dc.AMIDevCode + " time " + dc.TimeStamp);
                List<Merenje> ss = d.dobaviTipMerenje();
                Console.WriteLine(ss[0].Tip);
            }


            Console.WriteLine("q - za disconnect");
            if(Console.ReadLine() == "q")
            {
                kanal.ukloniDevice(i);
               
                Console.WriteLine("device diskonektovan");

            }
            
            Console.ReadKey();
            
        }
    }
}
