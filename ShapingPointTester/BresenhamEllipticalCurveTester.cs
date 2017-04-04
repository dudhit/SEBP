using System.Collections.Generic;
using System.Windows;
using NUnit.Framework;

namespace SoloProjects.Dudhit.Utilities.UnitTestCircleBlueprint
{
  [TestFixture]
  class BresenhamEllipticalCurveTester
  {
    private BresenhamEllipticalCurve curve;
    private List<Point> data;
    [SetUp]
    public void init()
    {
      curve = new BresenhamEllipticalCurve(20,10);
      data = new List<Point>();
      System.Diagnostics.Trace.WriteLine("setep complete");
    }

    [Test]
    public void Rungenerator()
    {
      curve.BeginCalculations();
      data=curve.GetCurve();
      foreach(Point p in data)
      {
        System.Diagnostics.Trace.WriteLine(p.ToString());
      }
      Assert.Greater(data.Count, 1);
    }

    [TearDown]
    public void Dispose()
    { curve=null; }
  }
}
