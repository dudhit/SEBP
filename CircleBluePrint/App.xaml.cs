using SoloProjects.Dudhit.SpaceEngineers.SEBP.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
      private  BlueprintModel bpm;
      
        private void BeginSEPB(object sender, StartupEventArgs e)
      {
        if(e.Args!=null&&e.Args.Length>0)
        {

          CommandLineHandler clh = new CommandLineHandler(e.Args);

        }
        else
        {

          MainWindow SEbpUI = new MainWindow();
          SEbpUI.ShowDialog();
        }
      }

        private void HandleUnpredictedErrors(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
           string myError= string.Format("So sorry, I didn't expect things such as\n{0}\nto happen",e.Exception.ToString());
            MessageBox.Show(myError,"TOTALLY DIDN'T PLAN FOR THIS",MessageBoxButton.OK,MessageBoxImage.Error);
        }


   

    }
}
