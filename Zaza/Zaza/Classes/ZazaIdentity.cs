using System;
using System.Collections.Generic;

namespace Zaza.Classes
{
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

		public static ZazaIdentity Current
		{
			get
			{
				if (Core.Core.Session["Identity"] == null)
				{
					Core.Core.Session["Identity"] = new ZazaIdentity();
				}
				return (ZazaIdentity)Core.Core.Session["Identity"];
			}
			set { Core.Core.Session["Identity"] = value; }
		}



		//  Public Property AllowedModulePages As New List(Of WebsiteStructure.WebsiteModulePagesMetadata)
		//  Public Property IsSuperUser As Boolean

		//  Private _language As String


		//  Public Property User As Customer







	}
}
