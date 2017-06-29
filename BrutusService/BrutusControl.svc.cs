using System;
using System.Threading;

namespace BrutusService
{
    public class BrutusControl : IBrutusControl
    {
        BrutusControl() { }

        public void startBrutus(string encrypted_Message, string filename, CancellationTokenSource t_cancel)
        {
            Bruteforce.Instance.addTask(encrypted_Message, filename, t_cancel); 
        }

        public void killBrutus()
        {
            Bruteforce.Instance.killAll();
        }


        public bool stopBrutus(int task_ID, string fileName, string final_message, string key, string email, double fiability)
        {
            Console.WriteLine("TEST");
            if(Bruteforce.Instance.StopAndFinalize(task_ID,fileName, final_message, key, email, fiability))
            {
                //mailGen mg = new mailGen(final_message, key, fiability, email) ;
                return true;
            }
            return false;
        }
    }
}
