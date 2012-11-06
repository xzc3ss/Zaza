using System.Dynamic;
using System.Collections.Specialized;
using System.Web;
using System.Text;
using System.Globalization;
using System.IO;
using System.ComponentModel;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using Helpers;

namespace Zaza.Classes
{
  public class Grid
  {
    #region " Subclasses "

    //internal class Binder : GetMemberBinder
    //{
    //  public Binder(string name, bool ignoreCase)
    //  {
    //    base(name, ignoreCase);
    //  }
    //}

    //  Inherits GetMemberBinder
    //  Public Sub New(ByVal name As String, ByVal ignoreCase As Boolean)
    //    MyBase.New(name, ignoreCase)
    //  End Sub
    //  Public Overrides Function FallbackGetMember(ByVal target As DynamicMetaObject, ByVal errorSuggestion As DynamicMetaObject) As DynamicMetaObject
    //    Throw New NotImplementedException()
    //  End Function
    //End Class

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
    private GridRow _rows;
    private Boolean _sortDirectionSet;

    #endregion
  }
}