using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
namespace SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint.Collection
{

    public sealed class PointContainer
    {
     //   public  delegate void ProcessingChangedHandler(PointContainer pointContainer, ProcessInfoArgs processingstatus);
    //    public static event ProcessingChangedHandler Processing;
        private PointContainer()
        {
          
        }
        public static PointContainer Instance { get { return Nested.instance; } }
        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly PointContainer instance = new PointContainer();
        }
       private static List<Point3D> pointList;
 //      private static ProcessInfoArgs piArgs;
      //  private static ConcurrentBag<Point3D> pointList;
         private static Object multiThreadLock;

        static PointContainer()
        {
         //   pointList = new ConcurrentBag<Point3D>();
           pointList = new List<Point3D>();
            multiThreadLock = new Object();
   //          piArgs = new ProcessInfoArgs(0);
        }

        public static void Add(Point3D p)
        {
            if (pointList.Contains(p) == false)
            {
                lock (multiThreadLock)
                {
                    pointList.Add(p);
                }
        //        System.Diagnostics.Trace.WriteLine(Count());
                //if (Processing != null)
                //{
                //    piArgs.numPoints = pointList.Count;
                //    Processing(Instance, piArgs);
                //}
            }
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
        { pointList.Clear();
        //if (Processing != null)
        //{
        //    piArgs.numPoints=pointList.Count;
        //    Processing(Instance, piArgs);
        //}
        }


        public System.Collections.Generic.IEnumerable<Point3D> NextPoint
        {
            get
            {
                yield return Item(1);
     
            }
        }
    }
    //public class ProcessInfoArgs : EventArgs
    //{
    //  //  public  int NumPoints { get; set; }
    //    public  int numPoints;
    //  public  ProcessInfoArgs(int count)
    //    {
    //        this.numPoints = count;
    //    }
 
    //}
}
