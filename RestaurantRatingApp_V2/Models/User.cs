using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using RestaurantRatingApp_V2.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

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

        // public accessors
        [Required, StringLength(100), Display(Name = "Name")]
        public String Username { get { return this._username; } protected set { this._username = value; } }
        [Required, StringLength(100), Display(Name = "Type")]
        public UserType Type { get  { return this._userType; } protected set { this._userType = value; } }

        // setters
        // attributes are allowed to change only through verification by Utily Controller
        // if the action is verified then given attribute will be setted to given value
        // otherwise Exception is thrown

        public void SetAttr(uint authCode, String attr, Object value)
        {
            if (Utility.VerifyAction(authCode))
            {
                if (String.Equals(attr, "Username"))
                    this.Username = value as String;
                else if (String.Equals(attr, "Type"))
                    this.Type = (UserType)value;
                else
                    throw new Exception("unknown attribute: " + attr);
            }
            else
                throw new Exception("unauthorised action");
        }
        
        public User()
        {
            this.Username = String.Empty;
            this.Type = UserType.GUEST;
        }

        // main constructor
        public User(String username, UserType userType)
        {
            this.Username = username;
            this.Type= userType;
        }

        // functionalities

        void Login(String username, String pwd)
        {
            try { Utility.LoginUser(this, username, pwd); }
            catch (Exception e) { throw e; }
        }

        void Logout()
        {
            try { Utility.LogoutUser(this); }
            catch (Exception e) { throw e; }
        }

        void SignUp(String username, String pwd)
        {
            try { Utility.SignUpUser(this, username, pwd); }
            catch (Exception e) { throw e; }
        }

        void MakeSearch(StringBuilder sb)
        {
            var results = Utility.MakeSearch(sb);
            // display or pass results to view/despayer
        }

        void MakeApplication()
        {

        }

        void ReviewApplication()
        {

        }

        void DeleteUser()
        {

        }

        void MakeReview()
        {

        }

        void EditReview()
        {

        }

        void DeleteReview()
        {

        }
    }
}