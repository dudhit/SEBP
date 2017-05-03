using SoloProjects.Dudhit.SpaceEngineers.SEBP.Model;
using SoloProjects.Dudhit.SpaceEngineers.SEBP.View;
using SoloProjects.Dudhit.Utilities;
using System;
using System.Collections.Generic;
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
  public class CommandLineHandler : IDisposable
  {
    private ConsoleOutputs cmdOut;
    private string[] myStartingArgs;
    private CheckStartArguments myDictionaryOfArgs;
    private Style lookLikeConsoleText;
    public BlueprintModel MyBlueprint { get; set; }
    private Button killBtn;
    public bool CanClose { get; private set; }
    public CommandLineHandler(string[] args)
    {
      lookLikeConsoleText = Application.Current.FindResource("ConsoleText") as Style;
      cmdOut = new ConsoleOutputs();
      cmdOut.InitializeComponent();
      cmdOut.Show();
      DisplayHeaderMessage();
      this.myStartingArgs=args;
      CanClose=false;
    }

    private void DisplayHeaderMessage()
    {
      cmdOut.addedContent.Children.Add(new Label() { Content="SEBP commandline interface.\n use \"--help\" for detailed instructions", Style=lookLikeConsoleText });

    }

    public void Start()
    {

      if(ArgumentPreProcessing()&&MyBlueprint!=null)
      {
        ProcessArguments();

        myDictionaryOfArgs.SetEmptyWithDefaultValues();

        myDictionaryOfArgs.MyBlueprintModel=MyBlueprint;
        myDictionaryOfArgs.SetModel();

        if(myDictionaryOfArgs.MyBlueprintModel.HasUsableData)
        {
          MyBlueprint=myDictionaryOfArgs.MyBlueprintModel;

          //validate path is writable before wasting time/resources with other modules

          if(ProceedWithWriteableFolder(Path.Combine(MyBlueprint.BlueprintFilePath, MyBlueprint.BlueprintName)))
          {
            SelfTermination();
          }
          else
          {
            cmdOut.addedContent.Children.Add(new Label() { Content="Could not access folder, check your file access levels OR a correct path was given", Style=lookLikeConsoleText });
            InteractiveTermination();
          }
        }
        else
        {
          cmdOut.addedContent.Children.Add(new Label() { Content="Provided Data was missing or erroneous.", Style=lookLikeConsoleText });
          cmdOut.addedContent.Children.Add(new Label() { Content="Dampners on. thrusters on.", Style=lookLikeConsoleText });
          InteractiveTermination();
        }
      }
      else
      {
        ShowHelp();
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

    private void AddToOutput(string verbose, string labelName)
    {
      cmdOut.addedContent.Children.Add(new Label() { Content=verbose, Style=lookLikeConsoleText, Name=labelName });
    }
    /// <summary>
    /// todo
    /// </summary>
    /// <param name="verbose"></param>
    /// <param name="labelName"></param>
    private void UpdateExistingOutputContent(string verbose, string labelName)
    {
      //cmdOut.addedContent.Children.Contains(;

    }
    /// <summary>
    /// todo
    /// </summary>
    /// <param name="anElement"></param>
    private void RemoveOutputText(UIElement anElement)
    {

    }

    private void InteractiveTermination()
    {
      Style buttonStyle = Application.Current.FindResource("ConsoleButton") as Style;
      cmdOut.addedContent.Children.Add(killBtn=new Button { Content="Close Application", Height=40, Style=buttonStyle });
      killBtn.Click+=EndItAll;
    }

    private void SelfTermination()
    {
      CanClose=true;
      cmdOut.Hide();
      cmdOut.Close();
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

    private bool ArgumentPreProcessing()
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
            cmdOut.addedContent.Children.Add(new Label() { Content=helpFile.ReadLine(), Style=lookLikeConsoleText });
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
      cmdOut.addedContent.Children.Add(new Label() { Content="Processing input...", Style=lookLikeConsoleText });
      foreach(string s in myStartingArgs)
      {
        //     cmdOut.addedContent.Children.Add(new Label() { Content="argument: "+lineFromFile, Style=lookLikeConsoleText });
        if(s.Contains("="))
        {
          string[] result =   KeyValueExtraction(s, '=');
          if(myDictionaryOfArgs.ContainsKey(result[0]))
          {
            myDictionaryOfArgs[result[0]]=result[1];
            cmdOut.addedContent.Children.Add(new Label() { Content=result[0]+" set to: "+result[1], Style=lookLikeConsoleText });
          }
          else
            cmdOut.addedContent.Children.Add(new Label() { Content="unknown argument:"+result[0], Style=lookLikeConsoleText });
        }
        else
          cmdOut.addedContent.Children.Add(new Label() { Content="invalid usage or asignment of:"+s, Style=lookLikeConsoleText });
        //Thread.Sleep(500);
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
