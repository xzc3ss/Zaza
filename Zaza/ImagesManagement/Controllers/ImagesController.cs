using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ImagesManagement
{
  public class ImagesController : Controller
  {
    //
    // GET: /Images/

    public ActionResult Index()
    {
      return View();
    }

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
    }


  }
}



//Imports System.IO

//Namespace MaxipressImagesManagement
//  Public Class ImagesController
//    Inherits System.Web.Mvc.Controller

//    ' resize / crop image


//    Public Function ProcessOriginalImageUsingRoute(ByVal application As String, ByVal dir As String, ByVal bulk As String, ByVal id As String, ByVal size As String, ByVal mobile As String, ByVal imageFileName As String) As ActionResult
//      Dim staticPath = Server.MapPath("~/" & application)
//      ' replace old server.urlencoding when space = +
//      imageFileName = imageFileName.Replace("+", " ")

//      Dim picturePath = Path.Combine(staticPath, size, imageFileName)

//      If dir IsNot Nothing Then
//        staticPath = Server.MapPath("~/" & application & "/" & dir)
//        picturePath = Path.Combine(staticPath, size, imageFileName)
//      End If

//      If bulk IsNot Nothing AndAlso id IsNot Nothing Then
//        picturePath = Path.Combine(staticPath, bulk, id, size, imageFileName)
//      End If

//      ' for mobile apps we get image from database using order from url
//      If Not String.IsNullOrEmpty(mobile) Then
//        Dim images As New List(Of String)
//        If application = "Auto" Then
//          Dim dc As New AutoccasionEntity.AutoccasionEntities
//          images = (From c In dc.ItemMedias
//                    Where c.ItemID = id AndAlso Not c.Deleted Order By c.Order, c.AddedDate Select c.FileName).ToList
//        End If

//        If application = "Immo" Then
//          Dim dc As New AutoccasionEntity.ImmoTransit.ImmoTransitEntities
//          images = (From c In dc.AdPhotos
//                    Where c.AdID = id AndAlso Not c.Deleted Order By c.Order, c.AddedDate Select c.FileName).ToList
//        End If

//        If images.Count > 0 Then
//          Dim alphaChars = "abcdefghijklmnopqrstuvwxyz"
//          Dim index As Integer = 1
//          Dim separator As Int16 = imageFileName.IndexOf("_")
//          If separator < 0 Then
//            separator = imageFileName.IndexOf(".")
//          End If

//          Integer.TryParse(imageFileName.Substring(0, separator), index)

//          If index = 0 Then
//            index = alphaChars.IndexOf(imageFileName.Substring(0, separator))
//          End If

//          If index = 0 Then
//            index = 1
//          End If

//          If index > 0 AndAlso index <= images.Count Then
//            picturePath = Path.Combine(staticPath, bulk, id, size, mobile, imageFileName)
//            imageFileName = images.Item(index - 1)
//            If Not imageFileName.IndexOf(".jpg") > 0 Then
//              imageFileName = imageFileName & ".jpg"
//            End If
//          End If
//        End If
//      End If

//      If Not System.IO.File.Exists(picturePath) Then
//        Dim width As Integer = 0, height As Integer = 0

//        ' fix for mobile apps
//        Select Case size
//          Case "photomini"
//            size = "118c81"
//          Case "photo"
//            size = "600c400"
//        End Select

//        ' x - width / height
//        Dim separator As Char() = "x"
//        Dim metrics = size.Split(separator, StringSplitOptions.RemoveEmptyEntries)

//        If metrics.Count = 1 Then
//          ' m - maxwidth / maxheight
//          separator = "m"
//          metrics = size.Split(separator, StringSplitOptions.RemoveEmptyEntries)

//          If metrics.Count = 1 Then
//            ' c - width / height / crop
//            separator = "c"
//            metrics = size.Split(separator, StringSplitOptions.RemoveEmptyEntries)
//          End If
//        End If

//        If metrics.Count = 2 AndAlso Integer.TryParse(metrics(0), width) AndAlso Integer.TryParse(metrics(1), height) Then
//          Dim fullSizeImagePath = Path.Combine(staticPath, imageFileName)

//          If bulk IsNot Nothing AndAlso id IsNot Nothing Then
//            fullSizeImagePath = Path.Combine(staticPath, bulk, id, imageFileName)
//          End If

//          If System.IO.File.Exists(fullSizeImagePath) Then
//            If Core.CreateImage(fullSizeImagePath, picturePath, width, height, separator) Then
//              Return File(picturePath, "images/jpeg")
//            End If
//          End If
//        End If

//        ' no-image
//        staticPath = Server.MapPath("~/" & application)
//        Dim noImageFolderPath = Path.Combine(staticPath, "NoImage")
//        Dim noImagePath = Path.Combine(noImageFolderPath, "no_image.jpg")
//        If Not System.IO.File.Exists(noImagePath) Then Return Nothing

//        Dim noImageThumbnailPath = Path.Combine(noImageFolderPath, size, "no_image.jpg")
//        If Not System.IO.File.Exists(noImageThumbnailPath) Then
//          If Core.CreateImage(noImagePath, noImageThumbnailPath, width, height, separator) Then
//            Return File(noImageThumbnailPath, "images/jpeg")
//          End If
//        Else
//          Return File(noImageThumbnailPath, "images/jpeg")
//        End If

//        Return File(noImagePath, "images/jpeg")
//      End If

//      Return File(picturePath, "images/jpeg")
//    End Function

//    Public Function ProcessImage(ByVal fileName As String, ByVal application As String) As ActionResult
//      Dim staticPath = Server.MapPath("~/" & application)
//      Dim parts As List(Of String) = fileName.Split("/").ToList
//      Dim size = parts.ElementAt(parts.Count - 2)
//      Dim picturePath = Path.Combine(staticPath, parts.ElementAt(parts.Count - 4), parts.ElementAt(parts.Count - 3), size, parts.ElementAt(parts.Count - 1))

//      If Not System.IO.File.Exists(picturePath) Then
//        Dim width As Integer = 0, height As Integer = 0

//        ' x - width / height
//        Dim separator As Char() = "x"
//        Dim metrics = size.Split(separator, StringSplitOptions.RemoveEmptyEntries)

//        If metrics.Count = 1 Then
//          ' m - maxwidth / maxheight
//          separator = "m"
//          metrics = size.Split(separator, StringSplitOptions.RemoveEmptyEntries)

//          If metrics.Count = 1 Then
//            ' c - width / height / crop
//            separator = "c"
//            metrics = size.Split(separator, StringSplitOptions.RemoveEmptyEntries)
//          End If
//        End If

//        If metrics.Count = 2 AndAlso Integer.TryParse(metrics(0), width) AndAlso Integer.TryParse(metrics(1), height) Then
//          Dim fullSizeImagePath = Path.Combine(staticPath, parts.ElementAt(parts.Count - 4), parts.ElementAt(parts.Count - 3), parts.ElementAt(parts.Count - 1))
//          If System.IO.File.Exists(fullSizeImagePath) Then
//            If Core.CreateImage(fullSizeImagePath, picturePath, width, height, separator) Then
//              Return File(picturePath, "images/jpeg")
//            End If
//          End If
//        End If

//        ' no-image
//        Dim noImageFolderPath = Path.Combine(staticPath, "NoImage")
//        Dim noImagePath = Path.Combine(noImageFolderPath, "no_image.jpg")
//        If Not System.IO.File.Exists(noImagePath) Then Return Nothing

//        Dim noImageThumbnailPath = Path.Combine(noImageFolderPath, size, "no_image.jpg")
//        If Not System.IO.File.Exists(noImageThumbnailPath) Then
//          If Core.CreateImage(noImagePath, noImageThumbnailPath, width, height, separator) Then
//            Return File(noImageThumbnailPath, "images/jpeg")
//          End If
//        Else
//          Return File(noImageThumbnailPath, "images/jpeg")
//        End If

//        Return File(noImagePath, "images/jpeg")
//      End If

//      Return File(picturePath, "images/jpeg")
//    End Function

//    <HttpPost()>
//    Function ProcessImageReturnPath(ByVal fileName As String) As String
//      Dim staticPath = Server.MapPath("~/uploads")
//      Dim parts As List(Of String) = fileName.Split("/").ToList
//      Dim size = parts.ElementAt(parts.Count - 2)
//      Dim picturePath = Path.Combine(staticPath, parts.ElementAt(parts.Count - 4), parts.ElementAt(parts.Count - 3), size, parts.ElementAt(parts.Count - 1))

//      If Not System.IO.File.Exists(picturePath) Then
//        Dim width As Integer = 0, height As Integer = 0

//        ' x - width / height
//        Dim separator As Char() = "x"
//        Dim metrics = size.Split(separator, StringSplitOptions.RemoveEmptyEntries)

//        If metrics.Count = 1 Then
//          ' m - maxwidth / maxheight
//          separator = "m"
//          metrics = size.Split(separator, StringSplitOptions.RemoveEmptyEntries)

//          If metrics.Count = 1 Then
//            ' c - width / height / crop
//            separator = "c"
//            metrics = size.Split(separator, StringSplitOptions.RemoveEmptyEntries)
//          End If
//        End If

//        If metrics.Count = 2 AndAlso Integer.TryParse(metrics(0), width) AndAlso Integer.TryParse(metrics(1), height) Then
//          Dim fullSizeImagePath = Path.Combine(staticPath, parts.ElementAt(parts.Count - 4), parts.ElementAt(parts.Count - 3), parts.ElementAt(parts.Count - 1))
//          If System.IO.File.Exists(fullSizeImagePath) Then
//            If Core.CreateImage(fullSizeImagePath, picturePath, width, height, separator) Then
//              Return picturePath
//            End If
//          End If
//        End If

//        ' no-image
//        Dim noImageFolderPath = Path.Combine(staticPath, "NoImage")
//        Dim noImagePath = Path.Combine(noImageFolderPath, "no_image.jpg")
//        If Not System.IO.File.Exists(noImagePath) Then Return Nothing

//        Dim noImageThumbnailPath = Path.Combine(noImageFolderPath, size, "no_image.jpg")
//        If Not System.IO.File.Exists(noImageThumbnailPath) Then
//          If Core.CreateImage(noImagePath, noImageThumbnailPath, width, height, separator) Then
//            Return noImageThumbnailPath
//          End If
//        End If
//        Return noImagePath
//      End If

//      Return picturePath
//    End Function

//  End Class
//End Namespace
