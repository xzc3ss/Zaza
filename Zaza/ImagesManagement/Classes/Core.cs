using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ImageResizer;
using System.IO;
namespace ImagesManagement.Classes
{
  public class Core
  {
    public Boolean CreateImage(string originalFileName, string newFileName, int width, int height, string separator)
    {
      try
      {
        var r = new ResizeSettings();
        switch (separator)
        {
          case "x":
            r.Width = width;
            r.Height = height;
          case "m":
            r.MaxHeight = height;
            r.MaxWidth = width;
          case "c":
            r.Width = width;
            r.Height = height;
            r.CropMode = CropMode.Auto;
        }
        var directory = newFileName.Substring(0, newFileName.LastIndexOf('\\'));
        if (!IO.Directory.Exists(directory))
        {
          IO.Directory.CreateDirectory(directory);
        }
        ImageBuilder.Current.Build(originalFileName, newFileName, new ResizeSettings(r));
        return true;
      }
      catch { return false; }
    }
  }
}


