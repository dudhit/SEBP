using SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint.view;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint
{
    public partial class MainWindow : Window
    {
     
        // private string shapeSelected;
   
        #region shape settings tab controls

        private void SetAxisRadius()
        {
            double xRadius = 0;
            double yRadius = 0;
            double zRadius = 0;
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
            string shapeSelected = "";
            if (makeQuater.IsChecked == true && (makeCircle.IsChecked == true || makeElipse.IsChecked == true))
            { shapeSelected = "QuaterRing"; }
            if (makeQuater.IsChecked == true && (makeSphere.IsChecked == true || makeElipsoid.IsChecked == true))
            { shapeSelected = "QuaterSphere"; }

            if (makeSemi.IsChecked == true && (makeCircle.IsChecked == true || makeElipse.IsChecked == true))
            { shapeSelected = "SemiRing"; }

            if (makeSemi.IsChecked == true && (makeSphere.IsChecked == true || makeElipsoid.IsChecked == true))
            { shapeSelected = "HemiSphere"; }

            if (makeFull.IsChecked == true && (makeCircle.IsChecked == true || makeElipse.IsChecked == true))
            { shapeSelected = "FullRing"; }

            if (makeFull.IsChecked == true && (makeSphere.IsChecked == true || makeElipsoid.IsChecked == true))
            { shapeSelected = "FullSphere"; }

            shapeSettings = new WorkingArgs(xRadius, yRadius, zRadius, lowTol, highTol, shapeSelected);

        }

        private void ActionRefreshView(object sender, RoutedEventArgs e)
        {
            if (!IsGeneratingPreview)
            {
             //   refreshPreviewBut.Content = "Cancel preview";
                refreshPreviewBut.Content = "Disabled Feature";
                IsGeneratingPreview = true;
             //   StartStopCalculating(sender, e);
             //       
            // threeDView = new ModelViewer();
           //   threeDView.Owner = this;

                //bool? previewResult = threeDView.ShowDialog();
                //if (previewResult != null && (bool)previewResult == true)
                //{
                  
                //}
                //else
                //{  IsGeneratingPreview = false;
                //    refreshPreviewBut.Content = "show preview"; }
            }
            else
            {
                IsGeneratingPreview = false;
                //cancel preview
                refreshPreviewBut.Content = "show preview";
            }


        }


        #region radio buttons

        private void WantsCircle(object sender, RoutedEventArgs e)
        {
            //disable / hide y and z controllers
            DisableControl(radTwoSlide);
            DisableControl(radTwoTxt);
            DisableControl(radTwolbl);
            DisableControl(radThreeSlide);
            DisableControl(radThreeTxt);
            DisableControl(radThreelbl);
            shapeSettingChanged = true;
        }

        private void WantsElipse(object sender, RoutedEventArgs e)
        {
            //enable y controllers
            EnableControl(radTwoSlide);
            EnableControl(radTwoTxt);
            EnableControl(radTwolbl);
            shapeSettingChanged = true;
        }


        private void WantsSphere(object sender, RoutedEventArgs e)
        {
            WantsCircle(this, e);
            shapeSettingChanged = true;
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
            shapeSettingChanged = true;
        }

        private void WantsWhole(object sender, RoutedEventArgs e)
        {
            shapeSettingChanged = true;
        }

        private void WantsQuarter(object sender, RoutedEventArgs e)
        {
            shapeSettingChanged = true;
        }

        private void WantsHalf(object sender, RoutedEventArgs e)
        {
            shapeSettingChanged = true;
        }


        private void WantsFrame(object sender, RoutedEventArgs e)
        {
            shapeSettingChanged = true;
        }

        #endregion
        #region sliders and textboxes


        private void RoundValue(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sender.GetType() == typeof(Slider))
            {
                //   System.Diagnostics.Trace.WriteLine(e.NewValue);
                Slider s = (Slider)sender as Slider;
                s.Value = Math.Round(s.Value, 0);
                shapeSettingChanged = true;
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

                } shapeSettingChanged = true;

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
                    if (num >= 10 && num <= 100)
                    {
                        t.Text = Math.Round(num, 0).ToString();
                    }
                }
                else { t.Text = "10"; }
                shapeSettingChanged = true;
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
