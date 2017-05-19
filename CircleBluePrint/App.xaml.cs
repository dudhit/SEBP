using SoloProjects.Dudhit.SpaceEngineers.SEBP.Model;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.View;
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
    private const string LOG_FILE = "sepb_error_log.log";
    private string logFolder = Directory.GetCurrentDirectory();
    private string logPath;
    private ConsoleOutputs cmdOut;
  //  private Style lookLikeConsoleText;
    private MainWindow SEbpUI;
    private void BeginSEPB(object sender, StartupEventArgs e)
    {
      try
      {
        logPath=Path.Combine(logFolder, LOG_FILE);
        if(e.Args!=null&&e.Args.Length>0)
        {
        //  lookLikeConsoleText = Application.Current.FindResource("ConsoleText") as Style;
          cmdOut = new ConsoleOutputs();
          cmdOut.Show();
          cmdOut.Start(e.Args);
      
        }
        else
        {
          SEbpUI =  new MainWindow();
          SEbpUI.Show();
        }

        //Logging("Begin SEBP");
        //using(SEBluePrintController appController= new SEBluePrintController(e))
        //{
        //  appController.BeginSEPBAsync();
        //  Logging("End");

      }
      catch(Exception ex)
      {
        FileSystemHelper.Logging(logPath, ex.ToString());
        FileSystemHelper.Logging(logPath, ex.Source.ToString());
        FileSystemHelper.Logging(logPath, ex.Message.ToString());
        FileSystemHelper.Logging(logPath, ex.InnerException.ToString());
        FileSystemHelper.Logging(logPath, ex.StackTrace.ToString());
        FileSystemHelper.Logging(logPath, ex.Data.ToString());
      }
    }





    private void HandleUnpredictedErrors(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
      FileSystemHelper.Logging(Path.Combine(Directory.GetCurrentDirectory(), "CriticalErrors.log"), e.Exception.ToString());
    }




  }
}
