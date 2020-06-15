using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [ServiceContract]
    public interface Ixmldatoteka
    {
        [OperationContract]
        void XMLWrite(DevClass d);
        [OperationContract]
        void deleteXML();
        [OperationContract]
        string readXML();
    }
}
