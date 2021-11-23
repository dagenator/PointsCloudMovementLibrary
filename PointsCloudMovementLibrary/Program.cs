using System;
using System.Collections.Generic;
using System.Globalization;
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

            var OldCloud = ReadFile(path);

            var cloudCalculator = new CloudMoveLibrary();
            var newCloud = cloudCalculator.CloudRecalculationByPointToPoint(OldCloud.ToArray(), newBase, xEnd, yEnd, zEnd).ToArray();
            
        }
        private static IEnumerable<Point3D> ReadFile(string path)
        {
            return File.ReadAllLines(path)
                .SkipWhile(x => !x.Contains("end_header"))
                .Skip(1)
                .Select(line =>
                {
                    var xyz = line
                        .Split(' ')
                        .Select(l => double.Parse(l, CultureInfo.InvariantCulture)).ToList();
                    return new Point3D(xyz[0], xyz[1], xyz[2]);
                });
        }
    }
}