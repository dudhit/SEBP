using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloProjects.Dudhit.Utilities
{
  public class WindowsRegistryStuff
  {
    /// <summary>
    /// find out if key exista
    /// </summary>
    /// <param name="mainKey">root</param>
    /// <param name="subkey">path</param>
    /// <returns>true is key exists</returns>
    public static bool KeyHasSubkey(string mainKey, string subkey)
    {
      RegistryKey regKey = RegistryMainKeySetter(mainKey, subkey);
      bool hasKey;
      return hasKey =(regKey != null)?true:false;
    }

    private static RegistryKey RegistryMainKeySetter(string mainKey, string subkey)
    {
      RegistryKey regKey=null;
      switch(mainKey.ToLower())
      {
        case "users":
          regKey = Registry.Users.OpenSubKey(@subkey);
          break;
        case "performancedata":
          regKey = Registry.PerformanceData.OpenSubKey(@subkey);
          break;
        case "localmachine":
          regKey = Registry.LocalMachine.OpenSubKey(@subkey);
          break;
        case "currentconfig":
          regKey = Registry.CurrentConfig.OpenSubKey(@subkey);
          break;
        case "classesroot":
          regKey = Registry.ClassesRoot.OpenSubKey(@subkey);
          break;
        case "currentuser":
          regKey = Registry.CurrentUser.OpenSubKey(@subkey);
          break;
      }
      return regKey;
    }

    /// <summary>
    /// probe registry for a value
    /// </summary>
    /// <param name="mainKey">Root registry branch</param>
    /// <param name="subkey">path from root</param>
    /// <param name="valueKey">key you want the value for</param>
    /// <returns>string</returns>
    public static string RegistryGetValue(string mainKey, string subkey, string valueKey)
    {
      string value=string.Empty;
      if(KeyHasSubkey(mainKey, subkey))
      {
        RegistryKey lookUp= RegistryMainKeySetter(mainKey, subkey);
        return lookUp.GetValue(valueKey).ToString();
      }
      return value;
    }
  }
}
