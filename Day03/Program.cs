using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03
{
    public class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day03.txt");
        static void Main()
        {
            Part1();
            Part2();
        }

        private static void Part1()
        {
            int validCount = 0;
            foreach (var line in input)
            {
                var sides = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                if (IsValidTriangle(int.Parse(sides[0]), int.Parse(sides[1]), int.Parse(sides[2])))
                    validCount++;
            }
            Console.WriteLine($"Part 1: {validCount}");
        }

        private static void Part2()
        {
            int validCount = 0;

            // Iterate through the rows of the input in blocks of 3 rows at a time
            for (int row = 0; row < input.Length; row += 3)
            {
                var rowA = input[row].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var rowB = input[row + 1].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var rowC = input[row + 2].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                // Check each triangle from the block of 3 rows
                // Triangles are grouped by column rather than row within the block of 3 rows
                // e.g. Triangle 1 = rowA[col 0], rowB[col 0], rowC[col 0]

                for (int col = 0; col < 3; col++)
                    if (IsValidTriangle(int.Parse(rowA[col]), int.Parse(rowB[col]), int.Parse(rowC[col])))
                        validCount++;

            }

            Console.WriteLine($"Part 2: {validCount}");
        }

        /// <summary>
        /// A triangle is valid if the sum of the 2 smallest side lengths are
        /// greater than the length of the largest side
        /// </summary>
        private static bool IsValidTriangle(int sideA, int sideB, int sideC)
        {
            List<int> sides = new List<int>() { sideA, sideB, sideC };
            sides.Sort();
            return sides[0] + sides[1] > sides[2];
        }

    }
}
