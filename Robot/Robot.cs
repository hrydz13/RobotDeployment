using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RobotsRUs
{
    /// <summary>
    /// A robot class which can navigate to a set of coordinates and a heading
    /// based on an intial position and navigation instructions
    /// </summary>
    public class Robot
    {
        public enum CompassPoints
        {
            North = 0,
            East = 1,
            South = 2,
            West = 3
        }

        private int x;
        private int y;
        private int orientation;
        private int lowerX;
        private int lowerY;
        private int upperX;
        private int upperY;

        /// <summary>
        /// Initial Position (as x,y) and Heading (as a compass point)
        /// </summary>
        private string positionHeading;
        public string PositionHeading
        {
            get{
                return positionHeading;
            }
            set{
                positionHeading = value;
            }
        }

        /// <summary>
        /// Navigation instrictions
        /// </summary>
        private string instructions;
        public string Instructions
        {
            get{
                return instructions;
            }
            set{
                instructions = value;
            }
        }

        public Robot(int upperX, int upperY)
        {
            //Fixed lower bounds
            lowerX = 0;
            lowerY = 0;

            this.upperX = upperX;
            this.upperY = upperY;
        }

        /// <summary>
        /// Navigates to a new set of coordinates and heading based on instructions entered
        /// </summary>
        public void Navigate()
        {
            try
            {
                if (upperX != 0 && upperY != 0)
                {
                    GetInitialPosition();

                    string error = "";
                    instructions = instructions.ToUpper();

                    for (int i = 0; i < instructions.Length; i++)
                    {
                        char step = instructions[i];

                        switch (step)
                        {
                            case 'R'://rotate by 90 degrees clockwise
                                orientation++;
                                if (orientation == 4) orientation = 0;
                                break;
                            case 'L'://rotate by 90 degrees anti-clockwise
                                orientation--;
                                if (orientation == -1) orientation = 3;
                                break;
                            case 'M':
                                error = Move();
                                break;
                            default:
                                error = "Invalid step found.";
                                break;
                        }

                        if (error != "")
                            break;
                    }
                    if (error != "")
                        positionHeading = "Error: " + error;
                    else
                        positionHeading = string.Format("{0} {1} {2}", x, y, GetOrientation(orientation));
                }
            }
            catch
            {
                positionHeading = "Inavlid input.";
            }
        }

        /// <summary>
        /// Converts the initial position data to x,y coordinates and orientation
        /// </summary>
        private void GetInitialPosition()
        {
            //check validity
            if (positionHeading != "")
            {
                string[] values = positionHeading.Split(' ');
                if (values.GetLength(0) == 3)
                {
                    x = Convert.ToInt32(values[0]);
                    y = Convert.ToInt32(values[1]);
                    orientation = (int)GetOrientation(values[2]);
                }
            }
        }

        /// <summary>
        /// Moves one step in the current direction
        /// </summary>
        /// <returns>new x or y position</returns>
        private string Move()
        {
            string error = "";

            switch ((CompassPoints)orientation)
            {
                case CompassPoints.North:
                    y++;
                    if (y > upperY)
                        error = "Exceeded upper Y";
                    break;
                case CompassPoints.East:
                    x++;
                    if(x > upperX)
                        error = "Exceeded upper X";
                    break;
                case CompassPoints.South:
                    y--;
                    if(y < lowerY)
                        error = "Exceeded lower Y";
                    break;
                case CompassPoints.West:
                    x--;
                    if (x < lowerX)
                        error = "Exceeded lower X";
                    break;
                default:
                    break;
            }
            return error;
        }

        #region Helper Methods
        /// <summary>
        /// Gets the orientation as a CompassPoint
        /// </summary>
        /// <param name="orientation"></param>
        /// <returns>Compass point enum</returns>
        private CompassPoints GetOrientation(string orientation)
        {
            orientation = orientation.ToUpper();

            CompassPoints compassPoint = CompassPoints.North;

            switch (orientation)
            {
                case "N":
                    compassPoint = CompassPoints.North;
                    break;
                case "E":
                    compassPoint = CompassPoints.East;
                    break;
                case "S":
                    compassPoint = CompassPoints.South;
                    break;
                case "W":
                    compassPoint = CompassPoints.West;
                    break;
                default:
                    break;
            }
            return compassPoint;
        }

        /// <summary>
        /// Gets the orientation as a string
        /// </summary>
        /// <param name="orientation"></param>
        /// <returns></returns>
        private string GetOrientation(int orientation)
        {
            string result = "";

            switch ((CompassPoints)orientation)
            {
                case CompassPoints.North:
                    result = "N";
                    break;
                case CompassPoints.East:
                    result = "E";
                    break;
                case CompassPoints.South:
                    result = "S";
                    break;
                case CompassPoints.West:
                    result = "W";
                    break;
                default:
                    break;
            }

            return result;
        }
        #endregion Helper Methods
    }
}
