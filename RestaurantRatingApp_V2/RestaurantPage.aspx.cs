using System;
using System.Collections.Generic;
using RestaurantRatingApp_V2.Models;
using RestaurantRatingApp_V2.Controllers;
using RestaurantRatingApp_V2.Models.RestaurantRatingApp_V2.Models;

namespace RestaurantRatingApp_V2
{
    public partial class RestaurantPage : System.Web.UI.Page
    {
        static string ResName;

        protected void Page_Load(object sender, EventArgs e)
        {
            ResName = Request.QueryString["Name"];
            

        }

        public static Restaurant GetRestaurant()
        {   
            Restaurant restaurant = DbAccess.SelectRestaurant(ResName);
            return restaurant;
        }

        public static List<Review> GetReviews()
        {
            List<Review> reviews = new List<Review>();

            reviews = Utility.GetReviewsByRestaurantName(ResName, -1);

            return reviews;

        }
        private void BindReviews()
        {
            // Replace this with code to fetch reviews from your data source.
            List<Review> reviews = Utility.GetReviewsByRestaurantName(ResName, -1);

            ReviewListView.DataBind();
        }

    }
}