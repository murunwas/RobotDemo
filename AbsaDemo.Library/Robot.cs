using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AbsaDemo.Library
{
    public class Robot
    {
        private readonly List<Movement> listOfMovements;
        private int numberOfRightTurns = 0;

        public Robot()
        {
            listOfMovements = new List<Movement>();
        }
        private List<string> GetDirection(string direction)
        {
            if (string.IsNullOrEmpty(direction))
            {
                throw new Exception($"{nameof(direction)} is required.");
            }

            return direction.Trim().Split(',').ToList();

        }

        private (string coordinate, string steps) GetCoordinateWithNumberOfSteps(string cooredinate)
        {
            if (string.IsNullOrEmpty(cooredinate))
            {
                throw new Exception($"{nameof(cooredinate)} must not be empty.");
            }

            var coord = Regex.Match(cooredinate, @"[a-zA-Z]+").Value;

            if (!string.IsNullOrEmpty(coord))
            {
                coord = coord.ToUpper();
            }

            var steps = Regex.Match(cooredinate, @"\d+").Value;

            return (coord, steps);
        }

        private void AddNumberOfBlocksVisited(string direction, int numberOfSteps)
        {
            for (int i = 1; i <= numberOfSteps; i++)
            {

                if (listOfMovements.Any())
                {
                    var last = listOfMovements.LastOrDefault();
                    switch (direction)
                    {
                        case AppConstants.NORTH:
                            AddCoordinates(last.Longitude + 1, last.Latitude);
                            break;
                        case AppConstants.EAST:
                            AddCoordinates(last.Longitude, last.Latitude + 1);
                            break;
                        case AppConstants.SOUTH:
                            AddCoordinates(last.Longitude - 1, last.Latitude);
                            break;
                        case AppConstants.WEST:
                            AddCoordinates(last.Longitude, last.Latitude - 1);
                            break;
                    }
                }
                else
                {
                    AddCoordinates(1, 1);
                }

            }


        }

        private void AddCoordinates(int longitude, int latitude)
        {
            listOfMovements.Add(new Movement
            {
                Longitude = longitude,
                Latitude = latitude
            });
        }


        public void StartMovement(string direction)
        {
            GetDirection(direction).ForEach(x => {

                (string dir, string steps) = GetCoordinateWithNumberOfSteps(x);

                if (dir.Equals(AppConstants.EAST))
                {
                    numberOfRightTurns += 1;
                }

                if (int.TryParse(steps, out int numberOfsteps))
                {
                    AddNumberOfBlocksVisited(dir, numberOfsteps);
                }

            });

            var count = listOfMovements.GroupBy(x => x.GetCoordinates()).Count();

            //Console.WriteLine($"Number of blocks visited: {listOfMovements.Count}");
            Console.WriteLine($"Number of unique blocks visited: {count}");
            Console.WriteLine($"Number of right turns: {numberOfRightTurns}");
        }

    }
}
