using NUnit.Framework;
using SoloProjects.Dudhit.SpaceEngineers.SEBP;

namespace Dudhit.UnitTestCircleBlueprint
{
  [TestFixture]
  class StartArgCheckerTester
  {
    private CheckStartArguments csa;
    [SetUp]
    public void init()
    {
      csa = new CheckStartArguments();
      csa.MyBlueprintModel=new BlueprintModel();
      System.Diagnostics.Trace.WriteLine("setep complete");
    }

    [TearDown]
    public void Dispose()
    { csa=null; }


    [Test]
    public void testDefaultSetup()
    {
      csa.SetEmptyWithDefaultValues();
   Assert.AreEqual(csa["bpname"], "SEBP"); 
   Assert.AreEqual(csa["x"], "10");
       Assert.AreEqual(csa["y"], "10"); 
     Assert.AreEqual(csa["z"], "10"); 
  Assert.AreEqual(csa["shape"], "circle"); 
  Assert.AreEqual(csa["shape_fraction"], "quarter"); 
  Assert.AreEqual(csa["armour"], "Normal"); 
    Assert.AreEqual(csa["size"], "Large");
    Assert.AreEqual(csa["colour"], "hsv");
     Assert.AreEqual(csa["h"], "0"); 
       Assert.AreEqual(csa["s"], "-100"); 
      Assert.AreEqual(csa["v"], "-90"); 
  }

    [Test]
    public void BlueprintValuesSetTest()
    {
      csa.SetEmptyWithDefaultValues();
      csa.SetModel();
      Assert.AreEqual(csa.MyBlueprintModel.BlueprintName, "SEBP");
      Assert.AreEqual(csa.MyBlueprintModel.XAxis, 10);
      Assert.AreEqual(csa.MyBlueprintModel.YAxis, 10);
      Assert.AreEqual(csa.MyBlueprintModel.ZAxis,10);
      Assert.AreEqual(csa.MyBlueprintModel.Shape, "circle");
      Assert.AreEqual(csa.MyBlueprintModel.ShapeFraction, "quarter");
      Assert.AreEqual(csa.MyBlueprintModel.BlockArmour, "Normal");
      Assert.AreEqual(csa.MyBlueprintModel.BlockSize, "Large");
      Assert.AreEqual(csa.MyBlueprintModel.BlockColour.H,0);
      Assert.AreEqual(csa.MyBlueprintModel.BlockColour.S,-100);
      Assert.AreEqual(csa.MyBlueprintModel.BlockColour.V, -90); 
    }
  }
}

