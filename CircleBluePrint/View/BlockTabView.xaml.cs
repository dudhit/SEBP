using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SoloProjects.Dudhit.UserInterfaces.EventArguments;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.EventArguments;
using SoloProjects.Dudhit.UserInterfaces;
using System.Windows.Media.Media3D;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.ViewModel;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.View
{
    /// <summary>
    /// Interaction logic for BlockTabView.xaml
    /// </summary>
  public partial class BlockTabView : UserControl
    {

    public BlockTabView()
    {
      InitializeComponent();
    }

    public BlockTabView(IBlockTabViewModel vm):this()
    {

      this.DataContext=vm;
    }





    

 



      
    }
}
