
using Prism.Modularity;
namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.Model
{
  public interface IBlockTabModel
  {
     System.Windows.Media.Media3D.Point3D ColourAsHSV
    {
      get;
      set;
    }

     string BlockSize
    {
      get;
      set;
    }

     string BlockType
    {
      get;
      set;
    }
  }
  public class BlockTabModel : IBlockTabModel,IModule
  {
        public System.Windows.Media.Media3D.Point3D ColourAsHSV
    {
      get;
      set;
    }

    public string BlockSize
    {
      get;
      set;
    }

    public string BlockType
    {
      get;
      set;
    }
   public BlockTabModel()
    { Initialize(); }

    #region IModule Members

    public void Initialize()
    {
      ColourAsHSV=new System.Windows.Media.Media3D.Point3D(1, 50, 50);
      BlockSize="Large";
      BlockType="LargeBlockArmorBlock";
    }

    #endregion
  }
}
