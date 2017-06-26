using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using dechifr_client.ServiceReference1;

namespace dechifr_client
{
    public sealed class BruteForce : ServiceReference1.MiddleWare
    {
        //thread safe singleton creation
        private static readonly Lazy<BruteForce> lazy = new Lazy<BruteForce>(() => new BruteForce());

        public static BruteForce Instance { get { return lazy.Value; } }

        List<Task> taskList = new List<Task>();
        
        public void addTask(string list, CancellationToken c)
        {
            //actual bruteforce loop
            taskList.Add(Task.Run(() => {
                //only stop when asked to s
                while(true)
                {
                    if(c.IsCancellationRequested){
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

        public string GetData(int value)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetDataAsync(int value)
        {
            throw new NotImplementedException();
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            throw new NotImplementedException();
        }

        public Task<CompositeType> GetDataUsingDataContractAsync(CompositeType composite)
        {
            throw new NotImplementedException();
        }
    }
}