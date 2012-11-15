using System;
using System.Web;
using System.Web.Mvc;
namespace Helpers
{

  public partial class ZazaPager
  {


    public static MvcHtmlString Pager(PagerData data)
    {
      if (data == null)
        return MvcHtmlString.Create(string.Empty);
      int currentPage = data.CurrentPage - 1;
      int visiblePageLinksCount = 10;
      int currentBulk = (int)Math.Ceiling((decimal)(currentPage + 1) / visiblePageLinksCount);
      int lastBulkIndex = (int)Math.Ceiling((decimal)(data.RecordsCount / data.RecordsPerPage) / visiblePageLinksCount);
      if (lastBulkIndex == 0)
      {
        lastBulkIndex = 1;
      }
      int lastPageIndex = (int)Math.Ceiling((decimal)data.RecordsCount / data.RecordsPerPage);
      if (data.ActionURL != null)
      {
        data.ActionURL = data.ActionURL.Replace("?&", "?");
      }



      if (!(lastPageIndex == 0))
      {
        // create the main container
        TagBuilder divPager = new TagBuilder("div");
        divPager.MergeAttribute("class", "pager");

        // create the inner html elements
        TagBuilder spanNav = new TagBuilder("span");
        spanNav.MergeAttribute("class", "spanNav");
        spanNav.SetInnerText(" ");
        divPager.InnerHtml += spanNav.ToString();

        for (int i = 0; i <= lastPageIndex - 1; i++)
        {
          if (i == 0)
          {
            // set the first and previous links
            TagBuilder aFirst = new TagBuilder("a");
            TagBuilder aPrevious = new TagBuilder("a");

            // set the classes, texts and href's
            aFirst.MergeAttribute("class", currentBulk == 1 && currentPage == 0 ? "nav-disabled" : "nav pager-first");
            //Remove "Page=1" from the pager url params so that the pages don't have the same content but with different urls
            aFirst.MergeAttribute("href", currentBulk == 1 && currentPage == 0 ? "#First" : data.ActionURL.Replace("?Page={0}", String.Empty).Replace("&Page={0}", String.Empty));
            aFirst.InnerHtml = "&#160;";


            aPrevious.MergeAttribute("class", currentBulk == 1 && currentPage == 0 ? "nav-disabled" : "nav pager-previous");
            // Remove "Page=1" from the pager url params so that the pages don't have the same content but with different urls

            aPrevious.MergeAttribute("href", currentBulk == 1 && currentPage == 0 ? "#Previous" : string.Format(data.ActionURL, currentPage));
            aPrevious.SetInnerText("");
            divPager.InnerHtml += aFirst.ToString() + aPrevious.ToString();

            //// set the previous bulk link
            //if (currentBulk != 1 && lastBulkIndex > 1)
            //{
            //  TagBuilder aPreviousBulk = new TagBuilder("a");
            //  aPreviousBulk.MergeAttribute("class", "nav");
            //  aPreviousBulk.MergeAttribute("href", string.Format(data.ActionURL, (currentBulk - 1) * visiblePageLinksCount));
            //  aPreviousBulk.SetInnerText("...");
            //  divPager.InnerHtml += aPreviousBulk.ToString();
            //}
          }

          // set the pages links
          if (i == currentPage)
          {
            // set the current page container
            TagBuilder spanCurrentPage = new TagBuilder("span");
            spanCurrentPage.MergeAttribute("class", "current");
            spanCurrentPage.SetInnerText((i + 1).ToString());
            spanNav.InnerHtml += spanCurrentPage.ToString();
          }
          else
          {
            TagBuilder aPageLink = new TagBuilder("a");
            if ((int)Math.Ceiling((decimal)(i + 1) / visiblePageLinksCount) != currentBulk)
            {
              aPageLink.MergeAttribute("class", "not-visible");
            }
            aPageLink.MergeAttribute("href", string.Format(data.ActionURL, i + 1));
            aPageLink.SetInnerText((i + 1).ToString());
            divPager.InnerHtml += aPageLink.ToString();
          }

          // set the next bulk link
          if (i == lastPageIndex - 1)
          {
            if (currentBulk != lastBulkIndex && lastBulkIndex > 1)
            {
              TagBuilder aNextBulk = new TagBuilder("a");
              aNextBulk.MergeAttribute("class", "nav");
              aNextBulk.MergeAttribute("href", string.Format(data.ActionURL, (currentBulk + 1) * visiblePageLinksCount - visiblePageLinksCount + 1));
              aNextBulk.SetInnerText("...");
              divPager.InnerHtml += aNextBulk.ToString();
            }

            // set the next and last links
            TagBuilder aNext = new TagBuilder("a");
            TagBuilder aLast = new TagBuilder("a");

            // set the classes, texts and href's
            aNext.MergeAttribute("class", currentBulk == lastBulkIndex && currentPage == lastPageIndex - 1 ? "nav-disabled" : "nav pager-next");
            aNext.MergeAttribute("href", currentBulk == lastBulkIndex && currentPage == lastPageIndex - 1 ? "#Next" : string.Format(data.ActionURL, currentPage + 2));
            aNext.InnerHtml = "&#160;";
            aLast.MergeAttribute("class", currentBulk == lastBulkIndex && currentPage == lastPageIndex - 1 ? "nav-disabled" : "nav pager-last");
            aLast.MergeAttribute("href", currentBulk == lastBulkIndex && currentPage == lastPageIndex - 1 ? "#Last" : string.Format(data.ActionURL, lastPageIndex));
            aLast.InnerHtml="&#160;";
            divPager.InnerHtml += aNext.ToString() + aLast.ToString();
          }
        }
        divPager.InnerHtml += spanNav.ToString();
        if (data.ShowGoTo)
        {
          var inputGoToPage = new TagBuilder("input");
          inputGoToPage.MergeAttribute("id", "bottom-pager-goto");
          inputGoToPage.MergeAttribute("maxlength", "5");
          inputGoToPage.MergeAttribute("class", "pager-goto field-numeric");
          divPager.InnerHtml += inputGoToPage.ToString(TagRenderMode.SelfClosing);
        }
       

        // create the inner html elements
        var spanPages = new TagBuilder("span");
        spanPages.MergeAttribute("class", "pages");
        divPager.InnerHtml += " ";
        spanPages.SetInnerText((string) HttpContext.GetGlobalResourceObject("SearchForm", "Page"));
      
        var curentPageSpan = new TagBuilder("span");
        curentPageSpan.MergeAttribute("class", "curent-page");
        curentPageSpan.InnerHtml += " ";
        curentPageSpan.InnerHtml += data.CurrentPage.ToString();
        spanPages.InnerHtml += curentPageSpan.ToString();
        spanPages.InnerHtml += " ";
        spanPages.InnerHtml += HttpContext.GetGlobalResourceObject("SearchForm", "PageOf");
        spanPages.InnerHtml += " ";
        spanPages.InnerHtml += lastPageIndex.ToString();
        spanPages.InnerHtml += " ";
        spanPages.InnerHtml += HttpContext.GetGlobalResourceObject("SearchForm", "Pages");

        spanPages.SetInnerText(lastPageIndex.ToString());
        divPager.InnerHtml += " " ;

        divPager.InnerHtml += spanPages.ToString();
        //If data.ShowGoTo Then
        //  Dim aGoToPage As New TagBuilder("a")
        //  aGoToPage.MergeAttribute("class", "goto")
        //  aGoToPage.MergeAttribute("href", "javascript:void(0)")
        //  aGoToPage.MergeAttribute("onclick", "goToPage('" + String.Format(data.ActionURL, "").Replace("&Page=", "").Replace("&page=", "").Replace("?Page=", "") + IIf(data.ActionURL.IndexOf("?") > 0 AndAlso data.ActionURL.IndexOf("?Page=") < 0, "&Page=", "?Page=") + "','" + "bottom-pager-goto" + "');")
        //  aGoToPage.InnerHtml = "&#160;"
        //  divPager.InnerHtml += aGoToPage.ToString
        //End If



        return MvcHtmlString.Create(divPager.ToString());
      }
      else
      {
        return null;
      }
    }

  }

}