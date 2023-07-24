using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using RestaurantRatingApp_V2.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using RestaurantRatingApp_V2.Models.RestaurantRatingApp_V2.Models;


namespace RestaurantRatingApp_V2.Models
{
    public class User
    {
        public enum UserType : byte
        {
            GUEST = 0,
            SIGNED,
            ADMIN,
            APPLICANT
        }


        // private members
        private String _username;
        private UserType _userType;
        private String _restaurantName;


        // public accessors
        public String Username { get { return this._username; } protected set { this._username = value; } }

        public UserType Type { get { return this._userType; } protected set { this._userType = value; } }

        public String RestaurantName { get { return this._restaurantName; } protected set { this._restaurantName = value; } }


        // setters
        // attributes are allowed to change only through verification by Utily Controller
        // if the action is verified then given attribute will be set to given value
        // otherwise Exception is thrown

        public void SetAttr(uint authCode, String attr, Object value)
        {
            if (Utility.VerifyAction(authCode))
            {
                if (String.Equals(attr, "Username"))
                    this.Username = value as String;
                else if (String.Equals(attr, "Type"))
                    this.Type = (UserType)Enum.Parse(typeof(UserType), value.ToString());
                else if (String.Equals(attr, "RestaurantName"))
                    this.RestaurantName = value as String;
                else
                    throw new Exception("unknown attribute: " + attr);
            }
            else
                throw new Exception("unauthorized action");
        }


        public User()
        {
            this.Username = this.RestaurantName = String.Empty;
            this.Type = UserType.GUEST;
        }

        // main constructor
        public User(String username, UserType userType, String restaurantName = "")
        {
            this.Username = username;
            this.Type = userType;
            this.RestaurantName = restaurantName;
        }

        // functionalities

        public bool Login(String username, String pwd)
        {
            try { Utility.LoginUser(this, username, pwd); return true; }
            catch (Exception e) { throw e; }
        }

        public bool Logout()
        {
            try { Utility.LogoutUser(this); return true; }
            catch (Exception e) { throw e; }
        }

        public bool SignUp(String username, String pwd)
        {
            try { Utility.SignUpUser(this, username, pwd); return true; }
            catch (Exception e) { throw e; }
        }

        void MakeSearch(StringBuilder sb)
        {
            var results = Utility.MakeSearch(sb);
            // display or pass results to view/despayer
        }

        public bool AddRestaurant(Restaurant restaurant)
        {
            try
            {
                Utility.AddRestaurant(this, restaurant);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool RemoveRestaurant(Restaurant restaurant)
        {
            try
            {
                Utility.RemoveRestaurant(this, restaurant);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool MakeReview(Review review)
        {
            try
            {
                Utility.AddReview(this, review);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool RemoveReview(Review review)
        {
            try
            {
                Utility.RemoveReview(this, review);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}