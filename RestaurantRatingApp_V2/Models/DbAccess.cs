using RestaurantRatingApp_V2.Models.RestaurantRatingApp_V2.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System;
using static RestaurantRatingApp_V2.Models.User;



namespace RestaurantRatingApp_V2.Models
{
    public class DbAccess
    {
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

        public static List<Restaurant> SelectRestaurants()
        {
            List<Restaurant> RestaurantList = new List<Restaurant>();

            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;
                
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand("select * from Restaurant", connection);
                    SqlDataReader sdr = cm.ExecuteReader();

                    Restaurant.CousineType cousineType;

                    Enum.TryParse<Restaurant.CousineType>(sdr["resType"].ToString(), out cousineType);

                    while (sdr.Read())
                    {
                        RestaurantList.Add(
                            new Restaurant(
                                sdr["resName"].ToString(), 
                                sdr["resImgName"].ToString(), 
                                cousineType,
                                sdr["resDescription"].ToString(),
                                sdr["resOwner"].ToString()
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

            return RestaurantList;
        }

        public static List<Review> SelectReviews()
        {
            List<Review> ReviewsList = new List<Review>();

            try
            {
                string conString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand("select * from Reviews", connection);
                    SqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {
                        ReviewsList.Add(
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

            return ReviewsList;
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
    }
}