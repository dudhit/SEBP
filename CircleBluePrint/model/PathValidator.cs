using System.Windows.Controls;
using System.IO;
namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
  class PathValidator : ValidationRule
  {
    public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
    {
      if(value == null)
        return new ValidationResult(false, "value cannot be empty.");
      else
      {
       if( !Directory.Exists(value.ToString()))
          return new ValidationResult
          (false, "Path does not exist.");
      }
      return ValidationResult.ValidResult;
    }
  }
  class RadialValidator : ValidationRule
  {
    public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
    {
      if(value==null)
        return new ValidationResult(false, "Radius must be specified.");
      else
      { if((int)value>=10&&(int)value<=500) return ValidationResult.ValidResult; }
      return new ValidationResult(false, "Radius must be between 10 and 500");
    }
  }
}
