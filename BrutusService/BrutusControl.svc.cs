using System;
using System.Threading;

namespace BrutusService
{
    public class BrutusControl : IBrutusControl
    {
        
        private static object _brutusInstance;

        public void BrutusInstance(object instance)
        {
            _brutusInstance = instance;
        }

        public void killBrutus(int id, CancellationToken t_cancel)
        {
            throw new NotImplementedException();
        }

        public void startBrutus(string encrypted_Message, CancellationToken t_cancel)
        {
            throw new NotImplementedException();
        }

        public void stopBrutus(int task_Id, string final_message, string key, string email, double fiability)
        {
            throw new NotImplementedException();
        }
    }
}
