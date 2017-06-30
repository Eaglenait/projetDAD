using System;
using System.Threading;

namespace BrutusService
{
    public class BrutusControl : IBrutusControl
    {
        BrutusControl() { }

        public void startBrutus(string encrypted_Message, string filename, string taskId, CancellationTokenSource t_cancel)
        {
            Bruteforce.Instance.addTask(encrypted_Message, filename, taskId, t_cancel); 
        }

        public void killBrutus(int task_ID)
        {
            Bruteforce.Instance.killAll(task_ID);
        }

        public bool stopBrutus(int task_ID, string fileName, string final_message, string key, string email, double fiability)
        {
            try
            {
                if (Bruteforce.Instance.StopAndFinalize(task_ID, fileName, final_message, key, email, fiability))
                {
                    return true;
                }
                return false;
            }catch(Exception e) { Console.WriteLine(e.Message); return false; }
        }
    }
}
