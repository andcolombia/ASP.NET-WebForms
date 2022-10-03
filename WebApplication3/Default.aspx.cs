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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Session["url"] = string.Empty;
                this.LoginDiv.Visible = true;
                this.LoginDocumentoDiv.Visible = true;
                this.LoginTipoDocumentoDiv.Visible = true;
                this.PersonalizarDiv.Visible = false;
                this.LoginIn.Visible = false;
                this.ClaimsDiv.Visible = false;
                this.LoginDivLogout.Visible = false;
            }
            else
            {
                var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
                var user = userClaims.FindFirst("aud");
                this.LoginDiv.Visible = false;
                this.LoginIn.Visible = true;
                this.LoginDocumentoDiv.Visible = false;
                this.LoginTipoDocumentoDiv.Visible = false;
                this.PersonalizarDiv.Visible = true;
                this.textUser.Text = "Hello:  <b style='font - size:2rem'>" + user.Value + "</b>";
                this.ClaimsDiv.Visible = true;
                this.LoginDivLogout.Visible = true;
            }
        }
        protected void login_Click(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                string _tipoIdentificacion = this.TipoDocumento.SelectedValue;
                string _identificacion = this.Documento.Text;
                string _accion = "login";
                string redirectUri = ConfigurationManager.AppSettings["redirectUri"];
                var authenticationProperties = new AuthenticationProperties();
                if (!string.IsNullOrEmpty(_tipoIdentificacion) && !string.IsNullOrEmpty(_identificacion))
                    authenticationProperties.Dictionary.Add("login_hint", string.Format("{0},{1}", _tipoIdentificacion, _identificacion));
                if (!string.IsNullOrEmpty(_accion))
                {
                    if (_accion == "register")
                    {
                        string _nivel = "loa:2";
                        if (_tipoIdentificacion == "EM")
                            _nivel = "loa:1";
                        authenticationProperties.Dictionary.Add("acr_values", string.Format("action:{0} {1}", _accion, _nivel));
                    }
                    else
                        authenticationProperties.Dictionary.Add("acr_values", string.Format("action:{0}", _accion));
                }

                authenticationProperties.RedirectUri = redirectUri;
                var auth = HttpContext.Current.GetOwinContext().Authentication;
                auth.Challenge(authenticationProperties);
            }
        }

        protected void register_Click(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                string _tipoIdentificacion = this.TipoDocumento.SelectedValue;
                string _identificacion = this.Documento.Text;
                string _accion = "register";
                string redirectUri = ConfigurationManager.AppSettings["redirectUri"];
                var authenticationProperties = new AuthenticationProperties();
                if (!string.IsNullOrEmpty(_tipoIdentificacion) && !string.IsNullOrEmpty(_identificacion))
                    authenticationProperties.Dictionary.Add("login_hint", string.Format("{0},{1}", _tipoIdentificacion, _identificacion));
                if (!string.IsNullOrEmpty(_accion))
                {
                    if (_accion == "register")
                    {
                        string _nivel = "loa:2";
                        if (_tipoIdentificacion == "EM")
                            _nivel = "loa:1";
                        authenticationProperties.Dictionary.Add("acr_values", string.Format("action:{0} {1}", _accion, _nivel));
                    }
                    else
                        authenticationProperties.Dictionary.Add("acr_values", string.Format("action:{0}", _accion));
                }

                authenticationProperties.RedirectUri = redirectUri;
                var auth = HttpContext.Current.GetOwinContext().Authentication;
                auth.Challenge(authenticationProperties);
            }
        }
        protected void logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignOut.aspx");
        }

        protected void claims_Click(object sender, EventArgs e)
        {
            Response.Redirect("Claims.aspx");
        }

        protected async void personalizar_Click(object sender, EventArgs e)
        {
            var authenticationProperties = new AuthenticationProperties();
            var httpClient = new HttpClient();
            var userInfo = new UserInfoRequest();
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;

            userInfo.Address = ConfigurationManager.AppSettings["Authority"] + "/connect/userinfo";
            userInfo.Token = userClaims?.FindFirst("access_token")?.Value;
            var userInfoProfile = await httpClient.GetUserInfoAsync(userInfo);

            foreach (var claim in userInfoProfile.Claims)
            {
                if (claim.Type == "given_name")
                {
                    authenticationProperties.Dictionary.Add("login_hint", claim.Value);
                }
            }

            authenticationProperties.Dictionary.Add("acr_values", string.Format("action:manage"));
            var auth = HttpContext.Current.GetOwinContext().Authentication;
            auth.Challenge(authenticationProperties);
        }
    }
}