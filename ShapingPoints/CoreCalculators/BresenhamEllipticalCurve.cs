using System.Collections.Generic;
using System.Windows;

namespace SoloProjects.Dudhit.Utilities
{
  public class BresenhamEllipticalCurve
  {
  
    private long xAxis, yAxis, xChange, yChange, ellipseError, aSquare, bSquare, twoASquare, twoBSquare, stoppingX, stoppingY;
    private int xRadius, yRadius;
    private HashSet<Point> storePoints;

    public BresenhamEllipticalCurve(int xR, int yR)
    {

      if(xR <= 0 || yR <= 0) { throw new System.ArgumentException(string.Format("Each ellipse radius {0} & {1} must be greater than 0", xR, yR), "xRadius"); }
      storePoints = new HashSet<Point>();
      this.xRadius = xR;
      this.yRadius = yR;
      aSquare = xRadius * xRadius;
      bSquare = yRadius * yRadius;
      twoASquare = 2 * aSquare;
      twoBSquare = 2 * bSquare;
    }

    public void BeginCalculations()
    {
      setOne();
      setTwo();
    }

    private void setOne()
    {

      xAxis = xRadius;
      yAxis = 0;
      xChange = bSquare * (1 - (2 * xRadius));
      yChange = aSquare;

      ellipseError = 0;
      stoppingX = twoBSquare * xRadius;
      stoppingY = 0;
      while(stoppingX >= stoppingY)
      {

    
        Point tempPoint = new Point((int)xAxis, (int)yAxis);
        if(!storePoints.Contains(tempPoint))
          storePoints.Add(tempPoint);
        yAxis++;
        stoppingY += twoASquare;
        ellipseError += yChange;
        yChange += twoASquare;

        if((2 * ellipseError + xChange) > 0)
        {
          xAxis--;
          stoppingX -= twoBSquare;
          ellipseError += xChange;
          xChange += twoBSquare;

      }

    
      }
    }

    private void setTwo()
    {
      xAxis = 0;
      yAxis = yRadius;
      xChange = bSquare;
      yChange = aSquare * (1 - (2 * yRadius));

      ellipseError = 0;
      stoppingX = 0;
      stoppingY = twoASquare * yRadius;
      while(stoppingX <= stoppingY)
      {
        Point tempPoint = new Point((int)xAxis, (int)yAxis);
        if(!storePoints.Contains(tempPoint))
          storePoints.Add(tempPoint);
    
        xAxis++;
        stoppingX += twoBSquare;
        ellipseError += xChange;
        xChange += twoBSquare;
        if((2 * ellipseError + yChange) > 0)
        {
          yAxis--;
          stoppingY -= twoASquare;
          ellipseError += yChange;
          yChange += twoASquare;
        }
     }

    }

    public HashSet<Point> GetCurve()
    {
      return this.storePoints;
    }
  }
}
