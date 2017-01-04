using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.view
{
    /// <summary>
    /// Interaction logic for ModelViewer.xaml
    /// </summary>
    public partial class ModelViewer : Window, INotifyPropertyChanged
    {
        public ModelViewer()
        {
            InitializeComponent();
        }



        private Point3D camera;
        public Point3D Camera { get { return camera; } set { camera = value; OnPropertyChanged("Camera"); } }

        #region Mouse event handling
        private bool leftMouse = false;
        private bool rightMouse = false;
        private Point mouseStart;

        public void lMouseDown(object sender, MouseButtonEventArgs e)
        {
            leftMouse = true;
            mouseStart = e.GetPosition(wPanel);
        }
        public void rMouseDown(object sender, MouseButtonEventArgs e)
        {
            rightMouse = true;
            mouseStart = e.GetPosition(wPanel);
        }
        public void lMouseUp(object sender, MouseButtonEventArgs e)
        {
            leftMouse = false;
        }
        public void rMouseUp(object sender, MouseButtonEventArgs e)
        {
            rightMouse = false;
        }

        public void mouseTrack(object sender, MouseEventArgs e)
        {
            Point3D pp = Camera;
            Point p = e.GetPosition(wPanel);
            double Bound = 50;
            if (leftMouse)
            {
                if (mouseStart.X < p.X + 10)
                {
                    pp.X = (pp.X < Bound) ? pp.X += 1 : pp.X = Bound;
                    updateScene(pp);
                }
                if (mouseStart.X > p.X - 10)
                {
                    pp.X = (pp.X > -Bound) ? pp.X -= 1 : pp.X = -Bound;
                    updateScene(pp);
                }
                if (mouseStart.Y < p.Y + 10)
                {
                    pp.Y = (pp.Y < Bound) ? pp.Y += 1 : pp.Y = Bound;
                    updateScene(pp);
                }
                if (mouseStart.Y > p.Y - 10)
                {
                    pp.Y = (pp.Y > -Bound) ? pp.Y -= 1 : pp.Y = -Bound;
                    updateScene(pp);
                }
            }
            if (rightMouse)
            {
                if (mouseStart.X < p.X + 10)
                {
                    pp.Z = (pp.Z < Bound) ? pp.Z += 1 : pp.Z = Bound;
                    updateScene(pp);
                }
                if (mouseStart.X > p.X - 10)
                {
                    pp.Z = (pp.Z > -Bound) ? pp.Z -= 1 : pp.Z = -Bound;
                    updateScene(pp);
                }
            }

            //   xDisp.Content = p.X + ":" + p.Y;
            //   yDisp.Content = mouseStart.X + ":" + mouseStart.Y;
            //   camPoint.Content = pp.X + ":" + pp.Y + ":" + pp.Z;
        }

        private void updateScene(Point3D pp)
        {
            //      myCam.Position = pp;// new Point3D(pp.X, pp.Y, pp.Z);
            //      myCam.LookDirection = new Vector3D(pp.X * -1, pp.Y * -1, pp.Z * -1);
            //      PreviewThreeD.Camera = myCam;
        }
        #endregion

        # region generic event handler
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(String info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion

        // var binding = new Binding("MyProperty");
        //BindingOperations.SetBinding(_textBlock, TextBlock.TextProperty, binding);
    }
}
