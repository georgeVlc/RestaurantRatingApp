using RestaurantRatingApp_V2.Controllers;
using RestaurantRatingApp_V2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static RestaurantRatingApp_V2.Models.Restaurant;

namespace RestaurantRatingApp_V2
{
    public partial class RegisterRestaurant : System.Web.UI.Page
    {
        User user;
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
                Response.Redirect("Default.aspx");
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            Restaurant restaurant = new Restaurant();
            restaurant.Name = txtRestaurantName.Text;
            restaurant.Description = txtDescription.Text;
            Enum.TryParse<Restaurant.CousineType>(ddlCategory.Text, out CousineType cousineType);
            restaurant.Owner = user.UserName;
            restaurant.Type = CousineType.GREEK;
            DbAccess.InsertRestaurant(restaurant);

        }
    }
}
