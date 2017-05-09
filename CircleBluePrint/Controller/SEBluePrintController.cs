using Microsoft.Win32;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.BlueprintWriterLib;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.Model;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.View;
using SoloProjects.Dudhit.Utilities;
using System;
using System.Collections.Generic;
//using SoloProjects.Dudhit.SpaceEngineers.SEBP.EventArguments;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
  public class SEBluePrintController : IDisposable
  {
    private ConsoleOutputs cmdOut;
    private Style lookLikeConsoleText;
    private BlueprintModel masterBlueprint;
    private HashSet<Point3D> blueprintData;
    private string[] commandLineArguments;
    private ConsoleViewModel outputController;
    private Progress<MyTaskProgressReporter> progressIndicator;
    public SEBluePrintController(StartupEventArgs e)
    {
      this.commandLineArguments = e.Args;
      Initialise();

    
    }

    private void SetupOutputWindow()
    {
      lookLikeConsoleText = Application.Current.FindResource("ConsoleText") as Style;
      cmdOut = new ConsoleOutputs();
   //   cmdOut.InitializeComponent();
            cmdOut.Show();
      outputController = new ConsoleViewModel(cmdOut);
      DisplayHeaderMessage();
   
    }

    private void Initialise()
    {
      masterBlueprint = new BlueprintModel();
      progressIndicator = new Progress<MyTaskProgressReporter>(ReportProgress);
    }

    private void DisplayHeaderMessage()
    {
      outputController.AddTextToCollection("SEBP commandline interface.\n use \"--help\" for detailed instructions");

    }
    public  void BeginSEPB()
    {

      masterBlueprint = new BlueprintModel();
        if(commandLineArguments!=null&&commandLineArguments.Length>0)
        {
          SetupOutputWindow();

          NonUIControl();
          outputController.AddTextToCollection("You can safely close this window");
        }
        else
        {
          UIControl();
        }
   
    
    }

    private void UIControl()
    {
      MainWindow SEbpUI =  new MainWindow();
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
            writeBlueprint.MakeBaseStructure();
            writeBlueprint.BluePrintFileHandling();
          }
        }
        else
        {
          //"Data...is..bad. ugrh \n" msg

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


    private void ReportProgress(MyTaskProgressReporter progress)
    {
      string s   = progress.ProgressMessage;
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




    #endregion



    //  //    //     ps = new ProgressStatus();
    //  //    //       ps.BarMaximum=(Math.Abs((shapeSettings.xRadius - 1) * (shapeSettings.yRadius - 1) * (shapeSettings.zRadius - 1)));
    //  //    //     ps.BarMinimum = 0;
    //  //    //ps.Show();
    //  //    //   ps.progressBar.Minimum = 100;



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
