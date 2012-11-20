using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace Zaza.Presentation
{
  public partial class ZazaGrid
  {

    public static MvcHtmlString Grid(IEnumerable<object> source, IEnumerable<Helpers.GridColumn> columns = null, string tableStyle = "grid", string emptyTemplateText = null, string headerStyle = null, string rowStyle = null, string alternatingRowStyle = "alt-row", object htmlAttributes = null, string fieldNamePrefix = null, string sortColumn = null,
    string defaultSort = null, Helpers.SortDirection defaultSortDirection = Helpers.SortDirection.Ascending, Dictionary<string, string> columnHeaders = null, bool displayHeader = true, bool canSort = true, string sortFieldName = "sort", string sortDirectionFieldName = "sortdir", string pageFieldName = "page", bool displayEmptyGrid = false)
    {

      // intantiate a new grid
      Grid g = new Grid(source, columns, tableStyle, emptyTemplateText, headerStyle, rowStyle, alternatingRowStyle, htmlAttributes, fieldNamePrefix, sortColumn,
      defaultSort, defaultSortDirection, columnHeaders, displayHeader, canSort, sortFieldName, sortDirectionFieldName, pageFieldName, displayEmptyGrid);

      return g.GetHtml();
    }

    public static Helpers.GridColumn GridColumn(string columnName, string header = null, Func<object, object> format = null, string style = null, bool canSort = true, string headerStyle = null)
    {

      // instantiate a new column
      Helpers.GridColumn col = new Helpers.GridColumn();

      col.ColumnName = columnName;
      col.Header = header;
      col.Format = format;
      col.Style = style;
      col.CanSort = canSort;
      col.HeaderStyle = headerStyle;

      return col;
    }

  }


  //    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> list, string sortExpression)
  //        {
  //            sortExpression += "";
  //            string[] parts = sortExpression.Split(' ');
  //            bool descending = false;
  //            string property = "";

  //            if (parts.Length > 0 && parts[0] != "")
  //            {
  //                property = parts[0];

  //                if (parts.Length > 1)
  //                {
  //                    descending = parts[1].ToLower().Contains("esc");
  //                }

  //                PropertyInfo prop = typeof(T).GetProperty(property);

  //                if (prop == null)
  //                {
  //                    throw new Exception("No property '" + property + "' in + " + typeof(T).Name + "'");
  //                }

  //                if (descending)
  //                    return list.OrderByDescending(x => prop.GetValue(x, null));
  //                else
  //                    return list.OrderBy(x => prop.GetValue(x, null));
  //            }

  //            return list;
  //        }
  //}


  public static class OrderByExtention
  {

    private static readonly MethodInfo OrderByMethod = typeof(Queryable).GetMethods().Where(method => method.Name == "OrderBy").Where(method => method.GetParameters().Length == 2).Single();

    private static readonly MethodInfo OrderByDescendingMethod = typeof(Queryable).GetMethods().Where(method => method.Name == "OrderByDescending").Where(method => method.GetParameters().Length == 2).Single();

    public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string propertyName, string sortDirection)
    {
      ParameterExpression parameter = Expression.Parameter(typeof(TSource), null);
      Expression orderByPropertyName = Expression.Property(parameter, propertyName);
      LambdaExpression lambda = Expression.Lambda(orderByPropertyName, new ParameterExpression[] { parameter });

      MethodInfo genericMethod = default(MethodInfo);
      if (!string.IsNullOrEmpty(sortDirection) && sortDirection.ToLower() == "desc")
      {
        genericMethod = OrderByDescendingMethod.MakeGenericMethod(new Type[] {
                typeof(TSource),
                orderByPropertyName.Type
            });
      }
      else
      {
        genericMethod = OrderByMethod.MakeGenericMethod(new Type[] {
                typeof(TSource),
                orderByPropertyName.Type
            });
      }

      object ret = genericMethod.Invoke(null, new object[] {
            source,
            lambda
        });

      return (IQueryable<TSource>)ret;
    }

  }

}

