
namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
  public struct SeHSV
  {
    public float H;
    public float S;
    public float V;

    public SeHSV(float h, float s, float v)
    {
      this.H = (h>=0&&h<=360)?h:0;
      this.S = (s>=-100&&s<=100)?s:0;
      this.V = (v>=-100&&v<=100)?v:0;
    }
  }

  public struct StandardHSV
  {
    public float H;
    public float S;
    public float V;

    public StandardHSV(float h, float s, float v)
    {
      this.H = (h>=0&&h<=360)?h:0;
      this.S = (s>=0&&s<=100)?s:0;
      this.V = (v>=0&&v<=100)?v:0;
    }
  }

  public struct BlueprintHSV
  {
    public float H;
    public float S;
    public float V;

    public BlueprintHSV(float h, float s, float v)
    {
      this.H = (h>=0&&h<=360)?h:0;
      this.S = (s>=0&&s<=100)?s:0;
      this.V = (v>=0&&v<=100)?v:0;
    }
  }

  public struct RGB
  {
    public byte R;
    public byte G;
    public byte B;

    public RGB(int r, int g, int b)
    {
      this.R = (r>=0&&r<=255)?(byte)r :(byte)0;
      this.G =(g>=0&&g<=255)?(byte)g:(byte)0;
      this.B = (b>=0&&b<=255)?(byte)b:(byte)0;
    }
  }
}
