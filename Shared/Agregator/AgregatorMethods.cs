using Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Shared
{
    public class AgregatorMethods : IAgregator, Ixmldatoteka
    {
        public void deleteXML()
        {
            File.Delete("text.xml");
        }



        //public void pickDev()
        //{
        //    foreach (DevClass dc in Baza.devices.Values)
        //    {
        //        if (dc.Power == true && dc.Agr == 0)
        //        {
        //            dc.Agr = AMIAgrCode;
        //            devices.Add(dc);
        //        }
        //    }
        //}

        public void provera(Agregator ag, bool uspesno)
        {
            if (uspesno)
            {
                foreach (DevClass dc in Baza.devices.Values)
                {
                    Console.WriteLine( "lala");
                    XMLWrite(dc);
                    Baza.devices.Remove(dc.AMIDevCode);
                }
            }
        }
        


        public int formirajKljuc()
        {
            int i = 1;
            bool chk = Baza.slobodniAG.Any();
            if (chk)
            {
                i = Baza.slobodniAG.Last();
                Baza.slobodniAG.Remove(i);
                return i;
            }
            else
            {

                foreach (Agregator ag in Baza.agregators.Values)
                {
                    i++;
                }
                return i;
            }
        }

        public void dodajAgregator(int i, Agregator ag)
        {
            Baza.agregators.Add(i, ag);
        }

        public bool dobaviAgregator(int i, out Shared.Agregator a)
        {

            return Baza.agregators.TryGetValue(i, out a);
        }

        public int BazaACnt() {
            return Baza.agregators.Count();
        }
        public int BazaDCnt() {
            return Baza.devices.Count();
        }

        public string readXML()
        {
            string str = "";
            using (XmlReader xreader = XmlReader.Create("test.xml"))
            {
                while (xreader.Read())
                {
                    if (xreader.IsStartElement())
                    {
                        switch (xreader.Name.ToString())
                        {
                            case "DeviceCode":
                                str += "DevCode " + xreader.ReadString() + "\n";
                                break;
                            case "TimeStamp":
                                str += "TS " + xreader.ReadString() + "\n";
                                break;
                            case "MeasurementType":
                                str += "MT " + xreader.ReadString() + "\n";
                                break;
                            case "MeasurementValue":
                                str += "MV " + xreader.ReadString() + "\n";
                                break;
                        }
                    }
                }
                return str;
            }
        }

        public void turnDevOff(DevClass dc)
        {
            Baza.devices.Remove(dc.AMIDevCode);
        }

        //public void turnDevOn(DevClass dc)
        //{
        //    devices[devices.LastIndexOf(dc)].Power = true;
        //}

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
                    xmlWriter.WriteStartElement("Device");

                    xmlWriter.WriteElementString("DeviceCode", d.AMIDevCode.ToString());
                    xmlWriter.WriteElementString("TimeStamp", d.TimeStamp.ToString());
                    xmlWriter.WriteElementString("MeasurementType", d.ListMerenje[0].Tip.ToString());  //Dodaj izlistavanje
                    xmlWriter.WriteElementString("MeasurementValue", d.ListMerenje[0].Vrednost.ToString());

                    xmlWriter.WriteEndElement();

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
                XElement lastRow = rows.Last();                                     //nadji poslednji element
                                                                                    // XmlSerializer xs = new XmlSerializer(typeof(List<Merenje>));
                foreach (KeyValuePair<int, Merenje> kvp in d.ListMerenje)
                {
                    lastRow.AddAfterSelf(                                               //dodaj nakon poslednjeg elementa
                       new XElement("Device",
                       new XElement("DeviceCode", d.AMIDevCode.ToString()),
                       new XElement("TimeStamp", d.TimeStamp.ToString()),
                       new XElement("MeasurementType", kvp.Value.Tip.ToString()),      //ovde ce morati da se dodaje izlistavanje
                       new XElement("MeasurementValue", kvp.Value.Vrednost.ToString()))
                       );
                    xDocument.Save("test.xml");
                }
            }
        }

        public bool dobaviDev(int i, out DevClass d)
        {

            return Baza.devices.TryGetValue(i, out d);

        }

        public bool dobaviMer(int i, DevClass d, out Merenje m)
        {
            return d.ListMerenje.TryGetValue(i, out m);
        }
    }
}
