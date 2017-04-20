using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloProjects.Dudhit.Utilities.ShapingPoints
{
  class CurvesToEllipsoid
  {
    /*
     
        private int x, y, z;
        private HashSet<Point3D> yzCurve;
        private HashSet<Point3D> xzCurve;
        private HashSet<Point3D> xyCurve;
        public ProcesEllipse(int xr, int yr, int zr)
        {
            this.x = xr;
            this.y = yr;
            this.z = zr;

            yzCurve = new HashSet<Point3D>();
            xzCurve = new HashSet<Point3D>();
            xyCurve = new HashSet<Point3D>();
            PopulateCurves();
        }

        private void PopulateCurves()
        {
            //get 3 curves xy , xz , yz using 2dellipse class
            TwoDimensionalEllipse twoD;
            List<Point> tempList;
            //do yz curve

          
                twoD = new TwoDimensionalEllipse(y, z);
                twoD.BeginCalculations();
                tempList = twoD.GetCurve();
                foreach (Point p in tempList)
                {
                    Point3D yzPoint = new Point3D(0, p.X, p.Y);
                    if (!yzCurve.Contains(yzPoint)) yzCurve.Add(yzPoint);
                }
                twoD = null;
                tempList = null;
            //do xz curve
                twoD = new TwoDimensionalEllipse(x, z);
                twoD.BeginCalculations();
                tempList = twoD.GetCurve();
                foreach (Point p in tempList)
                {
                    Point3D xzPoint = new Point3D(p.X, 0, p.Y);
                    if (!xzCurve.Contains(xzPoint)) xzCurve.Add(xzPoint);
                }
                twoD = null;
                tempList = null;
            //do FIRST xy curve
                twoD = new TwoDimensionalEllipse(x, y);
                twoD.BeginCalculations();
                tempList = twoD.GetCurve();
                foreach (Point p in tempList)
                {
                    Point3D xyPoint = new Point3D(p.X, p.Y, 0);
                    if (!xyCurve.Contains(xyPoint)) xyCurve.Add(xyPoint);
                }
                twoD = null;
                tempList = null;
          


            //finally use max xAxis Zn and max yAxis Zn from xz and yz curves to make xy curves along z
                int xx = x;
                int yy = y;



                for (int zz = 0; zz <= z; zz++)
                {

                    Point3D xzPoint = new Point3D(xx, 0, zz);
                    Point3D yzPoint = new Point3D(0, yy, zz);
                    int numInXPlane = PointsAlongPlane(xzCurve, xzPoint, "-X");
                    int numInYPlane = PointsAlongPlane(yzCurve, yzPoint, "-Y");

                    bool xEnd = false;
                    bool yEnd = false;


                    while (!xEnd && !yEnd)
                    {
                        //if points exist
                        if (xzCurve.Contains(xzPoint) && yzCurve.Contains(yzPoint))
                        {
                            //assign to radius varibales MAKING SURE NO ZERO VALUES

                            //do next xy curve
                            try
                            {
                                twoD = new TwoDimensionalEllipse((int)xzPoint.X, (int)yzPoint.Y);
                                twoD.BeginCalculations();
                                tempList = twoD.GetCurve();
                                foreach (Point p in tempList)
                                {
                                    Point3D xyPoint = new Point3D(p.X, p.Y, zz);
                                    if (!xyCurve.Contains(xyPoint)) xyCurve.Add(xyPoint);
                                }
                                twoD = null;
                                tempList = null;

                            }
                            catch (ArgumentException nre) {System.Console.WriteLine(nre); }
                        }


                        if (numInXPlane > 0 && !xEnd&&xzPoint.X>1) { xzPoint.X--; numInXPlane--; } else { xEnd = true; }
                        if (numInYPlane > 0 && !yEnd && yzPoint.Y > 1) { yzPoint.Y--; numInYPlane--; } else { yEnd = true; }
                    }
                    if (xzPoint.X != xx && xzPoint.X != 0) xx = (int)xzPoint.X; if (yzPoint.Y != yy && yzPoint.Y != 0) yy = (int)yzPoint.Y;
                    if (PointsAlongPlane(xzCurve, xzPoint, "Z") == 0 && xx > 1) { xx--; }
                    if (PointsAlongPlane(yzCurve, yzPoint, "Z") == 0 && yy > 1) { yy--; }

                }

   

        }

        private int PointsAlongPlane(HashSet<Point3D> curve, Point3D point, string axis)
        {
            int count = 0;
            switch (axis)
            {
                case "-X":
                    while (curve.Contains(point))
                    {
                        //decrement x
                        point.X--;
                        count++;
                    }
                    break;
                case "-Y":
                    while (curve.Contains(point))
                    {
                        //decrement x
                        point.Y--;
                        count++;
                    }
                    break;
                case "-Z":
                    while (curve.Contains(point))
                    {
                        //decrement x
                        point.Z--;
                        count++;
                    }
                    break;
                case "X":
                    while (curve.Contains(point))
                    {
                        //decrement x
                        point.X++;
                        count++;
                    }
                    break;
                case "Y":
                    while (curve.Contains(point))
                    {
                        //decrement x
                        point.Y++;
                        count++;
                    }
                    break;
                case "Z":
                    while (curve.Contains(point))
                    {
                        //decrement x
                        point.Z++;
                        count++;
                    }
                    break;
                default:
                    System.Diagnostics.Trace.WriteLine("LOOP FAILING");
                    break;

            }
            return count - 1;
        }

    
     */

  }
}
