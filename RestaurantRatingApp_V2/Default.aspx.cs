using RestaurantRatingApp_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestaurantRatingApp_V2.Tests;
using RestaurantRatingApp_V2.Models.RestaurantRatingApp_V2.Models;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace RestaurantRatingApp_V2
{
    public partial class _Default : Page
    {
        private User user;
        protected void Page_Load(object sender, EventArgs e)
        {
     

            if (!IsPostBack)
            {
                if (Session["User"] != null)
                {
                    user = Session["User"] as User;
                    string username = user.Username;
                    string usertype = user.Type.ToString();
                }
                else
                {

                }


            }
        }


        private void LoginAdmin()
        {
            this.user.Login("admin_user", "1234");
        }


        /*protected void RunTests()
        {
            Test.SignUpUser("i_username", "4321");
            Test.LoginUser("a_username", "4321");
            Test.LogoutUser("a_username", "4321");

            Restaurant testRestaurant = new Restaurant(
                "test_restaurant1",
                "an_img.png",
                Restaurant.CousineType.ITALIAN,
                "some words",
                "h_username"
                );

            Test.AddRestaurant(testRestaurant);

            Review testReviw = new Review(
                "test_restaurant1",
                "h_username",
                1.9f
                );

            Test.MakeReview(testReviw);
            Test.RemoveReview(testReviw);
            Test.RemoveRestaurant(testRestaurant);
        }

        */


        public List<Restaurant> GetTopRatedRestaurants()
        {
            List<Restaurant> restaurants = DbAccess.SelectTopRated();

            return restaurants;
        }


        public List<String> GetCousineTypesAsStrings()
        {
            return Restaurant.GetCousineTypes().ConvertAll(x => x.ToString());
               
        }

        protected void register_click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterRestaurant.aspx");
        }
    }
}