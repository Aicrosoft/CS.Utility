using System;
using CS.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CS.UtilityTests.Utils
{

    [TestClass]
    public class GpsTestgs
    {


        [TestMethod]
        public void DistanceTest()
        {
            CodeTimer.Time("DistanceTest",100000, () =>
            {
                var dis0 = GpsHelper.GetDistance(new GpsPoint(0, 90.0), new GpsPoint(0, 89.9999));
            });
            var dis1 = GpsHelper.GetDistance(new GpsPoint(0, 90.0), new GpsPoint(0, 89.9999));
            var dis2 = GpsHelper.GetDistance(new GpsPoint(0, 0.0), new GpsPoint(0, 0.0001));
            Console.WriteLine(dis1);
            Console.WriteLine(dis2);
            Assert.AreEqual(dis2, 0.0111);
        }

    }
}