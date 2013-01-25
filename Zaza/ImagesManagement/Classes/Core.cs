using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ImageResizer;
using System.IO;
namespace ImagesManagement.Classes
{
  public static class Core
  {

    public static bool CreateImage(string originalFileName, string newFileName, int width, int height, string separator)
    {
      try
      {
        var r = new ResizeSettings();

        switch (separator)
        {
          case "x":
            r.Width = width;
            r.Height = height;
            break;
          case "m":
            r.MaxWidth = width;
            r.MaxHeight = height;
            break;
          case "c":
            r.Width = width;
            r.Height = height;
            r.CropMode = CropMode.Auto;
            break;
        }

        string directory = newFileName.Substring(0, newFileName.LastIndexOf("\\"));
        if (!System.IO.Directory.Exists(directory))
        {
          System.IO.Directory.CreateDirectory(directory);
        }

        ImageBuilder.Current.Build(originalFileName, newFileName, new ResizeSettings(r));

        return true;

      }
      catch (Exception ex)
      {
      }

      return false;
    }

  }
}


