using BrutusService.JavaEndpoint;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace BrutusService
{
    public sealed class Bruteforce : IDisposable
    {
        private static readonly Lazy<Bruteforce> lazy = new Lazy<Bruteforce>(() => new Bruteforce());
        public static Bruteforce Instance { get { return lazy.Value; } }

        List<Task> tasksList = new List<Task>();
        TupleList<string, CancellationTokenSource> taskListCancellationToken = new TupleList<string, CancellationTokenSource> { };

        ProjectEndpointClient service = new ProjectEndpointClient("projectEndpointhtt^p");

        //To java service end point

        /// <summary>
        /// Create Bruteforce task
        /// </summary>
        /// <param name="file"></param>
        /// <param name="c"></param>
        public void addTask(string file, string filename, CancellationTokenSource t_cancel)
        {
            //actual bruteforce loop
            tasksList.Add(Task.Factory.StartNew(() =>
            {
                taskListCancellationToken.Add(filename, t_cancel);
                Keygen k = new Keygen();

                //only stop when asked to stop
                Console.WriteLine("Creating bruteforce thread");
                
                //generate key in incremented size lenght
                k.keyEnum("", 1, 3, file, t_cancel);

                Console.WriteLine("finished");

            }, t_cancel.Token, TaskCreationOptions.AttachedToParent, TaskScheduler.Default));
        }

        public void killAll()
        {
            foreach(var s in taskListCancellationToken)
            {
                s.Item2.Cancel();
            }
        }

        public bool StopAndFinalize(int task_ID, string fileName, string final_message, string key, string email, double fiability)
        {
            if (taskListCancellationToken[task_ID].Item2 != null) { 
                taskListCancellationToken[task_ID].Item2.Cancel();
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            service.Close();
        }
    }

    /// <summary>
    /// Permits the use of coupled generic values in list format
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class TupleList<T1, T2> : List<Tuple<T1, T2>>
    {
        public void Add(T1 item, T2 item2)
        {
            Add(new Tuple<T1, T2>(item, item2));
        }
    }
}