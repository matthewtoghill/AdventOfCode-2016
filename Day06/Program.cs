using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day06
{
    public class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day06.txt");
        static void Main()
        {
            // Create a collection of dictionaries to hold the count of each letter found
            // Each dictionary represents a different column from the input data
            var columnCharacterFrequencies = new List<Dictionary<char, int>>();
            for (int i = 0; i < input[0].Length; i++)
                columnCharacterFrequencies.Add(new Dictionary<char, int>());

            foreach (var line in input)
            {
                // Check each character from the line
                for (int i = 0; i < line.Length; i++)
                {
                    // Update the dictionary for column i by incrementing the value for the character key
                    columnCharacterFrequencies[i].IncrementAt(line[i]);
                }
            }

            string maxValString = "";
            string minValString = "";

            // Check each column dictionary, get the Keys based on the Max and Min Values
            foreach (var item in columnCharacterFrequencies)
            {
                // Part 1 - Max Value per Dictionary
                var maxVal = item.OrderByDescending(d => d.Value).Take(1).Select(d => d.Key).ToArray();
                maxValString += new string(maxVal);

                // Part 2 - Min Value per Dictionary
                var minVal = item.OrderBy(d => d.Value).Take(1).Select(d => d.Key).ToArray();
                minValString += new string(minVal);
            }

            Console.WriteLine($"Part 1: {maxValString}");
            Console.WriteLine($"Part 2: {minValString}");
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
