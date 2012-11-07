using System;

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

    public Func<Object, Object> Format; 
   

 
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