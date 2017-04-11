using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloProjects.Dudhit.Utilities
{
    public class PointsToShape:IDisposable
    {
     /*  
     quartercircle 17 semicircle 33 fullcircle 65
     quarterellipse 18 semiellipse 34 fullellipse 66
     quartersphere 20 semisphere 36 fullsphere 68
     quarterellipsiod 24 semiellipsiod 40 fullellipsiod 72
       */
      /*
     17=  one run of BresenhamCircularCurve 
       * 33 =17 + 17 with x*-1
       * 65 =33 + 17 with y*-1 + 17 with x*-1 & y*-1
       
       *18=one run of BresenhamEllipticalCurve
         * 34 =18 +18 with x*-1
       * 66 =33 + 18 with y*-1 + 18 with x*-1 & y*-1
       */
      public PointsToShape()
      { }
 
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
