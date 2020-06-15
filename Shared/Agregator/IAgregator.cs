using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [ServiceContract]
    public interface IAgregator
    {
        //[OperationContract]
        //List<DevClass> DeviceList();

        [OperationContract]
        void provera(Agregator ag, bool uspesno);

        //[OperationContract]
        //void pickDev();
        
        //[OperationContract]
        //void turnDevOn(DevClass dc);

        [OperationContract]
        void turnDevOff(DevClass dc);
        [OperationContract]
        bool dobaviAgregator(int i, out Shared.Agregator a);
        [OperationContract]
        int BazaDCnt();
        [OperationContract]
        int BazaACnt();
        [OperationContract]
        bool dobaviDev(int i, out DevClass d);
        [OperationContract]
        bool dobaviMer(int i, DevClass d, out Merenje m);
        //[OperationContract]
        //List<DevClass> AMIaddDev(List<DevClass> devClasses);
    }
}
