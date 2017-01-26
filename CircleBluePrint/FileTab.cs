using SoloProjects.Dudhit.SpaceEngineers.SEBP.Collection;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;
using System.Windows;
using System.Windows.Documents;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
    public partial class MainWindow : Window, IDisposable
    {

    
  
    }

    public class WorkingArgs
    {
        public double xRadius;
        public double yRadius;
        public double zRadius;
        public string shapeSelected;

        public WorkingArgs(double x, double y, double z,  string shape)
        {
            this.xRadius = x;
            this.yRadius = y;
            this.zRadius = z;
            this.shapeSelected = shape;
        }
    }
}
