using dechifr_client.VerificationService;

namespace dechifr_client
{
    public sealed class Keygen
    {
        //list of possible chars
        private static readonly string[] validChars = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

        static ProjectEndpointClient cc = new ProjectEndpointClient();

        public Keygen() {}

        private static string encryptDecrypt(string input, char[] key)
        {
            char[] output = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = (char)(input[i] ^ key[i % key.Length]);
            }

            return new string(output);
        }

        /// <summary>
        /// Goes through every key in a range
        /// </summary>
        /// <param name="prefix">if you have the start of the key set it here</param>
        /// <param name="level">key length to begin with ex : 2 would start with key "aa"</param>
        /// <param name="maxlength">maximum key length</param>
        public void keyEnum(string prefix, int level, int maxlength, string msg)
        {
            level += 1;
            foreach (string c in validChars)
            {
                string key = prefix + c;
                char[] n_key = key.ToCharArray();

                
                string decrypted_message = encryptDecrypt(msg, n_key);
                //Console.WriteLine("decrypted_message =  {0}", decrypted_message);
                //Console.WriteLine("key =  {0}", key);

                cc.queueOperation(decrypted_message,key, "/test.txt");

                if (level < maxlength) keyEnum(prefix + c, level, maxlength, msg);
            }
        }
    }
}