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
  /// Interaction logic for Shell.xaml
  /// </summary>
  public partial class Shell : Window
  {
    public Shell()
    {
      InitializeComponent();
    }


    private void MainClose(object sender, System.ComponentModel.CancelEventArgs e)
    {
      System.Diagnostics.Trace.WriteLine(e.ToString());
      MessageBoxResult goodBye = MessageBox.Show("Do you intend on leaving? ", "exit app", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
      if(goodBye != MessageBoxResult.Yes)
      {
        e.Cancel = true;
      }
      else
      {
        //unsubscribe to tabs
      }
    }

  }
}
