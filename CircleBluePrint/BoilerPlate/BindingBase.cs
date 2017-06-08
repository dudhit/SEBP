using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
  public class BindingBase : INotifyPropertyChanged,ICommand
  {
    #region INotifyPropertyChanged Members
    public event PropertyChangedEventHandler PropertyChanged;

    public void RaisePropertyChanged(string prop)
    {
      PropertyChangedEventHandler handler =  PropertyChanged;
      if(handler != null)
        handler(this, new PropertyChangedEventArgs(prop));
    }
     #endregion

    #region ICommand Members

    public bool CanExecute(object parameter)
    {
      return parameter!=null;
    }

    public event EventHandler CanExecuteChanged;

    public void Execute(object parameter)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
