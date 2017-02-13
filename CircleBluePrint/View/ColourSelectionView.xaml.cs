
using System.Windows.Controls;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.ViewModel;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.View
{
  /// <summary>
  /// Interaction logic for ColourSelectionView.xaml
  /// </summary>
  public partial class ColourSelectionView : UserControl
  {
    public ColourSelectionView()
    {
      InitializeComponent();
    }

    public ColourSelectionView(IColourSelectionViewModel vm)
      : this()
    {
     // InitializeComponent();
      this.DataContext=vm;
    }


  }
}
