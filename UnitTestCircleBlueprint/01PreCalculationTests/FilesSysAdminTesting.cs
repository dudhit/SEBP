using NUnit.Framework;
using SoloProjects.Dudhit.SpaceEngineers.SEBP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloProjects.Dudhit.Utilities.UnitTestCircleBlueprint
{
  [TestFixture]
  class FilesSysAdminTesting
  {
    private FileSystemAdministrativeTools fsat;
    private string steamPath;
    private string steamUserName;
    private string steamUserId;
    private string providedName;


    [Test]
    public void DefaultInstanciation()
    {
       fsat = new FileSystemAdministrativeTools();
      ConstructorOutputs(fsat);
      fsat=null;
    }

    private void ConstructorOutputs(FileSystemAdministrativeTools fsat)
    {
      steamPath=fsat.SteamPath;
      steamUserId=fsat.SteamUserId;
      steamUserName=fsat.SteamUserName;
      System.Diagnostics.Trace.WriteLine(steamPath);
      System.Diagnostics.Trace.WriteLine(steamUserId);
      System.Diagnostics.Trace.WriteLine(steamUserName);
      Assert.IsNotEmpty(steamUserName);
      Assert.IsNotEmpty(steamUserId);
      DirectoryAssert.Exists(steamPath);
    }

    [Test]
    
    public void NameOnlyInstanciation()
    {
      this.providedName = "mel";
       fsat = new FileSystemAdministrativeTools(providedName);
      ConstructorOutputs(fsat);
      fsat=null;
    }

    [Test]
    public void ManualFolder()
    {
       fsat= new FileSystemAdministrativeTools();
       this.steamPath= fsat.ManuallySelectSaveFolder();
       fsat=null;
    }
    [Test]
    public void ShowBlueprintPath()
    {
     fsat=new FileSystemAdministrativeTools();
       DirectoryAssert.Exists(fsat.GetGameDataSaveLocation());
       fsat=null;
    }

    [Test]
    public void FolderMaking()
    {
      fsat=new FileSystemAdministrativeTools();
      Assert.IsTrue(fsat.FolderVerification(fsat.GetGameDataSaveLocation()));
     fsat=null; }
  }
}
