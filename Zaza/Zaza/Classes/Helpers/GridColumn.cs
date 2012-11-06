using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Helpers
{
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
    public Boolean CanSort
    {
      get;
      set;
    }
    public Func<object, object> Format;
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


  }
}