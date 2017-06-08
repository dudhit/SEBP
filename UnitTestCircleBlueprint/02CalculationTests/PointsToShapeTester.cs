using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media.Media3D;

namespace SoloProjects.Dudhit.Utilities.UnitTestCircleBlueprint
{
  [TestFixture]
  public class PointsToShapeTester
  {
    private PointsToShape pointsToShape;
    private HashSet<Point3D> data;
    
  

    //[Test]
    //public void AxisTestXPos()
    //{
    //  int length=10;
    //  PointsToShape.GlobalCurveSet.Clear();
    //     PointsToShape.LineAlongPlaneGenerator(0, length, "x");
    //  data=PointsToShape.GlobalCurveSet;
    //  foreach(Point3D p in data)
    //  {
    //    System.Diagnostics.Trace.WriteLine(p.ToString());
    //  }
    //  Assert.Greater(data.Count, length-1);
      //}
    //[Test]
    //public void AxisTestZNegToPos()
    //{
    //  int start=-10;
    //  int end =10;
    //  int length = end-start;
    //  PointsToShape.GlobalCurveSet.Clear();
    //  PointsToShape.LineAlongPlaneGenerator(start, end, "z");
    //  data=PointsToShape.GlobalCurveSet;
    //  foreach(Point3D p in data)
    //  {
    //    System.Diagnostics.Trace.WriteLine(p.ToString());
    //  }
    //  Assert.Greater(data.Count,length-1);
      //}
    //[Test]
    //public void AddPointToCollection()
    //{Point3D testPoint=new Point3D(1, 1, 1);
    //  PointsToShape.AddNewPointToGlobalSet(testPoint);
    //  data=PointsToShape.GlobalCurveSet;
    //  Assert.IsTrue(PointsToShape.GlobalCurveSet.Contains(testPoint));
    //  Assert.IsTrue(data.Contains(testPoint));
    //}

   // [Test]
   // public void TestPointConverter()
   // {
   //   HashSet<Point> smallSet;
   //   smallSet = GetTestData();
   //  HashSet<Point3D> testSet = PointsToShape.TwoDIntoThreeDPoint(smallSet, "xy", 1);
   //  foreach(Point3D p in testSet)
   //  {
   //    System.Diagnostics.Trace.WriteLine(p);
   //  }
   //testSet = PointsToShape.TwoDIntoThreeDPoint(smallSet, "xz", 9);
   //foreach(Point3D p in testSet)
   //{
   //  System.Diagnostics.Trace.WriteLine(p);
   //}
   //testSet = PointsToShape.TwoDIntoThreeDPoint(smallSet, "yz", 11);
   //  foreach(Point3D p in testSet)
   //  {
   //    System.Diagnostics.Trace.WriteLine(p);
   //  }
   // }


    [Test]
    public void QuarterCircle()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 17,false);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("qc.txt");
    }
 
    [Test]
     public void QuarterCircleSolid()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 17,true);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("qcs.txt");
    }
  
    [Test]
    public void QuarterEllipse()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 18,false);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("qe.txt");
      data=null;
      pointsToShape=null;
    }

      [Test]
    public void QuarterEllipseSolid()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 18,true);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("qes.txt");
      data=null;
      pointsToShape=null;
    }

    [Test]
    public void QuarterSphere()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 20,false);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("qs.txt");
      data=null;
      pointsToShape=null;
    }

     [Test]
    public void QuarterSphereSolid()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 20,true);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("qss.txt");
      data=null;
      pointsToShape=null;
    }

     [Test]
    public void QuarterEllipsoid()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 24, false);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("qed.txt");
      data=null;
      pointsToShape=null;
    }

   [Test]
    public void QuarterEllipsoidSolid()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 24, true);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("qeds.txt");
      data=null;
      pointsToShape=null;
    }

     [Test]
    public void SemiCircle()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 33, false);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("sc.txt");
      data=null;
      pointsToShape=null;
    }

    [Test]
    public void SemiCircleSolid()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 33, true);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("sc.txt");
      data=null;
      pointsToShape=null;
    }

   [Test]
    public void SemiEllipse()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 34, false);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("se.txt");
      data=null;
      pointsToShape=null;
    }

    [Test]
    public void SemiEllipseSolid()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 34, true);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("ses.txt");
      data=null;
      pointsToShape=null;
    }

      [Test]
    public void SemiSphere()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 36, false);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("ss.txt");
      data=null;
      pointsToShape=null;
    }

  [Test]
    public void SemiSphereSolid()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 36, true);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("sss.txt");
      data=null;
      pointsToShape=null;
    }

     [Test]
    public void SemiEllipsoid()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 40, false);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("sed.txt");
      data=null;
      pointsToShape=null;
    }
 
  [Test]
    public void SemiEllipsoidSolid()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 40, true);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("seds.txt");
      data=null;
      pointsToShape=null;
    }

    [Test]
    public void FullCircle()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 65, false);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("fc.txt");
      data=null;
      pointsToShape=null;
    }

      [Test]
    public void FullCircleSolid()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 65, true);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("fcs.txt");
      data=null;
      pointsToShape=null;
    }

  [Test]
    public void FullEllipse()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 66, false);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("fe.txt");
      data=null;
      pointsToShape=null;
    }

    [Test]
    public void FullEllipseSolid()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 66, true);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("fes.txt");
      data=null;
      pointsToShape=null;
    }

    [Test]
    public void FullSphere()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 68, false);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("fs.txt");
      data=null;
      pointsToShape=null;
    }

     [Test]
    public void FullSphereSolid()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 68, true);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("fss.txt");
      data=null;
      pointsToShape=null;
    }

    [Test]
    public void FullEllipsoid()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 72, false);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("fed.txt");
      data=null;
      pointsToShape=null;
    }

   [Test]
    public void FullEllipsoidSolid()
    {
      pointsToShape = new PointsToShape(40, 25, 10, 72, true);
      data=pointsToShape.GlobalCurveSet;
      Assert.Greater(data.Count, 1);
      OutputToFile("feds.txt");
      data=null;
      pointsToShape=null;
    }

    private void OutputToFile(string fileName)
    {
      string name = @"E:\lifeDocs\computing\cSharp\prototyping\ViewPortPreview\ViewPortPreview\bin\x64\Release\"+fileName;
      using(StreamWriter sw = new StreamWriter(name))
      {
        foreach(Point3D p in data)
        { sw.WriteLine(p); }
      }
    }


    private static HashSet<Point> GetTestData()
    {
      HashSet<Point> smallSet;
      smallSet= new HashSet<Point>();
      smallSet.Add(new Point(0, 0));
      smallSet.Add(new Point(0, 1));
      smallSet.Add(new Point(0, 2));
      smallSet.Add(new Point(1, 0));
      smallSet.Add(new Point(2, 0));
      smallSet.Add(new Point(1, 1));
      smallSet.Add(new Point(3, 0));
      smallSet.Add(new Point(3, 1));
      smallSet.Add(new Point(3, 2));
      smallSet.Add(new Point(3, 3));
      smallSet.Add(new Point(3, 4));
      smallSet.Add(new Point(3, 5));
      smallSet.Add(new Point(3, 6));
      smallSet.Add(new Point(3, 7));
      smallSet.Add(new Point(3, 8));
      smallSet.Add(new Point(3, 9));
      smallSet.Add(new Point(3, 10));
      smallSet.Add(new Point(3, 11));
      smallSet.Add(new Point(3, 12));
      smallSet.Add(new Point(3, 13));
      smallSet.Add(new Point(3, 14));
      smallSet.Add(new Point(3, 15));
      smallSet.Add(new Point(3, 16));
      smallSet.Add(new Point(3, 17));
      smallSet.Add(new Point(3, 18));
      smallSet.Add(new Point(3, 19));
      smallSet.Add(new Point(3, 20));
      smallSet.Add(new Point(3, 21));
      smallSet.Add(new Point(3, 22));
      smallSet.Add(new Point(3, 23));
      smallSet.Add(new Point(3, 24));
      smallSet.Add(new Point(3, 25));
      smallSet.Add(new Point(3, 26));
      smallSet.Add(new Point(3, 27));
      smallSet.Add(new Point(3, 28));
      smallSet.Add(new Point(3, 29));
      smallSet.Add(new Point(3, 30));
      smallSet.Add(new Point(3, 31));
      smallSet.Add(new Point(3, 32));
      smallSet.Add(new Point(3, 33));
      smallSet.Add(new Point(3, 34));
      smallSet.Add(new Point(3, 35));
      smallSet.Add(new Point(3, 36));
      smallSet.Add(new Point(3, 37));
      smallSet.Add(new Point(3, 38));
      smallSet.Add(new Point(3, 39));
      smallSet.Add(new Point(3, 40));
      smallSet.Add(new Point(3, 41));
      smallSet.Add(new Point(3, 42));
      smallSet.Add(new Point(3, 43));
      smallSet.Add(new Point(3, 44));
      smallSet.Add(new Point(3, 45));
      smallSet.Add(new Point(3, 46));
      smallSet.Add(new Point(3, 47));
      smallSet.Add(new Point(3, 48));
      smallSet.Add(new Point(3, 49));
      smallSet.Add(new Point(3, 50));
      smallSet.Add(new Point(3, 51));
      smallSet.Add(new Point(3, 52));
      smallSet.Add(new Point(3, 53));
      smallSet.Add(new Point(3, 54));
      smallSet.Add(new Point(3, 55));
      smallSet.Add(new Point(3, 56));
      smallSet.Add(new Point(3, 57));
      smallSet.Add(new Point(3, 58));
      smallSet.Add(new Point(3, 59));
      smallSet.Add(new Point(3, 60));
      smallSet.Add(new Point(3, 61));
      smallSet.Add(new Point(3, 62));
      smallSet.Add(new Point(3, 63));
      smallSet.Add(new Point(3, 64));
      smallSet.Add(new Point(3, 65));
      smallSet.Add(new Point(3, 66));
      smallSet.Add(new Point(3, 67));
      smallSet.Add(new Point(3, 68));
      smallSet.Add(new Point(3, 69));
      smallSet.Add(new Point(3, 70));
      smallSet.Add(new Point(3, 71));
      smallSet.Add(new Point(3, 72));
      smallSet.Add(new Point(3, 73));
      smallSet.Add(new Point(3, 74));
      smallSet.Add(new Point(3, 75));
      smallSet.Add(new Point(3, 76));
      smallSet.Add(new Point(3, 77));
      smallSet.Add(new Point(3, 78));
      smallSet.Add(new Point(3, 79));
      smallSet.Add(new Point(3, 80));
      smallSet.Add(new Point(3, 81));
      smallSet.Add(new Point(3, 82));
      smallSet.Add(new Point(3, 83));
      smallSet.Add(new Point(3, 84));
      smallSet.Add(new Point(3, 85));
      smallSet.Add(new Point(3, 86));
      smallSet.Add(new Point(3, 87));
      smallSet.Add(new Point(3, 88));
      smallSet.Add(new Point(3, 89));
      return smallSet;
    }
  }
}
