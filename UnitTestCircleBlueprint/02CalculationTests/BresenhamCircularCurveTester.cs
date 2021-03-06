﻿using NUnit.Framework;
using System.Collections.Generic;
using System.Windows;

namespace SoloProjects.Dudhit.Utilities.UnitTestCircleBlueprint
{
  [TestFixture]
  class BresenhamCircularCurveTester
  {
    private BresenhamCircularCurve curve;
    private HashSet<Point> data;
    [SetUp]
    public void init()
    {
      curve = new BresenhamCircularCurve(60);
      data = new HashSet<Point>();
      System.Diagnostics.Trace.WriteLine("setep complete");
    }

    [Test]
    public void RunGenerator()
    { curve.BeginCalculations();
    data=curve.GetCurve();
    foreach(Point p in data)
    { System.Diagnostics.Trace.WriteLine(p.ToString());
    }
    Assert.Greater(data.Count,1);
    }

    [TearDown]
    public void Dispose()
    { curve=null; }
  }
}
