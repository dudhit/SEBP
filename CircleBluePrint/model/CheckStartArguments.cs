using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
  public class CheckStartArguments : Dictionary<string, string>//Dictionary<>
  {
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

  }
}
