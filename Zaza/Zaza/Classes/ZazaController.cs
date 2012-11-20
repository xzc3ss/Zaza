using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zaza.Classes
{
	public class ZazaController : Controller
	{
		
		public WebsiteStructure.WebsitePage CurrentPageAction;
		public List<WebsiteStructure.WebsitePageMetadata> Breadcrumb;

		public void BuildDefaultBreadcrumb()
		{
			var breadcrumbPages = new List<WebsiteStructure.WebsitePageMetadata>();
			dynamic route = this.ControllerContext.RouteData.Values;
			string action = Convert.ToString(route["action"]);
			string controller = Convert.ToString(route["controller"]);
			WebsiteStructure.WebsitePageMetadata previousItem;

			if (ZazaIdentity.Current.AllowedModulePages.Count > 0)
			{
				breadcrumbPages.Add(ZazaIdentity.Current.AllowedModulePages[0].WebsitePagesMetadata[0]);

				foreach (var modulePages in ZazaIdentity.Current.AllowedModulePages)
				{
					foreach (var pageMetadata in modulePages.WebsitePagesMetadata)
					{
						if (pageMetadata.Action.ToLower().Equals(action) && pageMetadata.Controller.ToLower().Equals(controller))
						{
							// add the module by finding the page with the same action and controller as the module
							var metadata = modulePages.WebsiteModuleMetadata;
							foreach (var item in modulePages.WebsitePagesMetadata)
							{
								if (item.Controller == metadata.Controller && item.Action == metadata.Action)
								{
									previousItem = breadcrumbPages[breadcrumbPages.Count - 1];
									if (previousItem.Title != metadata.Title)
									{
										breadcrumbPages.Add(new WebsiteStructure.WebsitePageMetadata(item.WebsitePage, metadata.Title, metadata.Area, metadata.Controller, metadata.Action));
									}
									break;

								}
							}
							//	' add the page
							previousItem = breadcrumbPages[breadcrumbPages.Count - 1];
							if (!String.IsNullOrEmpty(pageMetadata.Title) && previousItem.Title != pageMetadata.Title)
							{
								breadcrumbPages.Add(pageMetadata);
							}

						}
					}

				}

			}

			Breadcrumb = breadcrumbPages;
			ViewData["Breadcrumb"] = Breadcrumb;

		}

		public string GenerateTitleFromBreadcrumb(List<WebsiteStructure.WebsitePageMetadata> currentBreadcrumb)
		{
			string myPageTitle = "Zaza";
			foreach (var item in currentBreadcrumb)
			{
				if (item.WebsitePage!=WebsiteStructure.WebsitePage.Users)
				{
				  myPageTitle += item.Title;
				}
			
			}
			return myPageTitle.Remove(myPageTitle.Length-3);
		}

	}
}




