using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{   [DataContract]
    public class Agregator
    {
        [Key]
        [DataMember]
        public int AMIAgrCode { get; set; }
        [DataMember]
        public int TimeStamp { get; set; }
        [DataMember]
        public List<DevClass> devs { get; set; }

        public Agregator() {

        }
        public Agregator(int aMIAgrCode)
        {
            this.AMIAgrCode = aMIAgrCode;
            this.TimeStamp = (Int32)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            this.devs.Add(new DevClass(1));
        }

        public Agregator(int aMIAgrCode, List<DevClass> ldc)
        {
            this.AMIAgrCode = aMIAgrCode;
            this.TimeStamp = (Int32)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            this.devs = ldc;
        }

        public void DeviceList()
        {
            foreach (DevClass d in Shared.Baza.devices.Values) {
                this.devs.Add(d);
            }
            
        }

        
    }
}
