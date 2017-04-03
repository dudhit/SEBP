using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using SoloProjects.Dudhit.SpaceEngineers.SEBP;
using SoloProjects.Dudhit.Utilites;
using System.Reflection;
using System.Linq.Expressions;

namespace Dudhit.UnitTestCircleBlueprint
{
  [TestFixture]
  public class CircleBlueprintTester
  {
    private BlueprintModel bpm;
    private string validPath ="C:\\Users\\dudhit\\AppData\\Roaming\\SpaceEngineers";
    private string validId="0123456789";
    private string validOwner="noob";
    private string validBpname="blueprint from SEBP";
    private int validXaxis=40;
    private int validYaxis=20;
    private int validZaxis=10;
    private string validShape="circle";
    private string validShapeFraction="quarter";
    private string validarmour="Normal";
    private string validSize="Large";
    private SeHSV validColour=new SeHSV(200, 50, 20);
    //arrange
    [SetUp]
    public void init()
    {
      bpm = new BlueprintModel();

      System.Diagnostics.Trace.WriteLine("setep complete");
    }

    [TearDown]
    public void Dispose()
    { bpm=null; }


    [Test]
    public void TestClass01Defaults()
    {
   
      //act
      //assert
      VerboseInformation("class can be used:", bpm.HasUsableData.ToString());
      Assert.False(bpm.HasUsableData);
      VerboseInformation("XAxis:", bpm.XAxis.ToString());
      Assert.Zero(bpm.XAxis);
      VerboseInformation("YAxis:", bpm.YAxis.ToString());
      Assert.Zero(bpm.YAxis);
      VerboseInformation("ZAxis:", bpm.ZAxis.ToString());
      Assert.Zero(bpm.ZAxis);
      VerboseInformation("BlockArmour:", bpm.BlockArmour.ToString());
      Assert.IsEmpty(bpm.BlockArmour);
      VerboseInformation("BlockSize:", bpm.BlockSize.ToString());
      Assert.IsEmpty(bpm.BlockSize);
      VerboseInformation("BlueprintFilePath:", bpm.BlueprintFilePath.ToString());
      Assert.IsEmpty(bpm.BlueprintFilePath);
      VerboseInformation("BlueprintName:", bpm.BlueprintName.ToString());
      Assert.IsEmpty(bpm.BlueprintName);
      VerboseInformation("FinalShape:", bpm.FinalShape.ToString());
      Assert.Zero(bpm.FinalShape);
      VerboseInformation("Shape:", bpm.Shape.ToString());
      Assert.IsNotNull(bpm.Shape);
      VerboseInformation("ShapeFraction:", bpm.ShapeFraction.ToString());
      Assert.IsNotEmpty(bpm.ShapeFraction);
      VerboseInformation("SteamId:", bpm.SteamId.ToString());
      Assert.IsEmpty(bpm.SteamId);
      VerboseInformation("SteamName:", bpm.SteamName.ToString());
      Assert.IsEmpty(bpm.SteamName);
      VerboseInformation("BlockColour H:", bpm.BlockColour.H.ToString());
      Assert.Zero(bpm.BlockColour.H);
      VerboseInformation("BlockColour S:", bpm.BlockColour.S.ToString());
      Assert.Zero(bpm.BlockColour.S);
      VerboseInformation("BlockColour V:", bpm.BlockColour.V.ToString());
      Assert.Zero(bpm.BlockColour.V);

    }
    [Test]
    public void TestClass02AddBadValues()
    {
      AddValidData();
      bpm.Shape="sqare";
      Assert.IsNotEmpty(bpm.Shape);
      bpm.BlockArmour="yes";
      Assert.IsNotEmpty(bpm.BlockArmour);
      bpm.BlockColour=new SeHSV(300, 200, 200);
      Assert.IsInstanceOf<SeHSV>(bpm.BlockColour);
      bpm.BlockSize="big";
      Assert.IsNotEmpty(bpm.BlockSize);
      bpm.BlueprintFilePath="..\\";
      Assert.IsNotEmpty(bpm.BlueprintFilePath);
      bpm.BlueprintName="";
      Assert.IsNotEmpty(bpm.BlueprintName);
      bpm.SteamId="";
      Assert.IsNotEmpty(bpm.SteamId);
      bpm.SteamName="";
      Assert.IsNotEmpty(bpm.SteamName);
      bpm.XAxis=3;
      Assert.AreEqual(validXaxis, bpm.XAxis);
      bpm.YAxis=1;
      Assert.AreEqual(validYaxis, bpm.YAxis);
      bpm.ZAxis=-40;
      Assert.AreEqual(validZaxis, bpm.ZAxis);

   
    }

  

    //private void BadDataRejectionTest(string existingGoodValue,string replacement)
    //{
    //  bpm.ShapeFraction="third";
    //  string changedValue=bpm.ShapeFraction;
    //  Assert.AreEqual(existingGoodValue, changedValue);
    //}

    [Test]
    public void TestClass03AddValues()
    {
      AddValidData();

      VerboseInformation("has a final shape:", bpm.FinalShape.ToString());

      Assert.AreEqual(validZaxis, bpm.ZAxis);
      Assert.AreEqual(validYaxis, bpm.YAxis);
      Assert.AreEqual(validXaxis, bpm.XAxis);
      Assert.IsNotEmpty(bpm.SteamName);
      Assert.IsNotEmpty(bpm.SteamId);
      Assert.IsNotEmpty(bpm.BlueprintName);
      Assert.IsNotEmpty(bpm.BlueprintFilePath);
      Assert.IsNotEmpty(bpm.BlockSize);
      Assert.IsNotEmpty(bpm.Shape);
      Assert.IsNotEmpty(bpm.ShapeFraction);
      Assert.IsNotEmpty(bpm.BlockArmour);
      Assert.IsInstanceOf<SeHSV>(bpm.BlockColour);
      Assert.IsTrue(bpm.HasUsableData);
    }

    private void AddValidData()
    {

      VerboseInformation("add shapefraction", validShapeFraction.ToString());
      bpm.ShapeFraction=validShapeFraction;
      VerboseInformation("validShape", validShape.ToString());
      bpm.Shape=validShape;
      VerboseInformation("validarmour", validarmour.ToString());
      bpm.BlockArmour=validarmour;
      VerboseInformation("validColour", validColour.ToString());
      bpm.BlockColour=validColour;
      VerboseInformation("validSize", validSize.ToString());
      bpm.BlockSize=validSize;
      VerboseInformation("validPath", validPath.ToString());
      bpm.BlueprintFilePath=validPath;
      VerboseInformation("validBpname", validBpname.ToString());
      bpm.BlueprintName=validBpname;
      VerboseInformation("validId", validId.ToString());
      bpm.SteamId=validId;
      VerboseInformation("validOwner", validOwner.ToString());
      bpm.SteamName=validOwner;
      VerboseInformation("validXaxis", validXaxis.ToString());
      bpm.XAxis=validXaxis;
      VerboseInformation("validYaxis", validYaxis.ToString());
      bpm.YAxis=validYaxis;
      VerboseInformation("validZaxis", validZaxis.ToString());
      bpm.ZAxis=validZaxis;
    }

    private void VerboseInformation(string message, string value)
    {
      string verboseMessage= string.Format("{0} {1}", message, value);
      System.Diagnostics.Trace.WriteLine(verboseMessage);
    }


    // [TestMethod]
    //[ExpectedException(typeof(ArgumentOutOfRangeException))]  
    //a method that expects an exception
  }
}
