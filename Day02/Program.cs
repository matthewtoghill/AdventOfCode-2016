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
                { "1", "2", "3" },
                { "4", "5", "6" },
                { "7", "8", "9" },
            };

            string keyCode = ExtractKeyCode(keypad, input, 2, 2);
            Console.WriteLine($"Part 1: {keyCode}");
        }

        private static void Part2()
        {
            // Define the keypad layout
            var keypad = new[,] {
                { " "," ","1"," "," " },
                { " ","2","3","4"," " },
                { "5","6","7","8","9" },
                { " ","A","B","C"," " },
                { " "," ","D"," "," " }
            };

            string keyCode = ExtractKeyCode(keypad, input, 3, 1);
            Console.WriteLine($"Part 2: {keyCode}");
        }


        private static string ExtractKeyCode(string[,] keypad, string[] instructions, int startRow, int startCol)
        {
            string keyCode = "";

            // Get the Height and Width lengths of the keypad array
            int keypadHeight = keypad.GetLength(0);
            int keypadWidth = keypad.GetLength(1);

            // Check the start row and col are within the bounds of the keypad array
            int row = 0 < startRow && startRow <= keypadHeight ? startRow - 1 : 0;
            int col = 0 < startCol && startCol <= keypadWidth ? startCol - 1 : 0;

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
                    if (newRow < 0 || newRow >= keypadHeight) continue;
                    if (newCol < 0 || newCol >= keypadWidth) continue;

                    // Update row and col values
                    if (keypad[newRow, newCol] != " ")
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
