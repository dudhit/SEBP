using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloProjects.Dudhit.Utilities.UnitTestCircleBlueprint._01bHelpers
{
  [TestFixture]
  class ProbeRegistryTester
  {

    [Test]
    public void KnownClassesRootKeysExist()
    {
      Assert.IsTrue(WindowsRegistryStuff.KeyHasSubkey("classesroot", "._sln80"));
    }
    [Test]
    public void KnownCurrentUserKeysExist()
    {
      Assert.IsTrue(WindowsRegistryStuff.KeyHasSubkey("currentuser", "Console"));
    }
    [Test]
    public void KnownLocalMachineKeysExist()
    {
      Assert.IsTrue(WindowsRegistryStuff.KeyHasSubkey("localmachine", "HARDWARE"));
    }
    [Test]
    public void KnownUsersKeysExist()
    {
      Assert.IsTrue(WindowsRegistryStuff.KeyHasSubkey("users", ".DEFAULT"));
    }
    [Test]
    public void KnownCurrentConfigKeysExist()
    {
      Assert.IsTrue(WindowsRegistryStuff.KeyHasSubkey("currentconfig", "Software"));
    }
    [Test]
    public void KnownDeepSubKeysExist()
    {
      Assert.IsTrue(WindowsRegistryStuff.KeyHasSubkey("currentconfig", @"System\CurrentControlSet\SERVICES\TSDDD"));
    }
    [Test]
    public void KnownKeysDoNotExist()
    {
      Assert.IsFalse(WindowsRegistryStuff.KeyHasSubkey("classesroot", "._sln81110"));
      Assert.IsFalse(WindowsRegistryStuff.KeyHasSubkey("currentuser", "Consolee"));
      Assert.IsFalse(WindowsRegistryStuff.KeyHasSubkey("localmachine", "HARDWAREe"));
      Assert.IsFalse(WindowsRegistryStuff.KeyHasSubkey("users", ".DEFAULTe"));
      Assert.IsFalse(WindowsRegistryStuff.KeyHasSubkey("currentconfig", "Softwaree"));
      Assert.IsFalse(WindowsRegistryStuff.KeyHasSubkey("currentconfig", @"System\CurrentControlSet\SERVICES\TSDDDD"));
    }
/*THESE TESTS ARE MACHINE / LOGON SPECIFIC AND MAY NEED TAILORING */
    [Test]
    public void GetKnownKeyValueB()
    {
      string expected="";
      string result =(string)WindowsRegistryStuff.RegistryGetValue("currentuser", @"Software\Mozilla\Firefox\Crash Reporter", "Email");
      Assert.AreEqual(expected,result );
      System.Diagnostics.Trace.WriteLine(string.Format("expected:{0} result:{1}", expected, result));
    }
    [Test]
    public void GetKnownKeyValueC()
    {
      string expected=@"res://mshtml.dll/blank.htm";
      string result =(string)WindowsRegistryStuff.RegistryGetValue("localmachine", @"SOFTWARE\Microsoft\Internet Explorer\AboutURLs", "blank");
      Assert.AreEqual(expected, result);
      System.Diagnostics.Trace.WriteLine(string.Format("expected:{0} result:{1}", expected, result));
 
    }
    [Test]
    public void GetKnownKeyValueD()
    {
      string expected=Environment.GetEnvironmentVariable("TEMP");
      string result =(string)WindowsRegistryStuff.RegistryGetValue("users", @".DEFAULT\Environment", "TEMP");
      Assert.AreEqual(expected, result);
      System.Diagnostics.Trace.WriteLine(string.Format("expected:{0} result:{1}", expected, result));

    }
    [Test]
    public void GetNonExistingKeyValue()
    {
      string expected="";
      string result = (string)WindowsRegistryStuff.RegistryGetValue("users", @".DEFAULT\Environment", "TEMPc");
      Assert.AreEqual(expected, result);
      System.Diagnostics.Trace.WriteLine(string.Format("expected:{0} result:{1}", expected, result));

    }
  }
}
