using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.model
{

  public class MyTextSliderModel : BindingBase
  {
    private string identifier;
    private string textValue;
    private double sliderMin;
    private double sliderMax;
    private double sliderValue;
    private double sliderHeight;

    public MyTextSliderModel():base()
    {
      identifier="this slider";
      textValue="5";
      sliderMax=10;
      sliderMin=1;
      sliderValue=5;
      sliderHeight=50;
      RaisePropertyChanged("Identifier");
      RaisePropertyChanged("TextValue");
      RaisePropertyChanged("SliderMin");
      RaisePropertyChanged("SliderMax");
      RaisePropertyChanged("SliderValue");
      RaisePropertyChanged("SliderHeight");
    }

    public string Identifier
    {
      get { return this.identifier; }
      set
      {
        if(value!=string.Empty)
        {
          this.identifier=value;
        }
        else
          Identifier=identifier;
        RaisePropertyChanged("Identifier");
      }
    }
    public string TextValue
    {
      get { return this.textValue; }
      set
      {
        double converted;
        if(Double.TryParse(value, out converted))
        {
          if(converted>=sliderMin&&converted<=sliderMax&&converted.ToString()!=textValue)
          {
            SliderValue=converted;
            this.textValue= converted.ToString();
          }
        }
        else
          TextValue=textValue;
        RaisePropertyChanged("TextValue");
      }
    }
    public double SliderMin
    {
      get { return this.sliderMin; }
      set
      {
        this.sliderMin= value;
        RaisePropertyChanged("SliderMin");
      }
    }
    public double SliderMax
    {
      get { return this.sliderMax; }
      set
      {
        this.sliderMax= value;
        RaisePropertyChanged("SliderMax");
      }
    }
    public double SliderValue
    {
      get { return this.sliderValue; }
      set
      {
        if(value!=sliderValue)
        {
          this.sliderValue=Math.Round(value);
        RaisePropertyChanged("SliderValue");
        textValue=sliderValue.ToString();
        RaisePropertyChanged("TextValue");
        }
      }
    }
    public double SliderHeight
    {
      get { return this.sliderHeight; }
      set
      {
        this.sliderHeight= value;
        RaisePropertyChanged("SliderHeight");
      }
    }
  }

}
