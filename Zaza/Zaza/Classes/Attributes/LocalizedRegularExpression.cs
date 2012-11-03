using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zaza.Classes.Attributes
{
	public class LocalizedRegularExpression : RegularExpressionAttribute, IClientValidatable
	{

		private string _classKey;
		private string _resourceKey;

		public LocalizedRegularExpression(string classKey, string resourceKey, string pattern) : base(pattern)
		{
			this._classKey = classKey;
			this._resourceKey = resourceKey;
		}

		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{

			return new RegularExpressionAttributeAdapter(metadata, context, this).GetClientValidationRules();
		}
	}
}