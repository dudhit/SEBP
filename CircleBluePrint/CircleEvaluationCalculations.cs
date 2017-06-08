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
    public class CircleEvaluationCalculations
    {
      
        private static System.Collections.Generic.IEnumerable<int> Axis(int radius)
        {
            for (int i = 0; i < radius; i++)
            {
                yield return i;
            }
        }
          }



}
