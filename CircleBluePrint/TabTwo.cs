using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint
{
    public partial class MainWindow : Window
    {

        #region shape settings tab controls

        private void SetAxisRadius()
        {
            if (plotData != null) { plotData.Clear(); }

            //validate radius values
            if (makeCircle.IsChecked == true)
            {
                xRadius = radOneSlide.Value;
                yRadius = radOneSlide.Value;
                zRadius = 0;
            }
            if (makeSphere.IsChecked == true)
            {
                xRadius = radOneSlide.Value;
                yRadius = radOneSlide.Value;
                zRadius = radOneSlide.Value;
            }
            if (makeElipse.IsChecked == true)
            {
                xRadius = radOneSlide.Value;
                yRadius = radTwoSlide.Value;
                zRadius = 0;
            }
            if (makeElipsoid.IsChecked == true)
            {
                xRadius = radOneSlide.Value;
                yRadius = radTwoSlide.Value;
                zRadius = radThreeSlide.Value;
            }
        }
        private void ActionRefreshView(object sender, RoutedEventArgs e)
        {
            StartTheCogs(this, e);
            List<Point3D> templist = new List<Point3D>();
            viewContainer.Content = null;
            foreach (Point3D p in plotData)
            {
                templist.Add(p);
            }

            previewViewer = new PreviewThreeD(templist);
            viewContainer.Content = previewViewer;
        }




        private void WantsCircle(object sender, RoutedEventArgs e)
        {
            //disable / hide y and z controllers
            DisableControl(radTwoSlide);
            DisableControl(radTwoTxt);
            DisableControl(radTwolbl);
            DisableControl(radThreeSlide);
            DisableControl(radThreeTxt);
            DisableControl(radThreelbl);

        }

        private void WantsElipse(object sender, RoutedEventArgs e)
        {
            //enable y controllers
            EnableControl(radTwoSlide);
            EnableControl(radTwoTxt);
            EnableControl(radTwolbl);
        }


        private void WantsSphere(object sender, RoutedEventArgs e)
        {
            WantsCircle(this, e);
        }

        private void WantsElipsoid(object sender, RoutedEventArgs e)
        {
            //enable Z controller

            EnableControl(radTwoSlide);
            EnableControl(radTwoTxt);
            EnableControl(radTwolbl);
            EnableControl(radThreeSlide);
            EnableControl(radThreeTxt);
            EnableControl(radThreelbl);
        }

        private void WantsWhole(object sender, RoutedEventArgs e)
        {
            //adjust formula loop 
        }

        private void WantsQuarter(object sender, RoutedEventArgs e)
        {
            //adjust formula loop 
        }

        private void WantsHalf(object sender, RoutedEventArgs e)
        {
            //adjust formula loop 
        }

        private void WantsSolid(object sender, RoutedEventArgs e)
        {
            //adjust formula loop tolerance
        }

        private void WantsFrame(object sender, RoutedEventArgs e)
        {
            //adjust formula loop tolerance
        }

        #region sliders and textboxes


        private void RoundValue(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sender.GetType() == typeof(Slider))
            {
                //   System.Diagnostics.Trace.WriteLine(e.NewValue);
                Slider s = (Slider)sender as Slider;
                s.Value = Math.Round(s.Value, 0);
            }
        }

        private void AdjustTolerance(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sender.GetType() == typeof(Slider))
            {
                //   System.Diagnostics.Trace.WriteLine(e.NewValue);
                Slider s = (Slider)sender as Slider;
                switch (s.Name)
                {

                    case "lowertoleranceSlide":
                        lowTol = Math.Round(s.Value / 100, 3);
                        break;

                    case "uppertoleranceSlide":
                        highTol = Math.Round(s.Value / 100, 3);
                        break;

                }
                System.Diagnostics.Trace.WriteLine(lowTol);
                System.Diagnostics.Trace.WriteLine(highTol);
            }
        }


        private void TextChanged(object sender, TextChangedEventArgs e)
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



        private void DisableControl(object o)
        {
            UIElement x = (UIElement)o as UIElement;

            x.IsEnabled = x.IsEnabled ? false : false;
            x.Visibility = x.IsVisible ? Visibility.Collapsed : Visibility.Collapsed;
        }

        private void EnableControl(object o)
        {
            UIElement x = (UIElement)o as UIElement;

            x.IsEnabled = !x.IsEnabled ? true : true;
            x.Visibility = !x.IsVisible ? Visibility.Visible : Visibility.Visible;
        }


        #endregion
        #endregion

  

    }
}
