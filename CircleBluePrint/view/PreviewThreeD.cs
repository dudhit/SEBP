using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint.Collection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Data;

namespace SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint
{
    class PreviewThreeD : Viewport3D, INotifyPropertyChanged
    {
        public static bool alreadyRunning;
        private Model3DGroup groupIt;
        private ModelVisual3D visual;
        private AmbientLight letThereBe;
        private DirectionalLight dl;
        private PerspectiveCamera myCam;
        private GeometryModel3D aShape;
        private double viewWidth;
        private double viewHeight;
           public Point3D Eye;
        // private List<Point3D> drawData;


        public Double ViewWidth
        {
            get { return this.viewWidth; }
            set
            {
                this.viewWidth = value;
                this.Width = viewWidth;
            }
        }
        public Double ViewHeight
        {
            get { return this.viewHeight; }
            set
            {
                this.viewHeight = value;
                this.Height = viewHeight;
            }
        }

        //  public PreviewThreeD(List<Point3D> plotThis, Point3D cam)
        public PreviewThreeD()
        {
            alreadyRunning = true;
           // this.Width = 300; this.Height = 300;
            this.ClipToBounds = true;
            this.IsHitTestVisible = false;
      //      System.Windows.Controls.WrapPanel wPanel = (System.Windows.Controls.WrapPanel)wPanel as WrapPanel;

        //    Binding myBinding = new Binding("ViewWidth");
      //     myBinding.Source = wPanel;
      //     BindingOperations.SetBinding(this.Width, WidthProperty, myBinding);
            /*
                 <Viewport3D x:Name="threeDprojection"   
                    Height="{Binding Height, ElementName=sp, Mode=OneWay}"
                    Width="{Binding Width, ElementName=sp, Mode=OneWay}" >
        </Viewport3D>
             */
            /*    this.AddHandler(Window.MouseLeftButtonDownEvent,new  MouseButtonEventHandler(lMouseDown));
                this.AddHandler(Window.MouseLeftButtonUpEvent, new MouseButtonEventHandler(lMouseUp));
                this.AddHandler(Window.MouseRightButtonDownEvent, new MouseButtonEventHandler(rMouseDown));
                this.AddHandler(Window.MouseRightButtonUpEvent, new MouseButtonEventHandler(rMouseUp));
                this.AddHandler(Window.MouseMoveEvent, new MouseEventHandler(mouseTrack));*/
                //this.btnTest.Click+=new RoutedEventHandler(btnTest_Click);
            //  this.drawData = plotThis;
        
            // CreateScene();
            // ScrollViewer.HorizontalScrollBarVisibility="Auto" ScrollViewer.VerticalScrollBarVisibility="Auto" ScrollViewer.CanContentScroll="True"
        }


        #region scene/model setup
        /*To render the scene, add models and lights to a Model3DGroup,
         * then set the Model3DGroup as the Content of a ModelVisual3D.
         * Add the ModelVisual3D to the Children collection of the Viewport3D.
         * Add cameras to the Viewport3D by setting its Camera property.

Finally, add the Viewport3D to the window.
         * When the Viewport3D is included as the content of a layout element like Canvas,
         * specify the size of the Viewport3D by setting its Height and Width properties (inherited from FrameworkElement).
         */
        public void CreateScene()
        {

            visual = new ModelVisual3D();
            groupIt = new Model3DGroup();
            ConfigLight();
            SetCamera();

            aShape = MakeModel();
            groupIt.Children.Add(aShape);

            // groupIt.Children.Add(letThereBe);
            groupIt.Children.Add(dl);
            visual.Content = groupIt;
            DisplayScene();

        }

        private void DisplayScene()
        {
            this.Children.Add(visual);
            this.Camera = myCam;
        }

        private void SetCamera()
        {
            myCam = new PerspectiveCamera();

            myCam.Position = Eye;
            myCam.LookDirection = new Vector3D(Eye.X * -1, Eye.Y * -1, Eye.Z * -1);
            myCam.UpDirection = new Vector3D(0, 0, 1);
            myCam.FieldOfView = 60;
        }



        private void ConfigLight()
        {
            letThereBe = new AmbientLight();
            letThereBe.Color = Colors.Wheat;
            dl = new DirectionalLight();
            dl.Color = Colors.Violet;
            dl.Direction = new Vector3D(-10, -10, 0);
        }

        private GeometryModel3D MakeModel()
        {
            GeometryModel3D aCube = new GeometryModel3D();
            //   MeshGeometry3D mesh = new MeshGeometry3D();
            if (PointContainer.IsEmpty() == false)
            //if (drawData != null)
            {
                aCube.Geometry = MakeMesh();
            }
            else { MessageBox.Show("There is nothing to draw", "Logic error", MessageBoxButton.OK, MessageBoxImage.Error); }
            DiffuseMaterial dm = new DiffuseMaterial();
            SolidColorBrush scb = new SolidColorBrush();
            scb.Color = Colors.Red;
            scb.Opacity = 1;
            dm.Brush = scb;

            SpecularMaterial sm = new SpecularMaterial();
            scb.Color = Colors.Gray;
            sm.Brush = scb;

            // aCube.Geometry = mesh;
            aCube.Material = sm;
            aCube.Material = dm;
            return aCube;
        }

        private MeshGeometry3D MakeMesh()
        {
            Object lockable = new Object();
            MeshGeometry3D mesh = new MeshGeometry3D();
            Point3D p; Point3D temp = new Point3D(); 
            int[] joinVertgroups = new int[] { 0, 4, 2, 4, 6, 2, 0, 1, 2, 1, 3, 2, 1, 7, 3, 1, 5, 7, 0, 1, 4, 4, 1, 5, 4, 6, 7, 7, 5, 4, 6, 2, 3, 3, 7, 6 };
           //    Parallel.ForEach(OneCube(), c =>
          for (int c = 0; c < PointContainer.Count(); c++)
            {
                p = PointContainer.Item(c);
                for (double xV = -0.5; xV <= 0.5; xV++)
                {
                    for (double yV = -.5; yV <= .5; yV++)
                    {
                        for (double zV = -.5; zV <= .5; zV++)
                        {
                            temp = new Point3D(p.X + xV, p.Y + yV, p.Z + zV);
                               lock (lockable){  mesh.Positions.Add(temp);                             }
                        }
                    }
                }
             
                foreach (int pointRef in joinVertgroups)
                {
                      lock (lockable){
                    mesh.TriangleIndices.Add(pointRef + (c * 8));
                   }
                }
            }//);
            return mesh;

        }

        public static System.Collections.Generic.IEnumerable<int> OneCube()
        {
            for (int i = 0; i < PointContainer.Count(); i++)
            {
                yield return i;
            }
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
   }
}
