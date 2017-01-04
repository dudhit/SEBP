using SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint.Collection;
using SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;
using System.Windows;
using System.Windows.Documents;

namespace SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint
{
    public partial class MainWindow : Window, IDisposable
    {

        #region blueprint settings tab controls

        private void StartStopCalculating(object sender, RoutedEventArgs e)
        {
            if (!IsCalculating)
            {
                if (ValidateBPPathAndCustoms()) // if (blockSettingChanged||shapeSettingChanged)
                {

                    StartTheCogs();
                    if (blockSettingChanged && !shapeSettingChanged) { ConfigBlueprintBlocks(); BluePrintToFile(); }
                    else
                    {
                        if (shapeSettingChanged)
                        {
                            SetAxisRadius();
                            actionGenerate.Content = "Cancel Blueprint";
                            progressBarOne.Visibility = Visibility.Visible;
                            IsCalculating = true;
                            PlottingProcess();
                        }
                    }


                }
            }
            else
            {
                actionGenerate.Content = "Generate Blueprint";
                progressBarOne.Visibility = Visibility.Collapsed;
                IsCalculating = false;
                if (worker.IsBusy) { worker.CancelAsync(); }
            }
        }

        private void StartTheCogs()
        {

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
        }

        private void ConfigBlueprintBlocks()
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
            blockSettingChanged = false;
        }




        private void PlottingProcess()
        {


            PointContainer.Clear();

            //instantiate point list

            //get bulk points
            //     ps = new ProgressStatus();

            //       ps.BarMaximum=(Math.Abs((shapeSettings.xRadius - 1) * (shapeSettings.yRadius - 1) * (shapeSettings.zRadius - 1)));
            //     ps.BarMinimum = 0;


            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            //ps.Show();

            //  worker.RunWorkerAsync(10000);//send arguments and kick it off
            worker.RunWorkerAsync(shapeSettings);



            //   ps.progressBar.Minimum = 100;


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
  }
             
                    Window tempView = new Window();
                    tempView.Width = System.Windows.SystemParameters.MaximizedPrimaryScreenWidth / 2;
                    tempView.Height = System.Windows.SystemParameters.MaximizedPrimaryScreenHeight / 2;

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
                        dataSteamId.Text = lb.SelectedValue.ToString().Trim().Replace("\"", "");
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

        #region background worker



        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkingArgs parameters = e.Argument as WorkingArgs;
            CircleEvaluationCalculations seperateThread = new CircleEvaluationCalculations();

            //  ps.SubscribeToPointContainer();
            seperateThread.RadiusInXPlane = parameters.xRadius;
            seperateThread.RadiusInYPlane = parameters.yRadius;
            seperateThread.RadiusInZPlane = parameters.zRadius;
            seperateThread.LowToleranceEvaluation = parameters.lowTol;
            seperateThread.HighToleranceEvaluation = parameters.highTol;
            seperateThread.ShapeSelected = parameters.shapeSelected;
            seperateThread.BeginPointChecking();
            //for (int x = 0; x < parameters.xRadius; x++) { 
            //    int progressPercentage = Convert.ToInt32((x / parameters.xRadius) * 100);
            //    (sender as BackgroundWorker).ReportProgress(progressPercentage);
            // System.Threading.Thread.Sleep(1);
            //}
            //  
            // 
            //        result++;
            // 
            //    else{
            //        (sender as BackgroundWorker).ReportProgress(progressPercentage);
            //   

            //}
            e.Result = string.Format("blocks calculated:{0}", PointContainer.Count());

        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //    ps.BarMinimum=  e.ProgressPercentage;
            /*     if (e.UserState != null)
                     lbResults.Items.Add(e.UserState);
          */
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Canceled", "Calculating", MessageBoxButton.OK);
                //    actionGenerate.Content = "Generate Blueprint";
                //    progressBarOne.Visibility = Visibility.Collapsed;
                //     IsCalculating = false;
                //   ps.Close();

            }
            else
            {
                MessageBox.Show(e.Result.ToString(), "Complete", MessageBoxButton.OK);
                actionGenerate.Content = "Generate Blueprint";
                progressBarOne.Visibility = Visibility.Collapsed;
                IsCalculating = false;
                //   MessageBox.Show("Complete", "Calculating", MessageBoxButton.OK);
                //write to file

                BluePrintToFile();
                //     System.Diagnostics.Trace.WriteLine (ps.WindowState.ToString()) ;
                //      ps.Close();
                //       ps = null;
            }
        }
        #endregion
    }

    public class WorkingArgs
    {
        public double xRadius;
        public double yRadius;
        public double zRadius;
        public double lowTol;
        public double highTol;
        public string shapeSelected;

        public WorkingArgs(double x, double y, double z, double low, double high, string shape)
        {
            this.xRadius = x;
            this.yRadius = y;
            this.zRadius = z;
            this.lowTol = low;
            this.highTol = high;
            this.shapeSelected = shape;
        }
    }
}
