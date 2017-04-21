using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
 public class DataValidator : ValidationRule
  {
   public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
   {
     if(value==null)
       return new ValidationResult(false, "emtpy data");
     return ValidationResult.ValidResult;
   }
  }
}
