using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotsRUs;
using NUnit.Framework;


namespace RobotDeploymentTest
{
    [TestFixture]
    public class Tester
    {
        /// <summary>
        /// Tests the robot navigation with 2 inputs to determine if the output is correct
        /// </summary>
        public Tester()
        {
            TestRobot1();
            TestRobot2();
        }

        [Test]
        public void TestRobot1()
        {
            Robot robot = new Robot(7, 6);
            robot.PositionHeading = "2 4 E";
            robot.Instructions = "MMRMMRMRRM";
            robot.Navigate();
            Assert.AreEqual(robot.PositionHeading, "4 2 E");
        }

        [Test]
        public void TestRobot2()
        {
            Robot robot = new Robot(7, 6);
            robot.PositionHeading = "3 4 N";
            robot.Instructions = "LMLMLMLMM";
            robot.Navigate();
            Assert.AreEqual(robot.PositionHeading, "3 5 N");
        }



    }
}
