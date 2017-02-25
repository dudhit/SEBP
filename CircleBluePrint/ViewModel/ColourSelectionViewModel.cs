using Prism.Mvvm;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.ViewModel
{
  public class ColourSelectionViewModel : BindableBase, IColourSelectionViewModel
  {
    #region ColourSelectionModel
    private IColourSelectionModel colourModel;
    private bool colourIsChecked;
    private int colourTabIndex;
    private string controlGroupName;
    private string boxColour;
    private double boxHeight;
    private double boxWidth;

    public ColourSelectionViewModel(/*Func<string,*/ IColourSelectionModel/*>*/ model/*, string name*/)
    {
      this.colourModel=model/*(name)*/;
    }

    #region view bindings
    public bool ColourIsChecked
    {
      get
      {
        return colourModel.IsChecked;
      }
      set
      {
        if(SetProperty(ref colourIsChecked, value))
        {
          colourModel.IsChecked=colourIsChecked;
        }
      }
    }
    public int ColourTabIndex
    {
      get
      {
        return this.colourModel.ColourTabIndex;
      }
      set
      {
        if(SetProperty(ref colourTabIndex, value))
        {
          colourModel.ColourTabIndex=colourTabIndex;
        }
      }
    }
    public string ControlGroupName
    {
      get
      {
        return colourModel.GroupName;
      }
      set
      {
        if(SetProperty(ref controlGroupName, value))
        {
          colourModel.GroupName=controlGroupName;
        }
      }
    }
    public string BoxColour
    {
      get
      {
        return colourModel.BoxColour;
      }
      set
      {
        if(SetProperty(ref boxColour, value))
        {
          colourModel.BoxColour=boxColour;
        }
      }
    }
    public double BoxHeight
    {
      get
      {
        return colourModel.BoxHeight;
      }
      set
      {
        if(SetProperty(ref boxHeight, value))
        {
          colourModel.BoxHeight=boxHeight;
        }
      }
    }
    public double BoxWidth
    {
      get
      {
        return colourModel.BoxWidth;
      }
      set
      {
        if(SetProperty(ref boxWidth, value))
        {
          colourModel.BoxWidth=boxWidth;
        }
      }
    }

    #endregion


    #endregion

  }
}
