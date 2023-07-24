using RestaurantRatingApp_V2.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;


namespace RestaurantRatingApp_V2.Models
{
    namespace RestaurantRatingApp_V2.Models
    {
        public class Review
        {
            private String _username;
            private String _restaurantName;
            private float _rating;


            public String Username { get { return this._username; } protected set { this._username = value; } }
            
            public String RestaurantName { get { return this._restaurantName; } protected set { this._restaurantName = value; } }
            
            public float Rating { get { return this._rating; } protected set { this._rating = value; } }

            public Review()
            {
                this.Username = this.RestaurantName = String.Empty;
                this.Rating = 0.0f;
            }

            public Review(String restaurantName, String username, float rating)
            {
                this.RestaurantName = restaurantName;
                this.Username = username;
                this.Rating = rating;
            }

            // attributes are allowed to change only through verification by Utily Controller
            // if the action is verified then given attribute will be setted to given value
            // otherwise Exception is thrown

            public void SetAttr(uint authCode, String attr, ref Object value)
            {
                if (Utility.VerifyAction(authCode))
                {
                    if (String.Equals(attr, "Username"))
                        this.Username = (String)value;
                    else if (String.Equals(attr, "RestaurantName"))
                        this.RestaurantName = (String)value;
                    else if (String.Equals(attr, "Rating"))
                        this.Rating = (float)value;
                    else
                        throw new Exception("unknown attribute: " + attr);
                }
                else
                    throw new Exception("unauthorised action");
            }
        }
    }
}