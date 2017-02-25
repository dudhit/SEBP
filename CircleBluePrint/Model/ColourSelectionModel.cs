
using Prism.Modularity;
namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.Model
{
  public interface IColourSelectionModel
  {
     string Name
    {
      get;
      set;
    }
     int ColourTabIndex
    {
      get;
      set;
    }
     string GroupName
    {
      get;
      set;
    }
     bool IsChecked
    {
      get;
      set;
    }
     string BoxColour
    {
      get;
      set;
    }
     double BoxHeight
    {
      get;
      set;
    }
     double BoxWidth
    {
      get;
      set;
    }
  }

  public class ColourSelectionModel :IColourSelectionModel
  {
 public string Name
    {
      get;
      set;
    }
    public int ColourTabIndex
    {
      get;
      set;
    }
    public string GroupName
    {
      get;
      set;
    }
    public bool IsChecked
    {
      get;
      set;
    }
    public string BoxColour
    {
      get;
      set;
    }
    public double BoxHeight
    {
      get;
      set;
    }
    public double BoxWidth
    {
      get;
      set;
    }

    //#region IModule Members

    //public void Initialize()
    //{
    //  Name=string.Empty;
    //  BoxHeight=10;
    //  BoxWidth=10;
    //  BoxColour="#FFFFFFFF";
    //  IsChecked=false;
    //  GroupName=string.Empty;
    //  ColourTabIndex=-1;
    //}

    //#endregion
  }
}
