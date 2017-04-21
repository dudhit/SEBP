﻿using System;
using NUnit.Framework;
using SoloProjects.Dudhit.SpaceEngineers.SEBP;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace SoloProjects.Dudhit.Utilities.UnitTestCircleBlueprint
{
  [TestFixture]
  public class XmlTester
  {
  private  BlueprintModel myBLueprintModel;
 private   HashSet<Point3D> testData;
    FileSystemAdministrativeTools fsat;

    [Test]
    public void TestMethod1()
    {
      fsat = new FileSystemAdministrativeTools("dudhit");
      myBLueprintModel = new BlueprintModel();
      myBLueprintModel.BlockSize="Large";
      myBLueprintModel.BlockArmour="Normal";
      myBLueprintModel.BlueprintName="testclassbp";
      myBLueprintModel.BlueprintFilePath=fsat.GetGameDataSaveLocation();
      myBLueprintModel.BlockColour=new Utilites.SeHSV(0, -100, -90);
      myBLueprintModel.Shape="circle";
      myBLueprintModel.ShapeFraction="quarter";
      myBLueprintModel.Solid=false;
      myBLueprintModel.SteamId=fsat.SteamUserId;
      myBLueprintModel.SteamName=fsat.SteamUserName;
      myBLueprintModel.XAxis=11;
      myBLueprintModel.YAxis=12;
      myBLueprintModel.ZAxis=13;
      Assert.IsTrue(myBLueprintModel.HasUsableData);
      if(myBLueprintModel.HasUsableData)
      {
        testData =new HashSet<Point3D>();
        BluePrintXml mfh = new BluePrintXml(myBLueprintModel,testData);
      }
    }
  }
}
