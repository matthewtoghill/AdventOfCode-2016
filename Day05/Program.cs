using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Day05
{
    public class Program
    {
        private static readonly string input = "reyedfim";
        static void Main()
        {
            Part1();
            Part2();
        }

        private static void Part1()
        {
            string key = input;
            MD5 md5 = MD5.Create();
            int val = 0;
            string password = "";

            while (password.Length < 8)
            {
                byte[] hashBytes = CreateMD5HashBytes(ref md5, string.Format(key + val));

                string hash = ByteArrayToString(hashBytes, 3);

                if (hash.StartsWith("00000"))
                {
                    password += hash.Substring(5);
                }

                val++;
            }

            Console.WriteLine($"Part 1: {password}");
        }

        private static void Part2()
        {
            string key = input;
            MD5 md5 = MD5.Create();
            int val = 0;
            var passwordArray = new string('_', 8).ToArray();

            while (passwordArray.Contains('_'))
            {
                byte[] hashBytes = CreateMD5HashBytes(ref md5, string.Format(key + val));

                string hash = ByteArrayToString(hashBytes, 4);

                if (hash.StartsWith("00000") && int.TryParse(hash.Substring(5, 1), out int index) && index < 8 && passwordArray[index] == '_')
                {
                    passwordArray[index] = hash[6];
                    Console.WriteLine($"{string.Concat(passwordArray)}");
                }

                val++;
            }

            Console.WriteLine($"Part 2: {string.Concat(passwordArray)}");
        }

        private static byte[] CreateMD5HashBytes(string input)
        {
            MD5 md5 = MD5.Create();
            return CreateMD5HashBytes(ref md5, input);
        }

        private static byte[] CreateMD5HashBytes(ref MD5 md5, string input)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return hashBytes;
        }

        private static string ByteArrayToString(byte[] array)
        {
            return ByteArrayToString(array, array.Length);
        }

        private static string ByteArrayToString(byte[] array, int arrayItemsToConvert)
        {
            if (array.Length < arrayItemsToConvert) arrayItemsToConvert = array.Length;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < arrayItemsToConvert; i++)
            {
                sb.Append(array[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
