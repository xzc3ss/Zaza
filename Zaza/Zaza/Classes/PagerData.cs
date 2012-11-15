using System.Web;
using System.Text;

public class PagerData
{

  public int CurrentPage;
  public int RecordsCount;
  public int RecordsPerPage;
  public bool ShowGoTo;
  public string ActionURL;
  public string ElementHtmlID;

  public static PagerData BuildPagerData(int currentPage, int recordsCount, string elementHtmlId, string pageQueryStringKey = "page", int recordsPerPage = 100, bool showGoTo = false)
  {
    PagerData data = new PagerData();
    dynamic request = HttpContext.Current.Request;

    StringBuilder myQueryString = new StringBuilder();
    int validKeys = 0;
    for (int i = 0; i <= request.QueryString.Keys.Count - 1; i++)
    {
      string key = request.QueryString.Keys[i];

      if (!string.IsNullOrEmpty(key))
      {
        if (validKeys > 0)
          myQueryString.Append("&");

        myQueryString.Append(HttpUtility.UrlEncodeUnicode(key));
        myQueryString.Append("=");

        if (key == pageQueryStringKey)
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
    if (request.QueryString[pageQueryStringKey] == null)
      myQueryString.Append("&" + pageQueryStringKey + "={0}");

    data.ActionURL = request.Url.AbsolutePath + "?" + myQueryString.ToString();
    data.RecordsPerPage = recordsPerPage;
    data.ElementHtmlID = elementHtmlId;
    data.RecordsCount = recordsCount;
    data.CurrentPage = currentPage;
    data.ShowGoTo = showGoTo;

    return data;
  }

}