using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Media3D;
using Microsoft.Win32;
using SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint.Data;
using System.ComponentModel;

namespace SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    // an app to create a circular blueprint for space engineers
    // requires location of user saves folder:

    [SerializableAttribute]
    public partial class MainWindow : Window
    {
        //path --folder -- file variables

        private string userApp;
        private const string CONFIG_FILE = "config.ini";
        private string S_E_Home;
        private string S_E_B_P;
        private string localBP;
        private string bpFolder;
        private string steamUserName;
        private string steamUserId;
        private string steamPath;
        private string bpName;
        //   private List<Point3D> tempPointsToParallelise;

        //calculation variables
        //  private List<Point3D> plotData;
        private double xRadius;
        private double yRadius;
        private double zRadius;
        private double lowTol;
        private double highTol;

        //blocks
        private float blockColourHue;
        private float blockColourSaturation;
        private float blockColourValue;
        string gridSize;
        string armourType;
        //external view
        PreviewThreeD previewViewer;
   
      
        //run this to reset and on load without a save file
        #region main window flow and menu

        public MainWindow()
        {
            InitializeComponent();

            if (!File.Exists(CONFIG_FILE)) { FirstLoad(); } else { LoadUserSettings(); }
    
            //  PathHandler(this, new RoutedEventArgs());
            //   plotData = new List<Point3D>();

        }
  
        private void FirstLoad()
        {
            makeCircle.IsChecked = true;
            makeQuater.IsChecked = true;
            blockNormal.IsChecked = true;
            blockLarge.IsChecked = true;
            colGrey.IsChecked = true;
            dataNames.Text = "";
            dataSteamId.Text = "";
            dataSE_Path.Text = "";
            radOneSlide.Value = 10;
            radTwoSlide.Value = 10;
            radThreeSlide.Value = 10;
            uppertoleranceSlide.Value = 100;
            lowertoleranceSlide.Value = 100;
            window.Height = 450;
            window.Width = 500;
            window.Left = 20;
            window.Top = 20;
            ///FillColor = "";
         //   PathHandler();
            //get steam user name and install path
            GetSteamData();
            shapeSettingChanged = true;
        
      
            
        }

        private void GetSteamData()
        {

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey(@"Software\Valve\Steam");

            if (regKey != null)
            {
               steamPath = regKey.GetValue("SteamPath").ToString();
          
                steamUserName = regKey.GetValue("LastGameNameUsed").ToString();
                dataSteamName.Content = steamUserName;
            }
            else { dataSteamName.Content = "No Steam Name"; }
        }

        private void ResetToLoaded(object sender, RoutedEventArgs e)
        {
            S_E_Home = null;
            S_E_B_P = null;
            localBP = null;
            FirstLoad();
        }

        private void MainClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine(e.ToString());
            MessageBoxResult goodBye = MessageBox.Show("Do you intend on leaving? ", "exit app", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
            if (goodBye != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }



        private void MenuExit(object sender, RoutedEventArgs e)
        {
            this.Close();
                  }

        #endregion



        #region file handling
        //save user paths - radio buttons - colours -everything
        private void SaveUserSettings(object sender, RoutedEventArgs e)
        {

            if (!File.Exists(CONFIG_FILE))
            {
                try
                {
                    using (System.IO.File.Create(CONFIG_FILE)) { }
                }
                catch (UnauthorizedAccessException UAE) { MessageBox.Show(UAE.Message, "Cannot Save user data due to missing Access rights", MessageBoxButton.OK, MessageBoxImage.Information); }
            }

            try
            {
         
                UserSettings<string, string> us = new UserSettings<string, string>();
                    //tab1      
                    us.Add("SaveVersion", "1");
                    us.Add("S_E_B_P", S_E_B_P);
                    us.Add("S_E_Home", S_E_Home);
                    us.Add(dataNames.Name, dataNames.Text);
                    us.Add(dataSteamId.Name, dataSteamId.Text);
                    us.Add("localBP", localBP);
                    us.Add("steamUserName", steamUserName);

                    us.Add("window.Height", window.Height.ToString());
                    us.Add("window.Width", window.Width.ToString());
                    us.Add("window.Left", window.Left.ToString());
                    us.Add("window.Top", window.Top.ToString());
                    us.Add("window.WindowState", window.WindowState.ToString());

                    //tab2     
                    us.Add(uppertoleranceSlide.Name, uppertoleranceSlide.Value.ToString());
                    us.Add(lowertoleranceSlide.Name, lowertoleranceSlide.Value.ToString());
                    us.Add(radOneSlide.Name, radOneSlide.Value.ToString());
                    us.Add(radTwoSlide.Name, radTwoSlide.Value.ToString());
                    us.Add(radThreeSlide.Name, radThreeSlide.Value.ToString());
                    us.Add(makeCircle.Name, makeCircle.IsChecked.ToString());
                    us.Add(makeElipse.Name, makeElipse.IsChecked.ToString());
                    us.Add(makeSphere.Name, makeSphere.IsChecked.ToString());
                    us.Add(makeElipsoid.Name, makeElipsoid.IsChecked.ToString());
                    us.Add(makeQuater.Name, makeQuater.IsChecked.ToString());
                    us.Add(makeSemi.Name, makeSemi.IsChecked.ToString());
                    us.Add(makeFull.Name, makeFull.IsChecked.ToString());
                    //tab3         

                    us.Add("FillColor", FillColor.ToString());
                    us.Add(blockHeavy.Name, blockHeavy.IsChecked.ToString());
                    us.Add(blockLarge.Name, blockLarge.IsChecked.ToString());
                    us.Add(blockNormal.Name, blockNormal.IsChecked.ToString());
                    us.Add(blockSmall.Name, blockSmall.IsChecked.ToString());
                    us.Add(colCustom.Name, colCustom.IsChecked.ToString());
                    us.Add(colBlack.Name, colBlack.IsChecked.ToString());
                    us.Add(colBlack1.Name, colBlack1.IsChecked.ToString());
                    us.Add(colBlue.Name, colBlue.IsChecked.ToString());
                    us.Add(colBlue1.Name, colBlue1.IsChecked.ToString());
                    us.Add(colGreen.Name, colGreen.IsChecked.ToString());
                    us.Add(colGreen1.Name, colGreen1.IsChecked.ToString());
                    us.Add(colGrey.Name, colGrey.IsChecked.ToString());
                    us.Add(colGrey1.Name, colGrey1.IsChecked.ToString());
                    us.Add(colRed.Name, colRed.IsChecked.ToString());
                    us.Add(colRed1.Name, colRed1.IsChecked.ToString());
                    us.Add(colWhite.Name, colWhite.IsChecked.ToString());
                    us.Add(colWhite1.Name, colWhite1.IsChecked.ToString());
                    us.Add(colYellow.Name, colYellow.IsChecked.ToString());
                    us.Add(colYellow1.Name, colYellow1.IsChecked.ToString());
       
  using (StreamWriter appUserData = new StreamWriter(CONFIG_FILE))
                {
                    //System.Xml.XmlWriter writer = new System.Xml.XmlWriter();
        System.Xml.Serialization.XmlSerializer settingsToXML = new System.Xml.Serialization.XmlSerializer(us.GetType());
        settingsToXML.Serialize(appUserData, us);

      //foreach (KeyValuePair<string, string> kvp in us)
      //              {
      //                  appUserData.WriteLine(kvp);
      //              }
                    appUserData.Close();
                }
            }
            catch (FileNotFoundException FNF) { MessageBox.Show(FNF.Message, "Saving user data", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch (UnauthorizedAccessException UAE) { MessageBox.Show(UAE.Message, "Saving user data", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch (NullReferenceException nre) { MessageBox.Show(nre.Message, "Saving user data", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch (Exception ae) { MessageBox.Show(ae.Message, "Saving user data", MessageBoxButton.OK, MessageBoxImage.Information); }

           

        }

        private void LoadUserSettings()
        {
            UserSettings<string, string> settings = new UserSettings<string, string>();
            try
            {
                using (StreamReader appUserData = new StreamReader(CONFIG_FILE))
                {
                    while (appUserData.Peek() != -1)
                    {
                      //  settings.Add(new KeyValuePair<appUserData.ReadLine()>);
                        /*<UserControl ...>
    <TextBlock x:Name="myTextBlock" />
</UserControl>

In the code-behind file, you could write:

var myTextBlock = (TextBlock)this.FindName("myTextBlock");
                         
                         var enumerator = d.GetEnumerator();
    while (enumerator.MoveNext())
    {
	var pair = enumerator.Current;
	b += pair.Value;
    }
                         */
                        appUserData.ReadLine();
                    }

                    appUserData.Close();
                }
            }
            catch (FileNotFoundException FNF) { MessageBox.Show(FNF.Message, "Loading settings", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch (UnauthorizedAccessException UAE) { MessageBox.Show(UAE.Message, "Loading settings", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch (Exception ae) { MessageBox.Show(ae.Message, "Loading settings", MessageBoxButton.OK, MessageBoxImage.Information); }
            /*
            window.Width = double.Parse(settings[0]);
            window.Height = double.Parse(settings[1]);
            window.Left = double.Parse(settings[2]);
            window.Top = double.Parse(settings[3]);

            //tab1
            S_E_Home = settings[4];
            dataSE_Path.Text = settings[5];
            dataSteamId.Text = settings[6];
            dataNames.Text = settings[7];
            //tab2
            if (settings[8] == "True") makeCircle.IsChecked = true;
            if (settings[9] == "True") makeElipse.IsChecked = true;
            if (settings[10] == "True") makeSphere.IsChecked = true;
            if (settings[11] == "True") makeElipsoid.IsChecked = true;
            if (settings[12] == "True") makeQuater.IsChecked = true;
            if (settings[13] == "True") makeSemi.IsChecked = true;
            if (settings[14] == "True") makeFull.IsChecked = true;
            lowertoleranceSlide.Value = double.Parse(settings[17]);
            uppertoleranceSlide.Value = double.Parse(settings[18]);
            radOneSlide.Value = double.Parse(settings[19]);
            radTwoSlide.Value = double.Parse(settings[20]);
            radThreeSlide.Value = double.Parse(settings[21]);
            //tab3                   
            if (settings[22] == "True") blockNormal.IsChecked = true;
            if (settings[23] == "True") blockHeavy.IsChecked = true;
            if (settings[24] == "True") blockLarge.IsChecked = true;
            if (settings[25] == "True") blockSmall.IsChecked = true;
            if (settings[26] == "True") colBlack.IsChecked = true;
            if (settings[27] == "True") colBlack1.IsChecked = true;
            if (settings[28] == "True") colBlue.IsChecked = true;
            if (settings[29] == "True") colBlue1.IsChecked = true;
            if (settings[30] == "True") colRed.IsChecked = true;
            if (settings[31] == "True") colRed1.IsChecked = true;
            if (settings[32] == "True") colGreen.IsChecked = true;
            if (settings[33] == "True") colGreen1.IsChecked = true;
            if (settings[34] == "True") colYellow.IsChecked = true;
            if (settings[35] == "True") colYellow1.IsChecked = true;
            if (settings[36] == "True") colWhite.IsChecked = true;
            if (settings[37] == "True") colWhite1.IsChecked = true;
            if (settings[38] == "True") colGrey.IsChecked = true;
            if (settings[39] == "True") colGrey1.IsChecked = true;
            if (settings[40] == "True") colCustom.IsChecked = true;
            FillColor = Color.FromArgb(StripToIndvidualHex(settings[41], 'a'), StripToIndvidualHex(settings[41], 'r'), StripToIndvidualHex(settings[41], 'g'), StripToIndvidualHex(settings[41], 'b'));

            S_E_B_P = settings[42];
            localBP = settings[43];*/
        }

        private void ReloadSettings(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(CONFIG_FILE))
            {
                MessageBox.Show("There are no setting to load.\n either they have never been saved or the file has been removed.", "info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
            else { LoadUserSettings(); }
        }


        //give a hexadecimal string and specify r b g to return
        private byte StripToIndvidualHex(string hexString, char colour)
        {
            byte value = 0;
            if (!(string.IsNullOrWhiteSpace(hexString)) && hexString.Length == 9)
            {
                if (char.ToLower(colour) == 'a') { value = Convert.ToByte(hexString.Substring(1, 2), 16); }
                if (char.ToLower(colour) == 'r') { value = Convert.ToByte(hexString.Substring(3, 2), 16); }
                if (char.ToLower(colour) == 'g') { value = Convert.ToByte(hexString.Substring(5, 2), 16); }
                if (char.ToLower(colour) == 'b') { value = Convert.ToByte(hexString.Substring(7, 2), 16); }
            }
            return value;
        }


     


     
   

        private void BluePrintToFile()
        {
            try
            {
                BluePrintXml bluePrint = new BluePrintXml();
                bluePrint.Path = bpFolder + "\\bp.sbc";
                bluePrint.SteamUserId = steamUserId;
                bluePrint.SteamUserName = steamUserName;
                bluePrint.BluePrintName = bpName;
                bluePrint.GridSizeEnum = gridSize;
                bluePrint.BlockType = armourType;
                bluePrint.BlockColour = new Point3D(blockColourHue, blockColourSaturation, blockColourValue);

                //  foreach (Point3D p in plotData) { bluePrint.PopulateGrid = p; }
                bluePrint.MakeBaseStructure();


            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        #endregion

   
    










    }

}
