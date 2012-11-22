using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Zaza.Classes;
using Zaza.Models;
using System.Security.Principal;
//using Zaza.Filters;
using Zaza.Entities;
using Zaza.Classes.Managers;

namespace Zaza.Controllers
{


  public class AccountController : ZazaController
  {

    #region "Login"

    [HttpGet]
    public ActionResult Login()
    {
      // string returnUrl

      return View();
    }

    [HttpPost]
    public ActionResult Login(LoginModel model)
    {
      //, string returnUr
      var user = UsersManager.GetUserByEmail(model.Email);

      if (ModelState.IsValid && user != null)
      {
        FormsAuthentication.SetAuthCookie(model.Email, false);
        if ((user.Password == model.Password) && (user.Role.IsSuperUser))
        {

          return RedirectToAction("Index", "Default", new
          {
            area = "Admin"
          });

        }

      }
      else
      {
        ModelState.AddModelError("", "The user name or password provided is incorrect.");
      }



      return View(model);
    }
    #endregion

    //[ValidateAntiForgeryToken]
    [HttpPost]
    public ActionResult LogOff()
    {
      ZazaIdentity.Current = null;

      FormsAuthentication.SignOut();

      return RedirectToAction("Index", "Home");
    }

    // GET: /Account/Register

    [AllowAnonymous]
    public ActionResult Register()
    {
      return View();
    }

    // [ValidateAntiForgeryToken]
    // Post: /Account/Register
    [HttpPost]
    public ActionResult Register(RegisterModel model)
    {
      var newUSer = new User();
      if (ModelState.IsValid)
      {
        if (!UsersManager.CheckExistingUserByEmail(model.Email))
        {
          newUSer = UsersManager.CreateUserAndAccount(model);
          FormsAuthentication.SetAuthCookie(model.Email, false);
          ZazaIdentity.Current.CurrentUser = newUSer;
        }
        return RedirectToAction("Index", "Home");

      }

      return View(model);
    }


    //#region Helpers
    //private ActionResult RedirectToLocal(string returnUrl)
    //{
    //  if (Url.IsLocalUrl(returnUrl))
    //  {
    //    return Redirect(returnUrl);
    //  }
    //  else
    //  {
    //    return RedirectToAction("Index", "Home");
    //  }
    //}

    //public enum ManageMessageId
    //{
    //  ChangePasswordSuccess,
    //  SetPasswordSuccess,
    //  RemoveLoginSuccess,
    //}

    //internal class ExternalLoginResult : ActionResult
    //{
    //  public ExternalLoginResult(string provider, string returnUrl)
    //  {
    //    Provider = provider;
    //    ReturnUrl = returnUrl;
    //  }

    //  public string Provider
    //  {
    //    get;
    //    private set;
    //  }
    //  public string ReturnUrl
    //  {
    //    get;
    //    private set;
    //  }

    //  public override void ExecuteResult(ControllerContext context)
    //  {
    //    OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
    //  }
    //}

    //private static string ErrorCodeToString(MembershipCreateStatus createStatus)
    //{
    //  // See http://go.microsoft.com/fwlink/?LinkID=177550 for
    //  // a full list of status codes.
    //  switch (createStatus)
    //  {
    //    case MembershipCreateStatus.DuplicateUserName:
    //      return "User name already exists. Please enter a different user name.";

    //    case MembershipCreateStatus.DuplicateEmail:
    //      return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

    //    case MembershipCreateStatus.InvalidPassword:
    //      return "The password provided is invalid. Please enter a valid password value.";

    //    case MembershipCreateStatus.InvalidEmail:
    //      return "The e-mail address provided is invalid. Please check the value and try again.";

    //    case MembershipCreateStatus.InvalidAnswer:
    //      return "The password retrieval answer provided is invalid. Please check the value and try again.";

    //    case MembershipCreateStatus.InvalidQuestion:
    //      return "The password retrieval question provided is invalid. Please check the value and try again.";

    //    case MembershipCreateStatus.InvalidUserName:
    //      return "The user name provided is invalid. Please check the value and try again.";

    //    case MembershipCreateStatus.ProviderError:
    //      return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

    //    case MembershipCreateStatus.UserRejected:
    //      return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

    //    default:
    //      return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
    //  }
    //}
    //#endregion



  }


}
