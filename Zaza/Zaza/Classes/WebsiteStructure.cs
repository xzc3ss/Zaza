using System;
using System.Collections.Generic;
using System.Web.Routing;

namespace Zaza.Classes
{
	public class WebsiteStructure
	{
		public enum WebsiteModule
		{
		}
		public enum WebsitePage
		{
		}

		#region Classes

		public class WebsiteModuleMetadata
		{
			public WebsiteModule WebsiteModule;
			public string Action;
			public string Controller;
			public string Title;
			public string Area;
			public Boolean RenderAsDropDown = false;
			public Boolean IsCurrentModule = false;

			public WebsiteModuleMetadata(WebsiteModule myModule, string title, string area, string controller, string action, Boolean renderAsDropDown = false)
			{
				this.WebsiteModule = myModule;
				this.Action = action;
				this.Controller = title;
				this.Area = area;
				this.RenderAsDropDown = renderAsDropDown;
			}

			//Me.WebsiteModule = myModule

		}

		public class WebsitePageMetadata
		{
			public WebsitePage WebsitePage;
			public string Action;
			public string Controller;
			public string Area;
			public string Title;
			public Boolean RenderSubmenu;
			public IEnumerable<WebsitePageMetadata> SubMenuMetadata;
			public RouteValueDictionary RouteValues;
			public object RouteValueSources;
			public Boolean MustRender = true;
			public string HighlightedMenuAction
			{
				get;
				set;
			}
			public string HighlightedMenuController
			{
				get;
				set;
			}

			public WebsitePageMetadata(WebsitePage page, string title, string area, string controller, string action, Boolean renderSubmenu = false, IEnumerable<WebsitePageMetadata> subMenuMetadata = null,
																 object routeValueSources = null, string highlightedMenuController = null, string highlightedMenuAction = null)
			{
				this.WebsitePage = page;
				this.Action = action;
				this.Controller = controller;
				this.Area = area;
				this.Title = title;
				this.RenderSubmenu = renderSubmenu;
				this.SubMenuMetadata = subMenuMetadata;
				this.RouteValueSources = routeValueSources;
				this.HighlightedMenuController = highlightedMenuController;
				this.HighlightedMenuAction = highlightedMenuAction;

			}
		}

		public class WebsiteModulePages
		{

			public WebsiteModule WebsiteModule;
			public new List<WebsitePage> WebsitePages;

			public WebsiteModulePages(WebsiteModule myModule)
			{
				this.WebsiteModule = myModule;
			}
		}

		public class WebsiteModulePagesMetadata
		{
			public WebsiteModuleMetadata WebsiteModuleMetadata;
			public new List<WebsitePageMetadata> WebsitePagesMetadata;
			public WebsiteModulePagesMetadata(WebsiteModuleMetadata myModuleMetadata)
			{
				this.WebsiteModuleMetadata = myModuleMetadata;
			}


		}

		#endregion

		#region Methods

		private void SetIdentityAllowedPages(List<WebsitePage> allowedPages)
		{
			List<WebsiteModulePages> websiteStructure;
			List<WebsiteModuleMetadata> modulesMetadata;
			List<WebsitePageMetadata> pagesMetadata;
		}
		//Private Sub SetIdentityAllowedPages(ByVal allowedPages As List(Of WebsitePage))
		//    ' clean the allowed pages
		//    AutoccasionIdentity.Current.AllowedModulePages.Clear()

		//    Dim websiteStructure As List(Of WebsiteModulePages) = GetWebsiteStructure()
		//    Dim modulesMetadata As List(Of WebsiteModuleMetadata) = GetWebsiteModulesMetadata()
		//    Dim pagesMetadata As List(Of WebsitePageMetadata) = GetWebsitePagesMetadata()

		//    For Each modulePages As WebsiteModulePages In websiteStructure
		//      ' get the allowed pages for the current module
		//      Dim pages = modulePages.WebsitePages.Intersect(allowedPages).ToList

		//      If pages.Count > 0 Then
		//        ' find the module metadata
		//        Dim moduleMetadata As WebsiteModulePagesMetadata = Nothing
		//        For Each myModuleMetadata In modulesMetadata
		//          If myModuleMetadata.WebsiteModule = modulePages.WebsiteModule Then
		//            moduleMetadata = New WebsiteModulePagesMetadata(myModuleMetadata)

		//            ' add the pages metadata
		//            For Each allowedPage In pages
		//              For Each myPageMetadata In pagesMetadata
		//                If allowedPage = myPageMetadata.WebsitePage Then
		//                  moduleMetadata.WebsitePagesMetadata.Add(myPageMetadata)
		//                  Exit For
		//                End If
		//              Next
		//            Next

		//            Exit For
		//          End If
		//        Next

		//        ' add the found allowed module and pages to the identity object
		//        AutoccasionIdentity.Current.AllowedModulePages.Add(moduleMetadata)
		//      End If
		//    Next
		//  End Sub
		#endregion

		#region Metadata and Structure
		public List<WebsiteModuleMetadata> GetWebsiteModulesMetadata()
		{
			List<WebsiteModuleMetadata> websiteModulesMetadata = new List<WebsiteModuleMetadata>();
			return websiteModulesMetadata;
		}

		// Public Function GetWebsitePagesMetadata() As List(Of WebsitePageMetadata)
		//  Dim websitePagesMetadata As New List(Of WebsitePageMetadata)

		//  'websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.Home, "Home", "", "Home", "Index"))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.Search, "Search", "", "Search", "Index"))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.Home, HttpContext.GetGlobalResourceObject("Buttons", "Home"), "Admin", "Home", "Index"))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.TranslateInterface, HttpContext.GetGlobalResourceObject("Buttons", "TranslateInterface"), "Admin", "TranslateInterface", "Edit"))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.Customers, "Customers", "Admin", "Customers", "List"))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.Providers, "Providers", "Admin", "Providers", "List"))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.Items, HttpContext.GetGlobalResourceObject("Buttons", "Items"), "Admin", "Items", "List"))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.Profile, HttpContext.GetGlobalResourceObject("Buttons", "Profile"), "Admin", "Profile", "Index"))

		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.News, "News", "Admin", "News", "List"))

		//  Dim mappingSubmenu As New List(Of WebsitePageMetadata)
		//  mappingSubmenu.Add(WebsitePageMetadata.BuildSubmenuItem(HttpContext.GetGlobalResourceObject("Buttons", "MapBodyworks"), "Admin", "Mappings", "MapBodyworks", Nothing))
		//  mappingSubmenu.Add(WebsitePageMetadata.BuildSubmenuItem(HttpContext.GetGlobalResourceObject("Buttons", "MapColors"), "Admin", "Mappings", "MapColors", Nothing))
		//  mappingSubmenu.Add(WebsitePageMetadata.BuildSubmenuItem(HttpContext.GetGlobalResourceObject("Buttons", "MapFuelTypes"), "Admin", "Mappings", "MapFuelTypes", Nothing))
		//  mappingSubmenu.Add(WebsitePageMetadata.BuildSubmenuItem(HttpContext.GetGlobalResourceObject("Buttons", "MapInteriors"), "Admin", "Mappings", "MapInteriors", Nothing))
		//  mappingSubmenu.Add(WebsitePageMetadata.BuildSubmenuItem(HttpContext.GetGlobalResourceObject("Buttons", "MapTransmissions"), "Admin", "Mappings", "MapTransmissions", Nothing))
		//  mappingSubmenu.Add(WebsitePageMetadata.BuildSubmenuItem(HttpContext.GetGlobalResourceObject("Buttons", "MapOptions"), "Admin", "Mappings", "MapOptions", Nothing))
		//  mappingSubmenu.Add(WebsitePageMetadata.BuildSubmenuItem(HttpContext.GetGlobalResourceObject("Buttons", "MapModels"), "Admin", "Mappings", "MapModels", Nothing))
		//  ''submenupages
		//  'Admin - Mappings
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.MapBodyworks, HttpContext.GetGlobalResourceObject("Buttons", "MapBodyworks"), "admin", "Mappings", "MapBodyworks", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.MapColors, HttpContext.GetGlobalResourceObject("Buttons", "MapColors"), "admin", "Mappings", "MapColors", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.MapFuelTypes, HttpContext.GetGlobalResourceObject("Buttons", "MapFuelTypes"), "admin", "Mappings", "MapFuelTypes", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.MapInteriors, HttpContext.GetGlobalResourceObject("Buttons", "MapInteriors"), "admin", "Mappings", "MapInteriors", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.MapTransmissions, HttpContext.GetGlobalResourceObject("Buttons", "MapTransmissions"), "admin", "Mappings", "MapTransmissions", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.MapOptions, HttpContext.GetGlobalResourceObject("Buttons", "MapOptions"), "admin", "Mappings", "MapOptions", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.MapModels, HttpContext.GetGlobalResourceObject("Buttons", "MapModels"), "admin", "Mappings", "MapModels", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.Mappings, "Mappings", "Admin", "Mappings", "MapBodyworks", True, mappingSubmenu))

		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.Profile, HttpContext.GetGlobalResourceObject("Buttons", "Profile"), "Admin", "Profile", "List"))
		//  'submenu()
		//  Dim vehiclesSubMenuData As New List(Of WebsitePageMetadata)
		//  vehiclesSubMenuData.Add(WebsitePageMetadata.BuildSubmenuItem(HttpContext.GetGlobalResourceObject("Buttons", "VehicleModels"), "admin", "Vehicle", "ModelsList", Nothing))
		// ing))
		//  ''submenupages
		//  'Admin - Vehicle
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.BrandsList, HttpContext.GetGlobalResourceObject("Buttons", "VehicleBrands"), "admin", "Vehicle", "BrandsList", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.BodyworksList, HttpContext.GetGlobalResourceObject("Buttons", "VehicleBodyworks"), "admin", "Vehicle", "BodyworksList", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.ColorsList, HttpContext.GetGlobalResourceObject("Buttons", "VehicleColors"), "admin", "Vehicle", "ColorsList", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.ModelsList, HttpContext.GetGlobalResourceObject("Buttons", "VehicleModels"), "admin", "Vehicle", "ModelsList", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.TransmissionsList, HttpContext.GetGlobalResourceObject("Buttons", "VehicleTransmissions"), "admin", "Vehicle", "TransmissionsList", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.FuelTypesList, HttpContext.GetGlobalResourceObject("Buttons", "VehicleFuels"), "admin", "Vehicle", "FuelTypesList", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.InteriorsList, HttpContext.GetGlobalResourceObject("Buttons", "VehicleInteriors"), "admin", "Vehicle", "InteriorsList", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.OptionsList, HttpContext.GetGlobalResourceObject("Buttons", "VehicleOptions"), "admin", "Vehicle", "OptionsList", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.Vehicle, "Vehicle", "Admin", "Vehicle", "FuelTypesList", True, vehiclesSubMenuData))


		//  Dim euroTaxsubmenu As New List(Of WebsitePageMetadata)
		//  euroTaxsubmenu.Add(WebsitePageMetadata.BuildSubmenuItem(HttpContext.GetGlobalResourceObject("Buttons", "EuroTaxModels"), "Admin", "EuroTax", "Models", Nothing))
		//  euroTaxsubmenu.Add(WebsitePageMetadata.BuildSubmenuItem(HttpContext.GetGlobalResourceObject("Buttons", "EuroTaxBodyworks"), "Admin", "EuroTax", "Bodyworks", Nothing))
		//  euroTaxsubmenu.Add(WebsitePageMetadata.BuildSubmenuItem(HttpContext.GetGlobalResourceObject("Buttons", "EuroTaxFuels"), "Admin", "EuroTax", "Fuels", Nothing))
		//  euroTaxsubmenu.Add(WebsitePageMetadata.BuildSubmenuItem(HttpContext.GetGlobalResourceObject("Buttons", "EuroTaxTransmissions"), "Admin", "EuroTax", "Transmissions", Nothing))
		//  euroTaxsubmenu.Add(WebsitePageMetadata.BuildSubmenuItem(HttpContext.GetGlobalResourceObject("Buttons", "EuroTaxVersions"), "Admin", "EuroTax", "Versions", Nothing))
		//  euroTaxsubmenu.Add(WebsitePageMetadata.BuildSubmenuItem(HttpContext.GetGlobalResourceObject("Buttons", "EuroTaxOptions"), "Admin", "EuroTax", "Options", Nothing))


		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.Models, HttpContext.GetGlobalResourceObject("Buttons", "EuroTaxModels"), "admin", "EuroTax", "Models", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.Bodyworks, HttpContext.GetGlobalResourceObject("Buttons", "EuroTaxBodyworks"), "admin", "EuroTax", "Bodyworks", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.Fuels, HttpContext.GetGlobalResourceObject("Buttons", "EuroTaxFuels"), "admin", "EuroTax", "Fuels", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.Transmissions, HttpContext.GetGlobalResourceObject("Buttons", "EuroTaxTransmissions"), "admin", "EuroTax", "Transmissions", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.Options, HttpContext.GetGlobalResourceObject("Buttons", "EuroTaxOptions"), "admin", "EuroTax", "Options", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.Versions, HttpContext.GetGlobalResourceObject("Buttons", "EuroTaxVersions"), "admin", "EuroTax", "Versions", True))

		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.EuroTax, "EuroTax", "Admin", "EuroTax", "Bodyworks", True, euroTaxsubmenu))

		//  Dim configSubmenu As New List(Of WebsitePageMetadata)
		//  configSubmenu.Add(WebsitePageMetadata.BuildSubmenuItem(HttpContext.GetGlobalResourceObject("Buttons", "Export"), "Admin", "Export", "Index", Nothing))
		//  configSubmenu.Add(WebsitePageMetadata.BuildSubmenuItem(HttpContext.GetGlobalResourceObject("Buttons", "Automaticmail"), "Admin", "Configuration", "AutomaticMailResponse", Nothing))
		//  configSubmenu.Add(WebsitePageMetadata.BuildSubmenuItem(HttpContext.GetGlobalResourceObject("Buttons", "Banners"), "Admin", "Configuration", "ListBanners", Nothing))

		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.Export, HttpContext.GetGlobalResourceObject("Buttons", "Export"), "Admin", "Export", "Index", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.AutomaticMailResponse, HttpContext.GetGlobalResourceObject("Buttons", "Automaticmail"), "Admin", "Configuration", "AutomaticMailResponse", True))
		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.Banners, HttpContext.GetGlobalResourceObject("Buttons", "Banners"), "Admin", "Configuration", "ListBanners", True))

		//  websitePagesMetadata.Add(New WebsitePageMetadata(WebsitePage.AutomaticMailResponse, "Configuration", "Admin", "Configuration", "AutomaticMailResponse", True, configSubmenu))

		//  Return websitePagesMetadata
		//End Function

		public void BuildAllowedPages()
		{
			List<WebsitePage> allowedPages;
		}


		public void BuildAdminAllowedPages()
		{
			List<WebsitePage> allowedPages;
		}
		public void BuildSuperuserAllowedPages()
		{
			List<WebsitePage> allowedPages;
		}


		//Public Sub BuildAllowedPages()
		//  Dim allowedPages As New List(Of WebsitePage)

		//  ' create a subset of the website structure containing only allowed pages
		//  allowedPages.Add(WebsitePage.Home)
		//  allowedPages.Add(WebsitePage.Search)

		//  ' set the allowed pages with metadata to the current identity
		//  SetIdentityAllowedPages(allowedPages)
		//End Sub

		//Public Sub BuildAdminAllowedPages()
		//  Dim allowedPages As New List(Of WebsitePage)
		//  allowedPages.Add(WebsitePage.Home)
		//  allowedPages.Add(WebsitePage.Profile)
		//  allowedPages.Add(WebsitePage.Items)

		//  ' set the allowed pages with metadata to the current identity
		//  SetIdentityAllowedPages(allowedPages)
		//End Sub

		//Public Sub BuildSuperUserAllowedPages()
		//  Dim allowedPages As New List(Of WebsitePage)

		//  ' create a subset of the website structure containing only allowed pages
		//  allowedPages.Add(WebsitePage.Home)
		//  'allowedPages.Add(WebsitePage.Profile)
		//  allowedPages.Add(WebsitePage.Customers)
		//  allowedPages.Add(WebsitePage.News)

		//  'allowedPages.Add(WebsitePage.Mappings)
		//  'mappingsubmenu
		//  allowedPages.Add(WebsitePage.MapBodyworks)
		//  allowedPages.Add(WebsitePage.MapColors)
		//  allowedPages.Add(WebsitePage.MapFuelTypes)
		//  allowedPages.Add(WebsitePage.MapInteriors)
		//  allowedPages.Add(WebsitePage.MapTransmissions)
		//  allowedPages.Add(WebsitePage.MapOptions)
		//  allowedPages.Add(WebsitePage.MapModels)
		//  'vehicle
		//  'allowedPages.Add(WebsitePage.Vehicle)
		//  'vehicle submenu
		//  allowedPages.Add(WebsitePage.BrandsList)
		//  allowedPages.Add(WebsitePage.ModelsList)
		//  allowedPages.Add(WebsitePage.BodyworksList)
		//  allowedPages.Add(WebsitePage.ColorsList)
		//  allowedPages.Add(WebsitePage.FuelTypesList)
		//  allowedPages.Add(WebsitePage.InteriorsList)
		//  allowedPages.Add(WebsitePage.TransmissionsList)
		//  allowedPages.Add(WebsitePage.OptionsList)
		//  allowedPages.Add(WebsitePage.Vehicle)
		//  allowedPages.Add(WebsitePage.Items)
		//  'allowedPages.Add(WebsitePage.Providers)
		//  allowedPages.Add(WebsitePage.TranslateInterface)

		//  allowedPages.Add(WebsitePage.EuroTax)
		//  allowedPages.Add(WebsitePage.Models)
		//  allowedPages.Add(WebsitePage.Bodyworks)
		//  allowedPages.Add(WebsitePage.Fuels)
		//  allowedPages.Add(WebsitePage.Transmissions)
		//  allowedPages.Add(WebsitePage.Options)
		//  allowedPages.Add(WebsitePage.Versions)
		//  'allowedPages.Add(WebsitePage.Export)

		//  allowedPages.Add(WebsitePage.AutomaticMailResponse)
		//  allowedPages.Add(WebsitePage.Export)
		//  allowedPages.Add(WebsitePage.Banners)
		//  ' set the allowed pages with metadata to the current identity
		//  SetIdentityAllowedPages(allowedPages)
		//End Sub

		//Public Function GetWebsiteStructure() As List(Of WebsiteModulePages)
		//  Dim websiteStructure As New List(Of WebsiteModulePages)
		//  Dim modulePages As WebsiteModulePages
		//  ' home tab
		//  modulePages = New WebsiteModulePages(WebsiteModule.Home)
		//  modulePages.WebsitePages.Add(WebsitePage.Home)
		//  websiteStructure.Add(modulePages)

		//  ' search tab
		//  modulePages = New WebsiteModulePages(WebsiteModule.Search)
		//  modulePages.WebsitePages.Add(WebsitePage.Search)
		//  websiteStructure.Add(modulePages)

		//  ' TranslateInterface tab
		//  modulePages = New WebsiteModulePages(WebsiteModule.TranslateInterface)
		//  modulePages.WebsitePages.Add(WebsitePage.TranslateInterface)
		//  websiteStructure.Add(modulePages)

		//  ' Profile tab
		//  modulePages = New WebsiteModulePages(WebsiteModule.Profile)
		//  modulePages.WebsitePages.Add(WebsitePage.Profile)
		//  websiteStructure.Add(modulePages)

		//  ' Customers tab
		//  modulePages = New WebsiteModulePages(WebsiteModule.Customers)
		//  modulePages.WebsitePages.Add(WebsitePage.Customers)
		//  websiteStructure.Add(modulePages)

		//  ' News tab
		//  modulePages = New WebsiteModulePages(WebsiteModule.News)
		//  modulePages.WebsitePages.Add(WebsitePage.News)
		//  websiteStructure.Add(modulePages)

		//  ' Items tab
		//  modulePages = New WebsiteModulePages(WebsiteModule.Items)
		//  modulePages.WebsitePages.Add(WebsitePage.Items)
		//  websiteStructure.Add(modulePages)

		//  modulePages = New WebsiteModulePages(WebsiteModule.Mappings)
		//  modulePages.WebsitePages.Add(WebsitePage.MapBodyworks)
		//  modulePages.WebsitePages.Add(WebsitePage.MapColors)
		//  modulePages.WebsitePages.Add(WebsitePage.MapFuelTypes)
		//  modulePages.WebsitePages.Add(WebsitePage.MapInteriors)
		//  modulePages.WebsitePages.Add(WebsitePage.MapTransmissions)
		//  modulePages.WebsitePages.Add(WebsitePage.MapOptions)
		//  modulePages.WebsitePages.Add(WebsitePage.MapModels)
		//  websiteStructure.Add(modulePages)

		//  ' Providers tab
		//  modulePages = New WebsiteModulePages(WebsiteModule.Providers)
		//  modulePages.WebsitePages.Add(WebsitePage.Providers)
		//  websiteStructure.Add(modulePages)


		//  'Vehicle stuff
		//  modulePages = New WebsiteModulePages(WebsiteModule.Vehicle)
		//  modulePages.WebsitePages.Add(WebsitePage.FuelTypesList)
		//  modulePages.WebsitePages.Add(WebsitePage.ModelsList)
		//  modulePages.WebsitePages.Add(WebsitePage.BrandsList)
		//  modulePages.WebsitePages.Add(WebsitePage.BodyworksList)
		//  modulePages.WebsitePages.Add(WebsitePage.TransmissionsList)
		//  modulePages.WebsitePages.Add(WebsitePage.ColorsList)
		//  modulePages.WebsitePages.Add(WebsitePage.InteriorsList)
		//  modulePages.WebsitePages.Add(WebsitePage.OptionsList)
		//  websiteStructure.Add(modulePages)

		//  modulePages = New WebsiteModulePages(WebsiteModule.EuroTax)
		//  modulePages.WebsitePages.Add(WebsitePage.Models)
		//  modulePages.WebsitePages.Add(WebsitePage.Bodyworks)
		//  modulePages.WebsitePages.Add(WebsitePage.Fuels)
		//  modulePages.WebsitePages.Add(WebsitePage.Transmissions)
		//  modulePages.WebsitePages.Add(WebsitePage.Options)
		//  modulePages.WebsitePages.Add(WebsitePage.Versions)
		//  websiteStructure.Add(modulePages)

		//  'modulePages = New WebsiteModulePages(WebsiteModule.Export)
		//  'modulePages.WebsitePages.Add(WebsitePage.Export)
		//  'websiteStructure.Add(modulePages)

		//  modulePages = New WebsiteModulePages(WebsiteModule.Configuration)
		//  modulePages.WebsitePages.Add(WebsitePage.Export)
		//  modulePages.WebsitePages.Add(WebsitePage.AutomaticMailResponse)
		//  modulePages.WebsitePages.Add(WebsitePage.Banners)
		//  websiteStructure.Add(modulePages)

		//  Return websiteStructure
		//End Function



		#endregion

	}
}