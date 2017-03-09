using SoloProjects.Dudhit.SpaceEngineers.SEBP.Model;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Prism.Mvvm;
using Prism.Commands;
using System.Windows.Input;
using SoloProjects.Dudhit.Utilites;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System;
using Microsoft.Practices.Unity;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.ViewModel
{
  public class BlockTabViewModel : BindableBase, IBlockTabViewModel
  {
    private SoloProjects.Dudhit.UserInterfaces.CrudeColorPicker.CrudePicker ccp;
    private IBlockTabModel model;
    private ColourSelectionViewModel colselVM;
    private bool armourIsNormal;
    private bool sizeIsLarge;
    private bool customColourIsSelected;
    private string selectedColour;
    private string gridSize;
    private string armourType;
    #region IBlockTabViewModel Members


    public IBlockTabModel Model
    {
      get
      {
        return this.model;
      }
      set
      {
        if(SetProperty(ref this.model, value))
        {
          gridSize=model.BlockSize;
          armourType=model.BlockType;
          FillColour=ColourConverters.ConvertHsvToRgb(model.ColourAsHSV.X, model.ColourAsHSV.Y, model.ColourAsHSV.Z);
          BlockChangeModelToView();
        }
      }
    }

    public bool ArmourNormal
    {
      get
      {
        return armourIsNormal;
      }
      set
      {
        if(SetProperty(ref armourIsNormal, value))
        {
          BlockChangeBoolToModel();
        }

      }
    }

    public bool SizeLarge
    {
      get
      {
        return sizeIsLarge;
      }
      set
      {
        if(SetProperty(ref sizeIsLarge, value))
        {
          BlockChangeBoolToModel();
        }
      }
    }

    #endregion
    public Color FillColour
    {
      get;
      set;
    }

    #region constructors
    public BlockTabViewModel(IBlockTabModel model)
    {
      this.model=model;
      this.SetFill = new DelegateCommand<object>(
     this.SetFillColour, this.CanSetFillColour);
      selectedColour="#FFAABBCC";
      customColourIsSelected=false;
      BlockChangeModelToView();
   
    }
    #endregion

    #region Actions

    public ICommand SetFill
    {
      get;
      private set;
    }

    private void SetFillColour(object arg)
    {

      Color tempCol = SoloProjects.Dudhit.Utilites.ColourConverters.MakeAColourFromString(selectedColour);
      ccp = new SoloProjects.Dudhit.UserInterfaces.CrudeColorPicker.CrudePicker(tempCol);

      bool? dialogResult = ccp.ShowDialog();
      if(dialogResult != null && (bool)dialogResult == true)
      {
        selectedColour = ccp.SelectedColour.ToString();
        FillColour = ccp.SelectedColour;
      }
      ccp.Close();
    }

    private bool CanSetFillColour(object arg)
    {
      return customColourIsSelected;
    }

    private void BlockChangeBoolToModel()
    {
      gridSize = (sizeIsLarge == true) ? "Large" : "Small";
      switch(gridSize)
      {
        case "Large":
          armourType     = (armourIsNormal == true) ? "LargeBlockArmorBlock" : "LargeHeavyBlockArmorBlock";
          break;

        case "Small":
          armourType = (armourIsNormal == true) ? "SmallBlockArmorBlock" : "SmallHeavyBlockArmorBlock";
          break;

      }
      model.BlockType=armourType;
      model.BlockSize=gridSize;
    }

    private void BlockChangeModelToView()
    {
      SizeLarge=(gridSize=="Large")?true:false;
      ArmourNormal=(armourType=="SmallBlockArmorBlock"||armourType=="LargeBlockArmorBlock")?true:false;

    }
    public void UpdateColourValue(string colour)
    {

      //   System.Diagnostics.Trace.WriteLine(x.hexColour);
      //check if alpha not 255// ff and adjust accordingly
      FillColour = (Color)SoloProjects.Dudhit.Utilites.ColourConverters.MakeAColourFromString(colour);
      //if(FillColour.A != 255)     FillColour= new Color();
      //convert to HSV 
      Point3D firstConversion;
      Point3D secondConversion;
      firstConversion = SoloProjects.Dudhit.Utilites.ColourConverters.ConvertRgbToHsv(FillColour.R, FillColour.G, FillColour.B);
      secondConversion = SoloProjects.Dudhit.Utilites.ColourConverters.ConvertStandardHSVtoSEFormat((float)firstConversion.X, (float)firstConversion.Y, (float)firstConversion.Z);
      model.ColourAsHSV   = SoloProjects.Dudhit.Utilites.ColourConverters.ConvertSEFormatHSVtoBluePrintFormat((float)secondConversion.X, (float)secondConversion.Y, (float)secondConversion.Z);

    }



    #endregion
  
  
    
    private void ChangeColourObject(object sender, EventArgs e)
    {
      ICollectionView coll =(ICollectionView)sender;
      if(coll!=null)
      {
        this.colselVM = coll.CurrentItem as ColourSelectionViewModel;
        System.Diagnostics.Trace.WriteLine("collection changed");
      }
    }

  }
}
