using Microsoft.Win32;
using SoloProjects.Dudhit.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
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
    private const string SPACE_ENGINEERS_PATH =  "\\SpaceEngineers\\Blueprints\\local";
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
      string bpPath= userApp + SPACE_ENGINEERS_PATH;
      return bpPath;
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



    private void GetSteamData()
    {
      if(WindowsRegistryStuff.KeyHasSubkey("currentuser", @"Software\Valve\Steam"))
      {
        SteamPath=WindowsRegistryStuff.RegistryGetValue("currentuser", @"Software\Valve\Steam", "SteamPath");
        SteamUserName=WindowsRegistryStuff.RegistryGetValue("currentuser", @"Software\Valve\Steam", "LastGameNameUsed");
        if(string.IsNullOrEmpty(providedName))
        {
          providedName=SteamUserName;
        }
        if(getSteamId)//ignore all this if an id was given
        {
          if(!(providedName.Equals(SteamUserName)))
          { SteamUserName=providedName; }
          using(SoloProjects.Dudhit.SpaceEngineers.SEBP.SteamConfigReader.FindSteamID fsID =
           new SoloProjects.Dudhit.SpaceEngineers.SEBP.SteamConfigReader.FindSteamID(SteamPath, SteamUserName)) { SteamUserId = fsID.SteamID; }
        }
      }
      else
      {
        MessageBox.Show("Windows could not return the location of your Steam installation\n manual entry required", "Unknown Steam Installation location", MessageBoxButton.OK, MessageBoxImage.Warning);

      }
    }

  }
}
