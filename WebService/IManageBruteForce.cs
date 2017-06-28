using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading;

namespace BrutusService
{
   [ServiceContract]
    public interface IManageBruteForce
    {
        /// <summary>
        /// kill the task
        /// </summary>
        /// <param name="number"></param>
        /// <param name="t_cancel"></param>
        [OperationContract]
        void killBrutus(int number, CancellationToken t_cancel);

        /// <summary>
        /// Stops brutus from java
        /// </summary>
        /// <param name="task_id"></param>
        /// <param name="final_message"></param>
        /// <param name="key"></param>
        /// <param name="email"></param>
        /// <param name="fiability"></param>
        [OperationContract]
        void stopBrutus(int task_id, string final_message, string key, string email, double fiability);

        /// <summary>
        /// start a java instance
        /// </summary>
        /// <param name="file"></param>
        /// <param name="t_cancel"></param>
        [OperationContract]
        void StartBrutus(string file, CancellationToken t_cancel);

        //[OperationContract]

    }
}
