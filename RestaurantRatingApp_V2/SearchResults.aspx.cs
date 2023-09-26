using RestaurantRatingApp_V2.Controllers;
using RestaurantRatingApp_V2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        static string searchedterm;
        User user = new User();
      
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                selectedcategory = Request.QueryString["category"];
                searchedterm = Request.QueryString["searched"];
               
                
                Debug.WriteLine("Page Load Searched "+ searchedterm);

                if (!string.IsNullOrEmpty(selectedcategory))
                {
                    rbfilterlist.SelectedValue = selectedcategory;
                }

            }          
        }

        public List<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            if (string.IsNullOrEmpty(selectedcategory) && string.IsNullOrEmpty(searchedterm))           //No category and no search term
            {
                restaurants = Utility.GetRestaurants(-1);
                Restaurant.SortRestaurantsByWilsonScore(restaurants);
                return restaurants;
            }
            if (!string.IsNullOrEmpty(selectedcategory))                    //Category Selected
            {              
                Restaurant.CousineType cousineType;
                Enum.TryParse<Restaurant.CousineType>(selectedcategory, out cousineType);
                restaurants = Utility.GetRestaurantsByCousine(cousineType, -1);
                Restaurant.SortRestaurantsByWilsonScore(restaurants); 
                return restaurants;
            }

            if (!string.IsNullOrEmpty(searchedterm))                    //Searched Term
            {
                return user.MakeSearch(searchedterm, -1, true);
            }    

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