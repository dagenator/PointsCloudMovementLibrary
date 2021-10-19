using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
MathNet.Numerics.LinearAlgebra.Vector;

namespace PointsCloudMovementLibrary
{
    class CloudMoveLibrary
    {
        class Point3D
        {
            public Vector<double> V { private set; get; }

            public Point3D(double x, double y, double z)
            {
                V[0] = x;
                V[1] = y;
                V[2] = z;
            }


        }

        public Matrix<double> MovementMatrixCalcutation(Point3D x0, double x1, double y0, double y1, double z0, double z1)
        {

        }




    }
}
