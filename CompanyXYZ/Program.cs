﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyXYZCodingTest
{
    /// <summary>
    ///     Position encapsulation
    /// </summary>
    public class Position
    {
        public int East;
        public int North;
        public string Direction;

        /// <summary>
        /// Designated constructor
        /// </summary>
        /// <param name="east">Eastward position based on </param>
        /// <param name="north"></param>
        /// <param name="direction"></param>
        public Position(int east, int north, string direction)
        {
            this.East = east;
            this.North = north;
            this.Direction = direction;
        }

        
        /// <summary>
        ///     Rotate right depending on current direction
        /// </summary>
        public void TurnRight()
        {
            if (this.Direction == "N") { this.Direction = "E";  return; }
            if (this.Direction == "E") { this.Direction = "S";  return; }
            if (this.Direction == "S") { this.Direction = "W";  return; }
            
            // Current direction is west
            this.Direction = "N";
        }


        /// <summary>
        ///     Rotate left depending on the current position
        /// </summary>
        public void TurnLeft()
        {
            if (this.Direction == "N") { this.Direction = "W";  return; }
            if (this.Direction == "W") { this.Direction = "S";  return; }
            if (this.Direction == "S") { this.Direction = "E";  return; }

            // Current direction is East
            this.Direction = "N";
        }

        /// <summary>
        ///     Advance forward depending on current direction
        /// </summary>
        /// <param name="northBoundary"></param>
        /// <param name="eastBoundary"></param>
        public void Advance(int northBoundary, int eastBoundary)
        {
            if (this.Direction == "N" && this.North < northBoundary) { this.North++; }

            if (this.Direction == "E" && this.East < eastBoundary) { this.East++; }

            if (this.Direction == "S" && this.North >= 1) { this.North--; }

            if (this.Direction == "W" && this.East >= 1) { this.East--; }
        }
    }

    /// <summary>
    ///     Represents current grid position
    /// </summary>
    public class Grid
    {
        public int EastBoundary;
        public int NorthBoundary;

        /// <summary>
        ///     Designated constructor
        /// </summary>
        /// <param name="east">East boundary of grid</param>
        /// <param name="west">West boundary of grid</param>
        public Grid(int east, int west)
        {
            this.EastBoundary = east;
            this.NorthBoundary = west;
        }

    }

    /// <summary>
    ///     Class to represent a mars rover
    /// </summary>
    public class Rover
    {
        public Position CurrentPosition;

        /// <summary>
        ///     Designated constructor
        /// </summary>
        /// <param name="position">Initial starting position for rover</param>
        /// <param name="grid">Grid containing world boundaries</param>
        public Rover(Position position)
        {
            this.CurrentPosition = position;
        }

        public override string ToString()
        {
            var pos = this.CurrentPosition;
            return $"{pos.East} {pos.North} {pos.Direction}";
        }

        /// <summary>
        ///     Process the instructions for the 
        /// </summary>
        /// <param name="rover">Current rover</param>
        /// <param name="instructions">List of instructions</param>
        public static Rover ProcessInstructions (Rover rover, Grid grid, List<char> instructions)
        {
            var currentPosition = rover.CurrentPosition;

            foreach (var instruction in instructions)
            {
                if (instruction == 'L') { currentPosition.TurnLeft(); }
                
                if (instruction == 'R') { currentPosition.TurnRight(); }

                if (instruction == 'M') { currentPosition.Advance(grid.EastBoundary, grid.NorthBoundary); }
            }

            rover.CurrentPosition = currentPosition;

            return rover;
        }
    }

    public class InputProcessor
    {
        public  Grid SetupGrid(string inputOne, string inputTwo)
        {
            return new Grid(int.Parse(inputOne), int.Parse(inputTwo));
        }

        public  Rover CaptureRover(string inputOne, string inputTwo, string inputThree)
        {
            var position = new Position(int.Parse(inputOne), int.Parse(inputTwo), inputThree);
            return new Rover(position);
        }
    }

    class Program
    {
        static void Main()
        {
            var processor = new InputProcessor();

            // Take first line to setup grid
            var firstLine = Console.ReadLine().Split(" ");

            if (firstLine[0] == "\n" || firstLine.Length != 0)
            {
                var grid = processor.SetupGrid(firstLine[0], firstLine[1]);

                var rovers = new List<Rover>();

                while (Console.In.Peek() != -1)
                {
                    // Capture current rover and position
                    var roverLine = Console.ReadLine().Split(" ");
                    var rover = processor.CaptureRover(roverLine[0], roverLine[1], roverLine[2]);

                    var instructions = Console.ReadLine().ToCharArray().ToList();

                    var updatedRover = Rover.ProcessInstructions(rover, grid, instructions);

                    // Add updated rover to the list
                    rovers.Add(updatedRover);
                }


                // Print each captured rover
                foreach(var rover in rovers)
                {
                    Console.WriteLine(rover);
                }

            }
        }
    }
}
