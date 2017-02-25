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
      TempLoad();
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
    #region colour selection
    public ICollectionView ColoursRed
    {
      get;
      set;
    }
    public ICollectionView ColoursBlue
    {
      get;
      set;
    }
    public ICollectionView ColoursGreen
    {
      get;
      set;
    }
    public ICollectionView ColoursYellow
    {
      get;
      set;
    }
    public ICollectionView ColoursBlack
    {
      get;
      set;
    }
    public ICollectionView ColoursWhite
    {
      get;
      set;
    }
    public ICollectionView ColoursGrey
    {
      get;
      set;
    }
    public ICollectionView ColoursUser
    {
      get;
      set;
    }

    private void TempLoad()
    {
      ObservableCollection<IColourSelectionViewModel> customReds =new ObservableCollection<IColourSelectionViewModel>();
      //ObservableCollection<ColourSelectionViewModel> customBlues =new ObservableCollection<ColourSelectionViewModel>();
      //ObservableCollection<ColourSelectionViewModel> customGreens =new ObservableCollection<ColourSelectionViewModel>();
      //ObservableCollection<ColourSelectionViewModel> customBlacks =new ObservableCollection<ColourSelectionViewModel>();
      //ObservableCollection<ColourSelectionViewModel> customWhites =new ObservableCollection<ColourSelectionViewModel>();
      //ObservableCollection<ColourSelectionViewModel> customGreys =new ObservableCollection<ColourSelectionViewModel>();
      //ObservableCollection<ColourSelectionViewModel> customYellows =new ObservableCollection<ColourSelectionViewModel>();
      ObservableCollection<ColourSelectionViewModel> customUser =new ObservableCollection<ColourSelectionViewModel>();
      double boxWidth=36, boxHeight=27;
      bool selected=false;
      string grouping="customColour";

/*
      #region blues
      customBlues.Add(new ColourSelectionViewModel(new ColourSelectionModel
 {
   BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colBlue", BoxColour=   "#FF0A6DBF"
 },""));
      customBlues.Add(new ColourSelectionViewModel(new ColourSelectionModel
 {
   BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colBlue1", BoxColour=  "#FF1C5A8C"
 },""));
      customBlues.Add(new ColourSelectionViewModel(new ColourSelectionModel
 {
   BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colBlue2", BoxColour=   "#FF417199"
 },""));
      customBlues.Add(new ColourSelectionViewModel(new ColourSelectionModel
{
  BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colBlue3", BoxColour=    "#FF45535E"
},""));

      ColoursBlue =new ListCollectionView(customBlues);
      #endregion
      #region blacks
      customBlacks.Add(new ColourSelectionViewModel(new ColourSelectionModel
{
  BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colBlack", BoxColour=   "#FF404040"
},""));
      customBlacks.Add(new ColourSelectionViewModel(new ColourSelectionModel
      {
        BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name="colBlack1", BoxColour="#FF0D0D0D"
      }, ""));
      customBlacks.Add(new ColourSelectionViewModel(new ColourSelectionModel
{
  BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colBlack2", BoxColour=   "#FF595151"
}, ""));
      customBlacks.Add(new ColourSelectionViewModel(new ColourSelectionModel
{
  BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colBlack3", BoxColour=    "#FF3F3E3E"
}, ""));

      ColoursBlack =new ListCollectionView(customBlacks);
      #endregion
      #region greens
      customGreens.Add(new ColourSelectionViewModel(new ColourSelectionModel
  {
    BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colGreen", BoxColour=   "#FF448044"
  }, ""));
      customGreens.Add(new ColourSelectionViewModel(new ColourSelectionModel
 {
   BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colGreen1", BoxColour=   "#FF344C34"
 }, ""));
      customGreens.Add(new ColourSelectionViewModel(new ColourSelectionModel
{
  BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colGreen2", BoxColour=     "#FF507750"
}, ""));
      customGreens.Add(new ColourSelectionViewModel(new ColourSelectionModel
       {
         BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colGreen3", BoxColour=    "#FF455E45"
       }, ""));

      ColoursGreen =new ListCollectionView(customGreens);
      #endregion
  */  
      #region reds
      customReds.Add(new ColourSelectionViewModel(new ColourSelectionModel
{
  BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colRed", BoxColour=   "#FFCC0A0A"
}));
      customReds.Add(new ColourSelectionViewModel(new ColourSelectionModel
{
  BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colRed1", BoxColour=   "#FF991F1F"
}));
      customReds.Add(new ColourSelectionViewModel(new ColourSelectionModel
{
  BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colRed2", BoxColour=    "#FF9E4343"
}));
      customReds.Add(new ColourSelectionViewModel(new ColourSelectionModel
{
  BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colRed3", BoxColour=     "#FF991F1F"
}));


      ColoursRed =new ListCollectionView(customReds);
      #endregion
    #region whites
    /*    customWhites.Add(new ColourSelectionViewModel(new ColourSelectionModel
 {
   BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colWhite", BoxColour=    "#FFFFFFFF"
 }, ""));
      customWhites.Add(new ColourSelectionViewModel(new ColourSelectionModel
{
  BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name="colWhite1", BoxColour=   "#FFF2F2F2"
}, ""));
      customWhites.Add(new ColourSelectionViewModel(new ColourSelectionModel
{
  BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colWhite2", BoxColour=    "#FFCCB7B7"
}, ""));
      customWhites.Add(new ColourSelectionViewModel(new ColourSelectionModel
      {
        BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colWhite3", BoxColour=    "#FFB2AEAE"
      }, ""));

      ColoursWhite =new ListCollectionView(customWhites);
      #endregion
      #region greys
      customGreys.Add(new ColourSelectionViewModel(new ColourSelectionModel
{
  BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colGrey", BoxColour=   "#FFBFBFBF"
}, ""));
      customGreys.Add(new ColourSelectionViewModel(new ColourSelectionModel
{
  BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colGrey1", BoxColour=   "#FF8C8C8C"
}, ""));
      customGreys.Add(new ColourSelectionViewModel(new ColourSelectionModel
{
  BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colGrey2", BoxColour=    "#FF998E8E"
}, ""));
      customGreys.Add(new ColourSelectionViewModel(new ColourSelectionModel
      {
        BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colGrey3", BoxColour=    "#FF7F7F7F"
      }, ""));

      ColoursGrey =new ListCollectionView(customGreys);
      #endregion
      #region yellows
      customYellows.Add(new ColourSelectionViewModel(new ColourSelectionModel
 {
   BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colYellow", BoxColour=   "#FFFFC526"
 }, ""));
      customYellows.Add(new ColourSelectionViewModel(new ColourSelectionModel
{
  BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colYellow1", BoxColour=  "#FFCFA83E"
}, ""));
      customYellows.Add(new ColourSelectionViewModel(new ColourSelectionModel
{
  BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colYellow2", BoxColour=    "#FFBAA059"
}, ""));
      customYellows.Add(new ColourSelectionViewModel(new ColourSelectionModel
      {
        BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name= "colYellow3", BoxColour=    "#FFA08D58"
      }, ""));

      ColoursYellow =new ListCollectionView(customYellows);
 */ 
      #endregion

      customUser.Add(new ColourSelectionViewModel(new ColourSelectionModel
     {
       BoxHeight=boxHeight, BoxWidth=boxWidth, IsChecked=selected, GroupName=grouping, Name=  "colCustom", BoxColour= "#FFAAAAAA"
     }));
      ColoursUser =new ListCollectionView(customUser);

      ColoursRed.CurrentChanged+=ChangeColourObject;
      //ColoursBlue.CurrentChanged+=ChangeColourObject;
      //ColoursGreen.CurrentChanged+=ChangeColourObject;
      //ColoursYellow.CurrentChanged+=ChangeColourObject;
      //ColoursBlack.CurrentChanged+=ChangeColourObject;
      //ColoursWhite.CurrentChanged+=ChangeColourObject;
      //ColoursGrey.CurrentChanged+=ChangeColourObject;
      ColoursUser.CurrentChanged+=ChangeColourObject;
    }
    private void ChangeColourObject(object sender, EventArgs e)
    {
      ICollectionView coll =(ICollectionView)sender;
      if(coll!=null)
      {
        this.colselVM = coll.CurrentItem as ColourSelectionViewModel;
        System.Diagnostics.Trace.WriteLine("collection changed");
      }
    }

    #endregion
  }
}
