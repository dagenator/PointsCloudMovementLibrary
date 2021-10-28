using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace PointsCloudMovementLibrary
{
    class CloudMoveLibrary
    {
        private Matrix<double> CalculateMovementMatrix(Point3D startPoint, Point3D xEnd, Point3D yEnd, Point3D zEnd)
        {
            Point3D newX = xEnd - startPoint;
            Point3D newY = yEnd - startPoint;
            Point3D newZ = zEnd - startPoint;
            return Matrix<double>.Build.DenseOfColumnVectors(newX.Vector, newY.Vector, newZ.Vector);
        }

        private Point3D CalculateNewLocationOfOldBase(Point3D locationOfNewBasis, Matrix<double> reverseMatrix)
        {
            return new Point3D(reverseMatrix.Multiply(locationOfNewBasis.Vector));
        }

        public IEnumerable<Point3D> CloudRecalculation(Point3D[] oldCloud, Point3D startPoint,
                                                            Point3D xEnd, Point3D yEnd, Point3D zEnd)
        {
            Matrix<double> movementMatrix = CalculateMovementMatrix(startPoint, xEnd, yEnd, zEnd);
            Matrix<double> reverseMatrix = movementMatrix.Inverse();
            Point3D oldBaseInNewLocation = CalculateNewLocationOfOldBase(startPoint, reverseMatrix);

            foreach (Point3D oldPoint in oldCloud)
            {
                var vector = reverseMatrix.Multiply(oldPoint.Vector);
                var oldPointInNewBase = vector + oldBaseInNewLocation.Vector;
                yield return new Point3D(oldPointInNewBase);
            }
        }
    }
}