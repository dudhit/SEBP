using SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint
{
    public partial class MainWindow : Window, INotifyPropertyChanged,IDisposable
    {
        #region controls
        [NonSerialized]
        public WorkingArgs shapeSettings;

     //   ProgressStatus ps;
     //   private ModelViewer threeDView;
        #region tab1
        private bool IsCalculating;
        [NonSerialized]
        BackgroundWorker worker;
        private string userApp;
        private const string CONFIG_FILE = "config.ini";
        private string S_E_Home;
        private string S_E_B_P;
        private string localBP;
        private string bpFolder;
        private string steamUserName;
        private string steamUserId;
        private string steamPath;
        private string bpName;
        #endregion
        #region tab2
        private bool shapeSettingChanged;
        private bool blockSettingChanged;
        private bool IsGeneratingPreview;
        private double lowTol;
        private double highTol;
        //      private double sliderHeight;
        //    private double sliderMaxHeight;
        //    private double sliderMinHeight;
        //     private double SliderHeight { get { return sliderHeight; } set { sliderHeight = value; RaisePropertyChanged("SliderHeight"); } }
        //      private double SliderMinHeight { get { return sliderMinHeight; } set { sliderMinHeight = value; RaisePropertyChanged("SliderMinHeight"); } }
        //      private double SliderMaxHeight { get { return sliderMaxHeight; } set { sliderMaxHeight = value; RaisePropertyChanged("SliderMaxHeight"); } }
        /*
         binding the xaml 
         * 	MinHeight="{Binding SliderMinHeight, BindsDirectlyToSource=True, FallbackValue=100, NotifyOnSourceUpdated=True}"	
        MaxHeight="{Binding SliderMaxHeight, BindsDirectlyToSource=True,  NotifyOnSourceUpdated=True}"   
        Height="{Binding SliderMaxHeight, BindsDirectlyToSource=True,  NotifyOnSourceUpdated=True}"/>
         
         */
        public Color FillColor { get { return (Color)GetValue(FillColorProperty); } set { SetValue(FillColorProperty, value); } }
        #endregion
        #region tab3
        #region Dependency Property Fields

        public static readonly DependencyProperty FillColorProperty =
           DependencyProperty.Register
           ("FillColor", typeof(Color), typeof(MainWindow),
           new PropertyMetadata(Colors.Black));


        #endregion

        private float blockColourHue;
        private float blockColourSaturation;
        private float blockColourValue;
        private string gridSize;
        private string armourType;
        #endregion
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;



        private void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }




        
    }
}
