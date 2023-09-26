using RestaurantRatingApp_V2.Controllers;
using RestaurantRatingApp_V2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static RestaurantRatingApp_V2.Models.Restaurant;

namespace RestaurantRatingApp_V2
{
    public partial class RegisterRestaurant : System.Web.UI.Page
    {
        User user = new User();
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (Session["User"] != null)
            {
                user = Session["User"] as User;
                Debug.WriteLine(user.Username);
                Debug.WriteLine(user.Type);
              
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Utility.RestaurantExists(txtRestaurantName.Text)) 
            {
                Restaurant restaurant = new Restaurant();
                restaurant.Name = txtRestaurantName.Text;
                restaurant.Description = txtDescription.Text;
                Enum.TryParse<Restaurant.CousineType>(ddlCategory.Text, out CousineType cousineType);
                restaurant.Owner = user.Username;
                restaurant.Type = cousineType;               
                Utility.AddRestaurant(user, restaurant);
                SuccessText.Text = "Restaurant Has Been Created Succesfully";
                Response.Redirect("RestaurantPage.aspx?Name=" + restaurant.Name);
            }
            else
            {
                FailureText.Text = "Restaurant Credentials Provided Correspond With An Already Existing Restaurant";
                ErrorMessage.Visible = true;
            }
        }
    }
}
