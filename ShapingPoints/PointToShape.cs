using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    private bool solid;
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="myX"></param>
    /// <param name="myY"></param>
    /// <param name="myZ"></param>
    /// <param name="shape"></param>
    /// <param name="mass"></param>
    public PointsToShape(int myX, int myY, int myZ, int shape, bool mass)
    {
      GlobalCurveSet = new HashSet<Point3D>();
      this.radiusX=myX;
      this.radiusY=myY;
      this.radiusZ=myZ;
      this.shape=shape;
      this.solid=mass;
      DetermineProcessingPaths();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private bool DetermineProcessingPaths()
    {
      switch(this.shape)
      {//  quartercircle 17 x
        case 17:
          {
            MakeQuarterCircle();
            break;
          }//  semicircle 33    x
        case 33:
          {
            if(LineAlongPlaneGenerator(-1*radiusX, radiusX, "x", 0, 0))
            {
              MakeQuarterCircle();
              MirrorGlobalSetByAxis("x");
            }
            break;
          }// fullcircle 65  x
        case 65:
          {
            if(LineAlongPlaneGenerator(-1*radiusX, radiusX, "y", 0, 0))
            {
              MakeQuarterCircle();
              MirrorGlobalSetByAxis("x");
              MirrorGlobalSetByAxis("y");
            }
            break;
          }//  quarterellipse 18  xy
        case 18:
          {
            MakeQuarterEllipse();
            break;
          }//  semiellipse 34  xy
        case 34:
          {
            if(LineAlongPlaneGenerator(-1*radiusX, radiusX, "x", 0, 0))
            {
              MakeQuarterEllipse();
              MirrorGlobalSetByAxis("x");
            }
            break;
          }//   fullellipse 66 xy
        case 66:
          {
            if(LineAlongPlaneGenerator(-1*radiusY, radiusY, "y", 0, 0))
            {
              MakeQuarterEllipse();
              MirrorGlobalSetByAxis("x");
              MirrorGlobalSetByAxis("y");
            }
            break;
          }//   quartersphere 20 x
        case 20:
          {
            MakeQuarterSphere();
            break;
          }
        // semisphere 36  x
        case 36:
          {
            if(LineAlongPlaneGenerator(-1*radiusX, radiusX, "x", 0, 0)&&LineAlongPlaneGenerator(-1*radiusX, radiusX, "y", 0, 0))
            {
              MakeQuarterSphere();
              MirrorGlobalSetByAxis("x");
              MirrorGlobalSetByAxis("y");
            }
            break;
          }
        //  fullsphere 68 x
        case 68:
          {
            if(LineAlongPlaneGenerator(-1*radiusX, radiusX, "z", 0, 0))
            {
              MakeQuarterSphere();
              MirrorGlobalSetByAxis("x");
              MirrorGlobalSetByAxis("y");
              MirrorGlobalSetByAxis("z");
            }
            break;
          }
        //   quarterellipsiod 24  xyz
        case 24:
          {
            MakeQuaterEllipsoid();
            break;
          }
        //  semiellipsiod 40  xyz
        case 40:
          {
            if(LineAlongPlaneGenerator(-1*radiusX, radiusX, "x", 0, 0)&&LineAlongPlaneGenerator(-1*radiusY, radiusY, "y", 0, 0))
            {
              MakeQuaterEllipsoid();
              MirrorGlobalSetByAxis("x");
              MirrorGlobalSetByAxis("y");
            }
            break;
          }
        //   fullellipsiod 72 xyz
        case 72:
          {
            if(LineAlongPlaneGenerator(-1*radiusZ, radiusZ, "z", 0, 0))
            {
              MakeQuaterEllipsoid();
              MirrorGlobalSetByAxis("x");
              MirrorGlobalSetByAxis("y");
              MirrorGlobalSetByAxis("z");
            }
            break;
          }

      }
      return false;

    }
    /// <summary>
    /// 
    /// </summary>
    private void MakeQuaterEllipsoid()
    {
      if(LineAlongPlaneGenerator(0, radiusX, "x", 0, 0)&&LineAlongPlaneGenerator(0, radiusY, "y", 0, 0)&&LineAlongPlaneGenerator(0, radiusZ, "z", 0, 0))
      {
        BresenhamEllipticalCurve xz = new BresenhamEllipticalCurve(radiusX, radiusZ);
        xz.BeginCalculations();
        BresenhamEllipticalCurve yz = new BresenhamEllipticalCurve(radiusY, radiusZ);
        yz.BeginCalculations();
        BresenhamEllipticalCurve xy = new BresenhamEllipticalCurve(radiusX, radiusY);
        xy.BeginCalculations();
        AddSetToGlobalSet(TwoDIntoThreeDPoint(xz.GetCurve(), "xz", 0));
        AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", 0));
        AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xy", 0));
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
                AddSetToGlobalSet(TwoDIntoThreeDPoint(yz.GetCurve(), "yz", xx));
                if(solid)
                  Parallel.ForEach(yz.GetCurve(), solidP =>
                    {
                      LineAlongPlaneGenerator(0, xx, "x", (int)solidP.X, (int)solidP.Y);
                    });
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
    }
    /// <summary>
    /// 
    /// </summary>
    private void MakeQuarterSphere()
    {
      if(LineAlongPlaneGenerator(0, radiusX, "x", 0, 0)&&LineAlongPlaneGenerator(0, radiusX, "y", 0, 0)&&LineAlongPlaneGenerator(0, radiusX, "z", 0, 0))
      {
        BresenhamCircularCurve xy= new BresenhamCircularCurve(radiusX);
        xy.BeginCalculations();
        AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xy", 0));
        AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "xz", 0));
        AddSetToGlobalSet(TwoDIntoThreeDPoint(xy.GetCurve(), "yz", 0));
        if(solid)
        {
          Parallel.ForEach(xy.GetCurve(), p =>
           {
             LineAlongPlaneGenerator(0, (int)p.Y, "x", (int)p.X, 0);
             LineAlongPlaneGenerator(0, (int)p.Y, "z", (int)p.X, 0);
             LineAlongPlaneGenerator(0, (int)p.Y, "y", 0, (int)p.X);
           });
        }
        Parallel.ForEach(xy.GetCurve(), p =>
 {
   if(p.X>0)
   {
     BresenhamCircularCurve xz= new BresenhamCircularCurve((int)p.X);
     xz.BeginCalculations();
     AddSetToGlobalSet(TwoDIntoThreeDPoint(xz.GetCurve(), "xz", (int)p.Y));
     if(solid)
     {
       Parallel.ForEach(xz.GetCurve(), solidP =>
              {
                LineAlongPlaneGenerator(0, (int)solidP.X, "x", (int)p.Y, (int)solidP.Y);
              });
     }
   }
 }
 );
        xy=null;
      }
    }
    /// <summary>
    /// 
    /// </summary>
    private void MakeQuarterEllipse()
    {
      if(LineAlongPlaneGenerator(0, radiusX, "x", 0, 0)&& LineAlongPlaneGenerator(0, radiusY, "y", 0, 0))
      {
        BresenhamEllipticalCurve xyCurve = new BresenhamEllipticalCurve(radiusX, radiusY);
        xyCurve.BeginCalculations();
        AddSetToGlobalSet(TwoDIntoThreeDPoint(xyCurve.GetCurve(), "xy", 0));
        if(solid)
        {
          Parallel.ForEach(xyCurve.GetCurve(), p =>
           {
             LineAlongPlaneGenerator(0, (int)p.X, "x", (int)p.Y, 0);
           });
        }
        xyCurve=null;
      }
    }
    /// <summary>
    /// 
    /// </summary>
    private void MakeQuarterCircle()
    {
      if(LineAlongPlaneGenerator(0, radiusX, "x", 0, 0)&&LineAlongPlaneGenerator(0, radiusX, "y", 0, 0))
      {
        BresenhamCircularCurve xyCurve= new BresenhamCircularCurve(radiusX);
        xyCurve.BeginCalculations();
        AddSetToGlobalSet(TwoDIntoThreeDPoint(xyCurve.GetCurve(), "xy", 0));
        if(solid)
        {
          Parallel.ForEach(xyCurve.GetCurve(), p =>
             {
               LineAlongPlaneGenerator(0, (int)p.X, "x", (int)p.Y, 0);
             });
        }
        xyCurve=null;
      }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="axis"></param>
    private void MirrorGlobalSetByAxis(string axis)
    {
      Object sillyLock = new Object();
      HashSet<Point3D> tempSet= new HashSet<Point3D>();
      switch(axis)
      {
        case "x":
          {
            Parallel.ForEach(GlobalCurveSet, ppp =>
            {
              lock (sillyLock)
              { 
                tempSet.Add(new Point3D(-1*ppp.X, ppp.Y, ppp.Z));
              }
            }
            );

            break;
          }
        case "y":
          {
            Parallel.ForEach(GlobalCurveSet, ppp =>
            {
              lock (sillyLock) 
              { 
                tempSet.Add(new Point3D(ppp.X, -1*ppp.Y, ppp.Z));
              }
            }
              );
            break;
          }
        case "z":
          {
            Parallel.ForEach(GlobalCurveSet, ppp =>
            {
              lock(sillyLock)
              {
                tempSet.Add(new Point3D(ppp.X, ppp.Y, -1*ppp.Z));
              }
            }
            );
            break;
          }
      }
      AddSetToGlobalSet(tempSet);
      tempSet=null;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="anotherTempSet"></param>
    private void AddSetToGlobalSet(HashSet<Point3D> anotherTempSet)
    {
      Object sillyLock =new Object();
      Parallel.ForEach(anotherTempSet, ptd =>
       //foreach(Point3D ptd in anotherTempSet)
      {
        lock(sillyLock)
        {
          AddNewPointToGlobalSet(ptd);
        }
       });
    }

    /// <summary>
    /// takes a collection of 2d points and converts them to 3d points by specifing a new coordinate plane, a level within that plane and  
    /// </summary>
    /// <param name="twoDPointCollection"></param>
    /// <param name="plane"></param>
    /// <param name="fixedPlaneValue"></param>
    /// <returns></returns>
    private  HashSet<Point3D> TwoDIntoThreeDPoint(HashSet<Point> twoDPointCollection, string plane, int fixedPlaneValue)
    {

      Object sillyLock = new Object();
      HashSet<Point3D> temporaryCollection = new HashSet<Point3D>();
      switch(plane)
      {
        case "xy":
          {
            Parallel.ForEach(twoDPointCollection, twoDPoint =>
          // foreach(Point twoDPoint in twoDPointCollection)
            {
              lock(sillyLock)
              {
                temporaryCollection.Add(new Point3D(twoDPoint.X, twoDPoint.Y, fixedPlaneValue));
              }
          });
            break;
          }
        case "xz":
          {
            Parallel.ForEach(twoDPointCollection, twoDPoint =>
             //  foreach(Point twoDPoint in twoDPointCollection)
            {
              lock(sillyLock)
              { 
                temporaryCollection.Add(new Point3D(twoDPoint.X, fixedPlaneValue, twoDPoint.Y));
              }
             });
            break;
          }
        case "yz":
          {
            Parallel.ForEach(twoDPointCollection, twoDPoint =>
              //   foreach(Point twoDPoint in twoDPointCollection)
            {
              lock(sillyLock)
              {
                temporaryCollection.Add(new Point3D(fixedPlaneValue, twoDPoint.X, twoDPoint.Y));
              }
              });
            break;
          }
      }
      return temporaryCollection;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="summonThirdDimension"></param>
    private void AddNewPointToGlobalSet(Point3D summonThirdDimension)
    {
      if(!GlobalCurveSet.Contains(summonThirdDimension))
      { GlobalCurveSet.Add(summonThirdDimension); }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="axis"></param>
    /// <param name="yx"></param>
    /// <param name="zy"></param>
    /// <returns></returns>
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
