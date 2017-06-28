using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;

namespace BrutusService
{
    [ServiceContract]
    public interface IBrutusControl
    {
        [OperationContract]
        void BrutusInstance(object instance);

        [OperationContract]
        void startBrutus(string encrypted_Message, CancellationToken t_cancel);

        [OperationContract]
        void stopBrutus(int task_Id, string final_message, string key, string email, double fiability);

        [OperationContract]
        void killBrutus(int id, CancellationToken t_cancel);

    }
}
