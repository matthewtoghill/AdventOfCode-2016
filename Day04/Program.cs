using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day04
{
    public class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day04.txt");
        static void Main()
        {
            // Create List of Rooms from input
            List<Room> rooms = input.Select(line => new Room(line)).ToList();

            // Part 1: Sum of Sector IDs for all Valid Rooms
            Console.WriteLine($"Part 1: {rooms.Where(r => r.IsValid).Sum(r => r.SectorID)}");

            // Part 2: Sector ID of North Pole Room
            Console.WriteLine($"Part 2: {rooms.FirstOrDefault(r => r.Name.Contains("northpole")).SectorID}");
        }
    }
}
