using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace CircleBluePrint
{
    class PreviewThreeD : Viewport3D, INotifyPropertyChanged
    {
        private Model3DGroup groupIt;
        private ModelVisual3D visual;
        private AmbientLight letThereBe;
        private DirectionalLight dl;
        private PerspectiveCamera myCam;
        private GeometryModel3D aShape;
        private double viewWidth;
        private double viewHeight;
        private Point3D zoomOut;
        private List<Point3D> drawData;
        public Point3D ZoomOut
        {
            //  get { return this.viewWidth; }
            set { this.zoomOut = value; }
        }

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

        public PreviewThreeD(List<Point3D> plotThis)
        {

            this.Width = 300; this.Height = 300;
            this.ClipToBounds = false;
            this.IsHitTestVisible = false;
            this.drawData = plotThis;
            CreateScene();
            // ScrollViewer.HorizontalScrollBarVisibility="Auto" ScrollViewer.VerticalScrollBarVisibility="Auto" ScrollViewer.CanContentScroll="True"
        }


        /*To render the scene, add models and lights to a Model3DGroup,
         * then set the Model3DGroup as the Content of a ModelVisual3D.
         * Add the ModelVisual3D to the Children collection of the Viewport3D.
         * Add cameras to the Viewport3D by setting its Camera property.

Finally, add the Viewport3D to the window.
         * When the Viewport3D is included as the content of a layout element like Canvas,
         * specify the size of the Viewport3D by setting its Height and Width properties (inherited from FrameworkElement).
         */

        private void CreateScene()
        {
            groupIt = new Model3DGroup();
            visual = new ModelVisual3D();

            ConfigLight();
            SetCamera();

            aShape = MakeModel();
            groupIt.Children.Add(aShape);

            // groupIt.Children.Add(letThereBe);
            groupIt.Children.Add(dl);
            visual.Content = groupIt;
            this.Children.Add(visual);
            this.Camera = myCam;
        }

        private void SetCamera()
        {
            myCam = new PerspectiveCamera();

            myCam.Position = new Point3D(21, 21, 21);
            myCam.LookDirection = new Vector3D(-.5, -.5, -.5);
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
            if (drawData != null)
            {
                aCube.Geometry = MakeMesh();
            }
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
            MeshGeometry3D mesh = new MeshGeometry3D();
            Point3D p; Point3D temp = new Point3D();
            for (int i = 0; i < drawData.Count; i++)
            {
                p = drawData[i];
                for (double xV = -0.5; xV <= 0.5; xV++)
                {
                    for (double yV = -.5; yV <= .5; yV++)
                    {
                        for (double zV = -.5; zV <= .5; zV++)
                        {
                            //   s = string.Format("{0}, {1},{2}", xV, yV, zV);
                            //  System.Diagnostics.Trace.WriteLine(s);
                            temp = new Point3D(p.X + xV, p.Y + yV, p.Z + zV);

                            /*  if(!mesh.Positions.Contains(temp)){ */
                            mesh.Positions.Add(temp);
                            /*}*/

                        }
                    }
                }
                int[] joinVertgroups = new int[] { 0, 4, 2, 4, 6, 2, 0, 1, 2, 1, 3, 2, 1, 7, 3, 1, 5, 7, 0, 1, 4, 4, 1, 5, 4, 6, 7, 7, 5, 4, 6, 2, 3, 3, 7, 6 };
                foreach (int pointRef in joinVertgroups)
                {
                    mesh.TriangleIndices.Add(pointRef + (i * 8));
                }
            }
            return mesh;

        }



        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(String info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
