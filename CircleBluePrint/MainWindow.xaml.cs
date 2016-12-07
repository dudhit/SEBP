using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Samples.CustomControls;
using SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint;
using System.Windows.Media.Media3D;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;

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
        private string bpName;
        private List<Point3D> tempPointsToParallelise;

        //calculation variables
        private ObservableCollection<Point3D> plotData;
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
            plotData = new ObservableCollection<Point3D>();

            this.DataContext = plotData;
            thePoints.ItemsSource = plotData;
        }

        private void FirstLoad()
        {

            makeCircle.IsChecked = true;
            makeFrame.IsChecked = true;
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
            PathHandler();
            //get steam user name
            GetSteamUserName();

        }

        private void GetSteamUserName()
        {

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey(@"Software\Valve\Steam");

            if (regKey != null)
            {
                steamUserName = regKey.GetValue("LastGameNameUsed").ToString();
                dataSteamName.Content = steamUserName;
            }
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
            MainClose(this, new System.ComponentModel.CancelEventArgs());
        }

        private void ErrorCheckPlotData()
        {
            foreach (Point3D p3d in plotData)
            { System.Diagnostics.Trace.WriteLine(p3d); }
        }

        #endregion

        #region plotting

        /*  int numProc = System.Environment.ProcessorCount;
            int range = maxWait / numProc;
         Parallel.For(0,(int)xRadius+1,x=>
            double result;
                        result = EvalPoint3D(x, y, z);
                        Point3D aPoint = new Point3D((double)x, y, z);
                        SolidOrFrame(result, aPoint);
         */

        private void BeginPointChecking()
        {
            Object lockMe = new Object();
            int maxWait = (int)xRadius * (int)yRadius * (int)zRadius;
            tempPointsToParallelise = new List<Point3D>();
            Parallel.For((int)xRadius - 3, (int)xRadius + 1, x =>
            {
                Parallel.For((int)yRadius - 3, (int)yRadius, y =>
                  {
                      Parallel.For((int)zRadius - 3, (int)zRadius, z =>
                      {
                          lock (lockMe)
                          {
                              System.Diagnostics.Trace.Write("\nx:" + x + " y:" + y + " z:" + z + "\n");
                              tempPointsToParallelise.Add(new Point3D(x, y, z));
                          }
                      });

                  });
            });
            foreach (Point3D p in tempPointsToParallelise)
            {
                plotData.Add(p);
            }
        }
        // Summary:
        //     Calculates whether values x,y,z fit within the bounds of a circle/elipse/sphere(oid)
        //     
        //
        // Returns:
        //     double 0-2 for further processing of desired shape
        private double EvalPoint3D(Point3D point)
        {
            double result;
            if (makeCircle.IsChecked == true || makeElipse.IsChecked == true)
            {
                return result = (Math.Pow(point.X, 2d) / Math.Pow(xRadius, 2d)) + (Math.Pow(point.Y, 2d) / Math.Pow(yRadius, 2d));
            }
            else
            {
                return result = (Math.Pow(point.X, 2d) / Math.Pow(xRadius, 2d)) + (Math.Pow(point.Y, 2d) / Math.Pow(yRadius, 2d)) + (Math.Pow(point.Z, 2d) / Math.Pow(zRadius, 2d));
            }

        }

        private void SolidOrFrame(double result, Point3D p)
        {


            if (makeSolid.IsChecked == true && result <= highTol)
            {
                DoShapePlotting(p);
            }

            if (makeFrame.IsChecked == true && result >= lowTol && result <= highTol)
            {
                DoShapePlotting(p);
            }
        }
        private void DoShapePlotting(Point3D p)
        {
            if (makeQuater.IsChecked == true)
            { PPP(p); }
            if (makeSemi.IsChecked == true && (makeCircle.IsChecked == true || makeElipse.IsChecked == true))
            {
                PPP(p);
                NPP(p);
            }
            if (makeSemi.IsChecked == true && (makeSphere.IsChecked == true || makeElipsoid.IsChecked == true))
            {
                PPP(p); NPP(p); PPN(p);
                NPN(p);
            }
            if (makeFull.IsChecked == true && (makeCircle.IsChecked == true || makeElipse.IsChecked == true))
            {
                PPP(p); NPP(p);
                PNP(p);
                NNP(p);
            }
            if (makeFull.IsChecked == true && (makeSphere.IsChecked == true || makeElipsoid.IsChecked == true))
            {
                PPP(p);
                NPP(p);
                PPN(p);
                NPN(p);
                PNP(p);
                NNP(p);
                PNN(p);
                NNN(p);
            }
            //   return p;
        }
        #region transformations
        private void PPP(Point3D p)
        {
            if (plotData.Contains(p)) return;
            plotData.Add(p);
        }
        private void NPP(Point3D p)
        {
            PPP(new Point3D(p.X * -1, p.Y, p.Z));
        }
        private void PNP(Point3D p)
        {
            PPP(new Point3D(p.X, p.Y * -1, p.Z));
        }
        private void PPN(Point3D p)
        {
            PPP(new Point3D(p.X, p.Y, p.Z * -1));
        }
        private void NNP(Point3D p)
        {
            PPP(new Point3D(p.X * -1, p.Y * -1, p.Z));
        }
        private void PNN(Point3D p)
        {
            PPP(new Point3D(p.X, p.Y * -1, p.Z * -1));
        }
        private void NPN(Point3D p)
        {
            PPP(new Point3D(p.X * -1, p.Y, p.Z * -1));
        }
        private void NNN(Point3D p)
        {
            PPP(new Point3D(p.X * -1, p.Y * -1, p.Z * -1));
        }

        #endregion



        #endregion

        #region blueprint settings tab controls
        private void StartTheCogs(object sender, RoutedEventArgs e)
        {

            if (ValidateBPPathAndCustoms())
            {
                gridSize = (blockLarge.IsChecked == true) ? "Large" : "Small";
                switch (gridSize)
                {
                    case "Large":
                        armourType = (blockNormal.IsChecked == true) ? "LargeBlockArmorBlock" : "LargeHeavyBlockArmorBlock";
                        break;

                    case "Small":
                        armourType = (blockNormal.IsChecked == true) ? "SmallBlockArmorBlock" : "SmallHeavyBlockArmorBlock";
                        break;
                }
            }


            steamUserId = dataSteamId.Text;
            bpName = dataNames.Text;

            //instantiate point list
            SetAxisRadius();

            BeginPointChecking();


            bpFolder = localBP + "\\" + bpName;
            try
            {

                Directory.CreateDirectory(bpFolder);
            }
            catch (UnauthorizedAccessException UAE)
            {
                MessageBox.Show(UAE.Message, "info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
            //write to file

            BluePrintToFile();
            //   SplashScreen ss = new SplashScreen("/images/2016-11-24-10-17-53-472.png");
            //    ss.Show(false);
            //   TimeSpan ts = new TimeSpan(0, 0, 30);
            //    ss.Close(ts);
            //      }
        }

        private bool ValidateBPPathAndCustoms()
        {
            bool proceed = true;
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(dataSE_Path.Text)) { errorMessage += "You need to specify your Space Engineers save folder\n"; proceed = false; }
            if (string.IsNullOrWhiteSpace(dataSteamId.Text)) { errorMessage += "Your blueprint might not work without your Steam Id, test for yourself\n"; proceed = false; }
            if (string.IsNullOrWhiteSpace(dataNames.Text)) { errorMessage += "This was your chance to not have a generic name\n like Large Grid 4231 and you blew it\n"; proceed = false; }
            if (errorMessage != "") { MessageBox.Show(errorMessage, "Critical Data Missing", MessageBoxButton.OK, MessageBoxImage.Exclamation); }

            return proceed;
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
                using (StreamWriter appUserData = new StreamWriter(CONFIG_FILE))
                {
                    appUserData.WriteLine(window.Width);
                    appUserData.WriteLine(window.Height);
                    appUserData.WriteLine(window.Left);
                    appUserData.WriteLine(window.Top);

                    //tab1
                    appUserData.WriteLine(S_E_Home);
                    appUserData.WriteLine(dataSteamId.Text);
                    appUserData.WriteLine(dataNames.Text);
                    //tab2
                    appUserData.WriteLine(makeCircle.IsChecked);
                    appUserData.WriteLine(makeElipse.IsChecked);
                    appUserData.WriteLine(makeSphere.IsChecked);
                    appUserData.WriteLine(makeElipsoid.IsChecked);
                    appUserData.WriteLine(makeQuater.IsChecked);
                    appUserData.WriteLine(makeSemi.IsChecked);
                    appUserData.WriteLine(makeFull.IsChecked);
                    appUserData.WriteLine(makeFrame.IsChecked);
                    appUserData.WriteLine(makeSolid.IsChecked);
                    appUserData.WriteLine(lowertoleranceSlide.Value);
                    appUserData.WriteLine(uppertoleranceSlide.Value);
                    appUserData.WriteLine(radOneSlide.Value);
                    appUserData.WriteLine(radTwoSlide.Value);
                    appUserData.WriteLine(radThreeSlide.Value);
                    //tab3                   
                    appUserData.WriteLine(blockNormal.IsChecked);
                    appUserData.WriteLine(blockHeavy.IsChecked);
                    appUserData.WriteLine(blockLarge.IsChecked);
                    appUserData.WriteLine(blockSmall.IsChecked);
                    appUserData.WriteLine(colBlack.IsChecked);
                    appUserData.WriteLine(colBlack1.IsChecked);
                    appUserData.WriteLine(colBlue.IsChecked);
                    appUserData.WriteLine(colBlue1.IsChecked);
                    appUserData.WriteLine(colRed.IsChecked);
                    appUserData.WriteLine(colRed1.IsChecked);
                    appUserData.WriteLine(colGreen.IsChecked);
                    appUserData.WriteLine(colGreen1.IsChecked);
                    appUserData.WriteLine(colYellow.IsChecked);
                    appUserData.WriteLine(colYellow1.IsChecked);
                    appUserData.WriteLine(colWhite.IsChecked);
                    appUserData.WriteLine(colWhite1.IsChecked);
                    appUserData.WriteLine(colGrey.IsChecked);
                    appUserData.WriteLine(colGrey1.IsChecked);
                    appUserData.WriteLine(colCustom.IsChecked);
                    appUserData.WriteLine(FillColor.ToString());

                    // added as i go
                    appUserData.WriteLine(S_E_B_P);
                    appUserData.WriteLine(localBP);
                    appUserData.Close();
                }
            }
            catch (FileNotFoundException FNF) { MessageBox.Show(FNF.Message, "Saving user data", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch (UnauthorizedAccessException UAE) { MessageBox.Show(UAE.Message, "Saving user data", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch (Exception ae) { MessageBox.Show(ae.Message, "Saving user data", MessageBoxButton.OK, MessageBoxImage.Information); }



        }

        private void LoadUserSettings()
        {
            List<string> settings = new List<string>();
            try
            {
                using (StreamReader appUserData = new StreamReader(CONFIG_FILE))
                {
                    while (appUserData.Peek() != -1)
                    {
                        settings.Add(appUserData.ReadLine());

                    }

                    appUserData.Close();
                }
            }
            catch (FileNotFoundException FNF) { MessageBox.Show(FNF.Message, "Loading settings", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch (UnauthorizedAccessException UAE) { MessageBox.Show(UAE.Message, "Loading settings", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch (Exception ae) { MessageBox.Show(ae.Message, "Loading settings", MessageBoxButton.OK, MessageBoxImage.Information); }

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
            if (settings[15] == "True") makeFrame.IsChecked = true;
            if (settings[16] == "True") makeSolid.IsChecked = true;
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
            localBP = settings[43];
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


        private void Find_Path(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Feature not implemented", "info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }


        private void PathHandler()
        {

            userApp = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (S_E_Home == null)
            {
                S_E_Home = userApp + "\\SpaceEngineers";
            }
            if (S_E_B_P == null)
            { S_E_B_P = S_E_Home + "\\Blueprints"; }
            if (localBP == null)
            {
                localBP = S_E_B_P + "\\local";
            }
            dataSE_Path.Text = S_E_Home;
            dataSE_Path.Width = S_E_Home.Length;
            if (!Directory.Exists(S_E_Home))
            {
                string message = string.Format("Space Engineers save folder: {0} \ndoes not exist.\n Do you want to create it?\n\nNote: this is the expected location, If you have moved it select No, and use the browse feature to locate and set it. ", S_E_Home);
                MessageBoxResult result = MessageBox.Show(message, "Path location error", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        Directory.CreateDirectory(S_E_Home);
                        Directory.CreateDirectory(S_E_B_P);
                        Directory.CreateDirectory(localBP);
                    }
                    catch (UnauthorizedAccessException UAE)
                    {
                        MessageBox.Show(UAE.Message, "info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    }
                }
            }
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

                foreach (Point3D p in plotData) { bluePrint.PopulateGrid = p; }
                bluePrint.MakeBaseStructure();


            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        #endregion





    }
}
