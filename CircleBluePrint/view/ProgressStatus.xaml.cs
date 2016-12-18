using SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint.Collection;
using System.ComponentModel;
using System.Windows;

namespace SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint
{
    /// <summary>
    /// Interaction logic for ProgressStatus.xaml
    /// </summary>
    public partial class ProgressStatus : Window, INotifyPropertyChanged
    {
        private bool IsSubscribed;
        private double barMaximum;

        private double barMinimum;
        public ProgressStatus()
        {
            InitializeComponent();
            this.DataContext = this;
     
        }

        public void SubscribeToPointContainer()
        {
            if (!IsSubscribed)
            {
                IsSubscribed = true;
                PointContainer.Processing += new PointContainer.ProcessingChangedHandler(UpdateProgress);
            }
        }
        public  void UpdateProgress(PointContainer pc, ProcessInfoArgs pia)
        {
            progressBar.Minimum = pia.numPoints/barMaximum;
        }

        public double BarMinimum { get { return barMinimum; } set { barMinimum = value; RaisePropertyChanged("BarMinimum"); } }
        public double BarMaximum { get { return barMaximum; } set { barMaximum = value; RaisePropertyChanged("BarMaximum"); } }


        public event PropertyChangedEventHandler PropertyChanged;
     
       

        private void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
