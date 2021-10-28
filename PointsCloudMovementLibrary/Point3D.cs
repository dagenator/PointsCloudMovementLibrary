using System;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace PointsCloudMovementLibrary
{
    public class Point3D
    {
        public Vector<double> Vector { get; }

        public double X
        {
            get => Vector[0];
            set => Vector[0] = value;
        }

        public double Y
        {
            get => Vector[1];
            set => Vector[1] = value;
        }

        public double Z
        {
            get => Vector[2];
            set => Vector[2] = value;
        }

        public Point3D(Vector<double> vector)
        {
            Vector = vector;
        }

        public Point3D(double x, double y, double z)
        {
            Vector = Vector<double>.Build.DenseOfArray(new [] { x, y, z });
        }

        public static Point3D operator +(Point3D p1, Point3D p2)
        {
            return new Point3D(p1.Vector + p2.Vector);
        }

        public static Point3D operator -(Point3D p1, Point3D p2)
        {
            return new Point3D(p1.Vector - p2.Vector);
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }

        public override bool Equals(object other)
        {
            return other is Point3D point3D && Vector.AlmostEqual(point3D.Vector,0.000000001);
        }

        public override int GetHashCode()
        {
            return Vector.GetHashCode();
        }
    }
}