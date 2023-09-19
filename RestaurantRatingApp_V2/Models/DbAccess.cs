using RestaurantRatingApp_V2.Models.RestaurantRatingApp_V2.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System;
using static RestaurantRatingApp_V2.Models.User;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Security.Cryptography;
using static RestaurantRatingApp_V2.Models.Restaurant;
using System.Linq;
using System.Web.Configuration;
using System.Web.Security;


namespace RestaurantRatingApp_V2.Models
{
    public class DbAccess
    {
        private static void ChangeUserPassword(string username, string newPwd)
        {
            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand(
                        "update Users set Users.userPwd = '" + newPwd +
                        "' where Users.userName = '" + username + "';",
                        connection
                    );

                    SqlDataReader sdr = cm.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured while updating User " + username);
                throw e;
            }
        }

        public static void UpdateRestaurantRating(String restaurantName)
        {
            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand(
                        "update Restaurant set Restaurant.resRating = t.avg_rating " +
                        "from ( select Reviews.resName, avg(Reviews.revRating ) as avg_rating " +
                        "from Reviews where Reviews.resName like '" + restaurantName +
                        "' group by Reviews.resName ) t " +
                        "where t.resName = Restaurant.resName;",
                        connection
                    );

                    SqlDataReader sdr = cm.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured while updating Restaurant " + restaurantName + " rating");
                throw e;
            }
        }

        public static (User, String) SelectUser(String username)
        {
            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand("select * from Users where userName='" + username + "';", connection);
                    SqlDataReader sdr = cm.ExecuteReader();

                    sdr.Read();

                    User.UserType userType;
                    Enum.TryParse<User.UserType>(sdr["userType"].ToString(), out userType);

                    (User, String) res = (
                            new User(
                                username,
                                userType,
                                sdr["resName"].ToString()
                            ),
                            sdr["userPwd"].ToString()
                    );

                    return res;
                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured while reading user data");
                throw e;
            }
        }

        public static List<(User, String)> SelectUsers(int numOfUsers)
        {
            List<(User, String)> userList = new List<(User, String)>();

            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    string cmdText = numOfUsers == -1 ?
                        "select * from Users;" :
                        "select * from Users limit " + numOfUsers.ToString() + ";";

                    SqlCommand cm = new SqlCommand(cmdText, connection);
                    SqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {
                        User.UserType userType;
                        Enum.TryParse<User.UserType>(sdr["userType"].ToString(), out userType);

                        userList.Add(
                            (
                                new User(
                                    sdr["userName"].ToString(),
                                    userType,
                                    sdr["resName"].ToString()
                                ),
                                sdr["userPwd"].ToString()
                            )
                        );
                    }
                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured while reading user data");
                throw e;
            }

            return userList;
        }

        public static List<Restaurant> SelectRestaurants(int numOfRestaurants)
        {
            List<Restaurant> restaurantList = new List<Restaurant>();

            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    string cmdText = numOfRestaurants == -1 ?
                        "select * from Restaurant;" :
                        "select * from Restaurant limit " + numOfRestaurants.ToString() + " ;";

                    Debug.WriteLine(cmdText);
                    SqlCommand cm = new SqlCommand(cmdText, connection);
                    SqlDataReader sdr = cm.ExecuteReader();

                    Restaurant.CousineType cousineType;



                    while (sdr.Read())
                    {
                        Enum.TryParse<Restaurant.CousineType>(sdr["resType"].ToString(), out cousineType);
                        restaurantList.Add(
                            new Restaurant(
                                sdr["resName"].ToString(),
                                sdr["resImgName"].ToString(),
                                cousineType,
                                sdr["resDescription"].ToString(),
                                sdr["resOwner"].ToString(),
                                float.Parse(sdr["resRating"].ToString())
                            )
                        );
                    }
                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured while reading restuarant data");
                throw e;
            }

            return restaurantList;
        }

        public static List<Restaurant> SelectRestaurantsByCousine(Restaurant.CousineType cousineType, int numOfRestaurants)
        {
            List<Restaurant> restaurantList = new List<Restaurant>();

            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    string cmdText = numOfRestaurants == -1 ?
                        "select * from Restaurant where Restaurant.resType = '" + cousineType.ToString() + "';" :
                        "select * from Restaurant where Restaurant.resType = '" + cousineType.ToString() + "' limit " + numOfRestaurants + ";";

                    SqlCommand cm = new SqlCommand(cmdText, connection);
                    SqlDataReader sdr = cm.ExecuteReader();


                    while (sdr.Read())
                    {

                        restaurantList.Add(
                            new Restaurant(
                                sdr["resName"].ToString(),
                                sdr["resImgName"].ToString(),
                                cousineType,
                                sdr["resDescription"].ToString(),
                                sdr["resOwner"].ToString(),
                                float.Parse(sdr["resRating"].ToString())

                            )
                        );
                    }
                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured while reading restuarant data");
                throw e;
            }

            return restaurantList;
        }

        public static List<Restaurant> SelectRestaurantsByCousine(string cousineType, int numOfRestaurants)
        {
            List<Restaurant> restaurantList = new List<Restaurant>();

            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    string cmdText = numOfRestaurants == -1 ?
                        "select * from Restaurant where Restaurant.resType = '" + cousineType + "';" :
                        "select * from Restaurant where Restaurant.resType = '" + cousineType + "' limit " + numOfRestaurants + ";";

                    SqlCommand cm = new SqlCommand(cmdText, connection);
                    SqlDataReader sdr = cm.ExecuteReader();
                    Restaurant.CousineType cousinetype;
                    while (sdr.Read())
                    {
                        Enum.TryParse<Restaurant.CousineType>(sdr["resType"].ToString(), out cousinetype);
                        restaurantList.Add(
                            new Restaurant(
                                sdr["resName"].ToString(),
                                sdr["resImgName"].ToString(),
                                cousinetype,
                                sdr["resDescription"].ToString(),
                                sdr["resOwner"].ToString(),
                                float.Parse(sdr["resRating"].ToString())
                            )
                        );
                    }
                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured while reading restuarant data");
                throw e;
            }

            return restaurantList;
        }

        public static List<Review> SelectReviews(int numOfReviews)
        {
            List<Review> reviewsList = new List<Review>();

            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;
                string cmdText = numOfReviews == -1 ?
                    "select * from Reviews;" :
                    "select * from Reviews limit " + numOfReviews.ToString() + " ;";


                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand(cmdText, connection);
                    SqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {
                        reviewsList.Add(
                            new Review(
                                sdr["userName"].ToString(),
                                sdr["resName"].ToString(),
                                float.Parse(sdr["revRating"].ToString())
                            )
                        );
                    }
                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured while reading review data");
            }

            return reviewsList;
        }


        public static List<Review> SelectReviewsByUsername(string username, int numOfReviews)
        {
            List<Review> reviewsList = new List<Review>();

            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;
                string cmdText = numOfReviews == -1 ?
                    "select * from Reviews where Reviews.userName = '" + username + "';" :
                    "select * from Reviews where Reviews.userName = '" + username + "' limit " + numOfReviews.ToString() + ";";


                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand(cmdText, connection);
                    SqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {
                        reviewsList.Add(
                            new Review(
                                sdr["userName"].ToString(),
                                sdr["resName"].ToString(),
                                float.Parse(sdr["revRating"].ToString())
                            )
                        );
                    }
                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured while reading review data");
            }

            return reviewsList;
        }

        public static List<Review> SelectReviewsByRestaurantName(string restaurantName, int numOfReviews)
        {
            List<Review> reviewsList = new List<Review>();

            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;
                string cmdText = numOfReviews == -1 ?
                    "select * from Reviews where Reviews.resName = '" + restaurantName + "';" :
                    "select * from Reviews where Reviews.resName = '" + restaurantName + "' limit " + numOfReviews.ToString() + ";";


                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand(cmdText, connection);
                    SqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {
                        reviewsList.Add(
                            new Review(
                                sdr["userName"].ToString(),
                                sdr["resName"].ToString(),
                                float.Parse(sdr["revRating"].ToString())
                            )
                        );
                    }
                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured while reading review data");
            }

            return reviewsList;
        }

        public static void DeleteRestaurant(string restaurantName)
        {
            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand("delete from Restaurant where resName='" + restaurantName + "';", connection);
                    SqlDataReader sdr = cm.ExecuteReader();
                }

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand(
                        "update Users" +
                        " set Users.resName = NULL, Users.userType = 'SIGNED'" +
                        " where Users.resName = '" + restaurantName + "';",
                        connection);
                    SqlDataReader sdr = cm.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured while attempting to delete restaurant");
                throw e;
            }
        }

        public static void DeleteUser(string name)
        {
            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    // Opening Connection  
                    connection.Open();
                    // Creating SqlCommand objcet
                    SqlCommand cm = new SqlCommand("delete from Users where userName='" + name + "';", connection);
                    // Executing the SQL query  
                    SqlDataReader sdr = cm.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured while attempting to delete user");
                throw e;
            }
        }

        public static void DeleteReview(string userName, string restaurant)
        {
            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand("delete from Reviews where userName='" + userName + "' and resName='" + restaurant + "';", connection);
                    SqlDataReader sdr = cm.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured while attempting to delete review");
                throw e;
            }

            UpdateRestaurantRating(restaurant);
        }

        public static void InsertRestaurant(Restaurant r)
        {
            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    SqlCommand cm =
                        new SqlCommand(
                            "insert into Restaurant values ('" + r.Name +
                            "' , " + r.Rating + ", '" +
                            r.ImgName + "' , '" +
                            r.Type.ToString() + "' , '" +
                            r.Description + "' , '" + r.Owner + "');",
                            connection
                            );

                    SqlDataReader sdr = cm.ExecuteReader();
                    System.Diagnostics.Debug.WriteLine("Restaurant insert was successful");
                }

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    SqlCommand cm = new SqlCommand(
                        "update Users set Users.userType = 'APPLICANT'" +
                        ", Users.resName = '" + r.Name + "' where Users.userName = '" + r.Owner + "';",
                        connection
                    );

                    SqlDataReader sdr = cm.ExecuteReader();
                    System.Diagnostics.Debug.WriteLine("User update was successful");
                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured while attempting to insert restaurant");
                throw e;
            }
        }

        public static void InsertReview(Review r)
        {
            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    SqlCommand cm = new SqlCommand("insert into Reviews values ('" + r.Username +
                        "', '" + r.RestaurantName +
                        "', " + r.Rating + ");"
                        , connection);

                    SqlDataReader sdr = cm.ExecuteReader();
                    System.Diagnostics.Debug.WriteLine("Review insert was successful");
                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured while attempting to insert review");
                throw e;
            }

            UpdateRestaurantRating(r.RestaurantName);
        }

        public static void InsertUser(String username, String pwd)
        {
            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(conString))
                {

                    connection.Open();

                    SqlCommand cm = new SqlCommand("insert into Users (userName, userType, userPwd) values ('" + username +
                        "', '" + User.UserType.SIGNED.ToString() +
                        "', '" + pwd + "');",
                        connection);

                    SqlDataReader sdr = cm.ExecuteReader();
                    System.Diagnostics.Debug.WriteLine("User insert was successful");
                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured while attempting to insert user");
                throw e;
            }
        }


        //---------------------------------------------------------------------------//
        //Dimitris 
        //Methods to be called by the Utility class

        public static Restaurant SelectRestaurant(String name)
        {
            Debug.WriteLine("Name:" + name);
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand("select * from Restaurant where resName='" + name + "';", connection);
                    SqlDataReader sdr = cm.ExecuteReader();
                    Restaurant.CousineType cousineType;

                    sdr.Read();
                    Enum.TryParse<Restaurant.CousineType>(sdr["resType"].ToString(), out cousineType);
                    Restaurant restaurant = new Restaurant(
                                                    sdr["resName"].ToString(),
                                                    sdr["resImgName"].ToString(),
                                                    cousineType,
                                                    sdr["resDescription"].ToString(),
                                                    sdr["resOwner"].ToString(),
                                                    float.Parse(sdr["resRating"].ToString())
                                                );

                    return restaurant;

                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
                e.Data.Add("UserMessage", "An error occured while reading restuarant data");
                throw e;
            }

        }

        public static List<Restaurant> SelectTopRated()
        {
            List<Restaurant> restaurantList = new List<Restaurant>();
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand("SELECT * FROM Restaurant ORDER BY resRating DESC OFFSET 1 ROWS FETCH NEXT 4 ROWS ONLY;", connection);
                    SqlDataReader sdr = cm.ExecuteReader();
                    Restaurant.CousineType cousineType;



                    while (sdr.Read())
                    {
                        Enum.TryParse<Restaurant.CousineType>(sdr["resType"].ToString(), out cousineType);
                        restaurantList.Add(
                            new Restaurant(
                                sdr["resName"].ToString(),
                                sdr["resImgName"].ToString(),
                                cousineType,
                                sdr["resDescription"].ToString(),
                              sdr["resOwner"].ToString(),
                                float.Parse(sdr["resRating"].ToString())
                            )
                        );
                    }
                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured while reading restuarant data");
                throw e;
            }

            return restaurantList;
        }


        //---------------------Authentication Methods-------------------------//

        public static bool AuthenticateUser(string username, string password)
        {
            // Authenticate the user against your database
            string connectionString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT userPWD FROM Users WHERE userName = @Username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string storedPasswordHash = reader["userPwd"].ToString();

                    // Verify the entered password against the stored password hash
                    if (VerifyPassword(password, storedPasswordHash))
                    {
                        // Log in the user
                        FormsAuthentication.SetAuthCookie(username, false);
                        return true;
                    }
                }

                return false;
            }
        }

        public static bool UserExists(String username)
        {
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand("SELECT COUNT(*)  FROM Users WHERE userName = @Username",connection);
                    {
                        cm.Parameters.AddWithValue("@Username", username);
                        int count = (int)cm.ExecuteScalar();

                        if (count > 0) { return true; }
                  
                        return false;

                    }

                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured During Sign Up");
                throw e;
            }
        }


       



        private static bool VerifyPassword(string enteredPassword, string storedPasswordHash)  //Temporary until proper hashing and 
        {

            if (enteredPassword != storedPasswordHash)                    // implement encryption method for converting passwords and entered passwords to hashed 
            {
                return false;
            }
            return true;
        }


        public static User SelectUserByUsername(String username)
        {
            User res;
            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand("select * from Users where userName='" + username + "';", connection);
                    SqlDataReader sdr = cm.ExecuteReader();

                    sdr.Read();

                    User.UserType userType;
                    Enum.TryParse<User.UserType>(sdr["userType"].ToString(), out userType);

                    res = (
                            new User(
                                username,
                                userType,
                                sdr["resName"].ToString()
                            ));

                    return res;
                }
            }
            catch (Exception e)
            {
                e.Data.Add("UserMessage", "An error occured while reading user data");
                throw e;
            }
        }
    }
}

