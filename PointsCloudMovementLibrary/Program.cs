using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PointsCloudMovementLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input file path with point cloud");
            String path =  Console.ReadLine();
            
            Console.WriteLine("Input location of new base in old coordinate, like \"x y z\"");
            var tempArr = Console.ReadLine().Split();
            var newBase =  new Point3D(Double.Parse(tempArr[0]), Double.Parse( tempArr[1]), Double.Parse( tempArr[2]));
            
            Console.WriteLine("Input location of X vector end point of new base's vectors in old coordinate, like \"x y z\"");
            tempArr = Console.ReadLine().Split();
            var xEnd =  new Point3D(Double.Parse(tempArr[0]), Double.Parse( tempArr[1]), Double.Parse( tempArr[2]));
            
            Console.WriteLine("Input location of Y vector end point of new base's vectors in old coordinate, like \"x y z\"");
            tempArr = Console.ReadLine().Split();
            var yEnd =  new Point3D(Double.Parse(tempArr[0]), Double.Parse( tempArr[1]), Double.Parse( tempArr[2]));
            
            Console.WriteLine("Input location of Z vector end point of new base's vectors in old coordinate, like \"x y z\"");
            tempArr = Console.ReadLine().Split();
            var zEnd =  new Point3D(Double.Parse(tempArr[0]), Double.Parse( tempArr[1]), Double.Parse( tempArr[2]));

            StreamReader reader = new  StreamReader(path);
            List<Point3D> OldCloud = new List<Point3D>();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                tempArr = line.Split();
                var point =  new Point3D(Double.Parse(tempArr[0]), Double.Parse( tempArr[1]), Double.Parse( tempArr[2]));
                OldCloud.Add(point);
            }

            var cloudCalculator = new CloudMoveLibrary();
            var newCloud = cloudCalculator.CloudRecalculationByPointToPoint(OldCloud.ToArray(), newBase, xEnd, yEnd, zEnd).ToArray();
            
        }
    }
}