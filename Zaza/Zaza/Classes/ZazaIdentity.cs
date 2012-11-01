using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class ZazaIdentity
{
  public new List<WebsiteStructure.WebsiteModulePagesMetadata> AllowedModulePages
  {
    get;
    set;
  }
  public Boolean IsSuperUser
  {
    get;
    set;
  }

  private string _language;
  public string Language
  {
    get;
    set;
  }

  //public ZazaIdentity Current
  //{
  //  get
  //  {
  //    return _language;
  //  }
  //  //set
  //  //{
  //  //}
  //}

  //               Public Class AutoccasionIdentity

  //  Public Property AllowedModulePages As New List(Of WebsiteStructure.WebsiteModulePagesMetadata)
  //  Public Property IsSuperUser As Boolean

  //  Private _language As String

  //  Public Property Language As String
  //    Get
  //      If _language Is Nothing Then
  //        Return "nl"
  //      Else
  //        Return _language
  //      End If
  //    End Get
  //    Set(ByVal value As String)
  //      _language = value
  //    End Set
  //  End Property

  //  Public Property User As Customer

  //  Public Shared Property Current As AutoccasionIdentity
  //    Get
  //      If Session("Identity") Is Nothing Then
  //        Session("Identity") = New AutoccasionIdentity()
  //      End If
  //      Return Session("Identity")
  //    End Get
  //    Set(ByVal value As AutoccasionIdentity)
  //      Session("Identity") = value
  //    End Set
  //  End Property

  //End Class

}
