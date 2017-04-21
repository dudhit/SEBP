using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
  public class FileSystemAdministrativeTools
  {
    public string SteamPath { get; set; }
    public string SteamUserName { get; set; }
    public string SteamUserId { get; set; }
    private string providedName;
    private bool getSteamId;
    public FileSystemAdministrativeTools(string user) : this(user, true) { }
    public FileSystemAdministrativeTools() : this(null) { }
    public FileSystemAdministrativeTools(string user, bool getId)
    {
      this.providedName=user;
      this.getSteamId=getId;
      GetSteamData();
    }

    public string GetGameDataSaveLocation()
    {
      string userApp = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      return userApp + "\\SpaceEngineers\\Blueprints\\local";
    }

    public string ManuallySelectSaveFolder()
    {
      string filePath=string.Empty;
      Microsoft.Win32.OpenFileDialog findFolder = new Microsoft.Win32.OpenFileDialog();
      findFolder.Filter = "All files (*.*)|*.*";
      if(findFolder.ShowDialog() == true)
      {
        //split path into individual folders
        char[] delimiter = new char[] { '\\' };
        int fileNameLength = findFolder.FileName.Length;
        string[] individualFolders = findFolder.FileName.Split(delimiter);
        //rejoin excluding the file selected
        for(int i = 0;i < individualFolders.Length - 1;i++)
        {
          filePath += individualFolders[i] + "\\";
        }
        findFolder = null;
      }
      return filePath;
    }

    private bool RegistryHasSteamData()
    {
      RegistryKey regKey = Registry.CurrentUser;
      regKey = regKey.OpenSubKey(@"Software\Valve\Steam");

      if(regKey != null)
      {
        SteamPath = regKey.GetValue("SteamPath").ToString();
        SteamUserName = regKey.GetValue("LastGameNameUsed").ToString();
        if(string.IsNullOrEmpty(providedName))
          providedName=SteamUserName;
        return true;
      }
      return false;
    }

    private void GetSteamData()
    {
      if(RegistryHasSteamData())
      {
        if(getSteamId)//ignore all this if an id was given
        {
          if(!(providedName.Equals(SteamUserName)))
          { SteamUserName=providedName; }
          using(SoloProjects.Dudhit.SpaceEngineers.SEBP.View.FindSteamID fsID =
           new SoloProjects.Dudhit.SpaceEngineers.SEBP.View.FindSteamID(SteamPath, SteamUserName)) { SteamUserId = fsID.SteamID; }
        }
      }
      else
      {
        MessageBox.Show("Windows could not return the location of your Steam installation\n manual entry required", "Unknown Steam Installation location", MessageBoxButton.OK, MessageBoxImage.Warning);

      }
    }
  }
}
