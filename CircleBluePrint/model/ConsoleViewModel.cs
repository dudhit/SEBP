using SoloProjects.Dudhit.SpaceEngineers.SEBP.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
 public class ConsoleViewModel : INotifyPropertyChanged
  {
    private Style lookLikeConsoleText;
    private Style lookLikeConsoleBtn;
    private ObservableCollection<object> objectCollection;
    public ObservableCollection<object> ObjectCollection
    {
      get;
      set;

    }
    public ConsoleViewModel (ConsoleOutputs view) 
    {
      lookLikeConsoleText = Application.Current.FindResource("ConsoleText") as Style;
      lookLikeConsoleBtn = Application.Current.FindResource("ConsoleButton") as Style;
      objectCollection =new ObservableCollection<object>();
      view.DataContext=this;
      }

    public void AddTextToCollection(string myText)
    {
      objectCollection.Add(new Label() { Content=myText, Style=lookLikeConsoleText });
      ObjectCollection=objectCollection;
      RaisePropertyChanged("ObjectCollection");
    }
    public void AddButtonToCollection(string myText)
    {
      objectCollection.Add(new Button() { Content=myText, Style=lookLikeConsoleBtn });
      ObjectCollection=objectCollection;
      RaisePropertyChanged("ObjectCollection");
    }  
    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    private void RaisePropertyChanged(string prop)
    {
      PropertyChangedEventHandler handler =  PropertyChanged;
      if(handler != null)
        handler(this, new PropertyChangedEventArgs(prop));
    }
    #endregion
  }
}
