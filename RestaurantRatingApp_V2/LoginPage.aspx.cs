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
        protected void Page_Load(object sender, EventArgs e)
        {
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
                User user = new User();
                try
                {
                    user.Login(username, password);
                    Session["User"] = user;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                FormsAuthentication.SetAuthCookie(user.UserName, true);
                FormsAuthentication.RedirectFromLoginPage(user.Username, true);

            }
            else
            {
                FailureText.Text = "Invalid login attempt";
                ErrorMessage.Visible = true;

            }
        }
    }
}

