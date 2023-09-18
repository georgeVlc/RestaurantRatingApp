using RestaurantRatingApp_V2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace RestaurantRatingApp_V2
{
    public partial class SearchResults : System.Web.UI.Page
    {
        static string selectedcategory;
      
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                selectedcategory = Request.QueryString["category"];
            }

            
            if (!string.IsNullOrEmpty(selectedcategory))
            {
               // rbfilterlist.SelectedValue = selectedcategory;
            }
            

            
        }

        public List<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
           

            if (string.IsNullOrEmpty(selectedcategory))
            {
                restaurants = DbAccess.SelectRestaurants(-1);
            }
            else 
            {
               // Debug.WriteLine("On DB call" + selectedcategory);
                restaurants = DbAccess.SelectRestaurantsByCousine(selectedcategory, -1);     
            }

            Restaurant.SortRestaurantsByWilsonScore(restaurants);
            return restaurants;

        }

        protected void SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = rbfilterlist.SelectedValue;
            Debug.WriteLine("On Filter Save" + selectedValue);
            selectedcategory = selectedValue;
            ListView1.DataBind();
        }


        protected void removefilterButtonClick(object sender, EventArgs e)
        {
              Response.Redirect("SearchResults.aspx?");
        }

   


    }



}