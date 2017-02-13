using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.ViewModel
{
 public interface IFileTabViewModel
  {
       void Reset();
      
     void AutoPopulateSaveLocation();

         void Find_Path();    

         void UpdateSavePath();

         bool StringHasValue(string validate);



         void SnoopForSteamID();




         void TextBoxChanged();
  }
}
