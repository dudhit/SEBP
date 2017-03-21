using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SoloProjects.Dudhit.UserInterfaces.EventArguments;
//using SoloProjects.Dudhit.SpaceEngineers.SEBP.EventArguments;
using SoloProjects.Dudhit.UserInterfaces;
using System.Windows.Media.Media3D;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.View
{
    /// <summary>
    /// Interaction logic for BlockTabView.xaml
    /// </summary>
    public partial class BlockTabView : UserControl
    {
        private SoloProjects.Dudhit.UserInterfaces.CrudeColorPicker.CrudePicker ccp;
        private Color fillColour;
        private string gridSize;
        private string armourType;
        private Point3D hsv;
        public Color FillColour { get { return this.fillColour; } set { this.fillColour = value; } }

        public BlockTabView()
        {
            InitializeComponent();
            fillColour = Color.FromArgb(255, 255, 0, 0);
            blockNormal.IsChecked = true;
            blockLarge.IsChecked = true;
            //ImplementColourSubscritptions();
            colGrey1.IsChecked = true;
        }

        private void BlockChanged(object sender, RoutedEventArgs e)
        {
            NotifyBlockChange();
        }


        private void SetFill(object sender, RoutedEventArgs e)
        {
            colCustom.IsChecked = false;
            Color tempCol = SoloProjects.Dudhit.Utilites.ColourConverters.MakeAColourFromString(colCustom.BoxColour);
            ccp = new SoloProjects.Dudhit.UserInterfaces.CrudeColorPicker.CrudePicker(tempCol);

            bool? dialogResult = ccp.ShowDialog();
            if (dialogResult != null && (bool)dialogResult == true)
            {
                colCustom.BoxColour = ccp.SelectedColour.ToString();
                FillColour = ccp.SelectedColour;
            }
            ccp.Close();
            colCustom.IsChecked = true;
        }


        //private void ImplementColourSubscritptions()
        //{
        //    colRed.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colRed1.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colRed2.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colRed3.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colGrey.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colGreen.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colBlack.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colGrey1.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colGrey2.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colGrey3.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colWhite.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colBlack1.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colBlack2.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colBlack3.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colGreen1.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colGreen2.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colGreen3.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colWhite1.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colWhite2.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colWhite3.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colYellow.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colYellow1.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colYellow2.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colYellow3.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colCustom.ColourChangedEvent += new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //}


        public void UpdateColValue(object sender, ColourBoxEventArgs x)
        {
         //   System.Diagnostics.Trace.WriteLine(x.hexColour);
            //check if alpha not 255// ff and adjust accordingly
          FillColour = (Color)SoloProjects.Dudhit.Utilites.ColourConverters.MakeAColourFromString(x.hexColour);
            if (FillColour.A != 255) fillColour.A = 255;
            //convert to HSV 
            Point3D temp;
            hsv = SoloProjects.Dudhit.Utilites.ColourConverters.ConvertRgbToHsv(fillColour.R, fillColour.G, fillColour.B);
            temp = SoloProjects.Dudhit.Utilites.ColourConverters.ConvertStandardHSVtoSEFormat((float)hsv.X, (float)hsv.Y, (float)hsv.Z);
            hsv = SoloProjects.Dudhit.Utilites.ColourConverters.ConvertSEFormatHSVtoBluePrintFormat((float)temp.X, (float)temp.Y, (float)temp.Z);
            NotifyBlockChange();
        }

        private void NotifyBlockChange()
        {
            gridSize = (blockLarge.IsChecked == true) ? "Large" : "Small";
            switch (gridSize)
            {
                case "Large":
                    armourType = (blockNormal.IsChecked == true) ? "LargeBlockArmorBlock" : "LargeHeavyBlockArmorBlock";
                    break;

                case "Small":
                    armourType = (blockNormal.IsChecked == true) ? "SmallBlockArmorBlock" : "SmallHeavyBlockArmorBlock";
                    break;
            }  
            //BlockChangeEventArgs bcea = new BlockChangeEventArgs(hsv, gridSize, armourType);
            //OnBlockChangeEvent(bcea);
        }

        //private void OnBlockChangeEvent(BlockChangeEventArgs bcea)
        //{
        //    BlockChangeHandler handler = BlockChangedEvent;

        //    if (handler != null)
        //    {

        //        handler(this, bcea);
        //    }
        //}

        #region events
        //public delegate void BlockChangeHandler(object sender, BlockChangeEventArgs blockArgs);
        //// an instance of the delegate
        //public event BlockChangeHandler BlockChangedEvent;


        #endregion

        //private void UnSubscribe(object sender, RoutedEventArgs e)
        //{
        //    colRed.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colRed1.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colRed2.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colRed3.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colGrey.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colGreen.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colBlack.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colGrey1.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colGrey2.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colGrey3.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colWhite.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colBlack1.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colBlack2.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colBlack3.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colGreen1.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colGreen2.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colGreen3.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colWhite1.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colWhite2.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colWhite3.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colYellow.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colYellow1.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colYellow2.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colYellow3.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //    colCustom.ColourChangedEvent -= new ColourSampleSelection.ColourChangeHandler(UpdateColValue);
        //}

    }
}
