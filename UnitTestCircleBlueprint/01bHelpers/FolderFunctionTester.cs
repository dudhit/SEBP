using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoloProjects.Dudhit.Utilities;

namespace SoloProjects.Dudhit.Utilities.UnitTestCircleBlueprint
{
  [TestFixture]
  class FolderFunctionTester
  {
    [Test]
    public void FindExistingFolder()
    {

      string folder = @"C:\temp";
      DirectoryAssert.Exists(folder);
      Assert.IsTrue(FileSystemHelper.FolderVerification(folder));
    }

    [Test]
    public void FindNonExistingFolder()
    {
      string folder = @"C:\tempp";
      DirectoryAssert.DoesNotExist(folder);
      Assert.IsFalse(FileSystemHelper.FolderVerification(folder));
    }

    [Test]
    public void CreateExistingFolder()
    {
      string folder = @"C:\temp";
      DirectoryAssert.Exists(folder);
      Assert.IsFalse(FileSystemHelper.FolderCreation(folder));
    }

    [Test]
    public void CreateNonExistingFolder()
    {
      string folder = @"C:\temp\foldertesting";
      DirectoryAssert.DoesNotExist(folder);
      Assert.IsTrue(FileSystemHelper.FolderCreation(folder));
      DirectoryAssert.Exists(folder);
    }
  }
}
