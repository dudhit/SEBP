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
    //private ConsoleOutputs cmdOut;
    //private BlueprintModel masterBlueprint;
    //private HashSet<Point3D> blueprintData;
    //private string[] commandLineArguments;
    //public string[] CommandLineArguments { get; set; }
    //private ConsoleViewModel outputController;
    //public ConsoleViewModel OutputController { get; set; }
    //// private Progress<MyTaskProgressReporter> progressIndicator;

    public SEBluePrintController()
    {
    }
    //public async void BeginSEPBAsync()
    //{
    //  try
    //  {
        //    masterBlueprint =new BlueprintModel();
    //    if(commandLineArguments!=null&&commandLineArguments.Length>0)
    //    {
    //   //   Task<bool> firstLevel=  BulkProcessingLogic();
    // //     bool ready = await firstLevel;
    //    //  Task<int> workResult =   SomeNumberWorkAsync(null);
    // //     System.Diagnostics.Trace.WriteLine(" SomeNumberWorkAsync should have fired off");
    ////      int result  = await workResult;
    //    //  SetupOutputWindow();
    //    //  //construct Progress<T>, passing ReportProgress as the Action<T> 
    //    //  Progress<MyTaskProgressReporter> progressIndicator = new Progress<MyTaskProgressReporter>(ReportProgress);
    //    //  //call async method
    //    //  MyTaskProgressReporter stayInformed= await NonUIControl(progressIndicator);
    //    //  ReportProgress(stayInformed);
    //    }
    
    //  }
    //  catch(Exception ex)
    //  { MessageBox.Show(ex.ToString()); }

    //}
  



 
//    private async void BulkProcessingLogic()
//    {
//      using(CommandLineHandler commandLineHandler = new CommandLineHandler(commandLineArguments))
//      {
//        commandLineHandler.MyBlueprint=masterBlueprint;
//// await commandLineHandler.Start();
//        if(commandLineHandler.MyBlueprint.HasUsableData)
//        {
//          masterBlueprint=commandLineHandler.MyBlueprint;
//          if(GetPointData())
//          {
//            //.ProgressMessage="Calculations complete. Writing file...";
//            //access writer
//            BluePrintXml writeBlueprint =new BluePrintXml(masterBlueprint, blueprintData);
//            writeBlueprint.MakeBaseStructure();
//            writeBlueprint.BluePrintFileHandling();
//          }
//        }
//        else
//        {
//          //.ProgressMessage="Data...is..bad. ugrh \n";
     
//        }
//        commandLineHandler.Dispose();
//      } 
//       }

    //public bool GetPointData()
    //{
    //  if(masterBlueprint!=null&&masterBlueprint.HasUsableData)
    //  {
    //    //    call class to handle point and blueprint output
    //    using(PointsToShape pointsToShape = new PointsToShape(masterBlueprint.XAxis, masterBlueprint.YAxis, masterBlueprint.ZAxis, masterBlueprint.FinalShape, masterBlueprint.Solid))
    //    {
    //      blueprintData=pointsToShape.GlobalCurveSet;
    //    }
    //    if(blueprintData.Count>1)
    //      return true;
    //  }
    //  return false;
    //}



 



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
        //if(masterBlueprint!=null)
        //{
        //  masterBlueprint.Dispose();
        //  masterBlueprint=null;
        //}

      }

    }
    #endregion
  }
}
