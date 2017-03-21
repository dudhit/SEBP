using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
public  class CommandLineHandler
  {
    private string[] myStartingArgs;
    private CheckStartArguments myDictionaryOfArgs;
    private int argsIn;
    public CommandLineHandler(string[] args)
    {
      this.myStartingArgs=args;
      if(ArgumentPreProcessing())
      {
        //populate bpclass
        //process shape
        //output bp
      }


      else
      {
        Console.WriteLine("no args to process-will use UI");
      }
    }
    
    private bool ArgumentPreProcessing()
    {
      if(myStartingArgs[0].ToLower()=="/?"||myStartingArgs[0].ToLower()=="-help"||myStartingArgs[0].ToLower()=="--help")
        ShowHelp();
      else
      {
        ProcessArguments();
        //if(argsIn!=AcceptableNumberOfArguments)
        //{ Console.WriteLine("incorrect number of args received"); }{ return true; }
      }
      return false;
    }
    private void ShowHelp()
    {

      string pathToHelp =  "E:\\lifeDocs\\computing\\cSharp\\calcMod\\testmaths\\testmaths\\sebp_arg_help.txt";
      try
      {
        using(StreamReader helpFile = new StreamReader(pathToHelp))
        {
          while(helpFile.Peek() != -1)
          {

            Console.WriteLine(helpFile.ReadLine());
          }

          helpFile.Close();
        }
      }
      catch(FileNotFoundException FNF) { Console.WriteLine(FNF.Message); }
      catch(UnauthorizedAccessException UAE) { Console.WriteLine(UAE.Message); }
      catch(Exception ae) { Console.WriteLine(ae.Message); }

    }
    private void ProcessArguments()
    {
      argsIn=0;
      myDictionaryOfArgs = new CheckStartArguments();
      ShowDictionary();
      Console.WriteLine("Processing input...");
   //   bpm =new BlueprintModel();
      foreach(string s in myStartingArgs)
      {
        Console.WriteLine("argument: {0}", s);
        if(s.Contains("="))
        {
          string[] result =   KeyValueExtraction(s, '=');
          if(myDictionaryOfArgs.ContainsKey(result[0]))
          {
            myDictionaryOfArgs[result[0]]=result[1];

            //    Binding x = new Binding(result[0]);
            // x.Source=bpm.BlueprintFilePath
            //      x.ElementName=result[0];
            argsIn++;
          }
          else
            Console.WriteLine("invalid argument");
        }
        else
          Console.WriteLine("invalid assignment of value");
      }

      //if(args[0]=="-f"||args[0]=="/f")
      //{
      //  string argfile =File.Exists(args[1])?args[1]:string.Empty;
      //  Console.WriteLine(argfile);
      ShowDictionary();
      myStartingArgs=null;
      Console.WriteLine(argsIn);
      //}
    }

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

    private string[] KeyValueExtraction(string theString, char splitChar)
    {
      string[] keyValue= new string[2];
      int equalPositon=theString.IndexOf(splitChar, 0);

      keyValue[0] =theString.Substring(0, equalPositon).ToLower();
      keyValue[1] =theString.Substring(equalPositon+1, theString.Length-(equalPositon+1));
      Console.WriteLine(keyValue[0]);
      Console.WriteLine(keyValue[1]);
      return keyValue;
    }

  }
}
