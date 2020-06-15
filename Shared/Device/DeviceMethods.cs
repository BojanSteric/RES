using Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Shared
{
    public class DeviceMethods : IDevice
    {
        public List<DevClass> AMIaddDev(List<DevClass> devClasses)
        {
            List<DevClass> ldc = devClasses;
            int lastHC;     //GetHashCode???

            bool chk = !devClasses.Any();

            if (chk)
            {
                lastHC = 0;
            }
            else
            {
                lastHC = ldc.Last<DevClass>().AMIDevCode;
            }
            //DevClass dc = new DevClass((lastHC + 1), 10, List<measurmentType>, List<int>);
            DevClass dc = new DevClass(lastHC + 1);

            ldc.Add(dc);

            return ldc;
        }

        public bool dobaviDevice(int i, out DevClass d)
        {
            
            return Baza.devices.TryGetValue(i, out d);
            
        }

        public bool dobaviMerenje(int i, DevClass d, out Merenje m)
        {
            return d.ListMerenje.TryGetValue(i, out m);
        }

        public void DodajDevice(DevClass d)
        {
            if (!Baza.devices.ContainsKey(d.AMIDevCode))
            {
                Baza.devices.Add(d.AMIDevCode, d);
            }
        }

        public int formirajKljuc()
        {
            int i = 1;
            bool chk = Baza.slobodni.Any();
            if (chk)
            {
                i = Baza.slobodni.Last();
                Baza.slobodni.Remove(i);
                return i;
            }
            else
            {

                foreach (DevClass dc in Baza.devices.Values)
                {
                    i++;
                }
                return i;
            }
        }

        public void DodajMerenjaDevice(List<Merenje> merenja, DevClass d)
        {
            foreach (Merenje m in merenja)
            {
                d.DodajMerenje(m);
            }
        }

        public void ukloniDevice(int a)
        {
            Baza.devices.Remove(a);
            Baza.slobodni.Add(a);
        }
        public int randomType()
        {
            Random rType = new Random();
            return rType.Next(4);
        }

        public int randomValue()
        {
            Random rVal = new Random();

            return rVal.Next(1000);
        }

        public void XMLWrite(DevClass d)
        {
            if (!File.Exists("test.xml"))                                                    //ako fajl ne postoji
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;                                              //podesava fajl za pisanje new line + tab after element

                using (XmlWriter xmlWriter = XmlWriter.Create("test.xml", xmlWriterSettings)) //kreiraj fajl
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("Devices");
                    foreach (var item in Baza.devices[d.AMIDevCode].ListMerenje)
                    {
                        int j = item.Key;

                        xmlWriter.WriteStartElement("Device");
                        xmlWriter.WriteElementString("DeviceCode", d.AMIDevCode.ToString());
                        xmlWriter.WriteElementString("TimeStamp", d.TimeStamp.ToString());
                        xmlWriter.WriteElementString("MeasurementType", d.ListMerenje[j].Tip.ToString());  //Dodaj izlistavanje
                        xmlWriter.WriteElementString("MeasurementValue", d.ListMerenje[j].Vrednost.ToString());
                        xmlWriter.WriteEndElement();

                    }



                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();
                    xmlWriter.Close();
                }
            }
            else
            {  //koristi xml.Linq lib
                XDocument xDocument = XDocument.Load("test.xml");
                XElement root = xDocument.Element("Devices");                        //root element fajla
                IEnumerable<XElement> rows = root.Descendants("Device");            //pocetni podelement
                XElement lastRow = rows.Last();
                foreach (var item in Baza.devices[d.AMIDevCode].ListMerenje)
                {
                    int j = item.Key;//nadji poslednji element
                    lastRow.AddAfterSelf(                                               //dodaj nakon poslednjeg elementa
                   new XElement("Device",
                   new XElement("DeviceCode", d.AMIDevCode.ToString()),
                   new XElement("TimeStamp", d.TimeStamp.ToString()),
                   new XElement("MeasurementType", d.ListMerenje[j].Tip.ToString()),      //ovde ce morati da se dodaje izlistavanje
                   new XElement("MeasurementValue", d.ListMerenje[j].Vrednost.ToString()))
                );
                }

                xDocument.Save("test.xml");
            }

            
        }
        public void deleteXML()
        {
            File.Delete("test.xml");
        }
    }
}
