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
        }
        protected void RunTests()
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
    }
}