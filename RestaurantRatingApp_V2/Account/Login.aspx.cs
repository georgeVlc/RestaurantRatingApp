using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using RestaurantRatingApp_V2.Models;
using RestaurantRatingApp_V2.Models.RestaurantRatingApp_V2;


namespace RestaurantRatingApp_V2.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }

        }

        protected void LogIn(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;
     

            if (IsValid)
            {
                // Validate the users credentials
                if (DbAccess.AuthenticateUser(username, password))
                {
                    // Create a session for the user
                    Session["Username"] = username; // Store the user's ID in session
                                                    // Session["UserType"] = user; // Store the user's type in session


                    User user1 = DbAccess.SelectUserByUsername(username);
                    Session["User"]= user1;
                    Debug.WriteLine(user1.Type); 
                    Debug.WriteLine(user1.Username);
                    Response.Redirect("Default.aspx");


                }
                else
                {
                    FailureText.Text = "Invalid login attempt";
                    ErrorMessage.Visible = true;

                }

                /*



                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
                var result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        break;
                    case SignInStatus.LockedOut:
                        Response.Redirect("/Account/Lockout");
                        break;
                    case SignInStatus.RequiresVerification:
                        Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                        Request.QueryString["ReturnUrl"],
                                                        RememberMe.Checked),
                                          true);
                        break;
                    case SignInStatus.Failure:
                    default:
                        FailureText.Text = "Invalid login attempt";
                        ErrorMessage.Visible = true;
                        break;
                }
                */
            }


        }





    }
}
