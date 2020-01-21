using System;
using System.Collections.Generic;

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
            if (this.Direction == "N") { this.Direction = "E";  }
            if (this.Direction == "E") { this.Direction = "S";  }
            if (this.Direction == "S") { this.Direction = "W";  }
            
            // Current direction is west
            this.Direction = "N";
        }


        /// <summary>
        ///     Rotate left depending on the current position
        /// </summary>
        public void TurnLeft()
        {
            if (this.Direction == "N") { this.Direction = "W";  }
            if (this.Direction == "W") { this.Direction = "S";  }
            if (this.Direction == "S") { this.Direction = "E";  }

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
            if (this.Direction == "N" && this.North <= northBoundary) { this.North++; }

            if (this.Direction == "E" && this.East <= eastBoundary) { this.East++; }

            if (this.Direction == "S" && this.North > 1) { this.North--; }

            if (this.Direction == "W" && this.East > 1) { this.East--; }
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

        /// <summary>
        ///     Process the instructions for the 
        /// </summary>
        /// <param name="rover">Current rover</param>
        /// <param name="instructions">List of instructions</param>
        public static void ProcessInstructions (Rover rover, Grid grid, List<string> instructions)
        {
            var currentPosition = rover.CurrentPosition;

            foreach (var instruction in instructions)
            {
                if (instruction == "L") { currentPosition.TurnLeft(); }
                
                if (instruction == "R") { currentPosition.TurnRight(); }

                if (instruction == "M") { currentPosition.Advance(grid.EastBoundary, grid.NorthBoundary); }
            }

            rover.CurrentPosition = currentPosition;
        }
    }
   

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
