using SoloProjects.Dudhit.UserInterfaces.EventArguments;
using System.Windows;
using System.Windows.Controls;
using SoloProjects.Dudhit.UserInterfaces;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.EventArguments;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.View
{
    /// <summary>
    /// Interaction logic for ShapeTabView.xaml
    /// </summary>
    public partial class ShapeTabView : UserControl
    {
        private double xRadius = 10;
        private double yRadius = 10;
        private double zRadius = 10;
        private string shapeSelected;
        private string fraction;
        private string shape;
        public ShapeTabView()
        {
            InitializeComponent();
            ImplementTextSliderComboSubscriptions();
            yAxisCombo.TextValue = "10";
            zAxisCombo.TextValue = "10";
            xAxisCombo.TextValue = "10";
            makeCircle.IsChecked = true;
            makeQuater.IsChecked = true;
        }

        #region radio buttons
        private void WantsCircle(object sender, RoutedEventArgs e)
        {
            //disable / hide y and z controllers
            DisableControl(yAxisCombo);
            DisableControl(zAxisCombo);
            shape = "Circle";
            yRadius = xRadius;
            zRadius = xRadius;
            NotifyShapeChanged();
        }

        private void WantsEllipse(object sender, RoutedEventArgs e)
        {
            //enable y controllers
            EnableControl(yAxisCombo);
            DisableControl(zAxisCombo);
                        shape = "Ellipse";
            yAxisCombo.TextValue = xRadius.ToString();
                        zRadius = xRadius;
            NotifyShapeChanged();
        }

        private void WantsSphere(object sender, RoutedEventArgs e)
        {
            //disable / hide y and z controllers
            DisableControl(yAxisCombo);
            DisableControl(zAxisCombo);
            shape = "Sphere";
                        yRadius = xRadius;
            zRadius = xRadius;
            NotifyShapeChanged();
        }

        private void WantsEllipsoid(object sender, RoutedEventArgs e)
        {
            //enable Z& Z controller
            EnableControl(yAxisCombo);
            EnableControl(zAxisCombo);
            shape = "Ellipsoid";
            yAxisCombo.TextValue = xRadius.ToString();
            zAxisCombo.TextValue = xRadius.ToString();
            NotifyShapeChanged();
        }

        private void WantsQuarter(object sender, RoutedEventArgs e)
        {
            fraction = "Quarter";
            NotifyShapeChanged();
        }

        private void WantsHalf(object sender, RoutedEventArgs e)
        {
            fraction = "Semi";
            NotifyShapeChanged();
        }

        private void WantsWhole(object sender, RoutedEventArgs e)
        {
            fraction = "Full";
            NotifyShapeChanged();
        }

        #endregion
        private void ActionRefreshView(object sender, RoutedEventArgs e)
        {

        }

        private void NotifyShapeChanged()
        {
        shapeSelected = fraction+shape;
        //ShapeChangeEventArgs sce = new ShapeChangeEventArgs(shapeSelected, xRadius, yRadius, zRadius);
        // OnShapeChangeEvent(sce);
        }

        //private void OnShapeChangeEvent(ShapeChangeEventArgs sce)
        //{
        //    ShapeChangeHandler handler = ShapeChangedEvent;
        //    if (handler != null) handler(this, sce);
        //}

        #region sliders and textboxes
        private void ImplementTextSliderComboSubscriptions()
        {
            xAxisCombo.SliderChangedEvent += new TextSliderCombo.AxisChangeHandler(UpdateAxisValue);
            yAxisCombo.SliderChangedEvent += new TextSliderCombo.AxisChangeHandler(UpdateAxisValue);
            zAxisCombo.SliderChangedEvent += new TextSliderCombo.AxisChangeHandler(UpdateAxisValue);
        }

        public void UpdateAxisValue(object sender, TextSliderComboEventArgs tsComboArgs)
        {
            TextSliderCombo tsc = (sender as TextSliderCombo);
            if (tsc != null)
            {
                switch (tsc.Name.ToString())
                {
                    case "xAxisCombo":
                        xRadius = tsComboArgs.axisValue;
                        break;
                    case "yAxisCombo":
                        yRadius = tsComboArgs.axisValue;
                        break;
                    case "zAxisCombo":
                        zRadius = tsComboArgs.axisValue;
                        break;
                }
                NotifyShapeChanged();
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

        private void Unloading(object sender, RoutedEventArgs e)
        {
            xAxisCombo.SliderChangedEvent -= new TextSliderCombo.AxisChangeHandler(UpdateAxisValue);
            yAxisCombo.SliderChangedEvent -= new TextSliderCombo.AxisChangeHandler(UpdateAxisValue);
            zAxisCombo.SliderChangedEvent -= new TextSliderCombo.AxisChangeHandler(UpdateAxisValue);
        }
        #region events
        //public delegate void ShapeChangeHandler(object sender, ShapeChangeEventArgs shapeArgs);
        //// an instance of the delegate
        //public event ShapeChangeHandler ShapeChangedEvent;


        #endregion
    }
}
