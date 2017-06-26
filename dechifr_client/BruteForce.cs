using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using dechifr_client.ServiceReference1;

namespace dechifr_client
{
    public sealed class BruteForce : ProjectEndpoint
    {
        ProjectEndpointClient service = new ProjectEndpointClient();
        
        //thread safe singleton creation
        private static readonly Lazy<BruteForce> lazy = new Lazy<BruteForce>(() => new BruteForce());
        public static BruteForce Instance { get { return lazy.Value; } }

        List<Task> taskList = new List<Task>();

        public void addTask(string file, CancellationToken c)
        {
            //actual bruteforce loop
            taskList.Add(Task.Run(() => {
                //only stop when asked to stop
                Console.WriteLine("Creating bruteforce thread");
                while (!c.IsCancellationRequested)
                {
                    sendToJava(file,"bauer tu pue", "nique le système");
                }
            },c));
        }

        private void sendToJava(string d, string k, string filename)
        {
            service.queueOperation(d,k,filename);
        }

        public queueOperationResponse queueOperation(queueOperationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<queueOperationResponse> queueOperationAsync(queueOperationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}