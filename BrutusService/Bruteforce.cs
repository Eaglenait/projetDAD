using BrutusService.JavaEndpoint;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System;

namespace BrutusService
{
    public sealed class Bruteforce : IDisposable
    {
        private static readonly Lazy<Bruteforce> lazy = new Lazy<Bruteforce>(() => new Bruteforce());
        public static Bruteforce Instance { get { return lazy.Value; } }

        private static readonly string[] validChars = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

        private static List<Task> taskList = new List<Task>();
        private static List<CancellationTokenSource> cont_taskList = new List<CancellationTokenSource>();

        private ProjectEndpointClient service = new ProjectEndpointClient("projectEndpoint");

        private static string encryptDecrypt(string input, char[] key)
        {
            char[] output = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = (char)(input[i] ^ key[i % key.Length]);
            }
            byte[] bytes = Encoding.Default.GetBytes(output);

            return Encoding.UTF8.GetString(bytes);
        }

        //To java service end point

        /// <summary>
        /// Create Bruteforce task
        /// </summary>
        /// <param name="file"></param>
        /// <param name="t_cancel"></param>
        public void addTask(string file, string filename, string taskID, CancellationTokenSource t_cancel)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cont_taskList.Add(cts);

            //actual bruteforce loop
            taskList.Add(Task.Run(() =>
            {

                /*
                string prefix = "";
                int level = 1;
                int maxlength = 6;

                while (cont)
                {
                    if (!cont) { return; }

                    level += 1;
                    foreach(string c in validChars)
                    {
                        if (!cont) { return; }
                        string key = prefix + c;
                        char[] n_key = key.ToCharArray();

                        string decrypted_message = encryptDecrypt(file, n_key);

                        //ensure message is sent
                        while (!service.queueOperation(decrypted_message, key, filename, taskID)) { }
                        
                    }
                }
                */

                Keygen k = new Keygen();
                if (cts.IsCancellationRequested) { return; }
                //generate key in incremented size lenght
                for(int i = 1; i < 6; ++i)
                {
                    k.keyEnum("", 1, i, file, cts,filename);
                }

            }));
        }

        public void killAll(int task_ID)
        {
            foreach(var s in cont_taskList)
            {
                s.Cancel();
            }
            
            /*
            try
            {
                taskListCancellationToken[task_ID].Item2.Cancel();
                taskListCancellationToken[task_ID].Item1.Dispose();
                taskListCancellationToken.RemoveAt(task_ID);
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
            */
        }

        public bool StopAndFinalize(int task_ID, string fileName, string final_message, string key, string email, double fiability)
        {
            new mailGen(final_message, key, fiability, email);
            killAll(0);
            /*try
            {
                taskListCancellationToken[task_ID].Item2.Cancel();
                taskListCancellationToken[task_ID].Item1.Dispose();
                taskListCancellationToken.RemoveAt(task_ID);
                return true;
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }*/

            return true;
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