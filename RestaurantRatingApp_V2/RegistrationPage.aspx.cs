using RestaurantRatingApp_V2.Controllers;
using RestaurantRatingApp_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantRatingApp_V2
{
    public partial class RegistrationPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                Session.Abandon();
            }
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            if (IsValid)
            {
                if (!(Utility.UserExists(username)))
                {
                    User user = new User();
                    user.SignUp(username, password);
                    Session["User"] = user;
                    FormsAuthentication.SetAuthCookie(user.Username, true);
                    FormsAuthentication.RedirectFromLoginPage(user.Username, true);
                }
                else
                {
                    FailureText.Text = "Invalid Sign Up Attempt. A User With The Same Username Already Exists";
                    ErrorMessage.Visible = true;
                   
                }

            }
        }
    }
}