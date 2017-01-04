using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.view
{
    class FindSteamID:IDisposable
    {
     

             #region disposal

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~FindSteamID()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
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
