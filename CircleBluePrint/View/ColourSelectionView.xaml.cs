using SoloProjects.Dudhit.SpaceEngineers.SEBP.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    public ColourSelectionView(/*Func<string, */IColourSelectionViewModel/*> */ vm/*,string name*/)
      : this()
    {
 
      this.DataContext=vm/*(name)*/;
    }
  }
}
