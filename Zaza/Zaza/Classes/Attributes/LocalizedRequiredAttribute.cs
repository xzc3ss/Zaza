using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Zaza.Classes.Attributes
{
	public class LocalizedRequiredAttribute : RequiredAttribute, IClientValidatable
	{
		private string _classKey;
		private string _resourceKey;

		public LocalizedRequiredAttribute(string classKey, string resourceKey)
		{
			this._classKey = classKey;
			this._resourceKey = resourceKey;
		}

		public override bool IsValid(object value)
		{
			return !(string.IsNullOrEmpty((string)value));
		}

		public override string FormatErrorMessage(string name)
		{
			return (string)HttpContext.GetGlobalResourceObject(_classKey, _resourceKey);
		}

		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{

			return new RequiredAttributeAdapter(metadata, context, this).GetClientValidationRules();
		}
	}
}