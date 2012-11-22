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

    public LocalizedRegularExpression(string classKey, string resourceKey, string pattern)
      : base(pattern)
    {
      this._classKey = classKey;
      this._resourceKey = resourceKey;
    }

    public override string FormatErrorMessage(string name)
    {
      return (string)HttpContext.GetGlobalResourceObject(_classKey, _resourceKey);
    }
    public System.Collections.Generic.IEnumerable<System.Web.Mvc.ModelClientValidationRule> GetClientValidationRules(System.Web.Mvc.ModelMetadata metadata, System.Web.Mvc.ControllerContext context)
    {
      return new RegularExpressionAttributeAdapter(metadata, context, this).GetClientValidationRules();
    }
  }
}