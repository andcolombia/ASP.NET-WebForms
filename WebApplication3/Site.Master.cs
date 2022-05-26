using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_LoggingOut(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                Context.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            }
        }
    }
}