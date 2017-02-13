
using Prism.Mvvm;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.Model;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.ViewModel;
namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.ViewModel
{
  public class ColourSelectionViewModel : BindableBase,IColourSelectionViewModel
  {
    private ColourSelectionModel model;
    public ColourSelectionViewModel(ColourSelectionModel model)
    {
      this.model=model;

    }

    public bool ColourIsChecked
    {
      get;
      set;
    }

    private void ColourSelected()
    {
   
    }

    /*  IsChecked="{Binding ColourIsChecked}" TabIndex="{Binding ColourTabIndex}" GroupName="{Binding GroupName}">
        <WrapPanel>
            <Rectangle x:Name="wreked" Fill="{Binding BoxColour}" Height="{Binding BoxHeight}"
                    Width="{Binding BoxWidth}"></Rectangle>*/

  }
}
