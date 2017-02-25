using SoloProjects.Dudhit.SpaceEngineers.SEBP.Model;
using System.Windows.Input;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.ViewModel
{
  public interface IBlockTabViewModel
  {
    IBlockTabModel Model
    {
      get;
      set;
    }


    bool ArmourNormal
    {
      get;

      set;

    }
    bool SizeLarge
    {
      get;
      set;
    }

 
  }
}
