using System;
using System.ComponentModel;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
  public class BlueprintModel : INotifyPropertyChanged, IDataErrorInfo
  {
    private string blueprintFilePath;
    private string steamId;
    private string steamName;
    private string blueprintName;
    private int xAxis;
    private int yAxis;
    private int zAxis;
    private string shape;
    private string blockArmour;
    private string blockSize;
    private SeHSV blockColour;
    private byte modelState;
    public BlueprintModel()
    { modelState=0; }
    #region properties


    public string BlueprintFilePath
    {
      get { return this.blueprintFilePath; }
      set
      {
        if(NoEmptyString(value))
        {
          this.blueprintFilePath = value;
          this.modelState+=1;
          RaisePropertyChanged("BlueprintFilePath");
        }
      }
    }

    public string SteamId
    {
      get { return this.steamId; }
      set
      {
        if(NoEmptyString(value))
        {
          this.steamId = value;
          modelState+=2;
          RaisePropertyChanged("SteamId");
        }
      }
    }
    public string SteamName
    {
      get { return this.steamName; }
      set
      {
        if(NoEmptyString(value))
        {
          this.steamName = value;
          this.modelState+=1;
        RaisePropertyChanged("SteamName");}
      }
    }
    public string BlueprintName
    {
      get { return this.blueprintName; }
      set
      {
        if(NoEmptyString(value))
        {
          this.blueprintName = value;
          this.modelState+=1;
        
        RaisePropertyChanged("BlueprintName");
      }}
    }
    public int XAxis
    {
      get { return this.xAxis; }
      set
      {
        if(ValidDouble(value))
        {
          this.xAxis = value;
          this.modelState+=1;
          RaisePropertyChanged("XAxis");
        }
      }
    }
    public int YAxis
    {
      get { return this.yAxis; }
      set
      {
        if(ValidDouble(value))
        {
          this.yAxis = value;
          this.modelState+=1;
          RaisePropertyChanged("YAxis");
        }
      }
    }
    public int ZAxis
    {
      get { return this.zAxis; }
      set
      {
        if(ValidDouble(value))
        {
          this.zAxis = value;
          this.modelState+=1;
          RaisePropertyChanged("ZAxis");
        }
      }
    }
    public string Shape
    {
      get { return this.shape; }
      set
      {
        if(NoEmptyString(value))
        {
          this.shape = value;
          this.modelState+=1;
        }
        RaisePropertyChanged("Shape");
      }
    }
    public string BlockArmour
    {
      get { return this.blockArmour; }
      set
      {
        if(NoEmptyString(value))
        {
          this.blockArmour = value;
          this.modelState+=1;
        }
        RaisePropertyChanged("BloakArmour");
      }
    }
    public string BlockSize
    {
      get { return this.blockSize; }
      set
      {
        if(NoEmptyString(value))
        {
          this.blockSize = value;
          this.modelState+=1;
        }
        RaisePropertyChanged("BlockSize");
      }
    }
    public SeHSV BlockColour
    {
      get { return this.blockColour; }
      set
      {
        this.blockColour = value;
        this.modelState+=1;
        RaisePropertyChanged("BlockColour");
      }
    }

    public bool IsComplete { get { return this.modelState==(byte)255; } }
    #endregion

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
