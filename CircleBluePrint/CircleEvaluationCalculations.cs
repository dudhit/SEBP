using SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint
{
    class CircleEvaluationCalculations
    {

        private int xIntRadius;
        private int yIntRadius;
        private int zIntRadius;
        private double xRadius;
        private double yRadius;
        private double zRadius;
        private string shapeSelected;
        private double lowToleranceEvaluation;
        private double highToleranceEvaluation;
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
            // for (int y = 0; y <= yLoop; y++)
            Parallel.ForEach(Axis(yLoop), y =>
            {
                //for (int x = 0; x < xLoop; x++)
                Parallel.ForEach(Axis(xLoop), x =>
              {
                  tempPoint = new Point3D(x, y, 0);
                  SolidOrFrame(EvalPoint3D(tempPoint), tempPoint);
                  string result = string.Format("x:{0}y:{1}z:0", x, y);
                  System.Diagnostics.Trace.WriteLine(result);
              });
            }
        );

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
                     SolidOrFrame(EvalPoint3D(tempPoint), tempPoint);
                     string result = string.Format("x:{0}y:{1}z:{1}", x, y, z);
                     System.Diagnostics.Trace.WriteLine(result);
                 });
               });
            });

        }

        public static System.Collections.Generic.IEnumerable<int> Axis(int radius)
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
        //     Calculates whether values x,y,z fit within the bounds of a circle/elipse/sphere(oid)
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

        private void SolidOrFrame(double result, Point3D p)
        {


            //      if (makeSolid.IsChecked == true && result <= highTol)
            //      {
            //          DoShapePlotting(p);
            //      }
            //if (makeFrame.IsChecked == true && result >= lowTol && result <= highTol)
            if (result >= lowToleranceEvaluation && result <= highToleranceEvaluation)
            {
                DoShapePlotting(p, shapeSelected);
            }
        }
        private void DoShapePlotting(Point3D p, string shapeChoice)
        {
            switch (shapeChoice)
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



    }
}
