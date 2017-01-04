using SoloProjects.Dudhit.SpaceEngineers.SEBP.Collection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.Utility
{
    public class CircleEvaluationCalculations:IDisposable
    {

        private int xIntRadius = -1;
        private int yIntRadius = -1;
        private int zIntRadius = -1;
        private double xRadius = -1;
        private double yRadius = -1;
        private double zRadius = -1;
        private string shapeSelected = null;
        private double lowToleranceEvaluation = -1;
        private double highToleranceEvaluation = -1;
        //setters
        public double RadiusInXPlane { set { this.xRadius = value; this.xIntRadius = Convert.ToInt32(value); } }
        public double RadiusInYPlane { set { this.yRadius = value; this.yIntRadius = Convert.ToInt32(value); } }
        public double RadiusInZPlane { set { this.zRadius = value; this.zIntRadius = Convert.ToInt32(value); } }
        public double LowToleranceEvaluation { set { this.lowToleranceEvaluation = value; } }
        public double HighToleranceEvaluation { set { this.highToleranceEvaluation = value; } }
        public string ShapeSelected { set { this.shapeSelected = value; } }
        #region plotting

        public void BeginPointChecking()
        {
            if (!ValidateInputs()) return;

            int xLoop; int yLoop; int zLoop;
            xLoop = (xIntRadius > 0) ? xIntRadius + 1 : xIntRadius;
            yLoop = (yIntRadius > 0) ? yIntRadius + 1 : yIntRadius;
            zLoop = (zIntRadius > 0) ? zIntRadius + 1 : zIntRadius;
            MakeAxisPoints();

            if (zLoop == 0)
            {
                Process2Axis(xLoop, yLoop);
            }
            else
            {
                //do x y z
                Process3Axis(xLoop, yLoop, zLoop);
            }
        }

        private void Process2Axis(int xLoop, int yLoop)
        {
            Point3D tempPoint;
            Parallel.ForEach(Axis(yLoop), y =>
            {
                Parallel.ForEach(Axis(xLoop), x =>
              {
                  tempPoint = new Point3D(x, y, 0);
                  ProcessNewPoint(tempPoint);
              });
            }
        );
        }

        private double ProcessNewPoint(Point3D tempPoint)
        {
            double pointWithinBounds = EvalPoint3D(tempPoint);
            if (SolidOrFrame(pointWithinBounds))
                DoShapePlotting(tempPoint);
            //string result = string.Format("x:{0}y:{1}z:{2}", tempPoint.X, tempPoint.Y, tempPoint.Z);
            //System.Diagnostics.Trace.WriteLine(result);
            return pointWithinBounds;
        }
  
        private void Process3Axis(int xLoop, int yLoop, int zLoop)
        {
            Point3D tempPoint;
            Parallel.ForEach(Axis(zLoop), z =>
            //  foreach(int i in Axis(xLoop))
            {
                // for (int y = 0; y <= yLoop; y++)
                Parallel.ForEach(Axis(yLoop), y =>
               {
                   // for (int x = 0; x <= xLoop; x++)
                   Parallel.ForEach(Axis(xLoop), x =>
                 {
                     tempPoint = new Point3D(x, y, z);
                     ProcessNewPoint(tempPoint);
                 });
               });
            });

        }

        private static System.Collections.Generic.IEnumerable<int> Axis(int radius)
        {
            for (int i = 0; i < radius; i++)
            {
                yield return i;
            }
        }

        // Summary:
        //     Loops through each x,y,z individually to plot axis points
        //     
        private void MakeAxisPoints()
        {
            List<Point3D> tempAxisPoints = new List<Point3D>();
            Parallel.For(0, xIntRadius, x =>
            {
                PointContainer.Add(new Point3D(x, 0, 0));
            });
            Parallel.For(0, yIntRadius, y =>
            {
                PointContainer.Add(new Point3D(0, y, 0));
            });
            Parallel.For(0, zIntRadius, z =>
            {
                PointContainer.Add(new Point3D(0, 0, z));
            });
        }

        // Summary:
        //     Calculates whether values x,y,z fit within the bounds of a circle/ellipse/sphere(oid)
        //     
        //
        // Returns:
        //     double 0-2 for further processing of desired shape
        private double EvalPoint3D(Point3D point)
        {
            double result;

            if (shapeSelected == "SemiRing" || shapeSelected == "FullRing" || shapeSelected == "QuaterRing")
            {
                return result = (Math.Pow(point.X, 2d) / Math.Pow(xRadius, 2d)) + (Math.Pow(point.Y, 2d) / Math.Pow(yRadius, 2d));
            }
            else
            {
                return result = (Math.Pow(point.X, 2d) / Math.Pow(xRadius, 2d)) + (Math.Pow(point.Y, 2d) / Math.Pow(yRadius, 2d)) + (Math.Pow(point.Z, 2d) / Math.Pow(zRadius, 2d));
            }

        }

        private bool SolidOrFrame(double result)
        {
            bool canPlot;
            return canPlot = (result >= lowToleranceEvaluation && result <= highToleranceEvaluation) ? true : false;

        }

        private void DoShapePlotting(Point3D p)
        {
            switch (shapeSelected)
            {
                case "QuaterRing":
                    PPP(p); break;
                case "QuaterSphere":
                    PPP(p); break;
                case "SemiRing":

                    PPP(p); NPP(p); break;
                case "HemiSphere":

                    PPP(p); NPP(p); PPN(p); NPN(p); break;
                case "FullRing":

                    PPP(p); NPP(p); PNP(p); NNP(p); break;
                case "FullSphere":

                    PPP(p); NPP(p); PPN(p); NPN(p); PNP(p);
                    NNP(p); PNN(p); NNN(p); break;

            }
            //   return p;
        }
        #region transformations
        private void PPP(Point3D p)
        {
            PointContainer.Add(p);
        }
        private void NPP(Point3D p)
        {
            PPP(new Point3D(p.X * -1, p.Y, p.Z));
        }
        private void PNP(Point3D p)
        {
            PPP(new Point3D(p.X, p.Y * -1, p.Z));
        }
        private void PPN(Point3D p)
        {
            PPP(new Point3D(p.X, p.Y, p.Z * -1));
        }
        private void NNP(Point3D p)
        {
            PPP(new Point3D(p.X * -1, p.Y * -1, p.Z));
        }
        private void PNN(Point3D p)
        {
            PPP(new Point3D(p.X, p.Y * -1, p.Z * -1));
        }
        private void NPN(Point3D p)
        {
            PPP(new Point3D(p.X * -1, p.Y, p.Z * -1));
        }
        private void NNN(Point3D p)
        {
            PPP(new Point3D(p.X * -1, p.Y * -1, p.Z * -1));
        }

        #endregion

        #endregion

        private bool ValidateInputs()
        {
            bool passFail = true;
            StringBuilder sb = new StringBuilder();
            if (xIntRadius == -1)
            { sb.Append("X radius was not set.\n"); passFail = false; }
            if (yIntRadius == -1)
            { sb.Append("Y radius was not set.\n"); passFail = false; }
            if (zIntRadius == -1)
            { sb.Append("Z radius was not set.\n"); passFail = false; }
            if (string.IsNullOrEmpty(shapeSelected) == true)
            { sb.Append("Appropriate Shape not assigned.\n"); passFail = false; }
            if (lowToleranceEvaluation == -1)
            { sb.Append("Low tolerance was not set.\n"); passFail = false; }
            if (highToleranceEvaluation == -1)
            { sb.Append("High tolerance was not set.\n"); passFail = false; }
            sb.Append("Misuse of Class\nEnsure all public properties are set, before calling BeginPointChecking method.");

            if (!passFail) MessageBox.Show(sb.ToString(), "Class useage Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return passFail;
        }

             #region disposal

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~CircleEvaluationCalculations()
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
    }

}
