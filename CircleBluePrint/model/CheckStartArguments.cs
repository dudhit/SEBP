using SoloProjects.Dudhit.Utilites;
using System;
using System.Collections.Generic;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
  public class CheckStartArguments : Dictionary<string, string>,IDisposable//Dictionary<>,
  {
    public BlueprintModel MyBlueprintModel { get; set; }
  //  private BlueprintModel myBlueprintModel;
 
    public CheckStartArguments()
      : base()
    {
      LoadOptions();
    }
    /// <summary>
    /// expected runtime arguments
    /// </summary>
    private void LoadOptions()
    {
      //blueprint_file_path
      this.Add("path", "");
      //steam_id
      this.Add("id", "");
      //steam_name
      this.Add("owner", "");
      //blueprint_name
      this.Add("bpname", "");
      //x_axis
      this.Add("x", "");
      //y_axis
      this.Add("y", "");
      //z_axis
      this.Add("z", "");
      //shape
      this.Add("shape", "");
      //shape_fraction
      this.Add("fraction", "");
      //block_armour
      this.Add("armour", "");
      //block_size
      this.Add("size", "");
      //colour_h
      this.Add("h", "");
      //colour_s
      this.Add("s", "");
      //colour_v
      this.Add("v", "");
      //colour_r
      this.Add("r", "");
      //colour_g
      this.Add("g", "");
      //colour_b
      this.Add("b", "");
      //specify colour model
      this.Add("colour", "");
    }


    private bool IsNeedingDefault(string key)
    {
      return string.IsNullOrEmpty(GetDictionaryValue(key))&&KeyExists(key);
    }

    private bool KeyExists(string key)
    { return this.ContainsKey(key); }

    private string GetDictionaryValue(string key)
    {
      if(KeyExists(key))
        return this[key].ToString();
      return null;
    }
    public void SetEmptyWithDefaultValues()
    {
      FileSystemAdministrativeTools fileSystemAdministrativeTools;

      fileSystemAdministrativeTools = new FileSystemAdministrativeTools(this["owner"], IsNeedingDefault("id"));
      if(IsNeedingDefault("owner")) { this["owner"]=fileSystemAdministrativeTools.SteamUserName; }
      if(IsNeedingDefault("id")) { this["id"]=fileSystemAdministrativeTools.SteamUserId; }
      if(IsNeedingDefault("path")) { this["path"]=fileSystemAdministrativeTools.GetGameDataSaveLocation(); }
      if(IsNeedingDefault("bpname")) { this["bpname"]= "SEBP"; }
      if(IsNeedingDefault("x")) { this["x"]= "10"; }
      if(IsNeedingDefault("y")) { this["y"]= "10"; }
      if(IsNeedingDefault("z")) { this["z"]= "10"; }
      if(IsNeedingDefault("shape")) { this["shape"]= "circle"; }
      if(IsNeedingDefault("fraction")) { this["fraction"]= "quarter"; }
      if(IsNeedingDefault("armour")) { this["armour"]= "Normal"; }
      if(IsNeedingDefault("size")) { this["size"]= "Large"; }
      if(IsNeedingDefault("colour"))
      {
        this["colour"]= "hsv";
        if(IsNeedingDefault("h")) { this["h"]= "0"; }
        if(IsNeedingDefault("s")) { this["s"]= "-100"; }
        if(IsNeedingDefault("v")) { this["v"]= "-90"; }
      }
      else
      {
        if(this["colour"]!="rgb")
          return;
        if(IsNeedingDefault("r")) { this["r"]= "0"; }
        if(IsNeedingDefault("g")) { this["g"]= "0"; }
        if(IsNeedingDefault("b")) { this["b"]= "0"; }
      }

    
    }

    private string CapitaliseFirstLetter(string value)
    {
   //  System.Diagnostics.Trace.WriteLine( value.Substring(0, 1).ToUpper()+value.Substring(1, value.Length-1).ToLower());
     return  value.Substring(0, 1).ToUpper()+value.Substring(1, value.Length-1).ToLower();
      
    }

    public void SetModel()
    {
      //myBlueprintModel = new BlueprintModel();
      #region setColour
      switch(this["colour"])
      {
        case "hsv":
          {
            MyBlueprintModel.BlockColour= new SeHSV(float.Parse(this["h"]), float.Parse(this["s"]), float.Parse(this["v"]));
            break;
          }
        case "rgb":
          {
            ColourConverters.ConvertRgbToHsv(int.Parse(this["r"]), int.Parse(this["g"]), int.Parse(this["b"]));
            break;
          }
      }
      #endregion
      #region setArmour
      MyBlueprintModel.BlockArmour=CapitaliseFirstLetter(this["armour"]);

      #endregion
      #region BlockSize

      MyBlueprintModel.BlockSize= CapitaliseFirstLetter( this["size"]);
      #endregion
      #region BlueprintName
     MyBlueprintModel.BlueprintName=this["bpname"];
      #endregion
      #region BlueprintFilePath
      MyBlueprintModel.BlueprintFilePath=this["path"];
      #endregion
      #region Shape
      MyBlueprintModel.Shape=this["shape"].ToLower();
      #endregion
      #region Shape_fraction
      MyBlueprintModel.ShapeFraction=this["fraction"].ToLower();
      #endregion
      #region SteamId
      MyBlueprintModel.SteamId=this["id"];
      #endregion
      #region SteamName
     MyBlueprintModel.SteamName= this["owner"];
      #endregion
      #region setX
     MyBlueprintModel.XAxis=int.Parse(this["x"]);
      #endregion
      #region setY
     MyBlueprintModel.YAxis=int.Parse(this["y"]);
      #endregion
      #region setZ
     MyBlueprintModel.ZAxis=int.Parse(this["z"]);
      #endregion
   //   System.Diagnostics.Trace.WriteLine( myBlueprintModel.HasUsableData.ToString());
     // MyBlueprintModel=myBlueprintModel;
    }
            #region disposal

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~CheckStartArguments()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources  
              if(MyBlueprintModel != null)
              {
                MyBlueprintModel.Dispose();
                MyBlueprintModel = null;
              }
            }

        }
        #endregion

  }
}
