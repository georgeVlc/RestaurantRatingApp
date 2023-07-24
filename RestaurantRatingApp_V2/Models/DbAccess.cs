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

        public static (User, String) SelectUser(String username)
        {
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(ConString))
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
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
                e.Data.Add("UserMessage", "An error occured while reading user data");
                throw e;
            }
        }

        public static List<(User, String)> SelecAlltUsers()
        {
            List<(User, String)> userList = new List<(User, String)>();

            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand("select * from Users", connection);
                    SqlDataReader sdr = cm.ExecuteReader();
                    
                    while (sdr.Read())
                    {
<<<<<<< HEAD:RestaurantRatingApp_V2/Models/DbAccess.cs
                        User.UserType userType;
                        Enum.TryParse<User.UserType>(sdr["type"].ToString(), out userType);
                        UserList.Add(new User(sdr["name"].ToString(), userType));
=======
                        //System.Diagnostics.Debug.WriteLine(sdr["userName"].ToString());

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
>>>>>>> 96349fe (added API functionality):RestaurantRatingApp_V2/Models/dbAccess.cs
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
                e.Data.Add("UserMessage", "An error occured while reading user data");
                throw e;
            }

            return userList;
        }

        public static List<Restaurant> SelectRestaurants()
        {
            List<Restaurant> RestaurantList = new List<Restaurant>();
<<<<<<< HEAD:RestaurantRatingApp_V2/Models/DbAccess.cs
            var cousineType = new Dictionary<string, CousineType>(){
            {"Italian", CousineType.Italian},
            {"contemporary", CousineType.contemporary},
            {"Greek", CousineType.Greek},
            {"Asian", CousineType.Asian},
            {"Mexican", CousineType.Mexican}
            };
=======

>>>>>>> 96349fe (added API functionality):RestaurantRatingApp_V2/Models/dbAccess.cs
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;
                
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand("select * from Restaurant", connection);
                    SqlDataReader sdr = cm.ExecuteReader();

                    Restaurant.CousineType cousineType;

                    Enum.TryParse<Restaurant.CousineType>(sdr["resType"].ToString(), out cousineType);

                    while (sdr.Read())
                    {
<<<<<<< HEAD:RestaurantRatingApp_V2/Models/DbAccess.cs
                        CousineType type = cousineType[sdr["type"].ToString()];
                        RestaurantList.Add(new Restaurant(sdr["name"].ToString(), sdr["imageName"].ToString(), type));
=======
                        RestaurantList.Add(
                            new Restaurant(
                                sdr["resName"].ToString(), 
                                sdr["resImgName"].ToString(), 
                                cousineType,
                                sdr["resDescription"].ToString(),
                                sdr["resOwner"].ToString()
                            )
                        );
>>>>>>> 96349fe (added API functionality):RestaurantRatingApp_V2/Models/dbAccess.cs
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
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
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand("select * from Reviews", connection);
                    SqlDataReader sdr = cm.ExecuteReader();
<<<<<<< HEAD:RestaurantRatingApp_V2/Models/DbAccess.cs
                    System.Diagnostics.Debug.WriteLine("Reviews selection was successfull");
=======
                    
>>>>>>> 96349fe (added API functionality):RestaurantRatingApp_V2/Models/dbAccess.cs
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
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
                e.Data.Add("UserMessage", "An error occured while reading review data");
            }

            return ReviewsList;
        }

        public static void DeleteRestaurant(string restaurantName)
        {
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand("delete from Restaurant where resName='" + restaurantName + "';", connection);
                    SqlDataReader sdr = cm.ExecuteReader();
                }

                using (SqlConnection connection = new SqlConnection(ConString))
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
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
                e.Data.Add("UserMessage", "An error occured while attempting to delete restaurant");
                throw e;
            }
        }

        public static void DeleteUser(string name)
        {
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    // Opening Connection  
                    connection.Open();
                    // Creating SqlCommand objcet
                    SqlCommand cm = new SqlCommand("delete from Users where userName='" + name + "';", connection);
                    // Executing the SQL query  
                    SqlDataReader sdr = cm.ExecuteReader();
<<<<<<< HEAD:RestaurantRatingApp_V2/Models/DbAccess.cs
                    System.Diagnostics.Debug.WriteLine("Delete user was succesfull");
=======
>>>>>>> 96349fe (added API functionality):RestaurantRatingApp_V2/Models/dbAccess.cs
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
                e.Data.Add("UserMessage", "An error occured while attempting to delete user");
                throw e;
            }
        }

        public static void DeleteReview(string userName, string restaurant)
        {
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();
                    SqlCommand cm = new SqlCommand("delete from Reviews where userName='" + userName + "' and resName='" + restaurant + "';", connection);
                    SqlDataReader sdr = cm.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
                e.Data.Add("UserMessage", "An error occured while attempting to delete review");
                throw e;
            }
        }

        public static void InsertRestaurant(Restaurant r)
        {
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(ConString))
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

                using (SqlConnection connection = new SqlConnection(ConString))
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
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
                e.Data.Add("UserMessage", "An error occured while attempting to insert restaurant");
                throw e;
            }
        }

        public static void InsertReview(Review r)
        {
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(ConString))
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
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
                e.Data.Add("UserMessage", "An error occured while attempting to insert review");
                throw e;
            }
        }

        public static void InsertUser(String username, String pwd)
        {
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(ConString))
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
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
                e.Data.Add("UserMessage", "An error occured while attempting to insert user");
                throw e;
            }
        }
    }
}