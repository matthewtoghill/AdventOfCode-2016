using System;
using System.Collections.Generic;
using System.IO;

namespace Day01
{
    public class Program
    {
        public enum CardinalDirection
        {
            North,
            East,
            South,
            West
        }

        private static readonly string input = File.ReadAllText(@"..\..\..\data\day01.txt");
        static void Main()
        {
            Part1();
            Part2();
        }

        private static void Part1()
        {
            string[] directions = input.Split(new[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);

            int x = 0, y = 0;
            CardinalDirection cardinalDirection = CardinalDirection.North;

            foreach (var direction in directions)
            {
                string dir = direction.Substring(0, 1);
                int blocks = int.Parse(direction.Substring(1));
                cardinalDirection = GetNewCardinalDirection(cardinalDirection, dir);
                GetNewCoordinates(ref x, ref y, cardinalDirection, blocks);
            }

            Console.WriteLine($"Part 1: {Math.Abs(x) + Math.Abs(y)}");
        }

        private static void Part2()
        {
            HashSet<(int, int)> locations = new HashSet<(int, int)>();
            string[] directions = input.Split(new[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);

            int x = 0, y = 0;
            CardinalDirection cardinalDirection = CardinalDirection.North;

            foreach (var direction in directions)
            {
                string dir = direction.Substring(0, 1);
                int blocks = int.Parse(direction.Substring(1));

                cardinalDirection = GetNewCardinalDirection(cardinalDirection, dir);

                for (int steps = 0; steps < blocks; steps++)
                {
                    GetNewCoordinates(ref x, ref y, cardinalDirection, 1);

                    // Check if the location has been visited already, Add to locations set if new
                    if (locations.Contains((x, y)))
                    {
                        Console.WriteLine($"Part 2: {Math.Abs(x) + Math.Abs(y)}");
                        return;
                    }
                    else
                    {
                        locations.Add((x, y));
                    }
                }
            }
        }

        private static void GetNewCoordinates(ref int x, ref int y, CardinalDirection cardinalDirection, int blocks)
        {
            switch (cardinalDirection)
            {
                case CardinalDirection.North: y += blocks; break;
                case CardinalDirection.East: x += blocks; break;
                case CardinalDirection.South: y -= blocks; break;
                case CardinalDirection.West: x -= blocks; break;
                default: break;
            }
        }

        private static CardinalDirection GetNewCardinalDirection(CardinalDirection cardinalDirection, string dir)
        {
            switch (dir)
            {
                case "L":
                    cardinalDirection = cardinalDirection == CardinalDirection.North ? CardinalDirection.West : cardinalDirection - 1;
                    break;
                case "R":
                    cardinalDirection = cardinalDirection == CardinalDirection.West ? CardinalDirection.North : cardinalDirection + 1;
                    break;
                default:
                    break;
            }

            return cardinalDirection;
        }
    }
}
