using System.ComponentModel.DataAnnotations;
using Zaza.Classes.Attributes;

namespace Zaza.Entities
{
  [MetadataType(typeof(UserMetadata))]
  public partial class User
  {
  }

  public class UserMetadata
  {
    [ScaffoldColumn(false)]
    public int ID { get; set; }

    public System.String Email
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
