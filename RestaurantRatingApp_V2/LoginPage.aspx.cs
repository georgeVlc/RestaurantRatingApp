using Microsoft.AspNet.Identity.Owin;
using RestaurantRatingApp_V2.Account;
using RestaurantRatingApp_V2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantRatingApp_V2
{
    public partial class LoginPage : System.Web.UI.Page
    {
        User user = new User();
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "RegistrationPage.aspx";
            if (!IsPostBack)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
            }
        }


        protected void LogIn(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            if (IsValid)
            {
         
                try
                {
                    user.Login(username, password);
                    Session["User"] = user;
                    FormsAuthentication.SetAuthCookie(user.Username, true);
                    FormsAuthentication.RedirectFromLoginPage(user.Username, true);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    FailureText.Text = "Invalid Credentials Provided";
                    ErrorMessage.Visible = true;
                }
                

            }
            else
            {
                FailureText.Text = "Invalid login attempt";
                ErrorMessage.Visible = true;

            }
        }
    }
}

