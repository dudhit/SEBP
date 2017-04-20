using System.Collections.Generic;
using System.Windows;

namespace SoloProjects.Dudhit.Utilities
{
  public class BresenhamCircularCurve
  {

    private long xAxis, yAxis, xChange, yChange, radiusError;
    private HashSet<Point> storePoints;
    public BresenhamCircularCurve(int radius)
    {

      if(radius <= 0)
        throw new System.ArgumentException("Radius must be an integer greater then 0", "radius");
      storePoints = new HashSet<Point>();
      this.xAxis = radius;
      this.yAxis = 0;
      this.xChange = 1 - (2 * radius);
      this.yChange = 1;
      this.radiusError = 0;
      
    }

    public void BeginCalculations()
    {
      makeQuarter();
    }
    private void makeQuarter()
    {
      while(xAxis >= yAxis)
      {
        Point tempPoint = new Point((int)xAxis, (int)yAxis);
        if(!storePoints.Contains(tempPoint))
        {
          storePoints.Add(tempPoint);
          tempPoint = new Point((int)yAxis, (int)xAxis);
          storePoints.Add(tempPoint);
        }
        yAxis++;
        radiusError += yChange;
        yChange += 2;
        if(2 * radiusError + xChange > 0)
        {
          xAxis--;
          radiusError += xChange;
          xChange += 2;

        }
      }
    }

    public HashSet<Point> GetCurve()
    {
      return this.storePoints;
    }


  }
}
