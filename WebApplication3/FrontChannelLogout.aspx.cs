using System;
using System.Web;
using System.Web.UI;

namespace WebApplication3
{
    public partial class FrontChannelLogout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                string sidGet = "";
                if (!string.IsNullOrEmpty(Request.QueryString["sid"]))
                {
                    sidGet = Request.QueryString["sid"];
                }
                var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
                var sid = userClaims?.FindFirst("sid")?.Value;
                if (string.Equals(sidGet, sid, StringComparison.Ordinal))
                {
                    HttpContext.Current.GetOwinContext().Authentication.SignOut();
                }
            }
        }
    }
}