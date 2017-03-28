using SoloProjects.Dudhit.Utilites;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
  public class BlueprintModel : INotifyPropertyChanged, IDataErrorInfo
  {
    [Flags]private enum ShapeCombinations {nil=0,circle=1<<0,ellipse=1<<1,sphere=1<<2,ellipsiod=1<<3,quarter=1<<4,semi=1<<5,full=1<<6 }
    [Flags]
    private enum ModelState
    { nil=0, hasBlueprintFilePath=1<<0, hasSteamId=1<<1, hasSteamName=1<<2, hasBlueprintName=1<<3, hasXAxis=1<<4, hasYAxis=1<<5, hasZAxis=1<<6, hasShape=1<<7, hasFraction=1<<8, hasBlockArmour=1<<9, hasBlockSize=1<<10, hasBlockColour=1<<11, all= hasBlueprintFilePath|hasSteamId|hasSteamName|hasBlueprintName|hasXAxis| hasYAxis| hasZAxis| hasShape| hasFraction|hasBlockArmour| hasBlockSize| hasBlockColour }
    private string blueprintFilePath;
    private string steamId;
    private string steamName;
    private string blueprintName;
    private int xAxis;
    private int yAxis;
    private int zAxis;
    private string shape;
    private string shapeFraction;
    private int finalShape;
    private string blockArmour;
    private string blockSize;
    private SeHSV blockColour;
    private int modelState;

    public BlueprintModel()
    {
      modelState=0;
    }
    #region properties


    public string BlueprintFilePath
    {
      get { return this.blueprintFilePath; }
      set
      {
        ResetSetFlag( (int)ModelState.hasBlueprintFilePath);
        if(NoEmptyString(value))
        {
          this.blueprintFilePath = value;
          this.modelState+=(int)ModelState.hasBlueprintFilePath;
          RaisePropertyChanged("BlueprintFilePath");
        }
      }
    }


    public string SteamId
    {
      get { return this.steamId; }
      set
      {
        ResetSetFlag((int)ModelState.hasSteamId);
        if(NoEmptyString(value))
        {
          this.steamId = value.Trim();
          modelState+=(int)ModelState.hasSteamId;
          RaisePropertyChanged("SteamId");
        }
      }
    }
    public string SteamName
    {
      get { return this.steamName; }
      set
      {
        ResetSetFlag((int)ModelState.hasSteamName);
        if(NoEmptyString(value))
        {
          this.steamName = value.Trim();
          this.modelState+=(int)ModelState.hasSteamName;
          RaisePropertyChanged("SteamName");
        }
      }
    }
    public string BlueprintName
    {
      get { return this.blueprintName; }
      set
      {
        ResetSetFlag((int)ModelState.hasBlueprintName);
        if(NoEmptyString(value))
        {
          this.blueprintName = value.Trim();
          this.modelState+=(int)ModelState.hasBlueprintName;
          RaisePropertyChanged("BlueprintName");
        }
      }
    }
    public int XAxis
    {
      get { return this.xAxis; }
      set
      {
        ResetSetFlag((int)ModelState.hasXAxis);
        if(ValidDouble(value))
        {
          if(value>=10||value<=500)
          {
            this.xAxis = value;
            this.modelState+=(int)ModelState.hasXAxis;
            RaisePropertyChanged("XAxis");
          }
        }
      }
    }
    public int YAxis
    {
      get { return this.yAxis; }
      set
      {
        ResetSetFlag((int)ModelState.hasYAxis);
        if(ValidDouble(value))
        {
          if(value>=10||value<=500)
          {
            this.yAxis = value;
            this.modelState+=(int)ModelState.hasYAxis;
            RaisePropertyChanged("YAxis");
          }
        }
      }
    }
    public int ZAxis
    {
      get { return this.zAxis; }
      set
      {
        ResetSetFlag((int)ModelState.hasZAxis);
        if(ValidDouble(value))
        {
          if(value>=10||value<=500)
          {
            this.zAxis = value;
            this.modelState+=(int)ModelState.hasZAxis;
            RaisePropertyChanged("ZAxis");
          }
        }
      }
    }
    public string Shape
    {
      get { return Enum.Parse(typeof(ShapeCombinations), this.shape).ToString(); }
      set
      {
        ResetSetFlag((int)ModelState.hasShape);
        if(Enum.IsDefined(typeof(ShapeCombinations), value))
        {
          this.shape =  Enum.Parse(typeof(ShapeCombinations), value).ToString();
          this.modelState+=(int)ModelState.hasShape;
          RaisePropertyChanged("Shape");
          RaisePropertyChanged("FinalShape");
        }
      }
    }
    public string ShapeFraction
    {
      get { return Enum.Parse(typeof(ShapeCombinations), this.shapeFraction).ToString(); }
      set
      {
     
        ResetSetFlag((int)ModelState.hasFraction);
        if(Enum.IsDefined(typeof(ShapeCombinations), value))
        {
          this.shapeFraction =Enum.Parse(typeof(ShapeCombinations), value).ToString();
          this.modelState+=(int)ModelState.hasFraction;
          RaisePropertyChanged("ShapeFraction");
          RaisePropertyChanged("FinalShape");
        }
      }
    }
    public int FinalShape
    { 
      get 
    {
      return this.finalShape;
    } 
      set
    {
        int join=(int)ModelState.hasShape|(int)ModelState.hasFraction;
      if(IsFlagSet(this.modelState,join)==join)
      {
        JoinShapeCombinationsEnumStings(this.shape, this.shapeFraction);
    }
    } 
    }

    private int JoinShapeCombinationsEnumStings(  string value1,string value2)
    {
      return this.finalShape=(int)Enum.Parse(typeof(ShapeCombinations), value1)+(int)Enum.Parse(typeof(ShapeCombinations), value2);
    }
    public string BlockArmour
    {
      get { return this.blockArmour; }
      set
      {
        ResetSetFlag((int)ModelState.hasBlockArmour);
        if(NoEmptyString(value))
        {
          if(value.Equals("Normal")||value.Equals("Heavy"))
          {
            this.blockArmour = value;
            this.modelState+=(int)ModelState.hasBlockArmour;
            RaisePropertyChanged("BloakArmour");
          }
        }
      }
    }
    public string BlockSize
    {
      get { return this.blockSize; }
      set
      {
        ResetSetFlag((int)ModelState.hasBlockSize);
        if(NoEmptyString(value))
        {
          if(value.Equals("Large")||value.Equals("Small"))
          {
            this.blockSize = value;
            this.modelState+=(int)ModelState.hasBlockSize;
            RaisePropertyChanged("BlockSize");
          }
        }
      }
    }
    public SeHSV BlockColour
    {
      get { return this.blockColour; }
      set
      {
        ResetSetFlag((int)ModelState.hasBlockColour);
        this.blockColour = value;
        this.modelState+=(int)ModelState.hasBlockColour;
        RaisePropertyChanged("BlockColour");
      }
    }



    public bool IsComplete { get { return this.modelState==(int)ModelState.all; } }
    #endregion

    private void ResetSetFlag( int flag)
    {
      if(IsFlagSet(this.modelState, flag)==flag) { this.modelState-=flag; }
    }

    private int IsFlagSet(int flagTotal, int singleFlag)
    {
      return flagTotal&singleFlag;
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

    #region ValidationMethods

    private static bool NoEmptyString(string value)
    {
      return (!string.IsNullOrEmpty(value)||!string.IsNullOrWhiteSpace(value));
    }

    private static bool ValidDouble(double value)
    {
      return (value>=10&&value<=500);
    }
    #endregion

    #region IDataErrorInfo Members

    public string Error
    {
      get { throw new NotImplementedException(); }
    }

    public string this[string columnName]
    {
      get { throw new NotImplementedException(); }
    }

    #endregion

  }

}
