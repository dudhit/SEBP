using SoloProjects.Dudhit.Utilities;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Media3D;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
      private BlueprintModel masterBlueprint;
      private HashSet<Point3D> blueprintData;
        private void BeginSEPB(object sender, StartupEventArgs e)
      {  masterBlueprint = new BlueprintModel();
        if(e.Args!=null&&e.Args.Length>0)
        {
          using(CommandLineHandler commandLineHandler = new CommandLineHandler(e.Args)) {  
            commandLineHandler.MyBlueprint=masterBlueprint;
          commandLineHandler.Start();
          if(commandLineHandler.MyBlueprint.HasUsableData)
            masterBlueprint=commandLineHandler.MyBlueprint;
          commandLineHandler.Dispose();
       //    call class to handle point and blueprint output
          using(PointsToShape pointsToShape = new PointsToShape(masterBlueprint.XAxis, masterBlueprint.YAxis, masterBlueprint.ZAxis, masterBlueprint.FinalShape,masterBlueprint.Solid))
          {
            blueprintData=pointsToShape.GlobalCurveSet;
          }
          }  
        }
        else
        {

          MainWindow SEbpUI = new MainWindow();
          SEbpUI.ShowDialog();
        }
      }

        private void HandleUnpredictedErrors(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
           string myError= string.Format("So sorry, I didn't expect this stuff below to happen... \n{0}",e.Exception.ToString());
            MessageBox.Show(myError,"TOTALLY DIDN'T PLAN FOR THIS",MessageBoxButton.OK,MessageBoxImage.Error);
        }


   

    }
}
