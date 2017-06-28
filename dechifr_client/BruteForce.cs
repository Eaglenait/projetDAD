using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using dechifr_client.BrutusControl;
using dechifr_client.VerificationService;
using System.IO;

namespace dechifr_client
{
    public sealed class BruteForce
    {
        //thread safe singleton creation
        private static readonly Lazy<BruteForce> lazy = new Lazy<BruteForce>(() => new BruteForce());
        public static BruteForce Instance { get { return lazy.Value; } }

       List<Task> tasksList = new List<Task>();

        ProjectEndpointClient service = new ProjectEndpointClient();

        /// <summary>
        /// Create Bruteforce task
        /// </summary>
        /// <param name="file"></param>
        /// <param name="c"></param>
        public void addTask(string file, CancellationToken t_cancel)
        {

            //actual bruteforce loop
            tasksList.Add(Task.Factory.StartNew(() =>
            {
                Keygen k = new Keygen();

                //only stop when asked to stop
                Console.WriteLine("Creating bruteforce thread");

                k.keyEnum("", 1, 6, file);

                Console.WriteLine("finished");

            }, t_cancel, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default));
            
        }

        /// <summary>
        /// Sends data to Java
        /// </summary>
        /// <param name="d"></param>
        /// <param name="k"></param>
        /// <param name="filename"></param>
        private void sendToJava(string d, string k, string filename)
        {
            service.queueOperation(d, k, filename);
        }

        /// <summary>
        /// Kill task by id
        /// </summary>
        /// <param name="i"></param>
        public void killTask(int i)
        {

        }

        /// <summary>
        /// Kill task by filname
        /// </summary>
        /// <param name="filename"></param>
        public void killTask(string filename)
        {

        }

        /// <summary>
        /// XOR encryption or decryption
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string encryptDecrypt(string input, char[] key)
        {
            char[] output = new char[input.Length];

            for(int i = 0; i < input.Length; ++i)
            {
                output[i] = (char)(input[i] ^ key[i % key.Length]);
            }

            return new string(output);
        }
    }
}
