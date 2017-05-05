using Microsoft.Win32;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.BlueprintWriterLib;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.Model;
using SoloProjects.Dudhit.Utilities;
using System;
using System.Collections.Generic;
//using SoloProjects.Dudhit.SpaceEngineers.SEBP.EventArguments;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Media3D;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
  public class SEBluePrintController : IDisposable
  {
    private BlueprintModel masterBlueprint;
    private HashSet<Point3D> blueprintData;
    private string[] commandLineArguments;
    public SEBluePrintController(StartupEventArgs e)
    {
      this.commandLineArguments = e.Args;
      Initialise();
    }

    private void Initialise()
    {
      masterBlueprint = new BlueprintModel();
    }


    public void BeginSEPB()
    {
      masterBlueprint = new BlueprintModel();
      if(commandLineArguments!=null&&commandLineArguments.Length>0)
      {
        NonUIControl();
      }
      else
      {
        UIControl();
      }
 
    }

    private void UIControl()
    {
      MainWindow SEbpUI = new MainWindow();
      SEbpUI.ShowDialog();
    }

    private void NonUIControl()
    {
      using(CommandLineHandler commandLineHandler = new CommandLineHandler(commandLineArguments))
      {
        commandLineHandler.MyBlueprint=masterBlueprint;
        commandLineHandler.Start();
        if(commandLineHandler.MyBlueprint.HasUsableData)
        {
          masterBlueprint=commandLineHandler.MyBlueprint;
          if(GetPointData())
          {
            MessageBox.Show("calcs done..ready to output", "info...", MessageBoxButton.OK, MessageBoxImage.Information);
            //access writer
            BluePrintXml writeBlueprint =new BluePrintXml(masterBlueprint, blueprintData);
          }
        }
        else
        {
          string terminationMessage= string.Format("Data...is..bad. ugrh \n");
          MessageBox.Show(terminationMessage, "SEBP Stopping...", MessageBoxButton.OK, MessageBoxImage.Error);
  
        }
        commandLineHandler.Dispose();
      }
      MessageBox.Show("output done.", "Finished.", MessageBoxButton.OK, MessageBoxImage.Information);
        
    }

    public bool GetPointData()
    {
      if(masterBlueprint!=null&&masterBlueprint.HasUsableData)
      {
        //    call class to handle point and blueprint output
        using(PointsToShape pointsToShape = new PointsToShape(masterBlueprint.XAxis, masterBlueprint.YAxis, masterBlueprint.ZAxis, masterBlueprint.FinalShape, masterBlueprint.Solid))
        {
          blueprintData=pointsToShape.GlobalCurveSet;
        }
        if(blueprintData.Count>1)
          return true;
      }
      return false;
    }








    #region file handling




    #region UI_BUTTON_TOGGLE
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
    #endregion


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


    //private void PlottingProcess()
    //{


    //  //    PointContainer.Clear();

    //  //    //instantiate point list

    //  //    //get bulk points
    //  //    //     ps = new ProgressStatus();

    //  //    //       ps.BarMaximum=(Math.Abs((shapeSettings.xRadius - 1) * (shapeSettings.yRadius - 1) * (shapeSettings.zRadius - 1)));
    //  //    //     ps.BarMinimum = 0;


    //  //    worker.WorkerSupportsCancellation = true;
    //  //    worker.WorkerReportsProgress = true;
    //  //    worker.DoWork += worker_DoWork;
    //  //    worker.ProgressChanged += worker_ProgressChanged;
    //  //    worker.RunWorkerCompleted += worker_RunWorkerCompleted;
    //  //    //ps.Show();

    //  //    //  worker.RunWorkerAsync(10000);//send arguments and kick it off
    //  //    worker.RunWorkerAsync(shapeSettings);



    //  //    //   ps.progressBar.Minimum = 100;


    //}



    #region background worker



    //void worker_DoWork(object sender, DoWorkEventArgs e)
    //{
    //  //WorkingArgs parameters = e.Argument as WorkingArgs;
    //  //CircleEvaluationCalculations seperateThread = new CircleEvaluationCalculations();

    //  ////////  ps.SubscribeToPointContainer();
    //  //seperateThread.RadiusInXPlane = parameters.xRadius;
    //  //seperateThread.RadiusInYPlane = parameters.yRadius;
    //  //seperateThread.RadiusInZPlane = parameters.zRadius;
    //  //seperateThread.LowToleranceEvaluation = parameters.lowTol;
    //  //seperateThread.HighToleranceEvaluation = parameters.highTol;
    //  //seperateThread.ShapeSelected = parameters.shapeSelected;
    //  //seperateThread.BeginPointChecking();
    //  //////////for (int BlockChangeEventArgs = 0; BlockChangeEventArgs < parameters.xRadius; BlockChangeEventArgs++) { 
    //  //////////    int progressPercentage = Convert.ToInt32((BlockChangeEventArgs / parameters.xRadius) * 100);
    //  //////////    (sender as BackgroundWorker).ReportProgress(progressPercentage);
    //  ////////// System.Threading.Thread.Sleep(1);
    //  //////////}
    //  //////////  
    //  ////////// 
    //  //////////        result++;
    //  ////////// 
    //  //////////    else{
    //  //////////        (sender as BackgroundWorker).ReportProgress(progressPercentage);
    //  //////////   

    //  //////////}
    //  //e.Result = string.Format("blocks calculated:{0}", PointContainer.Count());

    //}

    //void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    //{
    //  //    ps.BarMinimum=  e.ProgressPercentage;
    //  /*     if (e.UserState != null)
    //           lbResults.Items.Add(e.UserState);
    //*/
    //}

    //void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    //{
    //  //if (e.Cancelled)
    //  //{
    //  //    MessageBox.Show("Canceled", "Calculating", MessageBoxButton.OK);
    //  //    ////////    actionGenerate.Content = "Generate Blueprint";
    //  //    ////////    progressBarOne.Visibility = Visibility.Collapsed;
    //  //    ////////     IsCalculating = false;
    //  //    ////////   ps.Close();

    //  //}
    //  //else
    //  //{
    //  //    MessageBox.Show(e.Result.ToString(), "Complete", MessageBoxButton.OK);
    //  //    actionGenerate.Content = "Generate Blueprint";
    //  //    progressBarOne.Visibility = Visibility.Collapsed;
    //  //    IsCalculating = false;
    //  //    ////////   MessageBox.Show("Complete", "Calculating", MessageBoxButton.OK);
    //  //    ////////write to file

    //  //    BluePrintToFile();
    //  //    ////////     System.Diagnostics.Trace.WriteLine (ps.WindowState.ToString()) ;
    //  //    ////////      ps.Close();
    //  //    ////////       ps = null;
    //  //}
    //}
    #endregion

    #region disposal

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    ~SEBluePrintController()
    {
      Dispose(false);
    }

    protected virtual void Dispose(bool disposing)
    {
      if(disposing)
      {
        if(masterBlueprint!=null)
        {
          masterBlueprint.Dispose();
          masterBlueprint=null;
        }

      }

    }
    #endregion
  }
}
