using IdentityModel.Client;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace WebApplication3
{
    public partial class Claims : System.Web.UI.Page
    {
        protected string oidcName { get; set; }
        protected string oidcUserName { get; set; }
        protected string oidcSubject { get; set; }
        protected string oidcTenantId { get; set; }
        protected dynamic userInfoProfile { get; set; }
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                var url = Session["url"]?.ToString();
                if (string.IsNullOrEmpty(url))
                {
                    Session["url"] = "Claims.aspx";
                    string redirectUri = ConfigurationManager.AppSettings["redirectUri"];
                    var authenticationProperties = new AuthenticationProperties();
                    authenticationProperties.RedirectUri = redirectUri;
                    var auth = HttpContext.Current.GetOwinContext().Authentication;
                    auth.Challenge(authenticationProperties);
                }
                else
                {
                    Session["url"] = string.Empty;
                    
                }

                var httpClient = new HttpClient();
                var userInfo = new UserInfoRequest();


                var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;

                //You get the user's first and last name below:
                this.oidcName = userClaims?.FindFirst("aud")?.Value;

                // The 'preferred_username' claim can be used for showing the username
                this.oidcUserName = userClaims?.FindFirst("aud")?.Value;

                // The subject/ NameIdentifier claim can be used to uniquely identify the user across the web
                this.oidcSubject = userClaims?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                // TenantId is the unique Tenant Id - which represents an organization in Azure AD
                this.oidcTenantId = userClaims?.FindFirst("iss")?.Value;

                userInfo.Address = ConfigurationManager.AppSettings["Authority"] + "/connect/userinfo";
                userInfo.Token = userClaims?.FindFirst("access_token")?.Value;

                var userInfoProfile = await httpClient.GetUserInfoAsync(userInfo);

                this.userInfoProfile = userInfoProfile.Claims;

            }
                
        }
    }
}