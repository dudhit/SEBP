using SoloProjects.Dudhit.SpaceEngineers.SEBP.EventArguments;
using System;
using System.Windows;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    // an app to create a circular blueprint for space engineers
    // requires location of user saves folder:


    public partial class MainWindow : Window, IDisposable
    {

        #region main window flow and menu
        private bool IsWorking;
        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();
            // subscribe to 3 tab events
            SubscribeToTabEvents();
            actionGenerate.IsEnabled = false;
            //   if (!File.Exists(CONFIG_FILE)) { FirstLoad(); } else { LoadUserSettings(); }

            //ps = new ProgressStatus();
            //  PathHandler(this, new RoutedEventArgs());
            //   plotData = new List<Point3D>();

        }

        private void FirstLoad()
        {
            //makeCircle.IsChecked = true;
            //      makeQuater.IsChecked = true;
            //blockNormal.IsChecked = true;
            //blockLarge.IsChecked = true;
            //colGrey.IsChecked = true;
            //dataNames.Text = "";
            //dataSteamId.Text = "";
            //dataSE_Path.Text = "";
            //     radOneSlide.Value = 10;
            //     radTwoSlide.Value = 10;
            //     radThreeSlide.Value = 10;
            //     uppertoleranceSlide.Value = 100;
            //     lowertoleranceSlide.Value = 100;
                  //        sliderMinHeight =250;
            //       sliderMaxHeight = 600;
            //       sliderHeight = 100;
            //       RaisePropertyChanged("SliderMinHeight");
            //       RaisePropertyChanged("SliderMaxHeight");
            //       RaisePropertyChanged("SliderHeight");
            ///FillColor = "";
            //   PathHandler();
            //get steam user name and install path
            //GetSteamRegistryData();
            //shapeSettingChanged = true;
            //blockSettingChanged = true;


        }


        private void SubscribeToTabEvents()
        {
            viewFileTab.FileChangedEvent += new View.FileTabView.FileChangeHandler(SetFileData);
            viewShapeTab.ShapeChangedEvent += new View.ShapeTabView.ShapeChangeHandler(SetShapeData);
            viewBlockTab.BlockChangedEvent += new View.BlockTabView.BlockChangeHandler(SetBlockData);
            //   viewBlockTab
        }

        private void SetFileData(object sender, FileChangeEventArgs fcea)
        {
            if (((App)Application.Current).controller != null) ((App)Application.Current).controller.UpdateFileData(fcea);
        }

        private void SetShapeData(object sender, ShapeChangeEventArgs scea)
        {
            if (((App)Application.Current).controller != null) ((App)Application.Current).controller.UpdateShapeData(scea);
        }

        private void SetBlockData(object sender, BlockChangeEventArgs bcea)
        {
            if (((App)Application.Current).controller != null) ((App)Application.Current).controller.UpdateBlockData(bcea);
        }

        private void MainClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine(e.ToString());
            MessageBoxResult goodBye = MessageBox.Show("Do you intend on leaving? ", "exit app", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
            if (goodBye != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
            else
            {
                //unsubscribe to tabs
                if (((App)Application.Current).controller != null) ((App)Application.Current).controller.UnSubscribeFromOthers();
                viewFileTab.FileChangedEvent -= new View.FileTabView.FileChangeHandler(SetFileData);
                viewShapeTab.ShapeChangedEvent -= new View.ShapeTabView.ShapeChangeHandler(SetShapeData);
                viewBlockTab.BlockChangedEvent -= new View.BlockTabView.BlockChangeHandler(SetBlockData);
            }
        }



        private void MenuExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion






        #region disposal

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~MainWindow()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources  
                //if (Encoding != null)
                //{
                //    Encoding.Dispose();
                //    Encoding = null;
                //}
            }

        }
        #endregion

        private void ReloadSettings(object sender, RoutedEventArgs e)
        {

        }

        private void SaveUserSettings(object sender, RoutedEventArgs e)
        {

        }

        private void ResetToLoaded(object sender, RoutedEventArgs e)
        {
            FirstLoad();
        }

        private void ResizeToContents(object sender, RoutedEventArgs e)
        {
            window.Width = 10;
            window.Height = 10;
            window.SizeToContent = System.Windows.SizeToContent.WidthAndHeight;

        }

        private void StartStopCalculating(object sender, RoutedEventArgs e)
        {
            if (!IsWorking)
            {
                actionGenerate.Content = "Cancel Blueprint";
                progressBarOne.Visibility = Visibility.Visible;
                //start working
            }
            else
            {
                actionGenerate.Content = "Generate Blueprint";
                progressBarOne.Visibility = Visibility.Hidden;

                //stop working    if (worker.IsBusy) { worker.CancelAsync(); }
            }
        }

        private void ControllerSubscribe(object sender, RoutedEventArgs e)
        {
            if (((App)Application.Current).controller != null) ((App)Application.Current).controller.SubscribeToOthers();
        }

        #region events
        public delegate void MainViewChangeHandler(object sender, MainViewChangeEventArgs mvArgs);
        // an instance of the delegate
        public event MainViewChangeHandler MainViewChangeEvent;


        #endregion
    }

}
