using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Media3D;

namespace SoloProjects.Dudhit.Utilities
{
  public class PointsToShape : IDisposable
  {
    private int radiusX;
    private int radiusY;
    private int radiusZ;
    private int shape;
    public HashSet<Point3D> GlobalCurveSet;
    /*  
    quartercircle 17 semicircle 33 fullcircle 65  x
    quarterellipse 18 semiellipse 34 fullellipse 66 xy
    quartersphere 20 semisphere 36 fullsphere 68 x
    quarterellipsiod 24 semiellipsiod 40 fullellipsiod 72 xyz

   17=  one run of BresenhamCircularCurve 
      33 =17 + 17 with x*-1
      65 =33 + 17 with y*-1 + 17 with x*-1 & y*-1
       
     18=one run of BresenhamEllipticalCurve
      34 =18 +18 with x*-1
      66 =33 + 18 with y*-1 + 18 with x*-1 & y*-1
      
     20 = xz and yz run of 17 then loop xy generation using xz yz per z 
     
     */
    public PointsToShape(int myX, int myY, int myZ, int shape)
    {
      GlobalCurveSet = new HashSet<Point3D>();
      this.radiusX=myX;
      this.radiusY=myY;
      this.radiusZ=myZ;
      this.shape=shape;
      DetermineProcessingPaths();
    }

    private bool DetermineProcessingPaths()
    {
      switch(this.shape)
      {//  quartercircle 17 x
        case 17:
          {
            if(LineAlongPlaneGenerator(0, radiusX, "x", 0, 0)&&LineAlongPlaneGenerator(0, radiusX, "y", 0, 0))
            {
              BresenhamCircularCurve pppQc= new BresenhamCircularCurve(radiusX);
              pppQc.BeginCalculations();
              AddSetToGlobalSet(TwoDIntoThreeDPoint(pppQc.GetCurve(), "xy", 0, 1, 1, 1));
              pppQc=null;
            }
            break;
          }//  semicircle 33    x
        case 33:
          {
            if(LineAlongPlaneGenerator(-1*radiusX, radiusX, "x", 0, 0)&&LineAlongPlaneGenerator(0, radiusX, "y", 0, 0))
            {
              BresenhamCircularCurve pppQc= new BresenhamCircularCurve(radiusX);
              pppQc.BeginCalculations();
              AddSetToGlobalSet(TwoDIntoThreeDPoint(pppQc.GetCurve(), "xy", 0, 1, 1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(pppQc.GetCurve(), "xy", 0, -1, 1, 1));
              pppQc=null;
            }
            break;
          }// fullcircle 65  x
        case 65:
          {
            if(LineAlongPlaneGenerator(-1*radiusX, radiusX, "x", 0, 0)&&LineAlongPlaneGenerator(-1*radiusX, radiusX, "y", 0, 0))
            {
              BresenhamCircularCurve pppQc= new BresenhamCircularCurve(radiusX);
              pppQc.BeginCalculations();
              AddSetToGlobalSet(TwoDIntoThreeDPoint(pppQc.GetCurve(), "xy", 0, 1, 1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(pppQc.GetCurve(), "xy", 0, -1, 1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(pppQc.GetCurve(), "xy", 0, 1, -1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(pppQc.GetCurve(), "xy", 0, -1, -1, 1));
              pppQc=null;
            }
            break;
          }//  quarterellipse 18  xy
        case 18:
          {
            if(LineAlongPlaneGenerator(0, radiusX, "x", 0, 0)&& LineAlongPlaneGenerator(0, radiusY, "y", 0, 0))
            {
              BresenhamEllipticalCurve pppQe = new BresenhamEllipticalCurve(radiusX, radiusY);
              pppQe.BeginCalculations();
              AddSetToGlobalSet(TwoDIntoThreeDPoint(pppQe.GetCurve(), "xy", 0, 1, 1, 1));
              pppQe=null;
            }
            break;
          }//  semiellipse 34  xy
        case 34:
          {
            if(LineAlongPlaneGenerator(-1*radiusX, radiusX, "x", 0, 0)&&LineAlongPlaneGenerator(0, radiusY, "y", 0, 0))
            {
              BresenhamEllipticalCurve pppQe = new BresenhamEllipticalCurve(radiusX, radiusY);
              pppQe.BeginCalculations();
              AddSetToGlobalSet(TwoDIntoThreeDPoint(pppQe.GetCurve(), "xy", 0, 1, 1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(pppQe.GetCurve(), "xy", 0, -1, 1, 1));
              pppQe=null;
            }
            break;
          }//   fullellipse 66 xy
        case 66:
          {
            if(LineAlongPlaneGenerator(-1*radiusX, radiusX, "x", 0, 0)&&LineAlongPlaneGenerator(-1*radiusY, radiusY, "y", 0, 0))
            {
              BresenhamEllipticalCurve pppQe = new BresenhamEllipticalCurve(radiusX, radiusY);
              pppQe.BeginCalculations();
              AddSetToGlobalSet(TwoDIntoThreeDPoint(pppQe.GetCurve(), "xy", 0, 1, 1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(pppQe.GetCurve(), "xy", 0, -1, 1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(pppQe.GetCurve(), "xy", 0, 1, -1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(pppQe.GetCurve(), "xy", 0, -1, -1, 1));
              pppQe=null;
            }
            break;
          }//   quartersphere 20 x
        case 20:
          {
            if(LineAlongPlaneGenerator(0, radiusX, "x", 0, 0)&&LineAlongPlaneGenerator(0, radiusX, "y", 0, 0)&&LineAlongPlaneGenerator(0, radiusX, "z", 0, 0))
            {
              BresenhamCircularCurve xz= new BresenhamCircularCurve(radiusX);
              xz.BeginCalculations();
              HashSet<Point> xzCurve = xz.GetCurve();
              AddSetToGlobalSet(TwoDIntoThreeDPoint(xzCurve, "xz", 0, 1, 1, 1));
              foreach(Point p in xzCurve)
              {
                if(p.X>0)
                {
                  BresenhamCircularCurve xy= new BresenhamCircularCurve((int)p.X);
                  xy.BeginCalculations();
                  AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xy", (int)p.Y, 1, 1, 1));
                }
              }
              xz=null;
            }
            break;
          }
        // semisphere 36  x
        case 36:
          {
            if(LineAlongPlaneGenerator(-1*radiusX, radiusX, "x", 0, 0)&&LineAlongPlaneGenerator(-1*radiusX, radiusX, "y", 0, 0)&&LineAlongPlaneGenerator(0, radiusX, "z", 0, 0))
            {
              BresenhamCircularCurve xz= new BresenhamCircularCurve(radiusX);
              xz.BeginCalculations();
              HashSet<Point> xzCurve = xz.GetCurve();
              AddSetToGlobalSet(TwoDIntoThreeDPoint(xzCurve, "xz", 0, 1, 1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(xzCurve, "xz", 0, -1, 1, 1));

              AddSetToGlobalSet(TwoDIntoThreeDPoint(xzCurve, "xy", 0, 1, 1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(xzCurve, "xy", 0, -1, 1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(xzCurve, "xy", 0, 1, -1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(xzCurve, "xy", 0, -1, -1, 1));

              foreach(Point p in xzCurve)
              {
                if(p.X>0)
                {
                  BresenhamCircularCurve xy= new BresenhamCircularCurve((int)p.X);
                  xy.BeginCalculations();
                  //LineAlongPlaneGenerator(0, (int)p.X, "z", (int)p.Y,0);
                  //LineAlongPlaneGenerator(0, (int)p.X, "y", 0, (int)p.Y);
                  AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xy", (int)p.Y, 1, 1, 1));
                  AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xy", (int)p.Y, -1, 1, 1));
                  AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xy", (int)p.Y, 1, -1, 1));
                  AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xy", (int)p.Y, -1, -1, 1));
                }

              }
              xz=null;
            }
            break;
          }
        //  fullsphere 68 x
        case 68:
          {
            if(LineAlongPlaneGenerator(-1*radiusX, radiusX, "x", 0, 0)&&LineAlongPlaneGenerator(-1*radiusX, radiusX, "y", 0, 0)&&LineAlongPlaneGenerator(-1*radiusX, radiusX, "z", 0, 0))
            {
              BresenhamCircularCurve xz= new BresenhamCircularCurve(radiusX);
              xz.BeginCalculations();
              HashSet<Point> xzCurve = xz.GetCurve();
              AddSetToGlobalSet(TwoDIntoThreeDPoint(xzCurve, "xz", 0, 1, 1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(xzCurve, "yz", 0, 1, 1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(xzCurve, "xy", 0, 1, 1, 1));

              foreach(Point p in xzCurve)
              {
                if(p.X>0)
                {
                  BresenhamCircularCurve xy= new BresenhamCircularCurve((int)p.X);
                  xy.BeginCalculations();
                  //todo play with planes
                  AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xy", (int)p.Y, 1, 1, 1));
                  AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xy", (int)p.Y, -1, 1, 1));
                  AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xy", (int)p.Y, 1, -1, 1));
                  AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xy", (int)p.Y, -1, -1, 1));
                  AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xy", (int)p.Y, 1, 1, -1));
                  AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xy", (int)p.Y, -1, 1, -1));
                  AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xy", (int)p.Y, 1, -1, -1));
                  AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xy", (int)p.Y, -1, -1, -1));
                }
              }
              xz=null;

            }
            break;
          }
        //   quarterellipsiod 24  xyz
        case 24:
          {
            if(LineAlongPlaneGenerator(0, radiusX, "x", 0, 0)&&LineAlongPlaneGenerator(0, radiusY, "y", 0, 0)&&LineAlongPlaneGenerator(0, radiusZ, "z", 0, 0))
            {
              BresenhamEllipticalCurve xz = new BresenhamEllipticalCurve(radiusX, radiusZ);
              xz.BeginCalculations();
              BresenhamEllipticalCurve yz = new BresenhamEllipticalCurve(radiusY, radiusZ);
              yz.BeginCalculations();
              BresenhamEllipticalCurve xy = new BresenhamEllipticalCurve(radiusX, radiusY);
              xy.BeginCalculations();
              AddSetToGlobalSet(TwoDIntoThreeDPoint(xz.GetCurve(), "xz", 0, 1, 1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", 0, 1, 1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xy", 0, 1, 1, 1));
              //generate yz curve along X using xy and xz curve points            


              int xx= radiusX;
              int yy=radiusY;
              int zz=radiusZ;
              while(xx>0)
              {
                while(yy>0)
                {
                  while(zz>0)
                  {
                    if(xy.GetCurve().Contains(new Point(xx, yy))&&xz.GetCurve().Contains(new Point(xx, zz)))
                    {
                      yz =new BresenhamEllipticalCurve(yy, zz);
                      yz.BeginCalculations();
                      AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", xx, 1, 1, 1));
                    }
                    zz--;
                  }
                  yy--;
                  zz=radiusZ;
                }
                xx--;
                yy=radiusY;
              }
              xy=null;
              yz=null;
              xz=null;
            }
            break;
          }
        //  semiellipsiod 40  xyz
        case 40:
          {
            if(LineAlongPlaneGenerator(-1*radiusX, radiusX, "x", 0, 0)&&LineAlongPlaneGenerator(-1*radiusY, radiusY, "y", 0, 0)&&LineAlongPlaneGenerator(0, radiusZ, "z", 0, 0))
            {    
              BresenhamEllipticalCurve xz = new BresenhamEllipticalCurve(radiusX, radiusZ);
              xz.BeginCalculations();
              BresenhamEllipticalCurve yz = new BresenhamEllipticalCurve(radiusY, radiusZ);
              yz.BeginCalculations();
              BresenhamEllipticalCurve xy = new BresenhamEllipticalCurve(radiusX, radiusY);
              xy.BeginCalculations();
              AddSetToGlobalSet(TwoDIntoThreeDPoint(xz.GetCurve(), "xz", 0, 1, 1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", 0, 1, 1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", 0, 1, -1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xy", 0, 1, 1, 1));
              //generate yz curve along X using xy and xz curve points            


              int xx= radiusX;
              int yy=radiusY;
              int zz=radiusZ;
              while(xx>0)
              {
                while(yy>0)
                {
                  while(zz>0)
                  {
                    if(xy.GetCurve().Contains(new Point(xx, yy))&&xz.GetCurve().Contains(new Point(xx, zz)))
                    {
                      yz =new BresenhamEllipticalCurve(yy, zz);
                      yz.BeginCalculations();
                      AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", xx, 1, 1, 1));
                      AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", xx, -1, 1, 1));
                      AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", xx, 1, -1, 1));
                      AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", xx, -1, -1, 1));
                    }
                    zz--;
                  }
                  yy--;
                  zz=radiusZ;
                }
                xx--;
                yy=radiusY;
              }
              xy=null;
              yz=null;
              xz=null;
            
            }
            break;
          }
        //   fullellipsiod 72 xyz
        case 72:
          {
            if(LineAlongPlaneGenerator(-1*radiusX, radiusX, "x", 0, 0)&&LineAlongPlaneGenerator(-1*radiusY, radiusY, "y", 0, 0)&&LineAlongPlaneGenerator(-1*radiusZ, radiusZ, "z", 0, 0))
            {
              BresenhamEllipticalCurve xz = new BresenhamEllipticalCurve(radiusX, radiusZ);
              xz.BeginCalculations();
              BresenhamEllipticalCurve yz = new BresenhamEllipticalCurve(radiusY, radiusZ);
              yz.BeginCalculations();
              BresenhamEllipticalCurve xy = new BresenhamEllipticalCurve(radiusX, radiusY);
              xy.BeginCalculations();
              AddSetToGlobalSet(TwoDIntoThreeDPoint(xz.GetCurve(), "xz", 0, 1, 1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", 0, 1, 1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", 0, 1, -1, 1));
              AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xy", 0, 1, 1, 1));
              //generate yz curve along X using xy and xz curve points            


              int xx= radiusX;
              int yy=radiusY;
              int zz=radiusZ;
              while(xx>0)
              {
                while(yy>0)
                {
                  while(zz>0)
                  {
                    if(xy.GetCurve().Contains(new Point(xx, yy))&&xz.GetCurve().Contains(new Point(xx, zz)))
                    {
                      yz =new BresenhamEllipticalCurve(yy, zz);
                      yz.BeginCalculations();
                      AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", xx, 1, 1, 1));
                      AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", xx, -1, 1, 1));
                      AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", xx, 1, -1, 1));
                      AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", xx, -1, -1, 1));
                      AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", xx, 1, 1, -1));
                      AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", xx, -1, 1, -1));
                      AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", xx, 1, -1, -1));
                      AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", xx, -1, -1, -1));
                    }
                    zz--;
                  }
                  yy--;
                  zz=radiusZ;
                }
                xx--;
                yy=radiusY;
              }
              xy=null;
              yz=null;
              xz=null;

            }
            break;
          }

      }
      return false;

    }

    private void AddSetToGlobalSet(HashSet<Point3D> anotherTempSet)
    {
      foreach(Point3D ptd in anotherTempSet)
      {
        AddNewPointToGlobalSet(ptd);
      }
    }

    /// <summary>
    /// takes a collection of 2d points and converts them to 3d points by specifing a new coordinate plane, a level within that plane and  
    /// </summary>
    /// <param name="twoDPointCollection"></param>
    /// <param name="plane"></param>
    /// <param name="fixedPlaneValue"></param>
    /// <param name="xSign"></param>
    /// <param name="ySign"></param>
    /// <param name="zSign"></param>
    /// <returns></returns>
    private HashSet<Point3D> TwoDIntoThreeDPoint(HashSet<Point> twoDPointCollection, string plane, int fixedPlaneValue, int xSign, int ySign, int zSign)
    {
      //if(xSign!=1||xSign!=-1||ySign!=1||ySign!=-1||zSign!=1||zSign!=-1)
      //  return null;
      HashSet<Point3D> temporaryCollection = new HashSet<Point3D>();
      switch(plane)
      {
        case "xy":
          {
            foreach(Point twoDPoint in twoDPointCollection)
            {
              temporaryCollection.Add(new Point3D(twoDPoint.X*xSign, twoDPoint.Y*ySign, fixedPlaneValue*zSign));
            }
            break;
          }
        case "xz":
          {
            foreach(Point twoDPoint in twoDPointCollection)
            {
              temporaryCollection.Add(new Point3D(twoDPoint.X*xSign, fixedPlaneValue*ySign, twoDPoint.Y*zSign));
            }
            break;
          }
        case "yz":
          {
            foreach(Point twoDPoint in twoDPointCollection)
            {
              temporaryCollection.Add(new Point3D(fixedPlaneValue*xSign, twoDPoint.X*ySign, twoDPoint.Y*zSign));
            }
            break;
          }
      }
      return temporaryCollection;
    }

    private void AddNewPointToGlobalSet(Point3D summonThirdDimension)
    {
      if(!GlobalCurveSet.Contains(summonThirdDimension))
      { GlobalCurveSet.Add(summonThirdDimension); }

    }

    private bool LineAlongPlaneGenerator(int start, int end, string axis, int yx, int zy)
    {
      if(start<=0&&end>0&&axis!=string.Empty)
      {
        switch(axis)
        {
          case "x":
            for(int i=start;i<end;i++)
            {
              AddNewPointToGlobalSet(new Point3D(i, yx, zy));
            }
            break;
          case "y":
            for(int i=start;i<end;i++)
            {
              AddNewPointToGlobalSet(new Point3D(yx, i, zy));
            }
            break;
          case "z":
            for(int i=start;i<end;i++)
            {
              AddNewPointToGlobalSet(new Point3D(yx, zy, i));
            }
            break;
        }
        return true;
      }
      return false;
    }

    #region disposal

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    ~PointsToShape()
    {
      Dispose(false);
    }

    protected virtual void Dispose(bool disposing)
    {
      if(disposing)
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
