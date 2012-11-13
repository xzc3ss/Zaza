using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using System.Reflection;
using System.Globalization;

namespace Helpers
{
  public enum SortDirection
  {
    Ascending,
    Descending
  }

  public enum ActionMode
  {
    AddNew,
    Edit,
    View,
    Print
  }

  public class GridRow : DynamicObject, IEnumerable<object>
  {
    private const BindingFlags BindFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.IgnoreCase;
    private CONCENTRA.RED.AUTNET.PRESENTATION.Grid _grid;
    private int _rowIndex;
    private object _value;
    private IEnumerable<object> _values;

    #region " Properties "

    public object Value
    {
      get
      {
        return _value;
      }
    }

    public CONCENTRA.RED.AUTNET.PRESENTATION.Grid Grid
    {
      get
      {
        return _grid;
      }
    }

    #endregion

    #region " Constructor "

    public GridRow(CONCENTRA.RED.AUTNET.PRESENTATION.Grid grid, object value, int rowIndex)
    {
      this._grid = grid;
      this._value = value;
      this._rowIndex = rowIndex;
      //Me._dynamic = value
    }

    #endregion

    #region " Methods "

    public object ItemValue(string name)
    {
      if (String.IsNullOrEmpty(name))
      {
        throw new ArgumentException("Argument cannot be null", "name");
      }
      object value;

      if (!CONCENTRA.RED.AUTNET.PRESENTATION.Grid.TryGetDynamicMember(this, name, out value))
      {
        throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Column {0} was not found", name));
      }
      return value;
    }

    public IEnumerator<object> GetEnumerator()
    {
      if (_values == null)
      {
        _values = _grid.ColumnNames.Select(c => CONCENTRA.RED.AUTNET.PRESENTATION.Grid.GetDynamicMember(this, c));
      }
      return _values.GetEnumerator();
    }

    public IEnumerator GetEnumerator1()
    {
      return GetEnumerator();
    }

    IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return GetEnumerator1();
    }

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
      result = null;
      if (!String.IsNullOrEmpty(binder.Name))
      {
        if (binder.Name == "ROW")
        {
          // rename?
          result = _rowIndex;
          return true;
        }

        // support '.' for navigation properties
        object obj = _value;
        string[] names = binder.Name.Split('.');
        for (int i = 0; i <= names.Length - 1; i++)
        {
          if ((obj == null) || !TryGetMember(obj, names[i], out result))
          {
            result = null;
            return false;
          }
          obj = result;
        }
        return true;
      }
      return false;
    }

    public override string ToString()
    {
      return _value.ToString();
    }

    private static bool TryGetMember(object obj, string name, out object result)
    {
      PropertyInfo property = obj.GetType().GetProperty(name, BindFlags);
      if ((property != null) && (property.GetIndexParameters().Length == 0))
      {
        result = property.GetValue(obj, null);
        return true;
      }
      result = null;
      return false;
    }
    #endregion
  }


  public class GridColumn
  {

    public string ColumnName
    {
      get;
      set;
    }
    public string Header
    {
      get;
      set;
    }
    public bool CanSort
    {
      get;
      set;
    }
    public Func<dynamic, dynamic> Format
    {
      get;
      set;
    }
    public string Style
    {
      get;
      set;
    }
    public string HeaderStyle
    {
      get;
      set;
    }

    // public GridColumn(string ColumnName, Func<object, object> Format = null, string Header = "", string Style = "", string HeaderStyle = "") 
    //{
    //     this.ColumnName = ColumnName;
    //     this.Header = Header;
    //     this.CanSort = CanSort;
    //     this.Format = Format;
    //     this.Style = Style;
    //     this.HeaderStyle = HeaderStyle;
    // }
  }
}