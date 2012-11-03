using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Zaza.Classes.Attributes
{
	public class LocalizedDisplayName : DisplayNameAttribute
	{
		private string _classKey;
		private string _resourceKey;

		public LocalizedDisplayName(string classKey, string resourceKey)
		{
			this._classKey = classKey;
			this._resourceKey = resourceKey;
		}


		public override string DisplayName
		{
			get
			{
				return (string)HttpContext.GetGlobalResourceObject(_classKey, _resourceKey);
			}
		}

	}
}