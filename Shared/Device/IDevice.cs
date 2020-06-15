using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [ServiceContract]
    public interface IDevice
    {
        [OperationContract]
        List<DevClass> AMIaddDev(List<DevClass> devClasses);
        [OperationContract]
        int randomValue();
        [OperationContract]
        int randomType();
        [OperationContract]
        void DodajMerenjaDevice(List<Merenje> merenja, DevClass d);
        [OperationContract]
        void DodajDevice(DevClass d);
        [OperationContract]
        bool dobaviDevice(int i, out DevClass d);
        [OperationContract]
        bool dobaviMerenje(int i, DevClass d, out Merenje m);

        [OperationContract]
        int formirajKljuc();

        [OperationContract]
        void XMLWrite(DevClass d);

        [OperationContract]
        void deleteXML();

        [OperationContract]
        void ukloniDevice(int a);
    }
}
