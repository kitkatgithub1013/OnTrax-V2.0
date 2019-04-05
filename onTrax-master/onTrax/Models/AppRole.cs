﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Web.Mvc;

//TODO: Change this using statement to match your project
using onTrax.DAL;

//TODO: Change this namespace to match your project
namespace onTrax.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }

        public AppRole(string name) : base(name) { }

        public virtual List<AppUser> AppUsers { get; set; }
    }

    public class AppRoleManager : RoleManager<AppRole>, IDisposable
    {
        public AppRoleManager(RoleStore<AppRole> store) : base(store) { }

        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context)
        {
            return new AppRoleManager(new RoleStore<AppRole>(context.Get<AppDbContext>()));
        }
    }


    //public static class IdentityHelpers
    //{
    //    public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
    //    {
    //        AppUserManager mgr = HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
    //        return new MvcHtmlString(mgr.FindById(id).UserName);
    //    }
    //}
}