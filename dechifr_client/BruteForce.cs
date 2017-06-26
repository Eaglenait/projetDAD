using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using dechifr_client.ServiceReference1;

namespace dechifr_client
{
    public sealed class BruteForce
    {
        readonly static char[] alpha = new char[26] {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};

        ProjectEndpointClient service = new ProjectEndpointClient();

        //thread safe singleton creation
        private static readonly Lazy<BruteForce> lazy = new Lazy<BruteForce>(() => new BruteForce());
        public static BruteForce Instance { get { return lazy.Value; } }

        List<Task> tasksList = new List<Task>();

        /// <summary>
        /// Create Bruteforce task
        /// </summary>
        /// <param name="file"></param>
        /// <param name="keysize">size of the key to test</param>
        /// <param name="c"></param>
        public void addTask(string file,int keysize, CancellationToken t_cancel)
        {
            //actual bruteforce loop
            tasksList.Add(Task.Factory.StartNew(() => {
                //only stop when asked to stop
                Console.WriteLine("Creating bruteforce thread");
                //index to loop the key
                UInt16 k_index = 0;
                char[] key = new char[keysize];

                key = genKey(keysize);

                while (true)
                {
                    if (t_cancel.IsCancellationRequested) { break; }

                    foreach(char c in file)
                    {
                        //xor here

                    }

                    //send data to java
                    service.queueOperation("message", "key", "filename");
                }
            }, t_cancel, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default));
        }

        private static char[] genKey(int keysize)
        {
            char[] key = new char[keysize];

            UInt16 pos = 0; //incremented char pos
            UInt16 alpha_at_pos = 0;
            //fist run return A
            if (key == null)
            {
                for (int i = 0; i < keysize; ++i)
                {
                    key[i] = alpha[0];
                }
                return key;
            }
            else
            {
                if(key[pos] != alpha[26])
                {
                    key[pos] = alpha[alpha_at_pos++];
                }
                else
                {
                    
                    pos++;
                }
                return key;
            }
        }

        /// <summary>
        /// Sends data to Java
        /// </summary>
        /// <param name="d"></param>
        /// <param name="k"></param>
        /// <param name="filename"></param>
        private void sendToJava(string d, string k, string filename)
        {
            service.queueOperation(d,k,filename);
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
        
        private static string encryptDecrypt(string input, char[] key)
        {
            char[] output = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = (char)(input[i] ^ key[i % key.Length]);
            }

            return new string(output);
        }
    }
}
