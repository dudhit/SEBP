using SoloProjects.Dudhit.SpaceEngineers.SEBP.model;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.Model;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.View;
using SoloProjects.Dudhit.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

    private void TestControl(object sender, StartupEventArgs e)
    {
      Window display = new Window();
      display.Show();
      UserControl uc1 = new RadioWithColouredBox();
      UserControl uc2 = new TextBoxSlider();
      StackPanel sp = new StackPanel();
      WrapPanel wp= new WrapPanel();
      Grid gg= new Grid();
      display.Content=sp;
      sp.Children.Add(uc1);
      sp.Children.Add(uc2);
      RadioWithColouredBoxViewModel vm1= new RadioWithColouredBoxViewModel();
      TextBoxSliderViewModel vm2= new TextBoxSliderViewModel();
      uc1.DataContext= vm1;
      uc2.DataContext=vm2;
      LoadData(vm1);
 
    }


    private static void LoadData(RadioWithColouredBoxViewModel vm1)
    {
      vm1.AddModel("custCol", false, 50, 41, "#FF0D0D0D");
      vm1.AddModel("custCol", false, 50, 41, "#FF1C5A8C");
      vm1.AddModel("custCol", false, 50, 41, "#FF991F1F");
      vm1.AddModel("custCol", false, 50, 41, "#FF344C34");
      vm1.AddModel("custCol", false, 50, 41, "#FFCFA83E");
      vm1.AddModel("custCol", false, 50, 41, "#FFF2F2F2");
      vm1.AddModel("custCol", false, 50, 41, "#FF8C8C8C");
      vm1.AddModel("custCol", false, 50, 41, "#FF404040");
      vm1.AddModel("custCol", false, 50, 41, "#FF0A6DBF");
      vm1.AddModel("custCol", false, 50, 41, "#FFCC0A0A");
      vm1.AddModel("custCol", false, 50, 41, "#FF448044");
      vm1.AddModel("custCol", false, 50, 41, "#FFFFC526");
      vm1.AddModel("custCol", false, 50, 41, "#FFFFFFFF");
      vm1.AddModel("custCol", false, 50, 41, "#FFBFBFBF");
      vm1.AddModel("custCol", false, 50, 41, "#FF595151");
      vm1.AddModel("custCol", false, 50, 41, "#FF417199");
      vm1.AddModel("custCol", false, 50, 41, "#FF9E4343");
      vm1.AddModel("custCol", false, 50, 41, "#FF507750");
      vm1.AddModel("custCol", false, 50, 41, "#FFBAA059");
      vm1.AddModel("custCol", false, 50, 41, "#FFCCB7B7");
      vm1.AddModel("custCol", false, 50, 41, "#FF998E8E");
      vm1.AddModel("custCol", false, 50, 41, "#FF3F3E3E");
      vm1.AddModel("custCol", false, 50, 41, "#FF45535E");
      vm1.AddModel("custCol", false, 50, 41, "#FF991F1F");
      vm1.AddModel("custCol", false, 50, 41, "#FF455E45");
      vm1.AddModel("custCol", false, 50, 41, "#FFA08D58");
      vm1.AddModel("custCol", false, 50, 41, "#FFB2AEAE");
      vm1.AddModel("custCol", false, 50, 41, "#FF7F7F7F");
      vm1.AddModel("custCol", false, 50, 41, "#80808080");
    }

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
