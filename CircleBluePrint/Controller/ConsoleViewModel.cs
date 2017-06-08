using SoloProjects.Dudhit.SpaceEngineers.SEBP.BlueprintWriterLib;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.Model;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.View;
using SoloProjects.Dudhit.Utilities;
using SoloProjects.Dudhit.Utilities.Curves;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
  public class ConsoleViewModel :BindingBase 
  {
    private HashSet<Point3D> blueprintData;
    private string[] startingArguments;
    public Visibility ProgressBarVisibility { get; set; }
    public bool ProgressBarIsIndeterminate { get; set; }
    public double ProgressBarPercentComplete { get; set; }
    private Style lookLikeConsoleText;
    private Style lookLikeConsoleBtn;
    private Style lookLikeConsoleLabel;
    private Style lookLikeConsoleBlock;
    private ObservableCollection<Control> controlCollection;
    public ObservableCollection<Control> ControlCollection { get; set; }
    private BlueprintModel masterBlueprint;
    private int argumentsSuccessfullyProcessed;
       public ConsoleViewModel(ConsoleOutputs view, string[] arguments):base()
    {
      view.DataContext=this;
      lookLikeConsoleText = Application.Current.FindResource("ConsoleTextBox") as Style;
      lookLikeConsoleBtn = Application.Current.FindResource("ConsoleButton") as Style;
      lookLikeConsoleLabel = Application.Current.FindResource("ConsoleLabel") as Style;
      lookLikeConsoleBlock = Application.Current.FindResource("ConsoleTextBlock") as Style;
      controlCollection =new ObservableCollection<Control>();
      ProgressBarIsIndeterminate = false;
      ProgressBarPercentComplete=0.01;
      ProgressBarVisibility=Visibility.Visible;
      RaisePropertyChanged("ProgressBarIsIndeterminate");
      RaisePropertyChanged("ProgressBarVisibility");
      RaisePropertyChanged("ProgressBarPercentComplete");
      AddTextToCollection("initiating...");
      startingArguments=arguments;
      StartAsync();
    }

    public void ClearCollection()
    {
      controlCollection.Clear();
      ControlCollection=controlCollection;
      RaisePropertyChanged("ControlCollection");
    }

    public void AddTextToCollection(string myText)
    {
      controlCollection.Add(new TextBox() { Text=myText, Style=lookLikeConsoleText, IsReadOnly=true });
      ControlCollection=controlCollection;
      RaisePropertyChanged("ControlCollection");
    }
    public void AddButtonToCollection(string myText)
    {
      controlCollection.Add(new Button() { Content=myText, Style=lookLikeConsoleBtn });
      ControlCollection=controlCollection;
      RaisePropertyChanged("ControlCollection");
    }
    public void ChangeProgressBarVisibility(string action)
    {
      switch(action.ToLower())
      {
        case "c": { ProgressBarVisibility=Visibility.Collapsed; break; }
        case "h": { ProgressBarVisibility=Visibility.Hidden; break; }
        case "v": { ProgressBarVisibility=Visibility.Visible; break; }
      }
      RaisePropertyChanged("ProgressBarVisibility");
    }
    public async void StartAsync()
    {
      masterBlueprint = new BlueprintModel();
      Progress<MyTaskProgressReporter> myFeedback = new Progress<MyTaskProgressReporter>(ShowFeedback);
      RefreshUI();

      Task<int> argumentHandlingTask =   SetupUsableData(myFeedback);
      AddTextToCollection("Checking supplied data..");
      argumentsSuccessfullyProcessed  =await argumentHandlingTask;
      switch(argumentsSuccessfullyProcessed)
      {
        case -1:
          {
            AddTextToCollection("Please correct inputs before trying again");
            break;
          }
        case 0:
          {
            ShowHelp();
            break;
          }
        case 1:
          {
            AddTextToCollection("Beginning shape calculations...");
        blueprintData =await GetPointData(myFeedback); // Task<bool> shapeCraftTask = GetPointDataAsync();
         //    blueprintData=await myData;  // shapesSuccessfullyCalculated = await shapeCraftTask;
       
            AddTextToCollection(string.Format("a whole {0} blocks have been generated",-1));// blueprintData.Count));
            break;
          }
      }
      if(blueprintData.Count>0) //  if(shapesSuccessfullyCalculated)
      {
        ClearCollection();
        AddTextToCollection(string.Format("Generating blueprint {0}", masterBlueprint.BlueprintName));
        //access writer
        using(BluePrintXml writeBlueprint =new BluePrintXml(masterBlueprint, blueprintData, myFeedback))
        {
          await Task.Run(() => { writeBlueprint.MakeBaseStructure(); });
          AddTextToCollection("making your blueprint");
          await Task.Run(() => { writeBlueprint.BluePrintFileHandling(); });
        }
        UpdateProgressBar(100);
      }
  
      AddTextToCollection(string.Format("*****************************\nThis window can now be closed\r*****************************")); 
    }

    private void RefreshUI()
    {
      ClearCollection();
      AddTextToCollection("SEBP commandline interface.\n use \"--help\" or /? for usage instructions");
    }
    private async Task<int> SetupUsableData(IProgress<MyTaskProgressReporter> progress)
    {
      using(CommandLineHandler commandLineHandler = new CommandLineHandler(startingArguments))
      {
        commandLineHandler.MyBlueprint=masterBlueprint;
        int cmdLineHandlerResult = await commandLineHandler.StartAsync(progress);
        if(commandLineHandler.MyBlueprint.HasUsableData&&cmdLineHandlerResult==1)
          masterBlueprint=commandLineHandler.MyBlueprint;
        return cmdLineHandlerResult;
      }
    }

    public async Task<HashSet<Point3D>> GetPointData(IProgress<MyTaskProgressReporter>  myFeedback)
    {
      if(masterBlueprint!=null&&masterBlueprint.HasUsableData)
      {
        //    call class to handle point and blueprint output
        using(PointsToShape pointsToShape = new PointsToShape(masterBlueprint.XAxis, masterBlueprint.YAxis, masterBlueprint.ZAxis, masterBlueprint.FinalShape, masterBlueprint.Solid, myFeedback))
        {
          await Task.Run(() => { pointsToShape.ProcessingShapeAsync(); });
          return pointsToShape.GlobalCurveSet;
       //   blueprintData= pointsToShape.GlobalCurveSet;
        }
      }
      return null;
    }


    private async Task<bool> SomeNumberWorkAsync(IProgress<MyTaskProgressReporter> progress)
    {
      double i;
      double countSize=20;
      // int progressPause=0;
      for(i=0;i<countSize;i++)
      {
        if(progress!=null/*&&progressPause==i-10*/)
        {
          //progressPause=i; 
          double percent= i/countSize*100;
          progress.Report(new MyTaskProgressReporter() { ProgressMessage=string.Format("loop {0} of 20", i), ProgressCounter= percent });
        }
        await Task.Delay(500);
      }
      return i==countSize;
    }

    private void ShowHelp()
    {
      string pathToHelp = Path.Combine(Directory.GetCurrentDirectory(), "sebp_arg_help.txt");
      try
      {
        using(StreamReader helpFile = new StreamReader(pathToHelp))
        {
          while(helpFile.Peek() != -1)
          {
            AddTextToCollection(helpFile.ReadLine());
          }
          helpFile.Close();
        }
      }
      catch(FileNotFoundException FNF) { FileSystemHelper.Logging(Path.Combine(Directory.GetCurrentDirectory(), "file.log"), FNF.Message); }
      catch(UnauthorizedAccessException UAE) { FileSystemHelper.Logging(Path.Combine(Directory.GetCurrentDirectory(), "file.log"), UAE.Message); }
      catch(Exception ae) { FileSystemHelper.Logging(Path.Combine(Directory.GetCurrentDirectory(), "file.log"), ae.Message); }
    }

    private void ShowFeedback(MyTaskProgressReporter status)
    {
      AddTextToCollection(status.ProgressMessage);
      UpdateProgressBar((int)status.ProgressCounter);
    }

    public void UpdateProgressBar(int value)
    {
      if(!(value<0||value>100))
      {
        ProgressBarIsIndeterminate=false;
        ProgressBarPercentComplete=value;
      }
      else
      {
        ProgressBarIsIndeterminate=true;
        ProgressBarPercentComplete=0;
      }
      RaisePropertyChanged("ProgressBarIsIndeterminate");
      RaisePropertyChanged("ProgressBarPercentComplete");
    }

  }
}
