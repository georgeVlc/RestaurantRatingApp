using RestaurantRatingApp_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestaurantRatingApp_V2.Tests;
using RestaurantRatingApp_V2.Models.RestaurantRatingApp_V2.Models;


namespace RestaurantRatingApp_V2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RunTests();
        }
        protected void RunTests()
        {
            //Test.SignUpUser("l_username", "4321");
            //Test.LoginUser("x_username", "4321");
            //Test.LogoutUser("x_username", "4321");

            //Restaurant testRestaurant = new Restaurant(
            //    "test_restaurant2",
            //    "an_img.png",
            //    Restaurant.CousineType.ITALIAN,
            //    "some words",
            //    "x_username"
            //    );

            //Test.AddRestaurant(testRestaurant);

            Review testReviw = new Review(
                "test_restaurant2",
                "x_username",
                3.9f
                );

            //Test.MakeReview(testReviw);
            Test.RemoveReview(testReviw);
            //Test.RemoveRestaurant(testRestaurant);
        }
    }
}