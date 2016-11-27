﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Samples.CustomControls;

namespace CircleBluePrint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    // an app to create a circular blueprint for space engineers
    // requires location of user saves folder:

    [SerializableAttribute]
    public partial class MainWindow : Window
    {
        //path --folder -- file variables

        private string userApp;
        private const string CONFIG_FILE = "config.ini";
        private string S_E_Home;
        private string S_E_B_P;
        private string localBP;

        //calculation variables
        private List<MyCube> plotData;
        private double xRadius;
        private double yRadius;
        private double zRadius;
        private string shape;


        public MainWindow()
        {
            InitializeComponent();

            firstLoad();
            PathHandler(this,new RoutedEventArgs());

        }
        //run this to reset and on load without a save file
        private void firstLoad()
        {
           makeCircle.IsChecked = true;
            makeFrame.IsChecked = true;
            make2D.IsChecked = true;
            makeQuater.IsChecked = true;
            blockNormal.IsChecked = true;
            blockLarge.IsChecked = true;
            colGrey.IsChecked = true;
            dataNames.Text = "";
            dataSteamId.Text = "";
            dataSE_Path.Text = "";
            radOneSlide.Value = 10;
            radTwoSlide.Value = 10;
            radThreeSlide.Value = 10;
           
        }
        #region file handling
        //save user paths - radio buttons - colours -everything
        private void SaveUser(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(CONFIG_FILE))
            {
                try
                {
                    using (System.IO.File.Create(CONFIG_FILE)) { }
                }
                catch (UnauthorizedAccessException UAE) { MessageBox.Show(UAE.Message, "Cannot Save user data", MessageBoxButton.OK, MessageBoxImage.Information); }
            }
            else
            {
                try
                {
                    using (StreamWriter appUserData = new StreamWriter(CONFIG_FILE))
                    {

                        foreach (MyCube c in plotData)
                        {
                            string tofile = string.Format("{0},{1},{2}", c.X(), c.Y(), c.Z());
                            appUserData.WriteLine(tofile);
                        }
                    }
                }
                catch (FileNotFoundException FNF) { MessageBox.Show(FNF.Message, "Saving user data", MessageBoxButton.OK, MessageBoxImage.Information); }
                catch (UnauthorizedAccessException UAE) { MessageBox.Show(UAE.Message, "Saving user data", MessageBoxButton.OK, MessageBoxImage.Information); }
                catch (Exception ae) { MessageBox.Show(ae.Message, "Saving user data", MessageBoxButton.OK, MessageBoxImage.Information); }

            }

        }


        private void Find_Path(object sender, RoutedEventArgs e)
        {
        /*    System.IO.DirectoryInfo dir = new DirectoryInfo(S_E_Home);
            System.Diagnostics.Trace.WriteLine(dir.Root);
            foreach (DirectoryInfo d in dir.Root.GetDirectories())
            { System.Diagnostics.Trace.WriteLine(d); }
           */
                      Microsoft.Win32.OpenFileDialog getPath = new Microsoft.Win32.OpenFileDialog();
                        Nullable<bool> result = getPath.ShowDialog();

                        // Process open file dialog box results
                        if (result == true)
                        {
                            // Open document
                           S_E_Home = getPath.InitialDirectory;
                            //https://msdn.microsoft.com/en-us/library/system.io.directoryinfo.aspx
                        }
              
        }


        private void PathHandler(object sender, RoutedEventArgs e)
        {

            userApp = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            S_E_Home = "C:\\temp\\test";// userApp + "\\SpaceEngineers";
            S_E_B_P = S_E_Home + "\\Blueprints";
            localBP = S_E_B_P + "\\local";
            dataSE_Path.Text = S_E_Home;
            dataSE_Path.Width = S_E_Home.Length;
            if (!Directory.Exists(S_E_Home))
            {
                string message = string.Format("SE folder: {0} \ndoes not exist.\n Do you want to create it?\n\nNote: this is the expected location, If you have moved it select No, and use the browse feature to locate and set it. ", S_E_Home);
                MessageBoxResult result = MessageBox.Show(message, "error", MessageBoxButton.YesNo, MessageBoxImage.Error, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        Directory.CreateDirectory(S_E_Home);
                        Directory.CreateDirectory(S_E_B_P);
                        Directory.CreateDirectory(localBP);
                    }
                    catch (UnauthorizedAccessException UAE)
                    {
                        MessageBox.Show(UAE.Message, "info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    }
                }
            }
        }


        private void ConfigHandler()
        {
            List<string> listOfStrings = new List<string>();
            try
            {
                if (File.Exists(CONFIG_FILE))
                {
                    using (StreamReader origFile = new StreamReader(CONFIG_FILE))
                    {
                        while (origFile.Peek() != -1)
                        {
                            listOfStrings.Add(origFile.ReadLine());

                        }
                        //  origFile.Close();

                    }
                }
                foreach (string s in listOfStrings)
                {
                    System.Diagnostics.Trace.WriteLine(s);
                }
            }

            catch (FileNotFoundException FNFE)
            {
                MessageBox.Show(FNFE.Message, "info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);

            }

            catch (UnauthorizedAccessException UAE)
            {
                MessageBox.Show(UAE.Message, "info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }



        #endregion

        #region plotting
        private void plotShape()
        {
           
            double result;

            for (double x = 0; x <= xRadius; x++)
            {
                for (double y = 0; y <= yRadius; y++)
                {
                    System.Diagnostics.Trace.Write("\nx:" + x + " y:" + y + "\n");
                    result = (Math.Pow(x, 2d) / Math.Pow(xRadius, 2d)) + (Math.Pow(y, 2d) / Math.Pow(yRadius, 2d));
                    if (result >= .95 && result <= 1.05)
                    {
                        plotData.Add(new MyCube(x, y, result));
                    }
                }
            }
        }
        #endregion

        #region blueprint settings tab controls

        private void startTheCogs(object sender, RoutedEventArgs e)
        {
            //check path/ blueprint name/ id
            //ascern radio button settings
            /*
             switch (shape){
             case "circle":
              
            xRadius = radOneSlide.Value;
            yRadius = radOneSlide.Value;
            zRadius = radOneSlide.Value;
             break;
             case "elipse":
            xRadius = radOneSlide.Value;
            yRadius = radTwoSlide.Value;
             break;
             case "": 
             break;
              case "":
             break;
                zRadius = radThreeSlide.Value;
             }
             */


            //validate radius values
        }

        #endregion

        #region shape settings tab controls


        private void wantsCircle(object sender, RoutedEventArgs e)
        {
            //disable / hide y and z controllers
            disableControl(radTwoSlide);
            disableControl(radTwoTxt);
            disableControl(radTwolbl);
            disableControl(radThreeSlide);
            disableControl(radThreeTxt);
            disableControl(radThreelbl);
            shape = "circle";
        }

        private void wantsElipse(object sender, RoutedEventArgs e)
        {
            //enable y controllers
            enableControl(radTwoSlide);
            enableControl(radTwoTxt);
            enableControl(radTwolbl);
           if (make3D.IsChecked == true)
            {
                enableControl(radThreeSlide);
                enableControl(radThreeTxt);
                enableControl(radThreelbl);
            }
           shape = "elipse";
        }
        private void wants3D(object sender, RoutedEventArgs e)
        {
            //enable Z controller
            if (makeElipse.IsChecked==true)
            {
                enableControl(radThreeSlide);
                enableControl(radThreeTxt);
                enableControl(radThreelbl);
            }
        }

        private void wants2D(object sender, RoutedEventArgs e)
        {
            //disable z controller
            disableControl(radThreeSlide);
            disableControl(radThreeTxt);
            disableControl(radThreelbl);
        }

        private void wantsWhole(object sender, RoutedEventArgs e)
        {
            //adjust formula loop 
            
        }

        private void wantsQuarter(object sender, RoutedEventArgs e)
        {
            //adjust formula loop 
        }

        private void wantsHalf(object sender, RoutedEventArgs e)
        {
            //adjust formula loop 
        }

        private void wantsSolid(object sender, RoutedEventArgs e)
        {
            //adjust formula loop tolerance
        }

        private void wantsFrame(object sender, RoutedEventArgs e)
        {
            //adjust formula loop tolerance
        }

       

        #region sliders and textboxes

       
        private void roundValue(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sender.GetType() == typeof(Slider))
            {
             //   System.Diagnostics.Trace.WriteLine(e.NewValue);
                Slider s = (Slider)sender as Slider;
                s.Value = Math.Round(s.Value, 0);
            }
        }


        private void textChanged(object sender, TextChangedEventArgs e)
        {
            double num;
            if (sender.GetType() == typeof(TextBox))
            {
              //  System.Diagnostics.Trace.WriteLine(e.Changes);
             //   System.Diagnostics.Trace.WriteLine(e.Source);
                TextBox t = (TextBox)sender as TextBox;
                if (double.TryParse(t.Text, out num))
                {
                    if (num >= 10 && num <= 500)
                    {
                        t.Text = Math.Round(num, 0).ToString();
                    }
                }
                else { t.Text = "10"; }

            }
        }
        #endregion

        private void disableControl(object o)
        {
            UIElement x = (UIElement)o as UIElement;

            x.IsEnabled = x.IsEnabled ? false:false ;
            x.Visibility = x.IsVisible ? Visibility.Collapsed : Visibility.Collapsed;
        }
        private void enableControl(object o)
        {
            UIElement x = (UIElement)o as UIElement;

            x.IsEnabled = !x.IsEnabled ?  true:true;
            x.Visibility = !x.IsVisible ? Visibility.Visible : Visibility.Visible;
        }

        #endregion

        #region block settings tab controls

        #region Dependency Property Fields

        public static readonly DependencyProperty FillColorProperty =
           DependencyProperty.Register
           ("FillColor", typeof(Color), typeof(MainWindow),
           new PropertyMetadata(Colors.Black));


        public Color FillColor
        {
            get
            {
                return (Color)GetValue(FillColorProperty);
            }
            set
            {
                SetValue(FillColorProperty, value);
            }
        }

        #endregion

        private void SetFill(object sender, RoutedEventArgs e)
        {

            //   Shape selectedShape = (Shape)GetValue(SelectedShapeProperty);

            Microsoft.Samples.CustomControls.ColorPickerDialog cPicker
                = new Microsoft.Samples.CustomControls.ColorPickerDialog();
            cPicker.StartingColor = FillColor;
            cPicker.Owner = this;

            bool? dialogResult = cPicker.ShowDialog();
            if (dialogResult != null && (bool)dialogResult == true)
            {

                //   if (selectedShape != null)
                //       selectedShape.Fill = new SolidColorBrush(cPicker.SelectedColor);
                FillColor = cPicker.SelectedColor;

            }
        }

        #endregion


        private void ResetToLoaded(object sender, RoutedEventArgs e)
        {
            firstLoad();
        }

        private void mainClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine(e.ToString());
            MessageBoxResult goodBye = MessageBox.Show("Do you intend on leaving? ", "exit app", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
            if (goodBye != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }



        private void menuExit(object sender, RoutedEventArgs e)
        {
            mainClose(this, new System.ComponentModel.CancelEventArgs());
        }


















    }
}
