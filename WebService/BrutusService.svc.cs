using System;
using System.Threading;

namespace BrutusService
{
    public class BrutusService : IManageBruteForce
    {
        public BrutusService() {}

        public void killBrutus(int number, CancellationToken t_cancel)
        {
            throw new NotImplementedException();
        }

        public void StartBrutus(string file, CancellationToken t_cancel)
        {
            throw new NotImplementedException();
        }

        public void stopBrutus(int task_id, string final_message, string key, string email, double fiability)
        {
            throw new NotImplementedException();
        }
    }
}
