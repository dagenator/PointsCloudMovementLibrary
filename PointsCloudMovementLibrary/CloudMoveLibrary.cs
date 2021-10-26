using System.Collections.Generic;
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
            Matrix<double> m =
                Matrix<double>.Build.DenseOfColumnVectors(newX.Vector, newY.Vector, newZ.Vector);
            return m;
        }

        private Point3D CalculateNewLocationOfOldBase(Point3D locationOfNewBasis, Matrix<double> reverseMatrix)
        {
            return new Point3D(reverseMatrix.Multiply(locationOfNewBasis.Vector));
        }

        public Point3D[] CloudRecalculation(Point3D[] oldCloud, Point3D startPoint, Point3D xEnd, Point3D yEnd,
            Point3D zEnd)
        {
            List<Point3D> newCloud = new List<Point3D>();
            Matrix<double> M = CalculateMovementMatrix(startPoint, xEnd, yEnd, zEnd);
            Matrix<double> revM = M.Inverse();
            Point3D oldBaseInNewLoc = CalculateNewLocationOfOldBase(startPoint, revM);


            foreach (Point3D oldPoint in oldCloud)
            {
                var v1 = revM.Multiply(oldPoint.Vector);
                var res = v1 + oldBaseInNewLoc.Vector;

                newCloud.Add(new Point3D(res));
            }

            return newCloud.ToArray();
        }
    }
}