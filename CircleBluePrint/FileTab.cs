using SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint.Collection;
using SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Documents;

namespace SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint
{
    public partial class MainWindow : Window
    {

        #region blueprint settings tab controls
        private void StartTheCogs(object sender, RoutedEventArgs e)
        {
            actionGenerate.IsEnabled = false;
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
                steamUserId = dataSteamId.Text;
                bpName = dataNames.Text;
                PathHandler();

                bpFolder = localBP + "\\" + bpName;
                try
                {
                    Directory.CreateDirectory(bpFolder);
                }
                catch (UnauthorizedAccessException UAE)
                {
                    MessageBox.Show(UAE.Message, "info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                }
                //start the heavy work
                //  PlottingProcess();
                //write to file

                //    BluePrintToFile();
            }
            actionGenerate.IsEnabled = true;
        }




        private void PlottingProcess()
        {
            if (shapeSettingChanged == false) return;
            PointContainer.Clear();

            //instantiate point list
            SetAxisRadius();
            //get bulk points
            CircleEvaluationCalculations seperateThread = new CircleEvaluationCalculations();
            ProgressStatus ps = new ProgressStatus();
            ps.Show();
            ps.SubscribeToEvaluator(seperateThread);
            seperateThread.RadiusInXPlane = xRadius;
            seperateThread.RadiusInYPlane = yRadius;
            seperateThread.RadiusInZPlane = zRadius;
            seperateThread.LowToleranceEvaluation = lowTol;
            seperateThread.HighToleranceEvaluation = highTol;
            seperateThread.ShapeSelected = shapeSelected;
            Thread calculationThread = new Thread(new ThreadStart(seperateThread.BeginPointChecking));
            calculationThread.Name = "Isolated from UI";
            calculationThread.Priority = ThreadPriority.Highest;
            try
            {
                calculationThread.Start();
            }
            catch (ThreadStateException te)
            {
                System.Diagnostics.Trace.Write(te.ToString());
            }
            calculationThread.Join();
            //     calculationThread = null;
            //     seperateThread = null;
            shapeSettingChanged = false;
            ps.Close();
        }



        private void SnoopForSteamID(object sender, RoutedEventArgs e)
        {
            //move this to FindSteamID class
            string openFile = steamPath.Replace('/', '\\');
            openFile += "\\config\\loginusers.vdf";
            List<string> fileContents = new List<string>();
            try
            {
                using (StreamReader appUserData = new StreamReader(openFile))
                {
                    while (appUserData.Peek() != -1)
                    {
                        fileContents.Add(appUserData.ReadLine());
                    }

                    appUserData.Close();

                    Window tempView = new Window();
                    tempView.Width = System.Windows.SystemParameters.MaximizedPrimaryScreenWidth/2;
                    tempView.Height = System.Windows.SystemParameters.MaximizedPrimaryScreenHeight/2;

                    tempView.Title = openFile;
                    System.Windows.Controls.StackPanel sp = new System.Windows.Controls.StackPanel();
                    System.Windows.Controls.Label instructions = new System.Windows.Controls.Label();
                    instructions.Content = "select the ID above the chosen user name, then close this window.";
                    instructions.HorizontalAlignment = HorizontalAlignment.Center;
                    System.Windows.Controls.ListBox lb = new System.Windows.Controls.ListBox();
                    foreach (string s in fileContents)
                    { lb.Items.Add(s); }
                    sp.Children.Add(instructions);
                    sp.Children.Add(lb);
                    tempView.Content = sp;

                    if (tempView.ShowDialog() == false)
                    {
                                          dataSteamId.Text= lb.SelectedValue.ToString().Trim().Replace("\"", "");
                                          }
                }
            }
            catch (FileNotFoundException FNF) { MessageBox.Show(FNF.Message, "Finding Steam Users", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch (UnauthorizedAccessException UAE) { MessageBox.Show(UAE.Message, "Finding Steam Users", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch (Exception ae) { MessageBox.Show(ae.Message, "Finding Steam Users", MessageBoxButton.OK, MessageBoxImage.Information); }


        }




        private bool ValidateBPPathAndCustoms()
        {
            bool proceed = true;
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(dataSE_Path.Text)) { errorMessage += "You need to specify your Space Engineers save folder\n"; proceed = false; }
            if (string.IsNullOrWhiteSpace(dataSteamId.Text)) { errorMessage += "This still works without YOUR Steam Id, but put some number\n"; proceed = false; }
            if (string.IsNullOrWhiteSpace(dataNames.Text)) { errorMessage += "This was your chance to not have a generic name\n like Large Grid 4231 and you blew it\n"; proceed = false; }
            if (errorMessage != "") { MessageBox.Show(errorMessage, "Critical Data Missing", MessageBoxButton.OK, MessageBoxImage.Exclamation); }

            return proceed;
        }

        private void PathHandler()
        {


            if (S_E_B_P == null)
            { S_E_B_P = S_E_Home + "\\Blueprints"; }
            if (localBP == null)
            {
                localBP = S_E_B_P + "\\local";
            }


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

        private void Find_Path(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog findFolder = new Microsoft.Win32.OpenFileDialog();
            findFolder.Filter = "All files (*.*)|*.*";
            if (findFolder.ShowDialog() == true)
            {
                S_E_Home = null;
                char[] kill = new char[] { '\\' };
                int j = findFolder.FileName.Length;
                string[] x = findFolder.FileName.Split(kill);
                for (int i = 0; i < x.Length - 1; i++)
                { S_E_Home += x[i] + "\\"; }
                dataSE_Path.MinWidth = S_E_Home.Length * 2;
                dataSE_Path.Text = S_E_Home;
                S_E_B_P = null; localBP = null;
            }
            findFolder = null;
        }

        private void AutoPopulateSaveLocation(object sender, RoutedEventArgs e)
        {
            userApp = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            S_E_Home = userApp + "\\SpaceEngineers";
            dataSE_Path.MinWidth = S_E_Home.Length * 2;
            dataSE_Path.Text = S_E_Home;
            S_E_B_P = null; localBP = null;
        }
        #endregion


    }
}
