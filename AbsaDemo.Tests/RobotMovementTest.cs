using System;
using System.Collections.Generic;
using System.Reflection;
using AbsaDemo.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbsaDemo.Tests
{

    [TestClass]
    public class RobotMovementTest
    {

        private Robot _robot;

        [TestInitialize]
        public void Setup()
        {
            _robot = new Robot();
        }




        [TestMethod]
        public void If_DirectionIsEmptyOrNull_ThrowException()
        {
            try
            {
                _robot.StartMovement("");
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "direction is required.");
            }


        }

        [TestMethod]
        public void GetCoordinateAndNumberOfSteps_IsEmptyOrNull_ThrowException()
        {
            try
            {
                MethodInfo methodInfo = typeof(Robot).GetMethod("GetCoordinateWithNumberOfSteps", BindingFlags.NonPublic | BindingFlags.Instance);
                object[] parameters = { "" };
                var selectedTuple = methodInfo.Invoke(_robot, parameters);
                Assert.IsNotNull(selectedTuple);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.InnerException.Message, "cooredinate must not be empty.");
            }


        }

        //[TestMethod]
        [DataTestMethod]
        [DataRow("N4,E2,S2,W4")]
        public void AddValidDirection_ReturnsToTal(string direction)
        {
            MethodInfo methodInfo = typeof(Robot).GetMethod("GetDirection", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { direction };
            var me = (List<string>)methodInfo.Invoke(_robot, parameters);
            Assert.AreEqual(me.Count, 4);
        }

        //[TestMethod]
        [DataTestMethod]
        [DataRow("N4")]
        public void GetCoordinateAndNumberOfSteps_ReturnsCoordinateAndSteps(string directionAndNumberOfSteps)
        {

            MethodInfo methodInfo = typeof(Robot).GetMethod("GetCoordinateWithNumberOfSteps", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { directionAndNumberOfSteps };
            (string coordinate, string numberOfSteps) = (ValueTuple<string, string>)methodInfo.Invoke(_robot, parameters);

            Assert.AreEqual(coordinate, "N");
            Assert.AreEqual(numberOfSteps, "4");
        }
    }
}
