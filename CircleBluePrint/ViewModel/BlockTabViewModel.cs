using SoloProjects.Dudhit.SpaceEngineers.SEBP.Model;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Prism.Mvvm;
using Prism.Commands;
using System.Windows.Input;
using System.Collections.Generic;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.View;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.ViewModel
{
  class BlockTabViewModel : BindableBase, IBlockTabViewModel
  {
    private SoloProjects.Dudhit.UserInterfaces.CrudeColorPicker.CrudePicker ccp;
    private BlockModel model;
    private string gridSize;
    private string armourType;
    private string selectedColour;
    private Point3D hsv;
    private bool sizeLarge;
    private bool armourNormal;
    private ObservableCollection<ColourSelectionViewModel> boxVMs;
    private ObservableCollection<ColourSelectionModel> boxModels;
    public Color FillColour
    {
      get;
      set;
    }

    #region model properties
    public Point3D ColourAsHSV
    {
      get
      {
        return model.ColourAsHSV;
      }
      set
      {
        model.ColourAsHSV=value;
      }
    }

    public string BlockSize
    {
      get
      {
        return model.BlockSize;
      }
      set
      {
        model.BlockSize= value;
      }
    }

    public string BlockType
    {
      get
      {
        return model.BlockType;
      }
      set
      {
        model.BlockType= value;
      }
    }
    #endregion
    #region view properties commands
    public bool ArmourNormal
    {
      get
      {
        return armourNormal;
      }
      set
      {
        SetProperty(ref armourNormal, value);
        NotifyBlockChange();
        System.Diagnostics.Trace.WriteLine(BlockType);
      }

    }
    public bool SizeLarge
    {
      get
      {
        return sizeLarge;
      }
      set
      {
        SetProperty(ref sizeLarge, value);
        // this.OnPropertyChanged(() => this.BlockType);
        NotifyBlockChange();
        System.Diagnostics.Trace.WriteLine(BlockSize);
      }
    }


    public ICommand SetFill
    {
      get;
      private set;
    }
    private bool CanSetFillColour(object arg)
    {
      return SizeLarge;
    }


    private ColourSelectionModel CheckboxModel(string name, string colour, int tab)
    {
      //string[] names = new string[] { "colBlack1", "colBlue1", "colRed1", "colGreen1", "colYellow1", "colWhite1", "colGrey1" };
      //string[] colours = new string[] { "#FF0D0D0D", "#FF1C5A8C", "#FF991F1F", "#FF344C34", "#FFCFA83E", "#FFF2F2F2", "#FF8C8C8C" };
     

      ColourSelectionModel checkboxModel = new ColourSelectionModel();
      checkboxModel.BoxColour=colour;
      checkboxModel.BoxHeight=27;
      checkboxModel.BoxWidth=36;
      checkboxModel.ColourTabIndex=tab;
      checkboxModel.GroupName="custCol";
      checkboxModel.Name=name;

      return checkboxModel;
    }
    /*     
<colSample:ColourSampleSelection BoxColour="#FF0D0D0D" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="100" x:Name="colBlack1" />
<colSample:ColourSampleSelection BoxColour="#FF1C5A8C" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="110" x:Name="colBlue1" />
<colSample:ColourSampleSelection BoxColour="#FF991F1F" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="120" x:Name="colRed1" />
<colSample:ColourSampleSelection BoxColour="#FF344C34" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="130" x:Name="colGreen1" />
<colSample:ColourSampleSelection BoxColour="#FFCFA83E" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="140" x:Name="colYellow1"/>
<colSample:ColourSampleSelection BoxColour="#FFF2F2F2" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="150" x:Name="colWhite1" />
<colSample:ColourSampleSelection BoxColour="#FF8C8C8C" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="160" x:Name="colGrey1" />
                                                                                                                                                  />
<colSample:ColourSampleSelection BoxColour="#FF404040" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="170" x:Name="colBlack"  />
<colSample:ColourSampleSelection BoxColour="#FF0A6DBF" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="180" x:Name="colBlue"  />
<colSample:ColourSampleSelection BoxColour="#FFCC0A0A" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="190" x:Name="colRed"  />
<colSample:ColourSampleSelection BoxColour="#FF448044" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="200" x:Name="colGreen"  />
<colSample:ColourSampleSelection BoxColour="#FFFFC526" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="210" x:Name="colYellow" />
<colSample:ColourSampleSelection BoxColour="#FFFFFFFF" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="220" x:Name="colWhite"  />
<colSample:ColourSampleSelection BoxColour="#FFBFBFBF" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="203" x:Name="colGrey"  />
                                                                                                                                                  />
<colSample:ColourSampleSelection BoxColour="#FF595151" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="240" x:Name="colBlack>2" />
<colSample:ColourSampleSelection BoxColour="#FF417199" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="250" x:Name="colBlue2"  />
<colSample:ColourSampleSelection BoxColour="#FF9E4343" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="260" x:Name="colRed2"  />
<colSample:ColourSampleSelection BoxColour="#FF507750" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="270" x:Name="colGreen2"  />
<colSample:ColourSampleSelection BoxColour="#FFBAA059" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="280" x:Name="colYellow2"  />
<colSample:ColourSampleSelection BoxColour="#FFCCB7B7" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="290" x:Name="colWhite2"  />
<colSample:ColourSampleSelection BoxColour="#FF998E8E" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="300" x:Name="colGrey2"  />
                                                                                                                                                  />
<colSample:ColourSampleSelection BoxColour="#FF3F3E3E" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="310" x:Name="colBlack3" />
<colSample:ColourSampleSelection BoxColour="#FF45535E" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="320" x:Name="colBlue3" />
<colSample:ColourSampleSelection BoxColour="#FF991F1F" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="330" x:Name="colRed3" />
<colSample:ColourSampleSelection BoxColour="#FF455E45" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="340" x:Name="colGreen3" />
<colSample:ColourSampleSelection BoxColour="#FFA08D58" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="350" x:Name="colYellow3" />
<colSample:ColourSampleSelection BoxColour="#FFB2AEAE" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="360" x:Name="colWhite3" />
<colSample:ColourSampleSelection BoxColour="#FF7F7F7F" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="370" x:Name="colGrey3" />
           
                 <colSample:ColourSampleSelection x:Name="colCustom" BoxColour="#FFAAAAAA" BoxWidth="36" BoxHeight="27" GroupName="custCol" ColourTabIndex="400" />
           
  */
    #endregion
    public BlockTabViewModel(BlockModel model)
    {
      this.model=model;
      FillColour = Color.FromArgb(255, 255, 0, 0);
      armourNormal= true;
      sizeLarge = true;
      // colGrey1.IsChecked = true;
      this.SetFill = new DelegateCommand<object>(
                                       this.SetFillColour, this.CanSetFillColour);
    }

    private void NotifyBlockChange()
    {
      gridSize = (sizeLarge == true) ? "Large" : "Small";
      switch(gridSize)
      {
        case "Large":
          armourType     = (armourNormal == true) ? "LargeBlockArmorBlock" : "LargeHeavyBlockArmorBlock";
          break;

        case "Small":
          armourType = (armourNormal == true) ? "SmallBlockArmorBlock" : "SmallHeavyBlockArmorBlock";
          break;

      }
      BlockSize=gridSize;
      BlockType=armourType;
      //BlockChangeEventArgs bcea = new BlockChangeEventArgs(hsv, gridSize, armourType);
      //OnBlockChangeEvent(bcea);
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
    public void UpdateColourValue(string colour)
    {
      //   System.Diagnostics.Trace.WriteLine(x.hexColour);
      //check if alpha not 255// ff and adjust accordingly
      FillColour = (Color)SoloProjects.Dudhit.Utilites.ColourConverters.MakeAColourFromString(colour);
      //if(FillColour.A != 255)
      //  FillColour.A = 255;
      //convert to HSV 
      Point3D temp;
      hsv = SoloProjects.Dudhit.Utilites.ColourConverters.ConvertRgbToHsv(FillColour.R, FillColour.G, FillColour.B);
      temp = SoloProjects.Dudhit.Utilites.ColourConverters.ConvertStandardHSVtoSEFormat((float)hsv.X, (float)hsv.Y, (float)hsv.Z);
      hsv = SoloProjects.Dudhit.Utilites.ColourConverters.ConvertSEFormatHSVtoBluePrintFormat((float)temp.X, (float)temp.Y, (float)temp.Z);

    }



  }
}
