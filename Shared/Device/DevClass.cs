using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [DataContract]
    public class DevClass
    {
        private int amidevcode;
        private Int32 timeStamp;
        private Dictionary<int, Merenje> listMerenje = new Dictionary<int, Merenje>();
        private bool power;
        private int agr;
        private int idmer;
        //properties
        [Key]
        [DataMember]
        public int AMIDevCode { get => amidevcode; set => amidevcode = value; }
        [DataMember]
        public Int32 TimeStamp { get => timeStamp; set => timeStamp = value; }
        [DataMember]
        public Dictionary<int, Merenje> ListMerenje { get => listMerenje; set => listMerenje = value; }
        [DataMember]
        public bool Power { get => power; set => power = value; }
        [DataMember]
        public int Agr { get => agr; set => agr = value; }
        [DataMember]
        public int idMer { get => idmer; set => idmer = value; }
        public DevClass() {     //konstruktor bez parametara
            this.AMIDevCode = -1;
            this.TimeStamp = (Int32)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            this.Power = false;
            this.Agr = 0;
        }

        public DevClass(int aMIDevCode) //konstruktor sa parametrima
        {
            this.AMIDevCode = aMIDevCode;
            this.TimeStamp = (Int32)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;   //UnixTimeStamp
            this.ListMerenje = new Dictionary<int, Merenje>();
            
            Power = true;
            Agr = 0;
        }
        public DevClass(int a, Dictionary<int, Merenje> m) {
            this.AMIDevCode = a;
            this.TimeStamp = (Int32)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;   //UnixTimeStamp
            foreach (KeyValuePair<int, Merenje> kvp in m) {
                ListMerenje.Add(kvp.Key, kvp.Value);
            }

            Power = true;
            Agr = 0;
        }
        
        public void DodajMerenje(Merenje m) {
            if (!ListMerenje.ContainsKey(m.Id)) {
                this.ListMerenje.Add(m.Id, m);
                
            }
        }

        public List<Merenje> dobaviTipMerenje() {
            List<Merenje> mm = new List<Merenje>();
            foreach (Merenje m in ListMerenje.Values) {
                mm.Add(m);
            }
            return mm;
            
        }


    }
}
