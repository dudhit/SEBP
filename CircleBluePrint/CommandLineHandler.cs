using SoloProjects.Dudhit.SpaceEngineers.SEBP.Model;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.View;
using SoloProjects.Dudhit.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
  public class CommandLineHandler : IDisposable, INotifyPropertyChanged
  {
    private string[] myStartingArgs;
    private CheckStartArguments myDictionaryOfArgs;
    public BlueprintModel MyBlueprint { get; set; }
    private IProgress<MyTaskProgressReporter> progressIndicator;
    public CommandLineHandler(string[] args)
    {
      this.myStartingArgs=args;

    }

    public async Task<int> StartAsync(IProgress<MyTaskProgressReporter> progress)
    {
      this.progressIndicator=progress;
      if(ArgumentAsksForHelp())
      {
return 0;
      }
      if(!ArgumentAsksForHelp()&&MyBlueprint!=null)
      {
        await Task.Run(() => { ProcessArguments(); });
        return 1;
      }
      return -1;
    }

    private bool ArgumentAsksForHelp()
    {
      if(myStartingArgs[0].ToLower()=="/?"||myStartingArgs[0].ToLower()=="-h"||myStartingArgs[0].ToLower()=="--help")
      {
        return true;
      }
   return false;  
    }

    private void ProcessArguments()
    {
      using(myDictionaryOfArgs = new CheckStartArguments())
      {
        double status=0;

        if(progressIndicator!=null)
          progressIndicator.Report(new MyTaskProgressReporter() { ProgressCounter=-1, ProgressMessage="Processing inputs..." });
        foreach(string s in myStartingArgs)
        {
          if(progressIndicator!=null)
            progressIndicator.Report(new MyTaskProgressReporter() { ProgressCounter=((status/ myStartingArgs.Length)*100), ProgressMessage=string.Format("\targument: {0}", s) });
          if(s.Contains("="))
          {
            string[] result =   KeyValueExtraction(s, '=');
            if(myDictionaryOfArgs.ContainsKey(result[0]))
            {
              myDictionaryOfArgs[result[0]]=result[1];
              if(progressIndicator!=null)
                progressIndicator.Report(new MyTaskProgressReporter() { ProgressCounter=-1, ProgressMessage=string.Format("\t\t{0} set to: {1}", result[0], result[1]) });
            }
            else
            {
              if(progressIndicator!=null)
                progressIndicator.Report(new MyTaskProgressReporter() { ProgressCounter=-1, ProgressMessage=string.Format("\t\tunknown argument: {0} ", result[0]) });
            }
          }
          else
          {
            if(progressIndicator!=null)
              progressIndicator.Report(new MyTaskProgressReporter() { ProgressCounter=-1, ProgressMessage=string.Format("\t\tinvalid usage or asignment of: {0} ", s) });
          }
          status++;
        }
        myStartingArgs=null;
        if(progressIndicator!=null)
          progressIndicator.Report(new MyTaskProgressReporter() { ProgressCounter=-1, ProgressMessage=string.Format("setting unspecified data...") });
        myDictionaryOfArgs.SetEmptyWithDefaultValues();
        myDictionaryOfArgs.MyBlueprintModel=MyBlueprint;
        myDictionaryOfArgs.SetModel();
        CheckBpData();
      }

    }


    private void CheckBpData()
    {
      if(myDictionaryOfArgs.MyBlueprintModel.HasUsableData)
      {
        //validate path is writable before wasting time/resources with other modules
        FolderTestAndFinish();
      }
      else
      {
        if(progressIndicator!=null)
          progressIndicator.Report(new MyTaskProgressReporter() { ProgressMessage="Provided Data was missing or erroneous." });
      }
    }

    private void FolderTestAndFinish()
    {
      string savePath=Path.Combine(MyBlueprint.BlueprintFilePath, MyBlueprint.BlueprintName);
      if(ProceedWithWriteableFolder(savePath))
      {
        MyBlueprint=myDictionaryOfArgs.MyBlueprintModel;
     if(progressIndicator!=null)   progressIndicator.Report(new MyTaskProgressReporter() { ProgressMessage="Preparing blueprint file space" });
      }
      else
      {
        if(progressIndicator!=null)
          progressIndicator.Report(new MyTaskProgressReporter() { ProgressMessage=string.Format("Could not access blueprint folder {0}, check your file access levels OR a correct path was given", savePath) });
      }

    }

    private bool ProceedWithWriteableFolder(string testPath)
    {

      if(FileSystemHelper.FolderVerification(testPath))
      {
        return WriteThumbToFolder(testPath);
      }
      if(FileSystemHelper.FolderCreation(testPath))
      {
        return WriteThumbToFolder(testPath);
      }
      return false;
    }

    private static bool WriteThumbToFolder(string testPath)
    {
      string imageResource = Path.Combine(Directory.GetCurrentDirectory(), "images\\thumb.png");
      return FileSystemHelper.CopyFile(imageResource, Path.Combine(testPath, "thumb.png"));
    }



    //#if DEBUG

    //    private void ShowDictionary()
    //    {
    //      foreach(var entry in myDictionaryOfArgs)
    //      {
    //        System.Diagnostics.Trace.Write("KEY:");
    //        System.Diagnostics.Trace.Write(entry.Key.ToString());
    //        System.Diagnostics.Trace.Write(" VALUE:");
    //        System.Diagnostics.Trace.WriteLine(entry.Value.ToString());
    //      }
    //    }

    //#endif
    private string[] KeyValueExtraction(string theString, char splitChar)
    {
      string[] keyValue= new string[2];
      int equalPositon=theString.IndexOf(splitChar, 0);

      keyValue[0] =theString.Substring(0, equalPositon).ToLower();
      keyValue[1] =theString.Substring(equalPositon+1, theString.Length-(equalPositon+1));
#if DEBUG
      //    System.Diagnostics.Trace.Write(keyValue[0]);
      //    System.Diagnostics.Trace.Write(keyValue[1]);
#endif
      return keyValue;
    }


    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    private void RaisePropertyChanged(string prop)
    {
      PropertyChangedEventHandler handler =  PropertyChanged;
      if(handler != null)
        handler(this, new PropertyChangedEventArgs(prop));
    }
    #endregion


    #region disposal

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    ~CommandLineHandler()
    {
      Dispose(false);
    }

    protected virtual void Dispose(bool disposing)
    {
      if(disposing)
      {
        // free managed resources  
        //if (Encoding != null)
        //{
        //    Encoding.Dispose();
        //    Encoding = null;
        //}
      }

    }
    #endregion


  }
}
