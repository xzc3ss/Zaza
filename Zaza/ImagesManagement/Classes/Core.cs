using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImagesManagement.Classes
{
  public class Core
  {
  }
}

//Imports ImageResizer
//Imports System.IO

//Module Core

//  Public Function CreateImage(ByVal originalFileName As String, ByVal newFileName As String, ByVal width As Integer, ByVal height As Integer, ByVal separator As String) As Boolean
//    Try
//      Dim r = New ResizeSettings()

//      Select Case separator
//        Case "x"
//          r.Width = width
//          r.Height = height
//        Case "m"
//          r.MaxWidth = width
//          r.MaxHeight = height
//        Case "c"
//          r.Width = width
//          r.Height = height
//          r.CropMode = CropMode.Auto
//      End Select

//      Dim directory As String = newFileName.Substring(0, newFileName.LastIndexOf("\"))
//      If Not IO.Directory.Exists(directory) Then
//        IO.Directory.CreateDirectory(directory)
//      End If

//      ImageBuilder.Current.Build(originalFileName, newFileName, New ResizeSettings(r))

//      Return True
//    Catch ex As Exception

//    End Try

//    Return False
//  End Function

//End Module
