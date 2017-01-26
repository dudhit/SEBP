using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public SEBluePrintController controller;
        private void BeginSEPB(object sender, StartupEventArgs e)
        {
            if(e.Args!=null)
            for (int i = 0; i < e.Args.Length; i++)
            {System.Diagnostics.Trace.WriteLine(e.Args[i].ToString()); }

            controller = new SEBluePrintController();
            
            MainWindow SEbpUI = new MainWindow();
            SEbpUI.ShowDialog();
        }

        private void HandleUnpredictedErrors(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
           string myError= string.Format("So sorry, I didn't expect things such as\n{0}\nto happen",e.Exception.ToString());
            MessageBox.Show(myError,"TOTALLY DIDN'T PLAN FOR THIS",MessageBoxButton.OK,MessageBoxImage.Error);
        }
    }
}
