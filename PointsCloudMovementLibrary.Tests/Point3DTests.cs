using System.Collections.Generic;
using NUnit.Framework;

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
                // yield return new TestCaseData(new Point3D(0.1, 1, 1), new Point3D(0.2, 1, 1), new Point3D(0.3, 2, 2));
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