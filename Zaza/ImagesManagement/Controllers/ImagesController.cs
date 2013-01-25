using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ImagesManagement.Classes;

namespace ImagesManagement
{
  public class ImagesController : System.Web.Mvc.Controller
  {

    public ActionResult ProcessImageUsingRoute(string application, string dir, string bulk, string id, string size, string imageFileName)
    {
      return ProcessOriginalImageUsingRoute(application, dir, bulk, id, size, "", imageFileName);
    }

    public ActionResult ProcessOriginalImageUsingRoute(string application, string dir, string bulk, string id, string size, string mobile, string imageFileName)
    {
      var staticPath = Server.MapPath("~/" + application);
      //replace old server.urlEncoding
      imageFileName = imageFileName.Replace("+", " ");
      var picturePath = Path.Combine(staticPath, size, imageFileName);

      if (!string.IsNullOrEmpty(dir))
      {
        staticPath = Server.MapPath("~/" + application + "/" + dir);
        picturePath = Path.Combine(staticPath, size, imageFileName);
      }

      if (!String.IsNullOrEmpty(bulk) && !string.IsNullOrEmpty(id))
      {
        picturePath = Path.Combine(staticPath, bulk, id, size, imageFileName);

      }

      if (!System.IO.File.Exists(picturePath))
      {
        int width = 0;
        int height = 0;

        // fix for mobile apps
        switch (size)
        {
          case "photomini":
            size = "118c81";
            break;
          case "photo":
            size = "600c400";
            break;
        }

        // x - width / height
        char[] separator = new char[] { 'x' };
        //r metrics = size.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        var metrics = size.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        if (metrics.Count() == 1)
        {
          // m - maxwidth / maxheight
          char[] sepa = { 'm' };
          metrics = size.Split(sepa, StringSplitOptions.RemoveEmptyEntries);

          if (metrics.Count() == 1)
          {
            // c - width / height / crop
            char[] sep = { 'c' };
            metrics = size.Split(sep, StringSplitOptions.RemoveEmptyEntries);
          }
        }

        if (metrics.Count() == 2 && int.TryParse(metrics[0], out width) && int.TryParse(metrics[1], out height))
        {
          var fullSizeImagePath = Path.Combine(staticPath, imageFileName);

          if (bulk != null && id != null)
          {
            fullSizeImagePath = Path.Combine(staticPath, bulk, id, imageFileName);
          }

          if (System.IO.File.Exists(fullSizeImagePath))
          {
            if (Core.CreateImage(fullSizeImagePath, picturePath, width, height, separator[0].ToString()))
            {
              return File(picturePath, "images/jpeg");
            }
          }
        }

        // no-image
        staticPath = Server.MapPath("~/" + application);
        var noImageFolderPath = Path.Combine(staticPath, "NoImage");
        var noImagePath = Path.Combine(noImageFolderPath, "no_image.jpg");
        if (!System.IO.File.Exists(noImagePath))
          return null;

        var noImageThumbnailPath = Path.Combine(noImageFolderPath, size, "no_image.jpg");
        if (!System.IO.File.Exists(noImageThumbnailPath))
        {
          if (Core.CreateImage(noImagePath, noImageThumbnailPath, width, height, separator.ToString()))
          {
            return File(noImageThumbnailPath, "images/jpeg");
          }
        }
        else
        {
          return File(noImageThumbnailPath, "images/jpeg");
        }

        return File(noImagePath, "images/jpeg");
      }
      return File(picturePath, "images/jpeg");

    }


    public string ProcessImageReturnPath(string fileName)
    {
      var staticPath = Server.MapPath("~/uploads");
      var parts = fileName.Split('/').ToList();
      var size = parts.ElementAt(parts.Count - 2);
      var picturePath = Path.Combine(staticPath, parts.ElementAt(parts.Count - 4), parts.ElementAt(parts.Count - 3), size, parts.ElementAt(parts.Count - 1));

      if (!System.IO.File.Exists(picturePath))
      {
        int width = 0;
        int height = 0;

        // x - width / height
        char[] separator =new char[]{'x'};
        var metrics = size.Split(separator, StringSplitOptions.RemoveEmptyEntries);

        if (metrics.Count() == 1)
        {
          // m - maxwidth / maxheight
          separator = new char[]{'m'};
          metrics = size.Split(separator, StringSplitOptions.RemoveEmptyEntries);

          if (metrics.Count() == 1)
          {
            // c - width / height / crop
            separator = new char[]{'c'};
            metrics = size.Split(separator, StringSplitOptions.RemoveEmptyEntries);
          }
        }

        if (metrics.Count() == 2 && int.TryParse(metrics[0], out width) && int.TryParse(metrics[1],out height))
        {
          var fullSizeImagePath = Path.Combine(staticPath, parts.ElementAt(parts.Count - 4), parts.ElementAt(parts.Count - 3), parts.ElementAt(parts.Count - 1));
          if (System.IO.File.Exists(fullSizeImagePath))
          {
            if (Core.CreateImage(fullSizeImagePath, picturePath, width, height, separator.ToString()))
            {
              return picturePath;
            }
          }
        }

        // no-image
        var noImageFolderPath = Path.Combine(staticPath, "NoImage");
        var noImagePath = Path.Combine(noImageFolderPath, "no_image.jpg");
        if (!System.IO.File.Exists(noImagePath))
          return null;

        var noImageThumbnailPath = Path.Combine(noImageFolderPath, size, "no_image.jpg");
        if (!System.IO.File.Exists(noImageThumbnailPath))
        {
          if (Core.CreateImage(noImagePath, noImageThumbnailPath, width, height, separator.ToString()))
          {
            return noImageThumbnailPath;
          }
        }
        return noImagePath;
      }

      return picturePath;
    }

    public ActionResult ProcessImage(string fileName, string application)
    {
      var staticPath = Server.MapPath("~/" + application);
      var parts = fileName.Split('/').ToList();
      var size = parts.ElementAt(parts.Count - 2);
      var picturePath = Path.Combine(staticPath, parts.ElementAt(parts.Count - 4), parts.ElementAt(parts.Count - 3), size, parts.ElementAt(parts.Count - 1));

      if (!System.IO.File.Exists(picturePath))
      {
        int width = 0;
        int height = 0;

        // x - width / height
        char[] separator = new char[]{'x'};
        var metrics = size.Split(separator, StringSplitOptions.RemoveEmptyEntries);

        if (metrics.Count() == 1)
        {
          // m - maxwidth / maxheight
          separator = new char[]{'m'};
          metrics = size.Split(separator, StringSplitOptions.RemoveEmptyEntries);

          if (metrics.Count() == 1)
          {
            // c - width / height / crop
            separator = new char[]{'c'};
            metrics = size.Split(separator, StringSplitOptions.RemoveEmptyEntries);
          }
        }

        if (metrics.Count() == 2 && int.TryParse(metrics[0], out width) && int.TryParse(metrics[1], out height))
        {
          var fullSizeImagePath = Path.Combine(staticPath, parts.ElementAt(parts.Count - 4), parts.ElementAt(parts.Count - 3), parts.ElementAt(parts.Count - 1));
          if (System.IO.File.Exists(fullSizeImagePath))
          {
            if (Core.CreateImage(fullSizeImagePath, picturePath, width, height, separator.ToString()))
            {
              return File(picturePath, "images/jpeg");
            }
          }
        }

        // no-image
        var noImageFolderPath = Path.Combine(staticPath, "NoImage");
        var noImagePath = Path.Combine(noImageFolderPath, "no_image.jpg");
        if (!System.IO.File.Exists(noImagePath))
          return null;

        var noImageThumbnailPath = Path.Combine(noImageFolderPath, size, "no_image.jpg");
        if (!System.IO.File.Exists(noImageThumbnailPath))
        {
          if (Core.CreateImage(noImagePath, noImageThumbnailPath, width, height, separator.ToString()))
          {
            return File(noImageThumbnailPath, "images/jpeg");
          }
        }
        else
        {
          return File(noImageThumbnailPath, "images/jpeg");
        }

        return File(noImagePath, "images/jpeg");
      }

      return File(picturePath, "images/jpeg");
    }



  }

}










