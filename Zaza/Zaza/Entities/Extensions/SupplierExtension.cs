using System.ComponentModel.DataAnnotations;

namespace Zaza.Entities
{
  [MetadataType(typeof(SupplierMetadata))]
  public partial class Supplier
  {
  }

  public class SupplierMetadata
  {
    [ScaffoldColumn(false)]
    public int ID { get; set; }
    //[LocalizedRegularExpression("ErrorMessages", "EmailIsNotValid", "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
    public System.String ContactEmail
    {
      get;
      set;
    }
    [ScaffoldColumn(false)]
    public System.String AddedDate
    {
      get;
      set;
    }


    [ScaffoldColumn(false)]
    public System.String CreatedDate { get; set; }
    [ScaffoldColumn(false)]
    public System.Boolean Deleted { get; set; }
  }
}
