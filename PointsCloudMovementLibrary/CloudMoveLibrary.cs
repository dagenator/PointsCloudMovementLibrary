using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace PointsCloudMovementLibrary
{
    class CloudMoveLibrary
    {
        public class Point3D
        {
            private Vector<double> V;

            public Point3D(Vector<double> v)
            {
                V = v;
            }

            public Point3D(double x, double y, double z)
            {
                V[0] = x;
                V[1] = y;
                V[2] = z;
            }

            public Vector<double> getVector()
            {
                return V;
            }

            public double getX()
            {
                return V.At(0);
            }

            public double getY()
            {
                return V.At(1);
            }

            public double getZ()
            {
                return V.At(2);
            }


        }




        private Matrix<double> CalculateMovementMatrix(Point3D startPoint, Point3D xEnd, Point3D yEnd, Point3D zEnd)
        {
            Func<Point3D, Point3D, Point3D> substract = (x, y) => new Point3D(x.getX() - y.getX(), x.getY() - y.getY(), x.getZ() - y.getZ());

            Point3D newX = substract(xEnd, startPoint);
            Point3D newY = substract(yEnd, startPoint);
            Point3D newZ = substract(zEnd, startPoint);

            Matrix<double> m = Matrix<double>.Build.DenseOfColumnVectors(newX.getVector(), newY.getVector(), newZ.getVector());
            return m;
        }

        private Point3D CalculateNewLocationOfOldBase(Point3D locationOfNewBasis, Matrix<double> reverseMatrix)
        {
            return new Point3D(reverseMatrix.Multiply(locationOfNewBasis.getVector()));
        }

        public Point3D[] CloudRecalculation(Point3D[] oldCloud, Point3D startPoint, Point3D xEnd, Point3D yEnd, Point3D zEnd)
        {
            List<Point3D> newCloud = new List<Point3D>();
            Matrix<double> M = CalculateMovementMatrix(startPoint, xEnd, yEnd, zEnd);
            Matrix<double> revM = M.Inverse();
            Point3D oldBaseInNewLoc = CalculateNewLocationOfOldBase(startPoint, revM);
            

            foreach (Point3D oldPoint in oldCloud)
            {
                var v1 =revM.Multiply(oldPoint.getVector());
                var res = v1 + oldBaseInNewLoc.getVector();

                newCloud.Add(new Point3D(res));
            }

            return newCloud.ToArray();
        }

        


    }
}
