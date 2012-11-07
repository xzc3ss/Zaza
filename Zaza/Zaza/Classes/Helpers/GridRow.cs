using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Globalization;
using Zaza.Classes;
namespace Helpers
{
  public class GridRow : DynamicObject, IEnumerable<object>
  {

    #region " Members "


    private const BindingFlags BindFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.IgnoreCase;
    private Grid _grid;
    private DynamicObject _dynamic;
    private int _rowIndex;
    private object _value;

    private IEnumerable<object> _values;
    #endregion

    #region " Properties "

    public object Value
    {
      get
      {
        return _value;
      }
    }

    public global::Zaza.Classes.Grid Grid
    {
      get
      {
        return _grid;
      }
    }

    #endregion

    #region " Constructor "

    public GridRow(global::Zaza.Classes.Grid grid, object value, int rowIndex)
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
      object value = null;
      if (!Grid.TryGetDynamicMember(this, name, ref value))
      {
        throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Column {0} was not found", name));
      }
      return value;
    }

    public IEnumerator<object> GetEnumerator()
    {
      if (_values == null)
      {
        _values = _grid.ColumnNames.Select((System.Object c) => Grid.GetDynamicMember(this, (string)c));
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

    public bool TryGetMember(GetMemberBinder binder, ref object result)
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
        if (_dynamic != null)
        {
          return _dynamic.TryGetMember(binder, out result);
        }

        // support '.' for navigation properties
        object obj = _value;
        string[] names = binder.Name.Split('.');
        for (int i = 0; i <= names.Length - 1; i++)
        {
          if ((obj == null) || !TryGetMember(obj, names[i], ref result))
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

    private static bool TryGetMember(object obj, string name, ref object result)
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

}


