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
using RestaurantRatingApp_V2.Controllers;

namespace RestaurantRatingApp_V2
{
    public partial class _Default : Page
    {
        private User user = new User();

        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (!IsPostBack)
            {
                if (Session["User"] != null)
                {
                    user = Session["User"] as User;

                    Debug.WriteLine(user.Type.ToString());
                    Debug.WriteLine(user.Username);
                }

            }
        }


        //----------------UI Methods----------------//

        public List<Restaurant> GetTopRatedRestaurants()
        {
            List<Restaurant> restaurants = DbAccess.SelectTopRated();           //To be changed to Utility
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

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string searched = Request["searchInput"];
            Debug.WriteLine(searched);
            Response.Redirect("SearchResults.aspx?searched=" + searched);
        }

        protected void SeeAll(object sender , EventArgs e)
        {
            Response.Redirect("SearchResults.aspx?");
        }

       
    }

}