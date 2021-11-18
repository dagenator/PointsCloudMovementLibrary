using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace PointsCloudMovementLibrary.Tests
{
    [TestFixture]
    public class Point3DTests
    {
        public static IEnumerable<TestCaseData> AdditionEqualsTestCases
        {
            get
            {
                yield return new TestCaseData(new Point3D(1, 1, 1), new Point3D(1, 1, 1), new Point3D(2, 2, 2));
                yield return new TestCaseData(new Point3D(-1, -1, -1), new Point3D(1, 1, 1), new Point3D(0, 0, 0));
                yield return new TestCaseData(new Point3D(0.1, 1, 1), new Point3D(0.2, 1, 1), new Point3D(0.3, 2, 2));
            }
        }
        
        public static IEnumerable<TestCaseData> SubtractionEqualsTestCases
        {
            get
            {
                yield return new TestCaseData(new Point3D(2, 2, 2), new Point3D(1, 1, 1), new Point3D(1, 1, 1));
                yield return new TestCaseData(new Point3D(-1, -1, -1), new Point3D(1, 1, 1), new Point3D(-2, -2, -2));
            }
        }


        [Test]
        public void CloudRecalculateTestSimple()
        {
            var lib = new CloudMoveLibrary();
            var oldCloud = new Point3D[] { new Point3D(3, 4, 0), new Point3D(6, 3, 0), new Point3D(2, 1, 0) };
            var startPoint = new Point3D(1,1,0);
            var xEnd = new Point3D(2, 1, 0);
            var yEnd = new Point3D(1, 2, 0);
            var zEnd = new Point3D(1, 1, 1);

            var rightCloud = new Point3D[] { new Point3D(2, 3, 0), new Point3D(5, 2, 0), new Point3D(1, 0, 0) };
            var res = lib.CloudRecalculationByPointToPoint(oldCloud, startPoint, xEnd, yEnd, zEnd).Cast<Point3D>().ToArray();
            Assert.AreEqual(rightCloud, res);
        }

        [Test]
        public void CloudRecalculateTestAfinn()
        {
            var lib = new CloudMoveLibrary();
            var oldCloud = new Point3D[] { new Point3D(3, 5, 0), new Point3D(3, 1, 0)};
            var startPoint = new Point3D(2, 2, 0);
            var xEnd = new Point3D(4, 4, 0);
            var yEnd = new Point3D(1, 3, 0);
            var zEnd = new Point3D(2, 2, 1);

            var rightCloud = new Point3D[] { new Point3D(1, 1, 0), new Point3D(0, -1, 0)};
            var res = lib.CloudRecalculationByPointToPoint(oldCloud, startPoint, xEnd, yEnd, zEnd).Cast<Point3D>().ToArray();
            Assert.AreEqual( rightCloud, res);
        }

        [Test]
        public void CloudRecalculateTest3DSimple()
        {
            var lib = new CloudMoveLibrary();
            var oldCloud = new Point3D[] { new Point3D(2, 2, 2), new Point3D(5, 6, 4) };
            var startPoint = new Point3D(2, 2, 2);
            var xEnd = new Point3D(3,2,2);
            var yEnd = new Point3D(2,3,2);
            var zEnd = new Point3D(2,2,3);

            var rightCloud = new Point3D[] { new Point3D(0, 0, 0), new Point3D(3, 4, 2) };
            var res = lib.CloudRecalculationByPointToPoint(oldCloud, startPoint, xEnd, yEnd, zEnd).Cast<Point3D>().ToArray();
            Assert.AreEqual(rightCloud, res);
        }

        [Test]
        public void CloudRecalculateTest3DAfinn()
        {
            var lib = new CloudMoveLibrary();
            var oldCloud = new Point3D[] { new Point3D(3, 5, 4), new Point3D(3, 1, 2) };
            var startPoint = new Point3D(2, 2, 2);
            var xEnd = new Point3D(4, 4, 2);
            var yEnd = new Point3D(1, 3, 2);
            var zEnd = new Point3D(2, 2, 4);

            var rightCloud = new Point3D[] { new Point3D(1, 1, 1), new Point3D(0, -1, 0) };
            var res = lib.CloudRecalculationByPointToPoint(oldCloud, startPoint, xEnd, yEnd, zEnd).Cast<Point3D>().ToArray();
            Assert.AreEqual(rightCloud, res);
        }


        [TestCaseSource(nameof(SubtractionEqualsTestCases))]
        public void SubtractionTest(Point3D p1, Point3D p2, Point3D res)
        {
            Assert.AreEqual(p1 - p2, res);
        }

        [TestCaseSource(nameof(AdditionEqualsTestCases))]
        public void AddTest(Point3D p1, Point3D p2, Point3D res)
        {
            Assert.AreEqual(p1 + p2, res);
        }
    }
}