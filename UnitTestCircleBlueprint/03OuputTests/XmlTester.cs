using System;
using NUnit.Framework;
using SoloProjects.Dudhit.SpaceEngineers.SEBP;
using SoloProjects.Dudhit.Utilities;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using System.IO;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.Model;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.BlueprintWriterLib;

namespace SoloProjects.Dudhit.Utilities.UnitTestCircleBlueprint
{
  [TestFixture]
  public class XmlTester
  {
    private BlueprintModel myBLueprintModel;
    private HashSet<Point3D> testData;
    private string testPath;
    private string blueprintName;
    private string blockSize;
    private string blockArmour;
    [Test]
    public void TestLargeLight()
    {
      testPath = Directory.GetCurrentDirectory();
       blueprintName="test1";
      Directory.CreateDirectory(Path.Combine(testPath, blueprintName));
      blockSize="Large";
      blockArmour="Normal";
            RepeatingChunk();
      FileAssert.Exists(Path.Combine(testPath, blueprintName, "bp.sbc"));
      System.Diagnostics.Trace.WriteLine(string.Format("Outputs found at:",Path.Combine(testPath,blueprintName)));
    }
    [Test]
    public void TestLargeHeavy()
    {
      testPath = Directory.GetCurrentDirectory();
       blueprintName="test2";
      Directory.CreateDirectory(Path.Combine(testPath, blueprintName));
      blockSize="Large";
      blockArmour="Heavy";
      RepeatingChunk();
      FileAssert.Exists(Path.Combine(testPath, blueprintName, "bp.sbc"));
      System.Diagnostics.Trace.WriteLine(string.Format("Outputs found at:", Path.Combine(testPath, blueprintName)));
    }
    [Test]
    public void TestSmallLight()
    {
      testPath = Directory.GetCurrentDirectory();
       blueprintName="test3";
      Directory.CreateDirectory(Path.Combine(testPath, blueprintName));
      blockSize="Small";
      blockArmour="Normal";
      RepeatingChunk();
      FileAssert.Exists(Path.Combine(testPath, blueprintName, "bp.sbc"));
      System.Diagnostics.Trace.WriteLine(string.Format("Outputs found at:", Path.Combine(testPath, blueprintName)));
    }
    [Test]
    public void TestSmallHeavy()
    {
      testPath = Directory.GetCurrentDirectory();
       blueprintName="test4";
      Directory.CreateDirectory(Path.Combine(testPath, blueprintName));
      blockSize="Small";
      blockArmour="Heavy";
      RepeatingChunk();
      FileAssert.Exists(Path.Combine(testPath, blueprintName, "bp.sbc"));
      System.Diagnostics.Trace.WriteLine(string.Format("Outputs found at:", Path.Combine(testPath, blueprintName)));
    }

    private void RepeatingChunk()
    {
      myBLueprintModel = new BlueprintModel();
      myBLueprintModel.BlockSize=blockSize;
      myBLueprintModel.BlockArmour=blockArmour;
      myBLueprintModel.BlueprintName=blueprintName;
      myBLueprintModel.BlueprintFilePath=testPath;
      myBLueprintModel.BlockColour=new Utilities.SeHSV(0, -100, -90);
      myBLueprintModel.Shape="circle";
      myBLueprintModel.ShapeFraction="quarter";
      myBLueprintModel.Solid=false;
      myBLueprintModel.SteamId="123456789";
      myBLueprintModel.SteamName="itsme";
      myBLueprintModel.XAxis=11;
      myBLueprintModel.YAxis=12;
      myBLueprintModel.ZAxis=13;
      Assert.IsTrue(myBLueprintModel.HasUsableData);
      if(myBLueprintModel.HasUsableData)
      {
        testData =new HashSet<Point3D>();
        testData.Add(new Point3D(0, 0, 0));
        testData.Add(new Point3D(1, 0, 0));
        testData.Add(new Point3D(2, 0, 0));
        testData.Add(new Point3D(0, 1, 0));
        Assert.Greater(testData.Count, 0);
        BluePrintXml mfh = new BluePrintXml(myBLueprintModel, testData);
        mfh.MakeBaseStructure();
        mfh.BluePrintFileHandling();
      }
    }
  }
}
