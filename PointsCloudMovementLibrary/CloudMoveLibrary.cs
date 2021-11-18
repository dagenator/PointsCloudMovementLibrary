using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace PointsCloudMovementLibrary
{
    public class CloudMoveLibrary
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
            return new Point3D(reverseMatrix.Multiply(-locationOfNewBasis.Vector));
        }

        /// <summary>
        /// Пересчет массива точек
        /// input  - координаты нового базиса в системе старого
        /// xEnd - координата конца вектора x (базисного вектора)
        /// </summary>
        /// <param name="oldCloud"></param>
        /// <param name="startPoint"></param>
        /// <param name="xEnd"></param>
        /// <param name="yEnd"></param>
        /// <param name="zEnd"></param>
        /// <returns></returns>
        public IEnumerable<Point3D> CloudRecalculationByPointToPoint(Point3D[] oldCloud, Point3D startPoint,
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

       
        /// <summary>
        /// Пересчет массива точек
        /// input  - матрица перемещения
        /// oldBaseInNewLocation = MovementVector
        /// </summary>
        /// <param name="oldCloud"></param>
        /// <param name="startPoint"></param>
        /// <param name="xEnd"></param>
        /// <param name="yEnd"></param>
        /// <param name="zEnd"></param>
        /// <returns></returns>
        public IEnumerable<Point3D> CloudRecalculationByMatrix(Point3D[] oldCloud, Matrix<double> movementMatrix, Point3D movementVector)
        {
            Matrix<double> reverseMatrix = movementMatrix.Inverse();

            foreach (Point3D oldPoint in oldCloud)
            {
                var vector = reverseMatrix.Multiply(oldPoint.Vector);
                var oldPointInNewBase = vector + movementVector.Vector;
                yield return new Point3D(oldPointInNewBase);
            }
        }
    }
}