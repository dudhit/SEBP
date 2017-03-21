using Microsoft.Win32;
//using SoloProjects.Dudhit.SpaceEngineers.SEBP.EventArguments;
using System.ComponentModel;
using System.Windows;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
  public class SEBluePrintController
  {
    //private string bpName;
    public string SteamPath { get; private set; }
    public string SteamUserName { get; private set; }
    public string SteamUserId { get; private set; }
    private BackgroundWorker worker;
    private bool haveBlockData;
    private bool haveShapeData;
    private bool haveFileData;
    private bool isWorking;

    public SEBluePrintController()
    {
      worker = new BackgroundWorker();
      Reset2();
    }

    private void Reset2()
    {
      haveBlockData = false;
      haveShapeData = false;
      haveFileData = false;
      isWorking = false;
    }

    public void SubscribeToOthers()
    {
      //((App)Application.Current).Windows[0].//

    }


    public void UnSubscribeFromOthers()
    {

    }

    public void GetSteamId()
    {
      if(GetSteamRegistryData())
      {
        using(SoloProjects.Dudhit.SpaceEngineers.SEBP.View.FindSteamID fsID =
           new SoloProjects.Dudhit.SpaceEngineers.SEBP.View.FindSteamID(SteamPath, SteamUserName)) { SteamUserId = fsID.SteamID; }
      }
      else
      {
        MessageBox.Show("Windows could not return the location of your Steam installation\n manual entry required", "Unknown Steam Installation location", MessageBoxButton.OK, MessageBoxImage.Warning);

      }
    }

    //public void UpdateFileData(FileChangeEventArgs data)
    //{
    //  this.fileData = data;
    //  haveFileData = true;
    //  //validate data
    //  string errorMessage = "";

    //  if(string.IsNullOrWhiteSpace(fileData.pathToSEfolder)) { errorMessage += "The blueprint requires the location of the Space Engineers save folder\n"; haveFileData = false; }
    //  if(string.IsNullOrWhiteSpace(fileData.steamName)) { errorMessage += "The blueprint requires a user name\n"; haveFileData = false; }
    //  if(string.IsNullOrWhiteSpace(fileData.steamId)) { errorMessage += "The blueprint requires a user steam id or something like it\n"; haveFileData = false; }
    //  if(string.IsNullOrWhiteSpace(fileData.bluePrintName)) { errorMessage += "This was your chance to not have a generic name\n like Large Grid 4231 and you blew it\n"; haveFileData = false; }
    //  if(!haveFileData) { MessageBox.Show(errorMessage, "Critical Data Missing", MessageBoxButton.OK, MessageBoxImage.Exclamation); }

    //}

  
    private void StartTheCogs(string steamUserId)
    {

      //SteamUserId = dataSteamId.Text;
      //bpName = dataNames.Text;
      //PathHandler();

      //bpFolder = localBP + "\\" + bpName;
      //try
      //{
      //    Directory.CreateDirectory(bpFolder);
      //}
      //catch (UnauthorizedAccessException UAE)
      //{
      //    MessageBox.Show(UAE.Message, "info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
      //}
    }


    private bool GetSteamRegistryData()
    {
      RegistryKey regKey = Registry.CurrentUser;
      regKey = regKey.OpenSubKey(@"Software\Valve\Steam");

      if(regKey != null)
      {
        SteamPath = regKey.GetValue("SteamPath").ToString();
        SteamUserName = regKey.GetValue("LastGameNameUsed").ToString();
        return true;
      }
      return false;
    }


    private void ResetToLoaded()
    {
      //saveRootLocation = null;
      //S_E_B_P = null;
      //localBP = null;
      //FirstLoad();
    }
    #region file handling
    //save user paths - radio buttons - colours -everything
    private void SaveUserSettings()
    {
      /*
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
                      us.Add("saveRootLocation", saveRootLocation);
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
                      //us.Add(uppertoleranceSlide.Name, uppertoleranceSlide.Value.ToString());
                      //us.Add(lowertoleranceSlide.Name, lowertoleranceSlide.Value.ToString());
                      //us.Add(radOneSlide.Name, radOneSlide.Value.ToString());
                      //us.Add(radTwoSlide.Name, radTwoSlide.Value.ToString());
                      //us.Add(radThreeSlide.Name, radThreeSlide.Value.ToString());
                      //us.Add(makeCircle.Name, makeCircle.IsChecked.ToString());
                      //us.Add(makeellipse.Name, makeellipse.IsChecked.ToString());
                      //us.Add(makeSphere.Name, makeSphere.IsChecked.ToString());
                      //us.Add(makeellipsoid.Name, makeellipsoid.IsChecked.ToString());
                      //us.Add(makeQuater.Name, makeQuater.IsChecked.ToString());
                      //us.Add(makeSemi.Name, makeSemi.IsChecked.ToString());
                      //us.Add(makeFull.Name, makeFull.IsChecked.ToString());
                      ////tab3         

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
                          //   appUserData.Close();
                      }
                  }
                  catch (FileNotFoundException FNF) { MessageBox.Show(FNF.Message, "Saving user data", MessageBoxButton.OK, MessageBoxImage.Information); }
                  catch (UnauthorizedAccessException UAE) { MessageBox.Show(UAE.Message, "Saving user data", MessageBoxButton.OK, MessageBoxImage.Information); }
                  catch (NullReferenceException nre) { MessageBox.Show(nre.Message, "Saving user data", MessageBoxButton.OK, MessageBoxImage.Information); }
                  catch (Exception ae) { MessageBox.Show(ae.Message, "Saving user data", MessageBoxButton.OK, MessageBoxImage.Information); }


                  */
    }

    private void LoadUserSettings()
    {
      //UserSettings<string, string> settings = new UserSettings<string, string>();
      //try
      //{
      //    using (StreamReader appUserData = new StreamReader(CONFIG_FILE))
      //    {
      //        while (appUserData.Peek() != -1)
      //        {
      //////////////                        //  settings.Add(new KeyValuePair<appUserData.ReadLine()>);
      //////////////                        /*<UserControl ...>
      //////////////    <TextBlock BlockChangeEventArgs:Name="myTextBlock" />
      //////////////</UserControl>

      //////////////In the code-behind file, you could write:

      //////////////var myTextBlock = (TextBlock)this.FindName("myTextBlock");

      //////////////                         var enumerator = d.GetEnumerator();
      //////////////    while (enumerator.MoveNext())
      //////////////    {
      //////////////    var pair = enumerator.Current;
      //////////////    b += pair.Value;
      //////////////    }
      //////////////                         */
      //            appUserData.ReadLine();
      //        }

      //        //    appUserData.Close();
      //    }
      //}
      //catch (FileNotFoundException FNF) { MessageBox.Show(FNF.Message, "Loading settings", MessageBoxButton.OK, MessageBoxImage.Information); }
      //catch (UnauthorizedAccessException UAE) { MessageBox.Show(UAE.Message, "Loading settings", MessageBoxButton.OK, MessageBoxImage.Information); }
      //catch (Exception ae) { MessageBox.Show(ae.Message, "Loading settings", MessageBoxButton.OK, MessageBoxImage.Information); }
      /*
      window.Width = double.Parse(settings[0]);
      window.Height = double.Parse(settings[1]);
      window.Left = double.Parse(settings[2]);
      window.Top = double.Parse(settings[3]);

      //tab1
      saveRootLocation = settings[4];
      dataSE_Path.Text = settings[5];
      dataSteamId.Text = settings[6];
      dataNames.Text = settings[7];
      //tab2
      if (settings[8] == "True") makeCircle.IsChecked = true;
      if (settings[9] == "True") makeellipse.IsChecked = true;
      if (settings[10] == "True") makeSphere.IsChecked = true;
      if (settings[11] == "True") makeellipsoid.IsChecked = true;
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

    private void ReloadSettings()
    {
      //if (!File.Exists(CONFIG_FILE))
      //{
      //    MessageBox.Show("There are no setting to load.\n either they have never been saved or the file has been removed.", "info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
      //}
      //else { LoadUserSettings(); }
    }


    private void PathHandler()
    {


      //    if (S_E_B_P == null)
      //    { S_E_B_P = saveRootLocation + "\\Blueprints"; }
      //    if (localBP == null)
      //    {
      //        localBP = S_E_B_P + "\\local";
      //    }


      //    if (!Directory.Exists(saveRootLocation))
      //    {
      //        string message = string.Format("Space Engineers save folder: {0} \ndoes not exist.\n Do you want to create it?\n\nNote: this is the expected location, If you have moved it select No, and use the browse feature to locate and set it. ", saveRootLocation);
      //        MessageBoxResult result = MessageBox.Show(message, "Path location error", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly);
      //        if (result == MessageBoxResult.Yes)
      //        {
      //            try
      //            {
      //                Directory.CreateDirectory(saveRootLocation);
      //                Directory.CreateDirectory(S_E_B_P);
      //                Directory.CreateDirectory(localBP);
      //            }
      //            catch (UnauthorizedAccessException UAE)
      //            {
      //                MessageBox.Show(UAE.Message, "info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
      //            }
      //        }
      //    }
    }



    //private void ActionRefreshView(object sender, RoutedEventArgs e)
    //{
    //    if (!IsGeneratingPreview)
    //    {
    //     //   refreshPreviewBut.Content = "Cancel preview";
    //        refreshPreviewBut.Content = "Disabled Feature";
    //        IsGeneratingPreview = true;
    //     //   StartStopCalculating(sender, e);
    //     //       
    //    // threeDView = new ModelViewer();
    //   //   threeDView.Owner = this;

    //        //bool? previewResult = threeDView.ShowDialog();
    //        //if (previewResult != null && (bool)previewResult == true)
    //        //{

    //        //}
    //        //else
    //        //{  IsGeneratingPreview = false;
    //        //    refreshPreviewBut.Content = "show preview"; }
    //    }
    //    else
    //    {
    //        IsGeneratingPreview = false;
    //        //cancel preview
    //        refreshPreviewBut.Content = "show preview";
    //    }


    //}







    private void BluePrintToFile()
    {
      //try
      //{
      //    BluePrintXml bluePrint = new BluePrintXml();
      //    bluePrint.Path = bpFolder + "\\bp.sbc";
      //    bluePrint.SteamUserId = SteamUserId;
      //    bluePrint.SteamUserName = steamUserName;
      //    bluePrint.BluePrintName = bpName;
      //    bluePrint.GridSizeEnum = gridSize;
      //    bluePrint.BlockType = armourType;
      //    bluePrint.BlockColour = new Point3D(blockColourHue, blockColourSaturation, blockColourValue);

      //    //  foreach (Point3D p in plotData) { bluePrint.PopulateGrid = p; }
      //    bluePrint.MakeBaseStructure();


      //}
      //catch (Exception e) { MessageBox.Show(e.Message); }
      //MessageBox.Show("Blueprint Saved", "info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);

    }

    #endregion


    private void PlottingProcess()
    {


      //    PointContainer.Clear();

      //    //instantiate point list

      //    //get bulk points
      //    //     ps = new ProgressStatus();

      //    //       ps.BarMaximum=(Math.Abs((shapeSettings.xRadius - 1) * (shapeSettings.yRadius - 1) * (shapeSettings.zRadius - 1)));
      //    //     ps.BarMinimum = 0;


      //    worker.WorkerSupportsCancellation = true;
      //    worker.WorkerReportsProgress = true;
      //    worker.DoWork += worker_DoWork;
      //    worker.ProgressChanged += worker_ProgressChanged;
      //    worker.RunWorkerCompleted += worker_RunWorkerCompleted;
      //    //ps.Show();

      //    //  worker.RunWorkerAsync(10000);//send arguments and kick it off
      //    worker.RunWorkerAsync(shapeSettings);



      //    //   ps.progressBar.Minimum = 100;


    }

    private bool ValidateBPPathAndCustoms()
    {
      bool proceed = true;
      string errorMessage = "";
      if(errorMessage != "") { MessageBox.Show(errorMessage, "Critical Data Missing", MessageBoxButton.OK, MessageBoxImage.Exclamation); }

      return proceed;
    }

    #region background worker



    void worker_DoWork(object sender, DoWorkEventArgs e)
    {
      //WorkingArgs parameters = e.Argument as WorkingArgs;
      //CircleEvaluationCalculations seperateThread = new CircleEvaluationCalculations();

      ////////  ps.SubscribeToPointContainer();
      //seperateThread.RadiusInXPlane = parameters.xRadius;
      //seperateThread.RadiusInYPlane = parameters.yRadius;
      //seperateThread.RadiusInZPlane = parameters.zRadius;
      //seperateThread.LowToleranceEvaluation = parameters.lowTol;
      //seperateThread.HighToleranceEvaluation = parameters.highTol;
      //seperateThread.ShapeSelected = parameters.shapeSelected;
      //seperateThread.BeginPointChecking();
      //////////for (int BlockChangeEventArgs = 0; BlockChangeEventArgs < parameters.xRadius; BlockChangeEventArgs++) { 
      //////////    int progressPercentage = Convert.ToInt32((BlockChangeEventArgs / parameters.xRadius) * 100);
      //////////    (sender as BackgroundWorker).ReportProgress(progressPercentage);
      ////////// System.Threading.Thread.Sleep(1);
      //////////}
      //////////  
      ////////// 
      //////////        result++;
      ////////// 
      //////////    else{
      //////////        (sender as BackgroundWorker).ReportProgress(progressPercentage);
      //////////   

      //////////}
      //e.Result = string.Format("blocks calculated:{0}", PointContainer.Count());

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
      //if (e.Cancelled)
      //{
      //    MessageBox.Show("Canceled", "Calculating", MessageBoxButton.OK);
      //    ////////    actionGenerate.Content = "Generate Blueprint";
      //    ////////    progressBarOne.Visibility = Visibility.Collapsed;
      //    ////////     IsCalculating = false;
      //    ////////   ps.Close();

      //}
      //else
      //{
      //    MessageBox.Show(e.Result.ToString(), "Complete", MessageBoxButton.OK);
      //    actionGenerate.Content = "Generate Blueprint";
      //    progressBarOne.Visibility = Visibility.Collapsed;
      //    IsCalculating = false;
      //    ////////   MessageBox.Show("Complete", "Calculating", MessageBoxButton.OK);
      //    ////////write to file

      //    BluePrintToFile();
      //    ////////     System.Diagnostics.Trace.WriteLine (ps.WindowState.ToString()) ;
      //    ////////      ps.Close();
      //    ////////       ps = null;
      //}
    }
    #endregion
  }
}
