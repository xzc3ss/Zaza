using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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
    //[LocalizedRegularExpression("ErrorMessages", "EmailIsNotValid", "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
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
