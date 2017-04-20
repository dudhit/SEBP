using NUnit.Framework;
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
    [SetUp]
    public void init()
    {
    pointsToShape = new PointsToShape(40, 25, 10, 72);
    data =pointsToShape.GlobalCurveSet;    new HashSet<Point3D>();
          System.Diagnostics.Trace.WriteLine("setep complete");
    }

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

    [Test]
    public void Rungenerator()
    {
      data=pointsToShape.GlobalCurveSet;
      foreach(Point3D p in data)
      {
        System.Diagnostics.Trace.WriteLine(p.ToString());
      }
      Assert.Greater(data.Count, 1);
      OutputToFile();
    }

    private void OutputToFile()
    {
      string name = @"E:\lifeDocs\computing\cSharp\prototyping\ViewPortPreview\ViewPortPreview\bin\x64\Release\dataFile.txt";
      using(StreamWriter sw = new StreamWriter(name))
      {
        foreach(Point3D p in data)
        { sw.WriteLine(p); }
      }
    }

    [TearDown]
    public void Dispose()
    {
      data=null;
     // pointsToShape=null;
    }

  }
}
