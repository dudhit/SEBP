using SoloProjects.Dudhit.Utilities;
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
using System.Windows.Shapes;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.View
{
  /// <summary>
  /// Interaction logic for ConsoleOutputs.xaml
  /// </summary>
  public partial class ConsoleOutputs : Window
  {
    private ConsoleViewModel viewModel;
    public ConsoleOutputs()
    {
      InitializeComponent();
     
    }
    public void Start(string[] args)
    {
      viewModel = new ConsoleViewModel(this, args);
    }


 
  }
}
