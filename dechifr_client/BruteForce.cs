using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace dechifr_client
{
    public sealed class BruteForce 
    {
        //thread safe singleton creation
        private static readonly Lazy<BruteForce> lazy = new Lazy<BruteForce>(() => new BruteForce());

        public static BruteForce Instance { get { return lazy.Value; } }

        List<Task> taskList = new List<Task>();
        
        public void addTask(string list, CancellationToken c)
        {
            //actual bruteforce loop
            taskList.Add(Task.Run(() => {
                //only stop when asked to stop
                while (true)
                {
                    if (c.IsCancellationRequested) {
                        break; //get out of loop
                    }
                }
            }));
        }

        /// <summary>
        /// generate a shit-ton of keys to be tested by a brute force task
        /// </summary>
        /// <returns>a list of random keys</returns>
        private List<string> getRandKey()
        {
            List<string> keys = new List<string>(100);

            return keys;
        }
    }
}