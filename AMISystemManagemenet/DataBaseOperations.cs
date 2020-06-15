using Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMISystemManagemenet
{
    public class DataBaseOperations
    {
        public void SaveData(DevClass dc)
        {
            using (AMIEntities entity = new AMIEntities())
            {
                foreach (Merenje m in dc.ListMerenje.Values) {

                    Device d = new Device()
                    {
                        
                        Devcode = dc.AMIDevCode.ToString(),
                        time = dc.TimeStamp.ToString(),
                        idMer = dc.ListMerenje[m.Id].Id.ToString(),

                    };
                entity.Devices.Add(d);
                entity.SaveChanges();
                }
                
            }
        }

        public void SaveAgregator(Shared.Agregator ag)
        {
            using (AMIEntities entity = new AMIEntities())
            {
                int i = 0;
                foreach (DevClass d in ag.devs)
                {
                    Agregator agt = new Agregator()
                    {
                        
                        idAgr = ag.AMIAgrCode.ToString(),
                        timestamp = ag.TimeStamp.ToString(),
                        idDC = ag.devs[i].AMIDevCode
                    };
                    i++;
                    entity.Agregators.Add(agt);
                    entity.SaveChanges();
                }

            }

        }

        public void SaveMerenje(Merenje m)
        {
            using (AMIEntities entity = new AMIEntities())
            {
                
                    Merenja mt = new Merenja()
                    {
                        tip = m.Tip.ToString(),
                        vrednost = m.Vrednost.ToString()
                    };
                    entity.Merenjas.Add(mt);
                    entity.SaveChanges();
                
                
            }
        }

        public void Isprazni()
        {
            using (AMIEntities entity = new AMIEntities())
            {
                List<Agregator> agrs = entity.Agregators.ToList<Agregator>();
                Shared.Agregator agr = new Shared.Agregator();
                foreach (var ag in agrs)
                {
                    entity.Agregators.Remove(ag);
                    entity.SaveChanges();
                }
            }
        }

        public void IsprazniM()
        {
            using (AMIEntities entity = new AMIEntities())
            {
                List<Merenja> agrs = entity.Merenjas.ToList<Merenja>();
                Shared.Merenje agr = new Shared.Merenje();
                foreach (var ag in agrs)
                {
                    entity.Merenjas.Remove(ag);
                    entity.SaveChanges();
                   
                }
            }
        }
        public void IsprazniD()
        {
            using (AMIEntities entity = new AMIEntities())
            {
                List<Device> agrs = entity.Devices.ToList<Device>();
                Shared.DevClass agr = new Shared.DevClass();
                foreach (var ag in agrs)
                {
                    entity.Devices.Remove(ag);
                    entity.SaveChanges();
                    
                }
            }
        }

        public List<Shared.Agregator> LoadAgregators()
        {
            List<Shared.Agregator> temp = new List<Shared.Agregator>();
           
            using (AMIEntities entity = new AMIEntities())
            {
                
                List<Agregator> agrs = entity.Agregators.ToList<Agregator>();
                
                foreach (var ag in agrs)
                {
                    Shared.Agregator agr = new Shared.Agregator();

                    agr.AMIAgrCode = Int32.Parse(ag.idAgr);
                    agr.TimeStamp = Int32.Parse(ag.timestamp);
                    int a = ag.idDC;

                    temp.Add(agr);
                }
            };
            return temp;
        }

        public List<DevClass> LoadDevices() {
            List<DevClass> devs = new List<DevClass>();
            using (AMIEntities entity = new AMIEntities())
            {
                List<Device> ds = entity.Devices.ToList<Device>();
                List<Merenja> ms = entity.Merenjas.ToList<Merenja>();
                int i = 0;
                foreach (var d in ds)
                {
                    DevClass dc = new DevClass();
                    
                    dc.AMIDevCode = Int32.Parse(d.Devcode);
                    dc.TimeStamp = Int32.Parse(d.time);
                    dc.idMer = ms[i].idMerenja;
                    
                    devs.Add(dc);

                    i++;
                }
            };
            return devs;
        }

        public List<Merenje> LoadMerenja() {
            List<Merenje> merenje = new List<Merenje>();
            using (AMIEntities entity = new AMIEntities())
            {
                List<Merenja> ms = entity.Merenjas.ToList<Merenja>();
               
                foreach (var m in ms)
                {
                    Shared.Merenje mm = new Shared.Merenje();
                    switch (m.tip.ToString()) {
                        case "ASnaga":
                            mm.Tip = measurmentType.ASnaga;
                            break;
                        case "RSnaga":
                            mm.Tip = measurmentType.RSnaga;
                            break;
                        case "Napon":
                            mm.Tip = measurmentType.Napon;
                            break;
                        case "Struja":
                            mm.Tip = measurmentType.Struja;
                            break;

                    }
                    mm.Id = m.idMerenja;
                    mm.Vrednost = Int32.Parse(m.vrednost);

                    merenje.Add(mm);

                }
            };
            return merenje;
        }


    }
}

