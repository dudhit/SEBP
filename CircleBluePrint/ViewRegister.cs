using Prism.Modularity;
using Prism.Regions;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
  class ViewRegister : IModule
  {
    private readonly IRegionViewRegistry regionViewRegistry;

    #region IModule Members

    public void Initialize()
    {
      regionViewRegistry.RegisterViewWithRegion("MenuRegion", typeof(Menu));
      regionViewRegistry.RegisterViewWithRegion("TabControlRegion", typeof(TabControl));
      regionViewRegistry.RegisterViewWithRegion("ShapeTabRegion", typeof(ShapeTabView));
      regionViewRegistry.RegisterViewWithRegion("ColourSelectionView", typeof(ColourSelectionView));
      regionViewRegistry.RegisterViewWithRegion("BlockTabRegion", typeof(BlockTabView));


    }

    #endregion
    public ViewRegister(IRegionViewRegistry regionViewReg)
    {
      this.regionViewRegistry=regionViewReg;
    }
  }


}
