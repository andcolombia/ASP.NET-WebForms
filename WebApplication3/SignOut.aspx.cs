using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class SignOut : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.GetOwinContext().Authentication.User.Identity.IsAuthenticated)
            {
                HttpContext.Current.GetOwinContext().Authentication.SignOut(
                   OpenIdConnectAuthenticationDefaults.AuthenticationType,
                   CookieAuthenticationDefaults.AuthenticationType);
            }
            Response.Redirect("~/");
        }
    }
}