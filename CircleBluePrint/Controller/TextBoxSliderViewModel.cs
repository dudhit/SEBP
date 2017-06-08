using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.model
{
  public class TextBoxSliderViewModel : BindingBase
  {

    public ObservableCollection<MyTextSliderModel> SliderCollection { get; set; }
    public double NumberItems { get; set; }
    public MyTextSliderModel mts1;
    public MyTextSliderModel mts2;
    public MyTextSliderModel mts3;
    public MyTextSliderModel mts4;
    public MyTextSliderModel mts5;
    public MyTextSliderModel mts6;
    public MyTextSliderModel mts7;
    public MyTextSliderModel mts8;
    private ObservableCollection<MyTextSliderModel> temp;
    public TextBoxSliderViewModel()
      : base()
    {
      temp = new ObservableCollection<MyTextSliderModel>();

      mts1 = new MyTextSliderModel();
      mts2 = new MyTextSliderModel();
      mts3 = new MyTextSliderModel();
      mts4 = new MyTextSliderModel();
      mts5 = new MyTextSliderModel();
      mts6 = new MyTextSliderModel();
      mts7 = new MyTextSliderModel();
      mts1.Identifier = "x";
      mts1.TextValue="6";
      mts1.SliderMin=10;
      mts1.SliderMax=100;
      mts1.SliderValue=6;
      mts2.Identifier = "y";
      mts2.TextValue="6";
      mts2.SliderMin=10;
      mts2.SliderMax=200;
      mts2.SliderValue=6;
      mts2.SliderHeight=200;
      mts3.Identifier = "z";
      mts3.TextValue="6";
      mts3.SliderMin=10;
      mts3.SliderMax=500;
      mts3.SliderValue=6;
      mts7.SliderValue=4;
      temp.Add(mts1);
      temp.Add(mts2);
      temp.Add(mts3);
      temp.Add(mts4);
      temp.Add(mts5);
      temp.Add(mts6);
      temp.Add(mts7);
      SliderCollection=temp;
      NumberItems=4;
      RaisePropertyChanged("SliderCollection");
      RaisePropertyChanged("NumberItems");

    }


  }

}
