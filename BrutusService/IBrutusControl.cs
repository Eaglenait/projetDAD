using System.ServiceModel;
using System.Threading;

namespace BrutusService
{
    [ServiceContract]
    public interface IBrutusControl
    {
        [OperationContract]
        void startBrutus(string encrypted_Message, string filename, string taskId, CancellationTokenSource t_cancel);

        [OperationContract]
        bool stopBrutus(int task_ID, string fileName, string final_message, string key, string email, double fiability);

        [OperationContract]
        void killBrutus(int task_ID);
    }
}
