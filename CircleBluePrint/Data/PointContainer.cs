

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
namespace SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint.Collection
{


    public static class PointContainer
    {
        private static List<Point3D> pointList;

        static PointContainer()
        {
            pointList = new List<Point3D>();
        }

        public static void Add(Point3D p)
        {
            if (pointList.Contains(p) == false)
                pointList.Add(p);
        }

        public static Point3D Item(int index)
        {
            if (index > pointList.Count - 1 || index < 0)
                throw new IndexOutOfRangeException("Index out of range, is not within defined limits, IS NOT THERE SO STOP LOOKING");

            return (Point3D)pointList[index];
        }
        public static bool IsEmpty()
        {
            return (pointList.Count > 0) ? false : true;
        }
        public static int Count()
        {
            return pointList.Count;
        }
        public static void Clear()
        { pointList.Clear(); }

    }

}
