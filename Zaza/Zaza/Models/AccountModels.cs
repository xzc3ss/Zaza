using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.ComponentModel;
using System.Globalization;
using Zaza.Classes.Attributes;

//namespace Zaza.Models
//{
//		public class UsersContext : DbContext
//		{
//				public UsersContext()
//						: base("DefaultConnection")
//				{
//				}

//				public DbSet<UserProfile> UserProfiles { get; set; }
//		}

//		[Table("UserProfile")]
//		public class UserProfile
//		{
//				[Key]
//				[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
//				public int UserId { get; set; }
//				public string UserName { get; set; }
//		}

//		public class RegisterExternalLoginModel
//		{
//				[Required]
//				[Display(Name = "User name")]
//				public string UserName { get; set; }

//				public string ExternalLoginData { get; set; }
//		}

//		public class LocalPasswordModel
//		{
//				[Required]
//				[DataType(DataType.Password)]
//				[Display(Name = "Current password")]
//				public string OldPassword { get; set; }

//				[Required]
//				[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
//				[DataType(DataType.Password)]
//				[Display(Name = "New password")]
//				public string NewPassword { get; set; }

//				[DataType(DataType.Password)]
//				[Display(Name = "Confirm new password")]
//				[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
//				public string ConfirmPassword { get; set; }
//		}

//		public class LoginModel
//		{
//				[Required]
//				[Display(Name = "User name")]
//				public string UserName { get; set; }

//				[Required]
//				[DataType(DataType.Password)]
//				[Display(Name = "Password")]
//				public string Password { get; set; }

//				[Display(Name = "Remember me?")]
//				public bool RememberMe { get; set; }
//		}



//		public class ExternalLogin
//		{
//				public string Provider { get; set; }
//				public string ProviderDisplayName { get; set; }
//				public string ProviderUserId { get; set; }
//		}
//}

namespace Zaza.Models
{
  public class LoginModel
  {
    //[LocalizedRequired("ErrorMessages", "UsernameRequired")]
    //[LocalizedDisplayName["Common", "Username"]]
    [LocalizedRegularExpression("ErrorMessages", "EmailIsNotValid", "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*|superuser")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [LocalizedRequired("ErrorMessages", "PasswordRequired")]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
    public int LoginAttempts { get; set; }


  }

  public class RegisterModel
  {
    [Required]
    [Display(Name = "User name")]
    public string UserName { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }

  public class RegisterPasswordModel
  {
    public string userID;
    private string _newPasswordValue;

    private string _confirmPasswordValue;
    [StringLength(20, MinimumLength = 6)]
    [DataType(DataType.Password)]
    [LocalizedDisplayName("Common", "NewPassword")]
    [LocalizedRequired("Common", "NewPasswordRequired", AllowEmptyStrings = false)]
    public string NewPassword
    {
      get { return _newPasswordValue; }
      set { _newPasswordValue = value; }
    }

    [StringLength(20, MinimumLength = 6)]
    [DataType(DataType.Password)]
    [LocalizedDisplayName("Common", "ConfirmPassword")]
    [LocalizedRequired("ErrorMessages", "ConfirmationPasswordNeeded", AllowEmptyStrings = false)]
    [LocalizedCompare("NewPassword", "ErrorMessages", "FieldsDoNotMatch", "Common", "NewPassword", "Common", "ConfirmPassword")]
    public string ConfirmPassword
    {
      get { return _confirmPasswordValue; }
      set { _confirmPasswordValue = value; }
    }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
  }

  public class ForgotPasswordModel
  {
    [LocalizedRequired("ErrorMessages", "UsernameRequired")]
    [LocalizedDisplayName("Common", "Username")]
    [LocalizedRegularExpression("ErrorMessages", "EmailIsNotValid", "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*|superuser")]
    public string UserEmail { get; set; }
  }
}