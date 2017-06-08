using SoloProjects.Dudhit.SpaceEngineers.SEBP.Model;
using SoloProjects.Dudhit.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
   private  void BeginSEPB(object sender, StartupEventArgs e)
    {
           SEBluePrintController appController= new SEBluePrintController(e);
 appController.BeginSEPB();
   System.Diagnostics.Trace.Write("done");
  //  appController.Dispose();
    }



    private void HandleUnpredictedErrors(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
      string myError= string.Format("So sorry, I didn't expect this stuff below to happen... \n{0}", e.Exception.ToString());
      MessageBox.Show(myError, "TOTALLY DIDN'T PLAN FOR THIS", MessageBoxButton.OK, MessageBoxImage.Error);
    }




  }
}
