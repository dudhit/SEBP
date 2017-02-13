using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.Model;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.View;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.ViewModel;
using System.Windows;


namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
  class Bootstrapper : UnityBootstrapper
  {
    protected override DependencyObject CreateShell()
    {
      return ServiceLocator.Current.GetInstance<Shell>();
    }

    protected override void InitializeShell()
    {
      base.InitializeShell();

      IUnityContainer container =ServiceLocator.Current.GetInstance<IUnityContainer>();
      container.RegisterType<IBlockTabViewModel, BlockTabViewModel>();
      container.RegisterType<IColourSelectionViewModel, ColourSelectionViewModel>();

      Application.Current.MainWindow=(Window)this.Shell;
      Application.Current.MainWindow.Show();
    }
    protected override void ConfigureModuleCatalog()
    {
      base.ConfigureModuleCatalog();
      ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
      moduleCatalog.AddModule(typeof(ViewRegister));
    }
  }
}
