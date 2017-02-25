using System.Windows.Media.Media3D;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.Model
{
 public interface IBlueprintData
  {

     string PathToSeFolder
    {
      get;
      set;
    }
     string SteamName
    {
      get;
      set;
    }
     string SteamId
    {
      get;
      set;
    }
     string BlueprintName
    {
      get;
      set;
    }
     Point3D ColourAsHSV
    {
      get;
      set;
    }
     string BlockSize
    {
      get;
      set;
    }
     string BlockType
    {
      get;
      set;
    }
     string Shape
    {
      get;
      set;
    }
     double RadiusX
    {
      get;
      set;
    }
     double RadiusY
    {
      get;
      set;
    }
     double RadiusZ
    {
      get;
      set;
    }

  
  }

 public class BlueprintData : IBlueprintData
 {
   #region IBlueprintData Members

   public string PathToSeFolder
   {
     get;
     set;
   }

   public string SteamName
   {
     get;
     set;
   }

   public string SteamId
   {
     get;
     set;
   }

   public string BlueprintName
   {
     get;
     set;
   }

   public Point3D ColourAsHSV
   {
     get;
     set;
   }

   public string BlockSize
   {
     get;
     set;
   }

   public string BlockType
   {
     get;
     set;
   }

   public string Shape
   {
     get;
     set;
   }

   public double RadiusX
   {
     get;
     set;
   }

   public double RadiusY
   {
     get;
     set;
   }

   public double RadiusZ
   {
     get;
     set;
   }

   #endregion
 }
}
