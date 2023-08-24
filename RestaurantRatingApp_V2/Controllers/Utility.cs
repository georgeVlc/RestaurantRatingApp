using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Text;
using RestaurantRatingApp_V2.Models;
using System.Diagnostics;
using RestaurantRatingApp_V2.Models.RestaurantRatingApp_V2.Models;


namespace RestaurantRatingApp_V2.Controllers
{
    public static class Utility
    {
        // code to verify requested actions
        // generated in runtime
        private static uint VERIFICATION_CODE = GenHashAsUint();

        // creates a byte array of sizeInKb * 1024 random values
        private static byte[] GetByteArray(uint sizeInKb)
        {
            Random rnd = new Random();
            byte[] b = new byte[sizeInKb * 1024]; // convert kb to byte

            rnd.NextBytes(b);

            return b;
        }

        // generates hash code by SHA256
        private static uint GenHashAsUint() { return BitConverter.ToUInt32(SHA256.Create().ComputeHash(GetByteArray(1)), 0); }


        // is used as request API for given actions related to parameter settings
        public static bool VerifyAction(uint authCode) { return authCode == VERIFICATION_CODE; }

        public static List<User> GetUsers(User user, int numOfUsers)
        {
            if (user.Type != User.UserType.ADMIN)
                throw new Exception("Unauthorized action, only ADMIN can perform this action");

            try
            {
                List<(User, string)> usersData = DbAccess.SelectUsers(numOfUsers);
                List<User> users = new List<User>();
                
                foreach ((User, string) item in usersData)
                    users.Add(item.Item1);

                return users;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<Restaurant> GetRestaurants(User user, int numOfRestaurants)
        {
            try { List<Restaurant> restaurants = DbAccess.SelectRestaurants(numOfRestaurants); return restaurants; }
            catch (Exception e) { throw e; }
        }

        public static List<Restaurant> GetRestaurantsByCousine(User user, Restaurant.CousineType cousineType, int numOfRestaurants)
        {
            try { List<Restaurant> restaurants = DbAccess.SelectRestaurantsByCousine(cousineType, numOfRestaurants); return restaurants; }
            catch (Exception e) { throw e; }
        }

        public static List<Review> GetReviews(int numOfReviews)
        {
            try { List<Review> reviews = DbAccess.SelectReviews(numOfReviews); return reviews; }
            catch (Exception e) { throw e; }
        }

        public static List<Review> GetReviewsByUsername(string username, int numOfReviews)
        {
            try { List<Review> reviews = DbAccess.SelectReviewsByUsername(username, numOfReviews); return reviews; }
            catch (Exception e) { throw e; };
        }

        public static List<Review> GetReviewsByRestaurantName(string restaurantName, int numOfReviews)
        {
            try { List<Review> reviews = DbAccess.SelectReviewsByRestaurantName(restaurantName, numOfReviews); return reviews; }
            catch (Exception e) { throw e; };
        }

        public static void LoginUser(User user, String username, String pwd)
        {
            try
            {
                (User, String) userData = DbAccess.SelectUser(username);
                
                if (!(userData.Item1 is null) && userData.Item2.Equals(pwd))
                {
                    user.SetAttr(VERIFICATION_CODE, "Username", username);
                    user.SetAttr(VERIFICATION_CODE, "Type", userData.Item1.Type);
                    user.SetAttr(VERIFICATION_CODE, "RestaurantName", userData.Item1.RestaurantName);
                }
                else
                    throw new Exception("User not found");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void LogoutUser(User user)
        {
            try
            {
                (User, String) userData = DbAccess.SelectUser(user.Username);

                if (userData.Item1.Username != String.Empty)
                {
                    user.SetAttr(VERIFICATION_CODE, "Username", String.Empty);
                    user.SetAttr(VERIFICATION_CODE, "Type", User.UserType.GUEST);
                    user.SetAttr(VERIFICATION_CODE, "RestaurantName", String.Empty);
                }
                else
                    throw new Exception("User not found");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void SignUpUser(User user, String username, String pwd)
        {
            try
            {
                DbAccess.InsertUser(username, pwd);
                user.SetAttr(VERIFICATION_CODE, "Username", username);
                user.SetAttr(VERIFICATION_CODE, "Type", User.UserType.SIGNED);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void AddRestaurant(User user, Restaurant restaurant)
        {
            try
            {
                if (user.Type == User.UserType.GUEST)
                    throw new Exception("Unauthorized action, you need to be SIGNED-IN to continue");

                //if (!user.RestaurantName.Equals(restaurant.Name))
                    //throw new Exception("Invalid operation, restaurant " + restaurant.Name + " is under different ownership");

                DbAccess.InsertRestaurant(restaurant);
                
                if (user.Type != User.UserType.ADMIN)
                    user.SetAttr(VERIFICATION_CODE, "Type", "APPLICANT");
                user.SetAttr(VERIFICATION_CODE, "RestaurantName", restaurant.Name);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void RemoveRestaurant(User user, Restaurant restaurant)
        {
            try
            {
                if (!(user.Type == User.UserType.APPLICANT || user.Type == User.UserType.ADMIN))
                    throw new Exception("You don't have a restaurant");

                if (!(user.RestaurantName.Equals(restaurant.Name)))
                    throw new Exception("Invalid operation, restaurant " + restaurant.Name + " is under different ownership");

                DbAccess.DeleteRestaurant(restaurant.Name);

                if (user.Type != User.UserType.ADMIN)
                    user.SetAttr(VERIFICATION_CODE, "Type", "SIGNED");
                user.SetAttr(VERIFICATION_CODE, "RestaurantName", String.Empty);
                restaurant.Owner = String.Empty;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void AddReview(User user, Review review)
        {
            try
            {
                if (user.Type == User.UserType.GUEST)
                    throw new Exception("Unauthorized action, you need to be SIGNED-IN to continue");

                DbAccess.InsertReview(review);
                // update restaurant rating?
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void RemoveReview(User user, Review review)
        {
            try
            {
                if (user.Type == User.UserType.GUEST)
                    throw new Exception("Unauthorized action, you need to be SIGNED-IN to continue");

                DbAccess.DeleteReview(user.Username, review.RestaurantName);
                // update restaurant rating?
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static StringBuilder MakeSearch(StringBuilder sb)
        {
            // DBHandler.MakeSearch
            // return results (maybe not string builder)
            return new StringBuilder();
        }
    }
}