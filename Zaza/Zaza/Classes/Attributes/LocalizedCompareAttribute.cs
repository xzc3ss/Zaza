using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zaza.Classes.Attributes
{
	public class LocalizedCompareAttribute : CompareAttribute
	{
		private string _classKey;
		private string _resourceKey;
		private string _classKeyFirstProperty;
		private string _resourceKeyFirstProperty;
		private string _classKeySecondProperty;
		private string _resourceKeySecondProperty;

		public LocalizedCompareAttribute(string otherProperty, string classKey, string resourceKey, string classKeyFirstProperty, string resourceKeyFirstProperty, string classKeySecondProperty, string resourceKeySecondProperty)
			: base(otherProperty)
		{
			this._classKey = classKey;
			this._resourceKey = resourceKey;
			this._classKeyFirstProperty = classKeyFirstProperty;
			this._resourceKeyFirstProperty = resourceKeyFirstProperty;
			this._classKeySecondProperty = classKeySecondProperty;
			this._resourceKeySecondProperty = resourceKeySecondProperty;
		}

		public override string FormatErrorMessage(string name)
		{
			return string.Format((string)HttpContext.GetGlobalResourceObject(_classKey, _resourceKey),
				HttpContext.GetGlobalResourceObject(_classKeyFirstProperty, _resourceKeyFirstProperty),
				HttpContext.GetGlobalResourceObject(_classKeySecondProperty, _resourceKeySecondProperty));
		}
	}
}