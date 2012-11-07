using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Helpers;
using Zaza.Classes;

namespace Helpers
{
  public partial class Zaza
  {

    public static MvcHtmlString Grid(IEnumerable<object> source,
                                 IEnumerable<GridColumn> columns = null,
                                string tableStyle = "grid",
                              string emptyTemplateText = null,
                              string headerStyle = null,
                              string rowStyle = null,
                              string alternatingRowStyle = "alt-row",
                              object htmlAttributes = null,
                               string fieldNamePrefix = null,
                               string sortColumn = null,
                               string defaultSort = null,
                               Dictionary<String, String> columnHeaders = null,
                               Boolean displayHeader = true,
                               Boolean canSort = true,
                               string sortFieldName = "sort",
                               string sortDirectionFieldName = "sortdir",
                               string pageFieldName = "page")
    {
      // intantiate a new grid
      var g = new Grid(source, columns, tableStyle, emptyTemplateText, headerStyle, rowStyle, alternatingRowStyle,
                       htmlAttributes, fieldNamePrefix, sortColumn, defaultSort, columnHeaders, displayHeader, canSort,
                       sortFieldName, sortDirectionFieldName, pageFieldName);
      return g.GetHtml();
    }




    public static GridColumn GridColumn(string columnName, string header, Func<Object, Object> format = null, string style = null, Boolean canSort = true, string headerStyle = null)
    {
      var col = new GridColumn
                  {
                    ColumnName = columnName,
                    Header = header,
                    Format = format,
                    Style = style,
                    CanSort = canSort,
                    HeaderStyle = headerStyle
                  };
      return col;
    }


  }
}