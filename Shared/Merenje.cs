using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public enum measurmentType { Napon, Struja, ASnaga, RSnaga };
    [DataContract]
    public class Merenje
    {
        int id;
        private measurmentType tip;
        private int vrednost;
        [DataMember]
        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }
        [DataMember]
        public measurmentType Tip
        {
            get { return tip; }
            set { this.tip = value; }
        }
        [DataMember]
        public int Vrednost
        {
            get { return vrednost; }
            set { this.vrednost = value; }
        }
        
        public Merenje() {
            id = -1;
            this.Tip = measurmentType.Napon;
            this.Vrednost = 0;
        }

        public Merenje(int id, measurmentType t, int v) {
            this.Id = id;
            this.Tip = t;
            this.Vrednost = v;
        }

    }
}
