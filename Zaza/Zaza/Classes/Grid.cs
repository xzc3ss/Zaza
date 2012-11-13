using System.Diagnostics;
using System.Dynamic;
using System.Collections.Specialized;
using System.Web;
using System.Text;
using System.Globalization;
using System.ComponentModel;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using Helpers;
using System.Linq;
using Zaza.Classes.Helpers;

namespace Zaza
{
  public class Grid
  {
    #region " Subclasses "


    internal class Binder : GetMemberBinder
    {
      public Binder(string name, Boolean ignoreCase)
        : base(name: name, ignoreCase: ignoreCase)
      {
      }

      public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
      {
        throw new NotImplementedException();
      }
    }


    #endregion

    #region " Members "

    private IEnumerable<object> _source;
    private IEnumerable<GridColumn> _columns;
    private object _htmlAttributes;
    private string _tableStyle;
    private string _emptyTemplateText;
    private string _headerStyle;
    private string _rowStyle;
    private string _alternatingRowStyle;
    private Boolean _displayHeader;
    private Boolean _canSort;
    private string _fieldNamePrefix;
    private string _sortFieldName;
    private string _sortDirectionFieldName;
    private string _sortColumn;
    private string _defaultSort;
    private SortDirection _sortDirection = SortDirection.Ascending;
    private string _pageFieldName;
    private Dictionary<string, string> _columnHeaders;
    private Type _elementType;
    private IEnumerable<string> _columnNames;
    private Boolean _sortColumnSet;
    private IList<GridRow> _rows;
    private Boolean _sortDirectionSet;

    #endregion

    #region " Constructors "

    public Grid(IEnumerable<object> source, IEnumerable<GridColumn> columns = null, string tableStyle = null, string emptyTemplateText = null, string headerStyle = null, string rowStyle = null, string alternatingRowStyle = null, object htmlAttributes = null, string fieldNamePrefix = null, string sortColumn = null,
                string defaultSort = null, Dictionary<string, string> columnHeaders = null, bool displayHeader = true, bool canSort = true, string sortFieldName = "sort", string sortDirectionFieldName = "sortdir", string pageFieldName = "page")
    {
      this._source = source;
      this._columns = columns;
      this._htmlAttributes = htmlAttributes;
      this._tableStyle = tableStyle;
      this._emptyTemplateText = emptyTemplateText;
      this._headerStyle = headerStyle;
      this._rowStyle = rowStyle;
      this._alternatingRowStyle = alternatingRowStyle;
      this._displayHeader = displayHeader;
      this._canSort = canSort;
      this._fieldNamePrefix = fieldNamePrefix;
      this._sortFieldName = sortFieldName;
      this._sortDirectionFieldName = sortDirectionFieldName;
      this._sortColumn = sortColumn;
      this._defaultSort = defaultSort;
      this._pageFieldName = pageFieldName;
      this._columnHeaders = columnHeaders;
    }

    #endregion

    #region " Properties Public "

    public IEnumerable<object> Source
    {
      get
      {
        return _source;
      }
    }

    public IEnumerable<string> ColumnNames
    {
      get
      {
        if (_columnNames == null)
        {
          _columnNames = GetDefaultColumnNames();
        }
        return _columnNames;
      }
    }

    public string SortColumn
    {
      get
      {
        if (!_sortColumnSet)
        {
          dynamic sortColumn__1 = QueryString[SortFieldName];
          if (!String.IsNullOrEmpty(sortColumn__1))
          {
            // navigation columns that contain '.' will be validated during the Sort operation
            // validate other properties up-front and ignore any bad columns passed via the query string
            if (sortColumn__1.Contains('.') || Enumerable.Contains(ColumnNames, sortColumn__1, StringComparer.OrdinalIgnoreCase))
            {
              _sortColumn = sortColumn__1;
            }
          }
          _sortColumnSet = true;
        }
        if (String.IsNullOrEmpty(_sortColumn))
        {
          return _defaultSort ?? String.Empty;
        }
        return _sortColumn;
      }
      set
      {
        if (!SortColumn.Equals(value, StringComparison.OrdinalIgnoreCase))
        {
          EnsureDataSourceNotSortedAndPaged();
          _sortColumn = value;
        }
        _sortColumnSet = true;
      }
    }

    public string SortFieldName
    {
      get
      {
        return FieldNamePrefix + _sortFieldName;
      }
    }

    public string FieldNamePrefix
    {
      get
      {
        return _fieldNamePrefix ?? string.Empty;
      }
    }

    public string SortDirectionFieldName
    {
      get
      {
        return FieldNamePrefix + _sortDirectionFieldName;
      }
    }

    public string PageFieldName
    {
      get
      {
        return FieldNamePrefix + _pageFieldName;
      }
    }

    public IList<GridRow> Rows
    {
      get
      {
        if (_rows == null)
        {
          _rows = Source.Select((System.Object o, int i) => new GridRow(this, o, i)).ToArray();
        }
        return _rows;
      }
    }

    public SortDirection SortDirection
    {
      get
      {
        if (!_sortDirectionSet)
        {
          string sortDirection__1 = QueryString[SortDirectionFieldName];
          if (sortDirection__1 != null)
          {
            if (sortDirection__1.Equals("DESC", StringComparison.OrdinalIgnoreCase) || sortDirection__1.Equals("DESCENDING", StringComparison.OrdinalIgnoreCase))
            {
              _sortDirection = SortDirection.Descending;
            }
          }
          _sortDirectionSet = true;
        }
        return _sortDirection;
      }
      set
      {
        if (_sortDirection != value)
        {
          EnsureDataSourceNotSortedAndPaged();
          _sortDirection = value;
        }
        _sortDirectionSet = true;
      }
    }

    #endregion

    #region " Properties Private "

    private Type ElementType
    {
      get
      {
        if (_elementType == null)
        {
          if (Source.FirstOrDefault() is DynamicObject)
          {
            _elementType = typeof(DynamicObject);
          }
          else
          {
            dynamic type = Source.GetType();
            if (type.IsArray)
            {
              _elementType = type.GetElementType();
            }
            else
            {
              Type[] interfaces = type.GetInterfaces();
              foreach (Type iface in interfaces)
              {
                if (iface.IsGenericType && !iface.ContainsGenericParameters && iface.Name.StartsWith("IEnumerable", StringComparison.OrdinalIgnoreCase))
                {
                  _elementType = iface.GetGenericArguments()[0];
                  break; // TODO: might not be correct. Was : Exit For
                }
              }
            }
          }
          Debug.Assert(_elementType != null);
        }
        return _elementType;
      }
    }

    private NameValueCollection QueryString
    {
      get
      {
        return HttpContext.Current.Request.QueryString;
      }
    }

    private void EnsureDataSourceNotSortedAndPaged()
    {
      if (_rows != null)
      {
        throw new InvalidOperationException("Property setter not supperted after data bound");
      }
    }

    #endregion

    #region "Methods"

    private static bool IsBindableType(Type type)
    {
      Type underlyingType = Nullable.GetUnderlyingType(type);
      if (underlyingType != null)
      {
        type = underlyingType;
      }
      return (type.IsPrimitive || type.Equals(typeof(string)) || type.Equals(typeof(DateTime)) || type.Equals(typeof(Decimal)) || type.Equals(typeof(Guid)) || type.Equals(typeof(DateTime)) || type.Equals(typeof(TimeSpan)));
    }

    private IEnumerable<string> GetDefaultColumnNames()
    {
      var dynObj = (DynamicObject)_source.FirstOrDefault();
      if (dynObj != null)
      {
        return dynObj.GetDynamicMemberNames();
      }
      else
      {

        return (from p in ElementType.GetProperties()
                where IsBindableType(p.PropertyType) &&
                      (p.GetIndexParameters().Length == 0)
                select p.Name).OrderBy(n => n, StringComparer.OrdinalIgnoreCase).ToArray();
      }

    }
    private IEnumerable<GridColumn> GetDefaultColumns()
    {
      IEnumerable<string> names = ColumnNames;
      return (from n in names
              select new GridColumn
                       {
                         ColumnName = n,
                         CanSort = true
                       }).ToArray();
    }

    private string GetTableHeaderHtml(IEnumerable<GridColumn> columns, string headerStyle)
    {
      TagBuilder tr = new TagBuilder("tr");
      if (!string.IsNullOrEmpty(headerStyle))
      {
        tr.MergeAttribute("class", headerStyle);
      }
      foreach (var column_loopVariable in columns)
      {
        var column = column_loopVariable;
        TagBuilder th = new TagBuilder("th");
        // uses header default when null, but clears header when empty
        bool headerIsEmpty = (column.Header != null) && (column.Header.Length == 0);

        string headerText = string.Empty;
        if (!headerIsEmpty)
        {
          if (column.Header == null)
          {
            if (_columnHeaders == null || !_columnHeaders.ContainsKey(column.ColumnName))
            {
              headerText = column.ColumnName;
            }
            else
            {
              headerText = _columnHeaders[column.ColumnName];
            }
          }
          else
          {
            headerText = column.Header;
          }
        }


        if (!_canSort || headerIsEmpty || string.IsNullOrEmpty(column.ColumnName) || !column.CanSort)
        {
          if (!string.IsNullOrEmpty(column.Header) || !string.IsNullOrEmpty(column.ColumnName))
          {
            th.SetInnerText(headerText);
          }
        }
        else
        {
          th.InnerHtml = GetSortLinkHtml(column.ColumnName, headerText);
        }
        tr.InnerHtml += th.ToString();
      }
      return tr.ToString();
    }

    private string GetSortLinkHtml(string column, string text = null)
    {
      if (string.IsNullOrEmpty(text))
      {
        text = column;
      }

      // add the sort glyth
      TagBuilder glyth = new TagBuilder("span");
      if (column == SortColumn)
      {
        glyth.MergeAttribute("class", "sorted");

        glyth.InnerHtml = SortDirection == SortDirection.Ascending ? "▲" : "▼";
      }
      else
      {
        glyth.InnerHtml = "▲";
      }

      // send the text as html (encoding done here)
      return GetLinkHtml(GetSortUrl(column), HttpUtility.HtmlEncode(text) + glyth.ToString());
    }

    public string GetSortUrl(string column)
    {
      if (!_canSort)
        throw new NotSupportedException("Not supported if sorting is disabled");
      if (string.IsNullOrEmpty(column))
        throw new ArgumentException("Argument cannot be null", "column");

      dynamic sort = SortColumn;
      dynamic sortDir = SortDirection.Ascending;
      if (column.Equals(sort, StringComparison.OrdinalIgnoreCase))
      {
        if (SortDirection == SortDirection.Ascending)
        {
          sortDir = SortDirection.Descending;
        }
      }

      var queryString = new NameValueCollection(2);
      queryString[SortFieldName] = column;
      queryString[SortDirectionFieldName] = GetSortDirectionString(sortDir);
      return GetPath(queryString, PageFieldName);
    }

    private string GetTableBodyHtml(IEnumerable<GridColumn> columns, string rowStyle, string alternatingRowStyle)
    {
      var sb = new StringBuilder();
      int r = 0;

      foreach (var row in Rows)
      {
        string style = GetRowStyle(r, rowStyle, alternatingRowStyle);
        var tr = new TagBuilder("tr");
        if (!String.IsNullOrEmpty(style))
        {
          tr.MergeAttribute("class", style);
        }
        foreach (var column in columns)
        {
          object value = column.Format == null ? HttpUtility.HtmlEncode(row.ItemValue(column.ColumnName)) : Format(f: column.Format, arg: row).ToString();
          tr.InnerHtml += GetTableCellHtml(column, (string)value ?? "&nbsp;");
        }
        sb.Append(tr.ToString());

        r += 1;
      }
      return sb.ToString();
    }

    private string GetRowStyle(int rowIndex, string rowStyle, string alternatingRowStyle)
    {
      StringBuilder style = new StringBuilder();

      if (rowIndex % 2 == 0)
      {
        if (!String.IsNullOrEmpty(rowStyle))
        {
          style.Append(rowStyle);
        }
      }
      else
      {
        if (!String.IsNullOrEmpty(alternatingRowStyle))
        {
          style.Append(alternatingRowStyle);
        }
      }

      return style.ToString();
    }
    #endregion

    #region "Friend Methods"
    internal string GetPath(NameValueCollection queryString__1, params string[] exclusions)
    {
      var temp = new NameValueCollection(QueryString);
      // update current query string in case values were set programmatically
      if (temp.AllKeys.Contains(SortFieldName))
      {
        if (String.IsNullOrEmpty(SortColumn))
        {
          temp.Remove(SortFieldName);
        }
        else
        {
          temp.Set(SortFieldName, SortColumn);
        }
      }
      if (temp.AllKeys.Contains(SortDirectionFieldName))
      {
        temp.Set(SortDirectionFieldName, GetSortDirectionString(SortDirection));
      }
      // remove fields from exclusions list
      foreach (object key_loopVariable in exclusions)
      {
        var key = key_loopVariable;
        temp.Remove((string)key);
      }
      // replace with new field values
      foreach (string key in queryString__1.Keys)
      {
        temp.Set(key, queryString__1[key]);
      }
      queryString__1 = temp;

      StringBuilder sb = new StringBuilder(HttpContext.Current.Request.Path);
      sb.Append("?");
      for (int i = 0; i <= queryString__1.Count - 1; i++)
      {
        if (i > 0)
        {
          sb.Append("&");
        }
        sb.Append(HttpUtility.UrlEncode(queryString__1.Keys[i]));
        sb.Append("=");
        sb.Append(HttpUtility.UrlEncode(queryString__1[i]));
      }
      return sb.ToString();
    }

    internal string GetLinkHtml(string path, string text)
    {
      TagBuilder linkTag = new TagBuilder("a");
      linkTag.MergeAttribute("href", path);

      //If [String].IsNullOrEmpty(AjaxUpdateContainerId) Then
      //  linkTag.MergeAttribute("href", path)
      //Else
      //  linkTag.MergeAttribute("href", "#")
      //  linkTag.MergeAttribute("onclick", GetContainerUpdateScriptInternal(path))
      //End If
      // linkTag.SetInnerText(text)


      // glyth change (Vlad - 20.12.2010)
      linkTag.InnerHtml = text;

      return linkTag.ToString();
    }
    #endregion
    #region " Methods Shared "

    static internal string GetSortDirectionString(SortDirection sortDir)
    {
      return (sortDir == SortDirection.Ascending) ? "ASC" : "DESC";
    }

    private static System.Web.WebPages.HelperResult Format(Func<object, object> f, object arg)
    {
      dynamic result = f(arg);

      return new System.Web.WebPages.HelperResult(tw =>
                                                    {
                                                      dynamic helper = result as System.Web.WebPages.HelperResult;

                                                      if (helper != null)
                                                      {
                                                        helper.WriteTo(tw);
                                                        return;
                                                      }

                                                      IHtmlString htmlString = result as IHtmlString;


                                                      if (htmlString != null)
                                                      {
                                                        tw.Write(htmlString);
                                                        return;
                                                      }

                                                      if (result != null)
                                                      {
                                                        tw.Write(HttpUtility.HtmlEncode(result));
                                                      }
                                                    });
    }

    private static string GetTableCellHtml(GridColumn column, string innerHtml)
    {
      var td = new TagBuilder("td");
      if (!string.IsNullOrEmpty(column.Style))
      {
        td.MergeAttribute("class", column.Style);
      }
      td.InnerHtml = innerHtml;
      return td.ToString();
    }

    static internal bool TryGetDynamicMember(DynamicObject o, string name, ref object result)
    {
      return o.TryGetMember(new Binder(name, true), out result);
    }

    static internal object GetDynamicMember(DynamicObject obj, string name)
    {
      object result = new object();
      if (TryGetDynamicMember(obj, name, ref result))
      {
        return result;
      }
      throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "Column {0} was not found", name));
    }

    static internal IDictionary<string, object> ObjectToDictionary(object instance)
    {
      IDictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
      if (instance != null)
      {
        PropertyDescriptor descriptor = default(PropertyDescriptor);
        var properties = TypeDescriptor.GetProperties(instance);
        var length = properties.Count;
        for (var i = 0; i <= length; i++)
        {
          object obj2 = properties[i].GetValue(instance);
          dictionary.Add(descriptor.Name, obj2);
        }

        //foreach (var desc in TypeDescriptor.GetProperties(instance))
        //{
        //  object obj2 = desc.GetValue(instance);
        //  dictionary.Add(desc.Name, obj2);
        //}
      }
      return dictionary;
    }

    #endregion

    #region " Methods Public "

    public MvcHtmlString GetHtml()
    {
      if (Rows.Count > 0)
      {
        if (_columns == null)
          _columns = GetDefaultColumns();

        TagBuilder table = new TagBuilder("table");
        if (!string.IsNullOrEmpty(_tableStyle))
        {
          table.MergeAttribute("class", _tableStyle);
        }
        table.MergeAttribute("cellspacing", "0");

        if (_htmlAttributes != null)
        {
          KeyValuePair<string, object> pair = default(KeyValuePair<string, object>);
          foreach (KeyValuePair<string, object> pair_loopVariable in ObjectToDictionary(_htmlAttributes))
          {
            pair = pair_loopVariable;
            bool replaceExisting = true;
            table.MergeAttribute(pair.Key, Convert.ToString(pair.Value, CultureInfo.InvariantCulture), replaceExisting);
          }
        }

        if (_displayHeader)
        {
          TagBuilder thead = new TagBuilder("thead");
          thead.InnerHtml = GetTableHeaderHtml(_columns, _headerStyle);
          table.InnerHtml += thead.ToString();
        }

        //' XHTML 1.1 requires that tfoot come before tbody
        //If footer IsNot Nothing Then
        //  Dim tfoot As New TagBuilder("tfoot")
        //  Dim tr As New TagBuilder("tr")
        //  If Not [String].IsNullOrEmpty(footerStyle) Then
        //    tr.MergeAttribute("class", footerStyle)
        //  End If
        //  Dim td As New TagBuilder("td")
        //  td.MergeAttribute("colspan", columns.Count().ToString(CultureInfo.InvariantCulture))
        //  td.InnerHtml = Format(footer, Nothing).ToString()
        //  tr.InnerHtml = td.ToString()
        //  tfoot.InnerHtml = tr.ToString()
        //  table.InnerHtml += tfoot.ToString()
        //End If

        TagBuilder tbody = new TagBuilder("tbody");
        tbody.InnerHtml += GetTableBodyHtml(_columns, _rowStyle, _alternatingRowStyle);
        //If fillEmptyRows Then
        //  tbody.InnerHtml += GetTableFillerRowsHtml(columns, rowStyle, alternatingRowStyle, emptyRowCellValue)
        //End If
        table.InnerHtml += tbody.ToString();

        return MvcHtmlString.Create(table.ToString());
      }
      else
      {
        TagBuilder divEmptyTemplateContainer = new TagBuilder("div");
        divEmptyTemplateContainer.InnerHtml += "<br /><br />";
        TagBuilder divEmptyTemplate = new TagBuilder("div");
        divEmptyTemplate.MergeAttribute("class", "empty-grid");
        divEmptyTemplate.InnerHtml = _emptyTemplateText.ToString();
        divEmptyTemplateContainer.InnerHtml += divEmptyTemplate.ToString();
        return MvcHtmlString.Create(divEmptyTemplateContainer.ToString());
      }
    }

    #endregion
  }
}
