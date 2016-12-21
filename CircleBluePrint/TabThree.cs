using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint
{
    public partial class MainWindow : Window
    {

        #region block settings tab controls

    
        private void SetFill(object sender, RoutedEventArgs e)
        {
            Microsoft.Samples.CustomControls.ColorPickerDialog cPicker
                      = new Microsoft.Samples.CustomControls.ColorPickerDialog();
            cPicker.StartingColor = FillColor;
            cPicker.Owner = this;
            bool? dialogResult = cPicker.ShowDialog();
            if (dialogResult != null && (bool)dialogResult == true)
            {
                FillColor = cPicker.SelectedColor;
            }
        }

        private void ColourChosen(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(RadioButton)) return;
            RadioButton rb = (RadioButton)sender;
            System.Diagnostics.Trace.WriteLine(0);
            float h; float s; float v;
            switch (rb.Name)
            {

                case "colBlack":
                    h = 0; s = -81; v = -30;
                    HSV_ColourPickerToBP_Val(h, s, v);
                    break;

                case "colBlack1":
                    h = 0; s = -96; v = -50;
                    HSV_ColourPickerToBP_Val(h, s, v);
                    break;
                case "colBlue":
                    h = 207; s = 15; v = 20;
                    HSV_ColourPickerToBP_Val(h, s, v);
                    break;
                case "colBlue1":
                    h = 207; s = 0; v = 0;
                    HSV_ColourPickerToBP_Val(h, s, v);
                    break;
                case "colGreen":
                    h = 120; s = -33; v = -5;
                    HSV_ColourPickerToBP_Val(h, s, v);
                    break;
                case "colGreen1":
                    h = 120; s = -48; v = -25;
                    HSV_ColourPickerToBP_Val(h, s, v);
                    break;
                case "colGrey":
                    h = 0; s = -85; v = 20;
                    HSV_ColourPickerToBP_Val(h, s, v);
                    break;
                case "colGrey1":
                    h = 0; s = -100; v = 0;
                    HSV_ColourPickerToBP_Val(h, s, v);
                    break;
                case "colRed":
                    h = 0; s = 15; v = 25;
                    HSV_ColourPickerToBP_Val(h, s, v);
                    break;
                case "colRed1":
                    h = 0; s = 0; v = 5;
                    HSV_ColourPickerToBP_Val(h, s, v);
                    break;
                case "colWhite":
                    h = 0; s = -80; v = 60;
                    HSV_ColourPickerToBP_Val(h, s, v);
                    break;
                case "colWhite1":
                    h = 0; s = -95; v = 40;
                    HSV_ColourPickerToBP_Val(h, s, v);
                    break;
                case "colYellow":
                    h = 44; s = 5; v = 46;
                    HSV_ColourPickerToBP_Val(h, s, v);
                    break;
                case "colYellow1":
                    h = 44; s = -10; v = 26;
                    HSV_ColourPickerToBP_Val(h, s, v);
                    break;
                case "colCustom":
                    MessageBox.Show(FillColor.ToString(),"custom color processing",MessageBoxButton.OK);
                    break;




            }
        }

        private void HSV_ColourPickerToBP_Val(float h, float s, float v)
        {
            blockColourHue = h / 360;
            blockColourSaturation = s / 100;
            blockColourValue = v / 100;

        }



        #endregion


    }
}
