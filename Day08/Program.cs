using System;
using System.IO;

namespace Day08
{
    public class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day08.txt");
        static void Main()
        {
            // Screen size = 6 tall by 50 wide
            char[,] screen = new char[6, 50];

            foreach (var line in input)
            {
                var vals = line.Split(new[] { " ", "x", "y", "=", "by", "rect", "rotate" }, StringSplitOptions.RemoveEmptyEntries);

                switch (vals.Length)
                {
                    case 2:
                        CreateRectangle(ref screen, int.Parse(vals[0]), int.Parse(vals[1]));
                        break;
                    case 3:
                        if (vals[0] == "row")
                            RotateRow(ref screen, int.Parse(vals[1]), int.Parse(vals[2]));
                        else
                            RotateColumn(ref screen, int.Parse(vals[1]), int.Parse(vals[2]));
                        break;
                    default:
                        break;
                }
            }

            // Get the total number of lit pixels
            int totalLit = 0;
            foreach (var item in screen)
                if (item == '#') totalLit++;

            Console.WriteLine($"Part 1: {totalLit}");

            Console.WriteLine($"\nPart 2:\n");
            PrintScreen(screen);
        }

        private static void CreateRectangle(ref char[,] screen, int width, int height)
        {
            for (int row = 0; row < height; row++)
                for (int col = 0; col < width; col++)
                    screen[row, col] = '#';
        }

        private static void RotateRow(ref char[,] screen, int row, int steps)
        {
            char[,] temp = (char[,])screen.Clone();
            int rowWidth = screen.GetLength(1);

            for (int i = 0; i < rowWidth; i++)
            {
                int index = (i + steps) % rowWidth;
                screen[row, index] = temp[row, i];
            }
        }

        private static void RotateColumn(ref char[,] screen, int col, int steps)
        {
            char[,] temp = (char[,])screen.Clone();
            int colWidth = screen.GetLength(0);

            for (int i = 0; i < colWidth; i++)
            {
                int index = (i + steps) % colWidth;
                screen[index, col] = temp[i, col];
            }
        }

        private static void PrintScreen(char[,] screen)
        {
            for (int row = 0; row < screen.GetLength(0); row++)
            {
                for (int col = 0; col < screen.GetLength(1); col++)
                {
                    Console.Write(screen[row, col]);
                }
                Console.WriteLine();
            }
        }
    }
}
