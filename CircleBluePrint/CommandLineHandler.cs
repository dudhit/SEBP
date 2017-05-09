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
    private Button killBtn;
    public bool CanClose { get; private set; }
    Progress<MyTaskProgressReporter> progressIndicator;
    private string myProgress;
    public string MyProgress { get { return this.myProgress; } set { value=this.myProgress; RaisePropertyChanged("MyProgress"); } }
    public CommandLineHandler(string[] args)
    {
      this.myStartingArgs=args;
      CanClose=false;
   
    }



    public void Start()
    {
      if(ArgumentPreProcessing()&&MyBlueprint!=null)
      {
        ProcessArguments();
        myDictionaryOfArgs.SetEmptyWithDefaultValues();
        myDictionaryOfArgs.MyBlueprintModel=MyBlueprint;
        myDictionaryOfArgs.SetModel();
        CheckBpData();
      }
      else
      {
        ShowHelp();
        InteractiveTermination();
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
        //       cmdOut.addedContent.Children.Add(new Label() { Content="Provided Data was missing or erroneous.", Style=lookLikeConsoleText });
        //        cmdOut.addedContent.Children.Add(new Label() { Content="Dampners on. thrusters on.", Style=lookLikeConsoleText });
        InteractiveTermination();
      }
    }

    private void FolderTestAndFinish()
    {
      if(ProceedWithWriteableFolder(Path.Combine(MyBlueprint.BlueprintFilePath, MyBlueprint.BlueprintName)))
      {
        MyBlueprint=myDictionaryOfArgs.MyBlueprintModel;
        SelfTermination();
      }
      else
      {
        //        cmdOut.addedContent.Children.Add(new Label() { Content="Could not access folder, check your file access levels OR a correct path was given", Style=lookLikeConsoleText });
        InteractiveTermination();
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



    private void InteractiveTermination()
    {
      //   Style buttonStyle = Application.Current.FindResource("ConsoleButton") as Style;
      //   cmdOut.addedContent.Children.Add(killBtn=new Button { Content="Close Application", Height=40, Style=buttonStyle });
      //   killBtn.Click+=EndItAll;
    }

    private void SelfTermination()
    {
      CanClose=true;
      //   cmdOut.Hide();
      //cmdOut.Close();
      if(myDictionaryOfArgs!=null)
      {
        myDictionaryOfArgs.Clear();
        myDictionaryOfArgs.Dispose();
      }
    }

    private void EndItAll(object sender, RoutedEventArgs e)
    {
      killBtn.Click-=EndItAll;
      SelfTermination();
    }

    private  bool ArgumentPreProcessing()
    {
      if(myStartingArgs[0].ToLower()=="/?"||myStartingArgs[0].ToLower()=="-h"||myStartingArgs[0].ToLower()=="--help")
      {
        return false;
      }
      else
      {
        return true;
      }
    }

    private void ShowHelp()
    {
      string pathToHelp =  "..\\..\\..\\sebp_arg_help.txt";
      try
      {
        using(StreamReader helpFile = new StreamReader(pathToHelp))
        {
          while(helpFile.Peek() != -1)
          {
            //msg    helpFile.ReadLine();
          }
          helpFile.Close();
        }
      }
      catch(FileNotFoundException FNF) { MessageBox.Show(FNF.Message); }
      catch(UnauthorizedAccessException UAE) { MessageBox.Show(UAE.Message); }
      catch(Exception ae) { MessageBox.Show(ae.Message); }
    }

    private void ProcessArguments()
    {
      myDictionaryOfArgs = new CheckStartArguments();
#if DEBUG
      //  ShowDictionary();
#endif
      //msg "Processing input..." ;

      foreach(string s in myStartingArgs)
      {
        //msg "argument: "+s ;
        if(s.Contains("="))
        {
          string[] result =   KeyValueExtraction(s, '=');
          if(myDictionaryOfArgs.ContainsKey(result[0]))
          {
            myDictionaryOfArgs[result[0]]=result[1];
            //msg ProgressMessage = result[0]+" set to: "+result[1] ;
          }
          else
          {
            //msg "unknown argument:"+result[0] ;
          }
        }
        else
        {
          //msg "invalid usage or asignment of:"+s ;
        }
      }
#if DEBUG
      //   ShowDictionary();
#endif
      myStartingArgs=null;

    }
#if DEBUG

    private void ShowDictionary()
    {
      foreach(var entry in myDictionaryOfArgs)
      {
        System.Diagnostics.Trace.Write("KEY:");
        System.Diagnostics.Trace.Write(entry.Key.ToString());
        System.Diagnostics.Trace.Write(" VALUE:");
        System.Diagnostics.Trace.WriteLine(entry.Value.ToString());
      }
    }

#endif
    private string[] KeyValueExtraction(string theString, char splitChar)
    {
      string[] keyValue= new string[2];
      int equalPositon=theString.IndexOf(splitChar, 0);

      keyValue[0] =theString.Substring(0, equalPositon).ToLower();
      keyValue[1] =theString.Substring(equalPositon+1, theString.Length-(equalPositon+1));
#if DEBUG
      //    cmdOut.addedContent.Children.Add(new Label() { Content=keyValue[0], Style=lookLikeConsoleText });
      //    cmdOut.addedContent.Children.Add(new Label() { Content=keyValue[1], Style=lookLikeConsoleText });
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
