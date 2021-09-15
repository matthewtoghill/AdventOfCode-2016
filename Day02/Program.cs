using System;
using System.IO;

namespace Day02
{
    public class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day02.txt");
        static void Main()
        {
            Part1();
            Part2();
        }

        private static void Part1()
        {
            // Define the keypad layout
            var keypad = new[,] {
                { "X", "X", "X", "X", "X" },
                { "X", "1", "2", "3", "X" },
                { "X", "4", "5", "6", "X" },
                { "X", "7", "8", "9", "X" },
                { "X", "X", "X", "X", "X" },
            };

            string keyCode = ExtractKeyCode(keypad, input, 2, 2);
            Console.WriteLine($"Part 1: {keyCode}");
        }

        private static void Part2()
        {
            // Define the keypad layout
            var keypad = new[,] {
                { "X","X","1","X","X" },
                { "X","2","3","4","X" },
                { "5","6","7","8","9" },
                { "X","A","B","C","X" },
                { "X","X","D","X","X" }
            };

            string keyCode = ExtractKeyCode(keypad, input, 2, 0);
            Console.WriteLine($"Part 2: {keyCode}");
        }


        private static string ExtractKeyCode(string[,] keypad, string[] instructions, int startRow, int startCol)
        {
            string keyCode = "";
            int row = startRow, col = startCol;

            foreach (var line in instructions)
            {
                foreach (var c in line)
                {
                    int newRow = row, newCol = col;

                    // Update possible newRow and newCol values based on input character
                    switch (c)
                    {
                        case 'U': newRow--; break;
                        case 'D': newRow++; break;
                        case 'L': newCol--; break;
                        case 'R': newCol++; break;
                        default: break;
                    }

                    // Check new row/col are within bounds of the keypad array
                    if (newRow < 0 || newRow >= keypad.GetLength(0)) continue;
                    if (newCol < 0 || newCol >= keypad.GetLength(0)) continue;

                    // Update row and col values
                    if (keypad[newRow, newCol] != "X")
                    {
                        row = newRow;
                        col = newCol;
                    }
                }

                // Store the keypad value at the row/col once instruction line has completed
                keyCode += keypad[row, col];
            }

            return keyCode;
        }
    }
}
