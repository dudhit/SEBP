using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.model
{
  public class MyRadioRectangleModel : BindingBase
  {
    private string radioGroup;
    private string radioTips;
    private bool radioChecked;
    private double radioWidth;
    private double radioHeight;
    private string rectFill;
  

    public MyRadioRectangleModel():base()
    {
    RadioGroup="mygrp";
    RadioChecked=false;
    RadioWidth=40;
    RadioHeight=40;
    RectFill="#FF808080";
    }

    public string RadioGroup
    {
      get { return this.radioGroup; }
      set
      {
        if(value!=radioGroup&&value!=string.Empty)
        {
          this.radioGroup=value;
          RaisePropertyChanged("RadioGroup");
        }
      }
    }
    public string RadioTips
    {
      get { return this.radioTips; }
      set
      {
        if(value!=radioTips&&value!=string.Empty)
        {
          this.radioTips=value;
          RaisePropertyChanged("RadioTips");
        }
      }
    }
    public bool RadioChecked
    {
      get { return this.radioChecked; }
      set
      {
        if(value!=radioChecked)
        {
          this.radioChecked=value;
          RaisePropertyChanged("RadioChecked");
        }
      }
    }
    public double RadioWidth
    {
      get { return this.radioWidth; }
      set { if(value!=radioWidth&&value>13) { this.radioWidth=value; RaisePropertyChanged("RadioWidth"); RectWidth=value-14; RaisePropertyChanged("RectWidth"); } }
    }
    public double RadioHeight
    {
      get { return this.radioHeight; }
      set { if(value!=radioHeight&&value>13) { this.radioHeight=value; RaisePropertyChanged("RadioHeight"); RectHeight=value-14; RaisePropertyChanged("RectHeight"); } }
    }
    public double RectWidth
    {
      get;
     private set ; 
    }
    public double RectHeight
    {
      get ;
      private set; 
    }
    public string RectFill
    {
      get { return this.rectFill; }
      set
      {
        if(value!=rectFill)
        {
          this.rectFill=value;
          RaisePropertyChanged("RectFill");
          RadioTips=RectFill;
        }
      }
    }
  }
}
