using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07
{
    public class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day07.txt");
        static void Main()
        {
            int totalTLS = 0, totalSSL = 0;

            // Check each line of the input and validate if they support TLS or SSL
            foreach (var line in input)
            {
                if (SupportsTLS(line)) totalTLS++;
                if (SupportsSSL(line)) totalSSL++;
            }

            Console.WriteLine($"Part 1: {totalTLS}");
            Console.WriteLine($"Part 2: {totalSSL}");
        }

        private static bool SupportsTLS(string ip) => HasABBA(ip);

        private static bool SupportsSSL(string ip) => HasABA(ip);

        private static bool HasABBA(string ip)
        {
            if (ip.Length < 4) return false;

            bool inHypernet = false;
            bool abbaFound = false;

            // Iterate through the string
            for (int i = 0; i < ip.Length - 3; i++)
            {
                // Get the substring of the next 4 characters
                string sub = ip.Substring(i, 4);
                char c = sub[0];

                // Set the Hypernet flag if an open square bracket is encountered
                if (c == '[') inHypernet = true;

                // Check if the substring is in the ABBA format
                if (sub[0] == sub[3] && sub[1] == sub[2] && sub[0] != sub[1])
                {
                    // If the ABBA substring was found in a Hypernet block then return false
                    if (inHypernet) return false;
                    abbaFound = true;
                }

                // End the Hypernet flag if a closed square bracket is encountered
                if (c == ']') inHypernet = false;
            }

            return abbaFound;
        }

        private static bool HasABA(string ip)
        {
            // Check minimum string length is met, i.e. aba[bab]
            if (ip.Length < 8) return false;

            // Store all ABA strings inside of Supernet sequences
            Dictionary<string, int> supernetABA = new Dictionary<string, int>();

            // Store all BAB strings inside of Hypernet sequences
            Dictionary<string, int> hypernetBAB = new Dictionary<string, int>();

            bool inHypernet = false;

            // Iterate through the string
            for (int i = 0; i < ip.Length - 2; i++)
            {
                // Get the substring of the next 3 characters
                string sub = ip.Substring(i, 3);
                char c = sub[0];

                if (c == '[') inHypernet = true;

                // Check the substring is in the ABA/BAB format and does not contain [ or ]
                if (sub[0] == sub[2] && sub[0] != sub[1] && !sub.Contains('[') && !sub.Contains(']'))
                {
                    if (inHypernet)
                        hypernetBAB.IncrementAt(sub);
                    else
                        supernetABA.IncrementAt(sub);
                }

                if (c == ']') inHypernet = false;
            }

            // Check each value in the supernetABA list
            foreach (var item in supernetABA)
            {
                // If the hypernetBAB list contains the equivalent BAB value then return true 
                if (hypernetBAB.ContainsKey($"{item.Key[1]}{item.Key[0]}{item.Key[1]}")) return true;
            }

            // No ABA, BAB combo found, return false
            return false;
        }
    }

    public static class DictionaryExtensions
    {
        public static void IncrementAt<T>(this Dictionary<T, int> dictionary, T key)
        {
            dictionary.TryGetValue(key, out int value);
            dictionary[key] = ++value;
        }
    }
}
