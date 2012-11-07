using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Zaza.Classes.Helpers
{
  public partial class Zaza
  {
    public static MvcHtmlString FirstLetterFilter(string queryStringKey = "firstLetter")
    {
      var request = HttpContext.Current.Request;
      // build querystring
      var myQueryString = new StringBuilder();
      var validKeys = 0;
      for (int j = 0; j <= request.QueryString.Keys.Count - 1; j++)
      {
        string key = request.QueryString.Keys[j];
        if (!string.IsNullOrEmpty(key) & key != "page")
        {
          if (validKeys > 0)
            myQueryString.Append("&");
          myQueryString.Append(HttpUtility.UrlEncodeUnicode(key));
          myQueryString.Append("=");
          if (key == queryStringKey)
          {
            myQueryString.Append("{0}");
          }
          else
          {
            myQueryString.Append(HttpUtility.UrlEncodeUnicode(request.QueryString[key]));
          }
          validKeys += 1;
        }
      }
      string currentQSLetter = string.Empty;
      if (request.QueryString[queryStringKey] == null)
      {
        if (request.QueryString.Count > 0)
        {
          myQueryString.Append("&" + queryStringKey + "={0}");
        }
        else
        {
          myQueryString.Append(queryStringKey + "={0}");
        }
      }
      else
      {
        currentQSLetter = request.QueryString[queryStringKey];
      }

      // html markup
      var divFirstLetter = new TagBuilder("div");
      divFirstLetter.MergeAttribute("class", "first-letter");

      // add description text
      TagBuilder descriptionSpan = new TagBuilder("span");
      descriptionSpan.SetInnerText("FirstLetter");
      descriptionSpan.MergeAttribute("class", "description");
      divFirstLetter.InnerHtml += descriptionSpan.ToString();

      if (string.IsNullOrEmpty(currentQSLetter))
      {
        var allSpan = new TagBuilder("a");
        allSpan.SetInnerText("All");
        allSpan.MergeAttribute("class", "current");
        divFirstLetter.InnerHtml += allSpan.ToString();
      }
      else
      {
        var allLink = new TagBuilder("a");
        allLink.SetInnerText("All");
        allLink.MergeAttribute("href", request.Url.AbsolutePath + "?" + string.Format(myQueryString.ToString(), ""));
        divFirstLetter.InnerHtml += allLink.ToString();
      }

      for (int i = 65; i <= 90; i++)
      {
        var currentLetter = Convert.ToChar(i).ToString();
        if (currentLetter == currentQSLetter)
        {
          var letterSpan = new TagBuilder("a");
          letterSpan.SetInnerText(currentLetter);
          letterSpan.MergeAttribute("class", "current");
          divFirstLetter.InnerHtml += letterSpan.ToString();
        }
        else
        {
          TagBuilder letterLink = new TagBuilder("a");
          letterLink.SetInnerText(currentLetter);
          letterLink.MergeAttribute("href", request.Url.AbsolutePath + "?" + string.Format(myQueryString.ToString(), currentLetter));
          divFirstLetter.InnerHtml += letterLink.ToString();
        }
      }
      return MvcHtmlString.Create(divFirstLetter.ToString());
    }
  }
}