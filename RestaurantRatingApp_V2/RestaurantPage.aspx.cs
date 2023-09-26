using System;
using System.Collections.Generic;
using RestaurantRatingApp_V2.Models;
using RestaurantRatingApp_V2.Controllers;
using RestaurantRatingApp_V2.Models.RestaurantRatingApp_V2.Models;
using AjaxControlToolkit;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace RestaurantRatingApp_V2
{
    public partial class RestaurantPage : System.Web.UI.Page
    {
        static string ResName;
        static int currentrating;
        private User user = new User();

        protected void Page_Load(object sender, EventArgs e)
        {
            ResName = Request.QueryString["Name"];


            if (Session["User"] != null)
            {
                user = Session["User"] as User;
                Debug.WriteLine(user.Type.ToString());
                Debug.WriteLine(user.Username);
            }

            List<Review> reviews = new List<Review>();

            reviews = Utility.GetReviewsByRestaurantName(ResName, -1);

            ReviewsListView.DataSource = reviews;
            ReviewsListView.DataBind();

        }

        public static Restaurant GetRestaurant()
        {   
            Restaurant restaurant = Utility.GetRestaurantByName(ResName);         
            return restaurant;
        }

        public static List<Review> GetReviews()
        {
            List<Review> reviews = new List<Review>();
            reviews = Utility.GetReviewsByRestaurantName(ResName, -1);
            return reviews;

        }
       

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (Session["User"] != null) 
            {
                if (!Utility.HasReviewed(user.Username, ResName))           //If he hasnt reviewed
                {
                    Review review = new Review(ResName, user.Username, (float)currentrating);
                    if (user.MakeReview(review))
                    {
                        Response.Redirect("RestaurantPage.aspx?Name=" + ResName);
                    }
                }
                else
                {
                    //Error message on page
                }
            } 
            else { Response.Redirect("LoginPage.aspx"); }

        }


        public void Rating1_Changed(object sender, RatingEventArgs e)
        {
            if (Int32.TryParse(e.Value, out currentrating))
            {
                Debug.Write(currentrating);
            }

        }

        public static Int64 GetIntegerPart(float x)
        {
            var integerPart = Math.Truncate(x);

            return x - integerPart >= 0.5 ?
                (Int64)Math.Ceiling(x) :
                (Int64)Math.Floor(x); ;
        }





        protected void ReviewsListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var ratingDiv = (HtmlGenericControl)e.Item.FindControl("ratingDiv");
                var rating = DataBinder.Eval(e.Item.DataItem, "Rating");

                rating = GetIntegerPart((float)rating);

                Debug.WriteLine(rating.GetType() == typeof(Int64));
                Debug.WriteLine(rating);


                // Generate gold stars as spans with the star character.
                for (Int64 i = 0; i < (Int64)rating; i++)
                {
                    var starSpan = new HtmlGenericControl("span");
                    starSpan.Attributes["class"] = "reviewstar";
                    starSpan.InnerHtml = "&#9733;"; // Unicode character for a star
                    ratingDiv.Controls.Add(starSpan);

                }


            }
        }





    }
}