using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using System.Reflection;
using System.Globalization;

namespace Helpers
{
  public class GridRow
  {
  }
}

//using System.Dynamic;
//using System.Reflection;
//using System.Globalization;

//Namespace Helpers

//  Public Class GridRow
//    Inherits DynamicObject
//    Implements IEnumerable(Of Object)

//#Region " Members "

//    Private Const BindFlags As BindingFlags = BindingFlags.[Public] Or BindingFlags.Instance Or BindingFlags.[Static] Or BindingFlags.IgnoreCase

//    Private _grid As Grid
//    Private _dynamic As DynamicObject
//    Private _rowIndex As Integer
//    Private _value As Object
//    Private _values As IEnumerable(Of Object)

//#End Region

//#Region " Properties "

//    Public ReadOnly Property Value() As Object
//      Get
//        Return _value
//      End Get
//    End Property

//    Public ReadOnly Property Grid() As Grid
//      Get
//        Return _grid
//      End Get
//    End Property

//#End Region

//#Region " Constructor "

//    Public Sub New(ByVal grid As Grid, ByVal value As Object, ByVal rowIndex As Integer)
//      Me._grid = grid
//      Me._value = value
//      Me._rowIndex = rowIndex
//      'Me._dynamic = value
//    End Sub

//#End Region

//#Region " Methods "

//    Public Function ItemValue(ByVal name As String) As Object
//      If [String].IsNullOrEmpty(name) Then
//        Throw New ArgumentException("Argument cannot be null", "name")
//      End If
//      Dim value As Object = Nothing
//      If Not Grid.TryGetDynamicMember(Me, name, value) Then
//        Throw New InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "Column {0} was not found", name))
//      End If
//      Return value
//    End Function

//    Public Function GetEnumerator() As IEnumerator(Of Object) Implements System.Collections.Generic.IEnumerable(Of Object).GetEnumerator
//      If _values Is Nothing Then
//        _values = _grid.ColumnNames.Select(Function(c) Grid.GetDynamicMember(Me, c))
//      End If
//      Return _values.GetEnumerator()
//    End Function

//    Public Function GetEnumerator1() As IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
//      Return GetEnumerator()
//    End Function

//    Public Overrides Function TryGetMember(ByVal binder As GetMemberBinder, ByRef result As Object) As Boolean
//      result = Nothing
//      If Not [String].IsNullOrEmpty(binder.Name) Then
//        If binder.Name = "ROW" Then
//          ' rename?
//          result = _rowIndex
//          Return True
//        End If
//        If _dynamic IsNot Nothing Then
//          Return _dynamic.TryGetMember(binder, result)
//        End If

//        ' support '.' for navigation properties
//        Dim obj As Object = _value
//        Dim names As String() = binder.Name.Split("."c)
//        For i As Integer = 0 To names.Length - 1
//          If (obj Is Nothing) OrElse Not TryGetMember(obj, names(i), result) Then
//            result = Nothing
//            Return False
//          End If
//          obj = result
//        Next
//        Return True
//      End If
//      Return False
//    End Function

//    Public Overrides Function ToString() As String
//      Return _value.ToString()
//    End Function

//    Private Overloads Shared Function TryGetMember(ByVal obj As Object, ByVal name As String, ByRef result As Object) As Boolean
//      Dim [property] As PropertyInfo = obj.[GetType]().GetProperty(name, BindFlags)
//      If ([property] IsNot Nothing) AndAlso ([property].GetIndexParameters().Length = 0) Then
//        result = [property].GetValue(obj, Nothing)
//        Return True
//      End If
//      result = Nothing
//      Return False
//    End Function

//#End Region

//  End Class

//End Namespace