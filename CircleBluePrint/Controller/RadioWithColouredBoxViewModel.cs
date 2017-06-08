using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.model
{
  public class RadioWithColouredBoxViewModel : BindingBase
  {

    public ObservableCollection<MyRadioRectangleModel> ControlCollection { get; set; }
    public double NumberItems { get; set; }
    public MyRadioRectangleModel rm1;
    public MyRadioRectangleModel rm2;
    public MyRadioRectangleModel rm3;
    public MyRadioRectangleModel rm4;
    public MyRadioRectangleModel rm5;
    public MyRadioRectangleModel rm6;
    public MyRadioRectangleModel rm7;
    public MyRadioRectangleModel rm8;
    public MyRadioRectangleModel rm9;
    public MyRadioRectangleModel rm10;
    public MyRadioRectangleModel rm11;
    public MyRadioRectangleModel rm12;
    public MyRadioRectangleModel rm13;
    public MyRadioRectangleModel rm14;
    public MyRadioRectangleModel rm15;
    public MyRadioRectangleModel rm16;
    public MyRadioRectangleModel rm17;
    public MyRadioRectangleModel rm18;
    public MyRadioRectangleModel rm19;
    public MyRadioRectangleModel rm20;
    public MyRadioRectangleModel rm21;
    public MyRadioRectangleModel rm22;
    public MyRadioRectangleModel rm23;
    public MyRadioRectangleModel rm24;
    public MyRadioRectangleModel rm25;
    public MyRadioRectangleModel rm26;
    public MyRadioRectangleModel rm27;
    public MyRadioRectangleModel rm28;
    public MyRadioRectangleModel rmCustom;

    private ObservableCollection<MyRadioRectangleModel> temp;
    public RadioWithColouredBoxViewModel()
      : base()
    {
      temp= new ObservableCollection<MyRadioRectangleModel>();

      NumberItems=7;
      ControlCollection=temp;
      RaisePropertyChanged("NumberItems");
      RaisePropertyChanged("ControlCollection");
    }

    public void ChangeDisplayColumns(int column)
    {
      if(column>0)
      {
        NumberItems=column;
        RaisePropertyChanged("NumberItems");
      }
    }

    public void AddModel(string group, bool isChecked, double width, double height, string colour)
    {
      temp=ControlCollection;
      temp.Add(new MyRadioRectangleModel() { RadioGroup=group, RadioChecked=isChecked, RadioWidth=width, RadioHeight=height, RectFill=colour });
      ControlCollection=temp;
      RaisePropertyChanged("ControlCollection");
    }

    public void RemoveModel(string colour)
    {
      temp= ControlCollection;
      foreach(MyRadioRectangleModel rrm in temp)
      {
        if(rrm.RectFill.Equals(colour))
        {
          temp.Remove(rrm);
        }
      }
      ControlCollection=temp;
      RaisePropertyChanged("ControlCollection");
    }


  }
}