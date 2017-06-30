using BrutusService.JavaEndpoint;
using System.Threading;
using System.Text;
using System;
using System.Threading.Tasks;

namespace BrutusService
{
    internal sealed class Keygen
    {
        //list of possible chars
        private static readonly string[] validChars = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

        static ProjectEndpointClient cc = new ProjectEndpointClient("projectEndpoint");

        public Keygen() { }

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

        /// <summary>
        /// Goes through every key in a range
        /// </summary>
        /// <param name="prefix">if you have the start of the key set it here</param>
        /// <param name="level">key length to begin with ex : 2 would start with key "aa"</param>
        /// <param name="maxlength">maximum key length</param>
        public void keyEnum(string prefix, int level, int maxlength, string msg, CancellationTokenSource f, string filename)
        {
            level += 1;
            foreach (string c in validChars)
            {
                if (f.IsCancellationRequested) { return; }

                string key = prefix + c;
                char[] n_key = key.ToCharArray();

                string decrypted_message = encryptDecrypt(msg, n_key);

                //Console.WriteLine("decrypted_message =  {0}", decrypted_message);
                //Console.WriteLine("key =  {0}", key);

                cc.queueOperation(decrypted_message, key, filename, "0");


               if (level < maxlength) keyEnum(prefix + c, level, maxlength, msg ,f, filename);
            }
        }
    }
}