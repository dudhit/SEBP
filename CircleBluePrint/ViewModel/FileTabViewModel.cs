using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.ViewModel
{
 //public class FileTabViewModel:IFileTabViewModel
 // {
 //   private string userApp;
 //   private string saveRootLocation;
 //   public string SteamName;
 //   public string SteamUserId;
 //   private string blueprintName;
 //   private bool hasDataPath;
 // FileTabModel model;
 //  public FileTabViewModel(FileTabModel model)
 //  {
 //    this.model=model;
 //    hasDataPath = false;
 //           Reset();
 //  }
 //   #region IFileTabViewModel Members

 //   public void Reset()
 //   {
 //     saveRootLocation = "";
 //     blueprintName = "";
 //     SteamName = "";
 //     SteamUserId = "";
 //   }

 //   public void AutoPopulateSaveLocation()
 //   {
 //     userApp = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
 //     saveRootLocation = userApp + "\\SpaceEngineers";
 //     UpdateSavePath();
 //   }

 //   public void Find_Path()
 //   {
 //     Microsoft.Win32.OpenFileDialog findFolder = new Microsoft.Win32.OpenFileDialog();
 //     findFolder.Filter = "All files (*.*)|*.*";
 //     if(findFolder.ShowDialog() == true)
 //     {
 //       saveRootLocation = null;
 //       //split path into individual folders
 //       char[] kill = new char[] { '\\' };
 //       int j = findFolder.FileName.Length;
 //       string[] x = findFolder.FileName.Split(kill);
 //       //rejoin excluding the file selected
 //       for(int i = 0;i < x.Length - 1;i++)
 //       {
 //         saveRootLocation += x[i] + "\\";
 //       }
 //       findFolder = null;
 //       UpdateSavePath();
 //     }
 //   }

 //   public void UpdateSavePath()
 //   {
 //     string errorMessage = "";
 //     if(!StringHasValue(saveRootLocation))
 //     {
 //       errorMessage += "The blueprint requires the location of the Space Engineers save folder\n";
 //     }
 //     else
 //     {
 //       dataSE_Path.MinWidth = saveRootLocation.Length * 2;
 //       dataSE_Path.Text = saveRootLocation;
 //     }
 //     if(errorMessage != "")
 //     {
 //       MessageBox.Show(errorMessage, "Critical Data Missing", MessageBoxButton.OK, MessageBoxImage.Exclamation);
 //     }

 //   }

 //   public bool StringHasValue(string validate)
 //   {
 //     bool check = (string.IsNullOrWhiteSpace(validate)) ? false : true;
 //     return check;
 //   }

 //   public void SnoopForSteamID()
 //   {
 //     // call find steamid class

 //     //((App)Application.Current).controller.GetSteamId();
 //     //SteamUserId = ((App)Application.Current).controller.SteamUserId;
 //     //SteamName = ((App)Application.Current).controller.SteamUserName;

 //     //dataSteamId.Text = SteamUserId;
 //     //dataSteamName.Text = SteamName;
 //   }

 //   public void TextBoxChanged()
 //   {
 //     TextBox tb = (sender as TextBox);
 //     if(tb != null)
 //     {
 //       switch(tb.Name.ToString())
 //       {
 //         case "dataSteamName":
 //           SteamName = dataSteamName.Text;
 //           break;
 //         case "dataSteamId":
 //           SteamUserId = dataSteamId.Text;
 //           break;
 //         case "dataNames":
 //           blueprintName = dataNames.Text;
 //           break;
 //       }
 //     }
 //   }

 //   #endregion
 // }
}
