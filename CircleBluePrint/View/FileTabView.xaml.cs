using SoloProjects.Dudhit.SpaceEngineers.SEBP.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.View
{
    /// <summary>
    /// Interaction logic for FileTabView.xaml
    /// </summary>
    public partial class FileTabView : UserControl
    {
        private string userApp;
        private string saveRootLocation;
        public string SteamName;
        public string SteamUserId;
        private string blueprintName;
        public FileTabView()
        {
            InitializeComponent();
        }

        public FileTabView(IFileTabViewModel vm) : this()
        {
          this.DataContext=vm;
            dataSE_Path.IsEnabled = false;
            Reset();
        }

        private void Reset()
        {
            saveRootLocation = "";
            blueprintName = "";
            SteamName = "";
            SteamUserId = "";
        }

        private void AutoPopulateSaveLocation(object sender, RoutedEventArgs e)
        {
            userApp = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            saveRootLocation = userApp + "\\SpaceEngineers";
            UpdateSavePath();
        }

        private void Find_Path(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog findFolder = new Microsoft.Win32.OpenFileDialog();
            findFolder.Filter = "All files (*.*)|*.*";
            if (findFolder.ShowDialog() == true)
            {
                saveRootLocation = null;
                //split path into individual folders
                char[] kill = new char[] { '\\' };
                int j = findFolder.FileName.Length;
                string[] x = findFolder.FileName.Split(kill);
                //rejoin excluding the file selected
                for (int i = 0; i < x.Length - 1; i++)
                {
                    saveRootLocation += x[i] + "\\";
                }
                findFolder = null;
                UpdateSavePath();
            }
        }

        private void UpdateSavePath()
        {
            string errorMessage = "";
            if(!StringHasValue(saveRootLocation))
            {
              errorMessage += "The blueprint requires the location of the Space Engineers save folder\n";
            }
            else
            {
                dataSE_Path.MinWidth = saveRootLocation.Length * 2;
                dataSE_Path.Text = saveRootLocation;
            }
            if (errorMessage != "") { MessageBox.Show(errorMessage, "Critical Data Missing", MessageBoxButton.OK, MessageBoxImage.Exclamation); }

        }

        private bool StringHasValue(string validate)
        {
            bool check = (string.IsNullOrWhiteSpace(validate)) ? false : true;
            return check;
        }



        private void SnoopForSteamID(object sender, RoutedEventArgs e)
        {
            // call find steamid class

            //((App)Application.Current).controller.GetSteamId();
            //SteamUserId = ((App)Application.Current).controller.SteamUserId;
            //SteamName = ((App)Application.Current).controller.SteamUserName;
         
            //dataSteamId.Text = SteamUserId;
            //dataSteamName.Text = SteamName;
          
        }



        #region events
        //public delegate void FileChangeHandler(object sender, FileChangeEventArgs fileArgs);
        //// an instance of the delegate
        //public event FileChangeHandler FileChangedEvent;

        #endregion

        private void TextBoxChanged(object sender, TextChangedEventArgs e)
        {
           
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                switch (tb.Name.ToString())
                {
                    case "dataSteamName":
                        SteamName = dataSteamName.Text;
                        break;
                    case "dataSteamId":
                        SteamUserId = dataSteamId.Text;
                        break;
                    case "dataNames":
                        blueprintName = dataNames.Text;
                        break;
                }
                //FileChangeEventArgs fce = new FileChangeEventArgs(saveRootLocation, SteamName, SteamUserId, blueprintName);
                //OnFileChangeEvent(fce);

            }
        }

        //private void OnFileChangeEvent(FileChangeEventArgs fce)
        //{
        //    FileChangeHandler handler = FileChangedEvent;
        //    if (handler != null) handler(this, fce);
        //}


    }
}
