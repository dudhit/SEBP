
namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.Model
{
  public class ColourSelectionModel 
  {

    // <colSample:ColourSampleSelection x:Name="colBlack1" BoxColour="#FF0D0D0D" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="100" />
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
}
}
