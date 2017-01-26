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
            if (ReadSteamLoginFile()) SearchForUserName();
        }

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

        private void SearchForUserName()
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
            Window tempView = new Window();
            tempView.Width = System.Windows.SystemParameters.MaximizedPrimaryScreenWidth / 2;
            tempView.Height = System.Windows.SystemParameters.MaximizedPrimaryScreenHeight / 2;

            tempView.Title = "Steam login date";// openFile;
            System.Windows.Controls.StackPanel sp = new System.Windows.Controls.StackPanel();
            System.Windows.Controls.Label instructions = new System.Windows.Controls.Label();
            instructions.Content = "select the ID above the chosen user name, then close this window.";
            instructions.Content += "\nNo refunds for wrong choices";
            instructions.HorizontalAlignment = HorizontalAlignment.Center;
            System.Windows.Controls.ListBox lb = new System.Windows.Controls.ListBox();
            foreach (string s in fileContents)
            { lb.Items.Add(s); }
            sp.Children.Add(instructions);
            sp.Children.Add(lb);
            tempView.Content = sp;

            if (tempView.ShowDialog() == false)
            {
                steamID = (lb.SelectedIndex != -1) ? lb.SelectedValue.ToString() : "2468 You Love To Masterbate";
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
