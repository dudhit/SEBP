using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.View
{
    class FindSteamID : IDisposable
    {
        private string steamPath;
        private string steamID;
        private List<string> fileContents;
        public string SteamID { get { return this.steamID; } private set { this.steamID = value; } }
        private string steamName;
        public string SteamName { get { return this.steamName; } private set { this.steamName = value; } }

        public FindSteamID(string steamPath, string steamName)
        {
    
            this.steamPath = steamPath;
            this.steamName = steamName;
            if (ReadSteamLoginFile()) GetSteamIDLinkedToUserName();
        }
      /// <summary>
      /// read the steam login file and if found stores in a list 
      /// </summary>
      /// <returns>true if successful read</returns>
        private bool ReadSteamLoginFile()
        {
            bool noErrors = false;
            //move this to FindSteamID class
            string openFile = steamPath.Replace('/', '\\');
            openFile += "\\config\\loginusers.vdf";
            fileContents = new List<string>();
            try
            {
                using (StreamReader appUserData = new StreamReader(openFile))
                {
                    while (appUserData.Peek() != -1)
                    {
                        fileContents.Add(appUserData.ReadLine().Trim().Replace("\"", ""));
                    }
                    noErrors = true;
                }
            }

            catch (FileNotFoundException FNF) { MessageBox.Show(FNF.Message, "Finding Steam Users", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch (UnauthorizedAccessException UAE) { MessageBox.Show(UAE.Message, "Finding Steam Users", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch (Exception ae) { MessageBox.Show(ae.Message, "Finding Steam Users", MessageBoxButton.OK, MessageBoxImage.Information); }
            return noErrors;
        }

        private void GetSteamIDLinkedToUserName()
        {
            int indexToGet;
            string accName = string.Format("AccountName\t\t{0}", steamName);
            string personalName = string.Format("PersonaName\t\t{0}", steamName);
            if (fileContents.Contains(accName))
            {
                indexToGet = fileContents.IndexOf(accName);
                steamID = fileContents[indexToGet - 2];
            }
            else if (fileContents.Contains(personalName))
            {
                indexToGet = fileContents.IndexOf(personalName);
                steamID = fileContents[indexToGet - 3];
            }
            else ManualSelection();

        }
        private void ManualSelection()
        {
            Window steamIdSelectionWindow = new Window();
            steamIdSelectionWindow.Width = System.Windows.SystemParameters.MaximizedPrimaryScreenWidth / 2;
            steamIdSelectionWindow.Height = System.Windows.SystemParameters.MaximizedPrimaryScreenHeight / 2;

            steamIdSelectionWindow.Title = "Steam login data";// openFile;
            System.Windows.Controls.ScrollViewer scrollViewController = new System.Windows.Controls.ScrollViewer();
            System.Windows.Controls.StackPanel stackPanelController = new System.Windows.Controls.StackPanel() ;
            System.Windows.Controls.Label instructionsLabel = new System.Windows.Controls.Label() {
              Content="The current Steam name DOES NOT match any users known to this system\n select the ID above the chosen user name, then close this window.\nNo refunds for wrong choices", FontSize=20 };
          
            instructionsLabel.HorizontalAlignment = HorizontalAlignment.Center;
            System.Windows.Controls.ListBox contentListBoxControl = new System.Windows.Controls.ListBox();
            foreach (string lineFromFile in fileContents)
            { contentListBoxControl.Items.Add(lineFromFile); }
            stackPanelController.Children.Add(instructionsLabel);
            stackPanelController.Children.Add(contentListBoxControl);
            scrollViewController.Content=stackPanelController;
            steamIdSelectionWindow.Content = scrollViewController;

            if (steamIdSelectionWindow.ShowDialog() == false)
            {
                steamID = (contentListBoxControl.SelectedIndex != -1) ? contentListBoxControl.SelectedValue.ToString() : "0";
            }
    

        }

        #region disposal

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~FindSteamID()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources  
                //if (Encoding != null)
                //{
                //    Encoding.Dispose();
                //    Encoding = null;
                //}
            }

        }
        #endregion
    }
}
