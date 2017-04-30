using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.BlueprintWriterLib
{
    //used to prevent encoding attributes being generated in XML file
  public class EncodingStringWriter : StringWriter, IDisposable
  {
    bool disposed = false;
    public override Encoding Encoding
    {
      get
      {
        return null;
      }
    }
    #region disposal

    ~EncodingStringWriter()
    {
            Dispose(false);
    }
            protected override void Dispose(bool disposing)
        {
          if(disposed)
            return;
                    if(disposing)
          {
            disposed = true;

            // Call the base class implementation.
            base.Dispose(disposing);
          }
                  }
    #endregion
       
  }
}
