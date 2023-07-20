using RestaurantRatingApp_V2.Models;
using RestaurantRatingApp_V2.Models.RestaurantRatingApp_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantRatingApp_V2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var restaurants = DbAccess.SelectRestaurants();
            foreach (Restaurant r in restaurants)
            {
                System.Diagnostics.Debug.WriteLine(r.Type);
            }
        }
    }
}