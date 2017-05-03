using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SoloProjects.Dudhit.Utilities
{
 public class FileSystemHelper
  {
    public static bool FolderCreation(string path)
    {
      try
      {
        if(!Directory.Exists(path))
        {
          Directory.CreateDirectory(path);
          return true;
        }
      }
      catch(UnauthorizedAccessException UAE)
      {
        MessageBox.Show(UAE.Message, "info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        return false;
      }
      return false;
    }

    public static bool FolderVerification(string path)
    {
      try
      {
        if(Directory.Exists(path))
        {
          return true;
        }
      }
      catch(UnauthorizedAccessException UAE)
      {
        MessageBox.Show(UAE.Message, "info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        return false;
      }
      return false;
    }
    public static bool CopyFile(string sourcePath, string destinationPath)
    {
      try
      {
        File.Copy(sourcePath, destinationPath,true);
        return true;
      }
      catch(UnauthorizedAccessException UAE)
      {
        MessageBox.Show(UAE.Message, "info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        return false;
      }
      }
  }
}
