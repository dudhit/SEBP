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


  }
}
