using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zaza.Entities;
using User = Zaza.Entities.User;

namespace Zaza.Classes.Managers
{
  public static class UsersManager
  {
    public static Boolean CheckExistingUserByEmail(string email)
    {
      var exists = false;
      var query = (from u in Core.DataContext.Users where u.Email == email select u).FirstOrDefault();
      if (query != null)
      {
        exists = true;
      }

      return exists;
    }

    public static Boolean CheckExistingUserById(int userID)
    {
      var exists = false;
      var query = (from u in Core.DataContext.Users where u.ID == userID select u).FirstOrDefault();
      if (query != null)
      {
        exists = true;
      }

      return exists;
    }

    public static User GetUserByEmail(string email)
    {
      var exists = false;
      var query = (from u in Core.DataContext.Users where u.Email == email select u).FirstOrDefault();
      return query;
    }

    public static User CreateNewUser(User newUser)
    {
      var exists = false;
      User createdUser = new User();
      createdUser = newUser;

      //var query = (from u in Core.DataContext.Users where u.ID == userID select u).FirstOrDefault();
      //if (query != null)
      //{
      //  exists = true;
      //}

      return createdUser;
    }
  }
}