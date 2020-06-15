using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMISystemManagemenet
{
    public class GraphModel
    {
        private List<Shared.Merenje> data = new List<Shared.Merenje>();
        private List<DevClass> dc = new List<DevClass>();
        public List<Shared.Merenje> Data { get => data; set => data = value; }
        public List<DevClass> DC { get => dc; set => dc = value; }
        DataBaseOperations dbo = new DataBaseOperations();

        public GraphModel() {
            DC = dbo.LoadDevices();
            foreach (DevClass d in DC)
            {
                data = dbo.LoadMerenja();
            }
            Data = data;
            
        }
    }
}
