using SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint
{
    /// <summary>
    /// Interaction logic for ProgressStatus.xaml
    /// </summary>
    public partial class ProgressStatus : Window
    {

        public void SubscribeToEvaluator(CircleEvaluationCalculations cec)
        { cec.Processing += new CircleEvaluationCalculations.ProcessingChangedHandler(UpdateMyContorls); }

        public void UpdateMyContorls(CircleEvaluationCalculations cec, ProcessInfoArgs pia)
        {
            updateLabel.Content = string.Format("Handled event x:{0},y:{1},z:{2}", pia.x, pia.y, pia.z);
            //   System.Diagnostics.Trace.WriteLine(string.Format("Handled event x:{0},y:{1},z:{2}",pia.x,pia.y,pia.z));
        }

    }
}
