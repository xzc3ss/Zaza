using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zaza.Classes;

namespace Zaza.Areas.Admin.Controllers
{
  [Authorize(Roles = "superuser")]
  public class UsersController : ZazaController
  {
    //
    // GET: /Admin/Users/

    public ActionResult Index()
    {
      return View();
    }

    public ActionResult List(int? page, string sort, string sortDir, string firstLetter)
    {
      //CurrentPageAction  = WebsiteStructure.WebsitePage.
      return View();
    }

    //Function List(ByVal page As Nullable(Of Integer), ByVal sort As String, ByVal sortDir As String, ByVal firstLetter As String, ByVal province As String, ByVal city As String, ByVal customerName As String, Optional ByVal providerID As Integer = 0) As ActionResult
    //  CurrentPageAction = WebsiteStructure.WebsitePage.Customers
    //  Dim geo = New GeoLocationEntities
    //  Dim pageSize As Integer = 20
    //  Dim pageNumber As Integer = 1
    //  If page.HasValue Then pageNumber = page
    //  Dim customerIds As New List(Of Integer)
    //  ViewData("providerID") = DropDownHelper.GetProviders()
    //  If Request.Form("province") IsNot Nothing Then
    //    province = Request.Form("province")
    //  End If
    //  If Request.Form("city") IsNot Nothing Then
    //    city = Request.Form("city")
    //  End If
    //  If Request.Form("customerName") IsNot Nothing Then
    //    customerName = Request.Form("customerName")
    //  End If

    //  If String.IsNullOrEmpty(sort) Then
    //    sort = "AddedDate"
    //    sortDir = "Desc"
    //    If HttpContext.Request.Cookies.AllKeys.Contains("CustomersSortColumn") AndAlso HttpContext.Request.Cookies.AllKeys.Contains("CustomersSortDir") Then
    //      Dim cookie As HttpCookie = HttpContext.Request.Cookies("CustomersSortColumn")
    //      If Not String.IsNullOrEmpty(cookie.Value) Then
    //        sort = cookie.Value
    //        cookie = HttpContext.Request.Cookies("CustomersSortDir")
    //        sortDir = cookie.Value
    //      End If
    //    End If
    //    Dim rvd As RouteValueDictionary = New RouteValueDictionary()
    //    If providerID <> 0 Then
    //      rvd.Add("providerID", providerID)
    //    End If
    //    rvd.Add("sort", sort)
    //    rvd.Add("sortDir", sortDir)
    //    If Not String.IsNullOrEmpty(province) Then
    //      rvd.Add("province", province)
    //    End If
    //    If Not String.IsNullOrEmpty(city) Then
    //      rvd.Add("city", city)
    //    End If
    //    If Not String.IsNullOrEmpty(customerName) Then
    //      rvd.Add("customerName", customerName)
    //    End If
    //    Return RedirectToAction("List", "Customers", rvd)
    //  Else
    //    Dim cookie As HttpCookie = New HttpCookie("CustomersSortColumn")
    //    cookie.Value = sort
    //    HttpContext.Response.Cookies.Add(cookie)
    //    cookie = New HttpCookie("CustomersSortDir")
    //    cookie.Value = sortDir
    //    HttpContext.Response.Cookies.Add(cookie)
    //  End If
    //  ' get contacts
    //  Dim dc = New AutoccasionEntities()
    //  Dim globalPostalCode As String = String.Empty
    //  Dim query = From c In dc.Customers Where Not c.Deleted AndAlso _
    //              c.ProviderID IsNot Nothing AndAlso _
    //              c.ID <> 1 AndAlso _
    //              If(providerID = 0, True, c.ProviderID = providerID) _
    //              Select New With {c.Name, _
    //                                            .ID = c.ID, _
    //                                            .Email = c.Email, _
    //                                            .AddedDate = c.AddedDate, _
    //                                            c.ProviderID, c.ChangedDate, _
    //                                            .Exported = If(c.NotExported.HasValue, c.NotExported.Value, False), _
    //                                            .ItemsCount = Aggregate x In dc.Items Where x.Customer.ID = c.ID AndAlso Not x.Deleted Into Count()}

    //  If Not String.IsNullOrEmpty(customerName) Then
    //    customerIds = (From i In dc.Customers Where Not i.Deleted AndAlso i.Name = customerName Select i.ID).ToList
    //  Else
    //    If Not String.IsNullOrEmpty(city) Then
    //      Dim startIndex As Integer = city.IndexOf("(")
    //      Dim length As Integer = city.Length
    //      Dim endIndex As Integer = city.IndexOf(")")
    //      Dim postCode As String = String.Empty
    //      If startIndex + 1 + 4 <= length Then
    //        postCode = city.Substring(startIndex + 1, 4)
    //      End If
    //      globalPostalCode = postCode
    //      Dim onlyCity As String = city.Substring(0, startIndex).Trim
    //      customerIds = (From i In dc.CustomerLocations Where Not i.Deleted AndAlso _
    //                     If(providerID = 0, True, If(i.Customer.ProviderID IsNot Nothing, i.Customer.ProviderID = providerID, True)) AndAlso _
    //                     If(onlyCity.ToLower <> String.Empty AndAlso i.City <> String.Empty, i.City = onlyCity.ToLower, False) AndAlso _
    //                     If(postCode <> String.Empty AndAlso i.PostalCode <> String.Empty, i.PostalCode = postCode, False) _
    //                     Select i.CustomerID).ToList
    //    Else
    //      Dim tempPostCodesByRegion = (From i In geo.GeoBELocations Where _
    //                                   i.Region1.Contains(province) OrElse i.Region2.Contains(province) _
    //                                   Order By i.ZIP Select i.ZIP Distinct).ToList

    //      customerIds = (From i In dc.CustomerLocations Where Not i.Deleted AndAlso _
    //                       tempPostCodesByRegion.Contains(i.PostalCode) Select i.CustomerID).ToList
    //    End If
    //  End If

    //  If Not String.IsNullOrEmpty(city) Then
    //    query = From i In query Where _
    //          If(customerIds.Count > 0, customerIds.Contains(i.ID), False)
    //  End If
    //  query = From i In query Where _
    //          If(customerIds.Count > 0, customerIds.Contains(i.ID), True)

    //  ' current page data
    //  If Not String.IsNullOrEmpty(firstLetter) Then
    //    query = From s In query
    //     Where s.Name.ToLower.StartsWith(firstLetter.ToLower)
    //  End If

    //  Dim totalContacts As Integer = query.Count
    //  ViewData("TotalRows") = totalContacts

    //  query = From s In query.OrderBy(sort, sortDir)
    //   Skip (pageNumber - 1) * pageSize
    //   Take (pageSize)

    //  ' translate column headers
    //  Dim columnHeaders As New Dictionary(Of String, String)
    //  columnHeaders.Add("Name", HttpContext.GetGlobalResourceObject("Common", "Name"))
    //  columnHeaders.Add("ExportID", HttpContext.GetGlobalResourceObject("Common", "ExportID"))
    //  columnHeaders.Add("Email", HttpContext.GetGlobalResourceObject("Common", "Email"))
    //  columnHeaders.Add("AddedDate", HttpContext.GetGlobalResourceObject("Common", "AddedDate"))
    //  ' other view data
    //  ViewData("PagerData") = PagerData.BuildPagerData(pageNumber, totalContacts, "pager-goto", "Page", showGoto:=True)

    //  ViewData("ColumnHeaders") = columnHeaders
    //  ViewData("Title") = GenerateTitleFromBreadcrumb(Breadcrumb)

    //  GetCustomersQueryString = Request.Url.ToString
    //  ViewData("customersBack") = GetCustomersQueryString
    //  Return View(query.ToList)
    //End Function
  }
}
