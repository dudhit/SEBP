using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.Model;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.View;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.ViewModel;
using System;
using System.Windows;


namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
  /* interfaces that it needs to Register are IServiceLocator, IModuleManager, IRegionManager, IModuleInitializer, IEventAggregator*/
  class Bootstrapper : UnityBootstrapper
  {
    //private BlockTabModel blockData =new BlockTabModel();
    //private IBlueprintData blueprintData =new BlueprintData();
    
    protected override DependencyObject CreateShell()
    {
      return ServiceLocator.Current.GetInstance<Shell>();
    }

    protected override void InitializeShell()
    {
      base.InitializeShell();

      IUnityContainer container =ServiceLocator.Current.GetInstance<IUnityContainer>();
      //container.RegisterInstance<IBlueprintData>(blueprintData);
      //container.RegisterInstance<IBlockTabModel>(blockData);
      //container.RegisterInstance(typeof(IBlockTabModel), "Logging", blockData);
   //   container.RegisterType<IBlockTabViewModel, BlockTabViewModel>();

  

 //     container.RegisterType<Func<string, IColourSelectionModel>>(        new InjectionFactory(c => new Func<string, IColourSelectionModel>(name => c.Resolve<IColourSelectionModel>(name))));

  //    container.RegisterType<Func<string, IColourSelectionViewModel>>(        new InjectionFactory(c => new Func<string, IColourSelectionViewModel>(name => c.Resolve<IColourSelectionViewModel>(name))));
      
      Application.Current.MainWindow=(Window)this.Shell;
      Application.Current.MainWindow.Show();
    }
    protected override void ConfigureModuleCatalog()
    {
      base.ConfigureModuleCatalog();
      ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
     // moduleCatalog.AddModule(typeof(BlockTabModel));
      //moduleCatalog.AddModule(typeof(BlockTabModel));
      moduleCatalog.AddModule(typeof(ViewRegister));
    }
  }
}
