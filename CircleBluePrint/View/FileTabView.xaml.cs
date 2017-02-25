using SoloProjects.Dudhit.SpaceEngineers.SEBP.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.View
{
    /// <summary>
    /// Interaction logic for FileTabView.xaml
    /// </summary>
    public partial class FileTabView : UserControl
    {
      
        public FileTabView()
        {
            InitializeComponent();
        }

        public FileTabView(IFileTabViewModel vm) : this()
        {
          this.DataContext=vm;
        
        }



    





    }
}
