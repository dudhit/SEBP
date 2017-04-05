using SoloProjects.Dudhit.SpaceEngineers.SEBP.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
  public class CommandLineHandler
  {
    private ConsoleOutputs cmdOut;
    private string[] myStartingArgs;
    private CheckStartArguments myDictionaryOfArgs;
    private Style lookLikeConsoleText;
    public BlueprintModel MyBlueprint { get; set; }
    private Button killBtn;
    public CommandLineHandler(string[] args)
    {
      lookLikeConsoleText = Application.Current.FindResource("ConsoleText") as Style;
      cmdOut = new ConsoleOutputs();
      cmdOut.InitializeComponent();
      Initialize();
      cmdOut.Show();
      this.myStartingArgs=args;
    }

    public void Start()
    {
   
      if(ArgumentPreProcessing()&&MyBlueprint!=null)
      {
      
        myDictionaryOfArgs.SetEmptyWithDefaultValues();

        myDictionaryOfArgs.MyBlueprintModel=MyBlueprint;
        myDictionaryOfArgs.SetModel();
        if(myDictionaryOfArgs.MyBlueprintModel.HasUsableData)
        {
          MyBlueprint=myDictionaryOfArgs.MyBlueprintModel;
          cmdOut.addedContent.Children.Add(new Label() { Content="Data accepted", Style=lookLikeConsoleText });
                //process shape
        //output bp 
        }
        else
        {
          cmdOut.addedContent.Children.Add(new Label() { Content="The were errors with data assigned to :"+myDictionaryOfArgs.MyBlueprintModel.ModelStateError(), Style=lookLikeConsoleText }); 
        }
     
      }
        
      else
      {
        cmdOut.addedContent.Children.Add(new Label() { Content="Please check help for argument usage", Style=lookLikeConsoleText });
      }
     Style buttonStyle = Application.Current.FindResource("ConsoleButton") as Style;
      cmdOut.addedContent.Children.Add(killBtn=new Button { Content="Close", Height=40, Width=80, Style=buttonStyle });
      killBtn.Click+=EndItAll;
    }

    private void EndItAll(object sender, RoutedEventArgs e)
    {
      killBtn.Click-=EndItAll;
      cmdOut.Hide();
            cmdOut.Close();
   //   myDictionaryOfArgs.MyBlueprintModel=null;
            myDictionaryOfArgs.Clear();
   //   myDictionaryOfArgs=null;
    }

    private void Initialize()
    {
      cmdOut.addedContent.Children.Add(new Label() { Content="SEBP commandline interface.\n type \"--help\" for detailed instructions", Style=lookLikeConsoleText });
    }

    private bool ArgumentPreProcessing()
    {
      if(myStartingArgs[0].ToLower()=="/?"||myStartingArgs[0].ToLower()=="-h"||myStartingArgs[0].ToLower()=="--help")
        ShowHelp();
      else
      {
        ProcessArguments();
        //if(argsIn!=AcceptableNumberOfArguments)
        //{ Console.WriteLine("incorrect number of args received"); }{ 
        return true; //}
      }
      return false;
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




  }
}
