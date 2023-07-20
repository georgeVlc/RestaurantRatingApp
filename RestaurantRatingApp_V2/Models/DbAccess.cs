

using RestaurantRatingApp_V2.Models.RestaurantRatingApp_V2.Models;
using static RestaurantRatingApp_V2.Models.Restaurant;
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

        public static List<User> SelectUsers()
        {
            List<User> UserList = new List<User>();
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    // Opening Connection  
                    connection.Open();
                    // Creating SqlCommand objcet
                    SqlCommand cm = new SqlCommand("select * from Users", connection);
                    // Executing the SQL query  
                    SqlDataReader sdr = cm.ExecuteReader();
                    System.Diagnostics.Debug.WriteLine("User select doen");
                    while (sdr.Read())
                    {
                        User.UserType userType;
                        Enum.TryParse<User.UserType>(sdr["type"].ToString(), out userType);
                        UserList.Add(new User(sdr["name"].ToString(), userType));
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
            }
            return UserList;
        }

        public static List<Restaurant> SelectRestaurants()
        {
            List<Restaurant> RestaurantList = new List<Restaurant>();
            var cousineType = new Dictionary<string, CousineType>(){
            {"Italian", CousineType.Italian},
            {"contemporary", CousineType.contemporary},
            {"Greek", CousineType.Greek},
            {"Asian", CousineType.Asian},
            {"Mexican", CousineType.Mexican}
            };
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    // Opening Connection  
                    connection.Open();
                    // Creating SqlCommand objcet
                    SqlCommand cm = new SqlCommand("select * from Restaurant", connection);
                    // Executing the SQL query  
                    SqlDataReader sdr = cm.ExecuteReader();
                    while (sdr.Read())
                    {
                        CousineType type = cousineType[sdr["type"].ToString()];
                        RestaurantList.Add(new Restaurant(sdr["name"].ToString(), sdr["imageName"].ToString(), type));
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
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
                    // Opening Connection  
                    connection.Open();
                    // Creating SqlCommand objcet
                    SqlCommand cm = new SqlCommand("select * from Reviews", connection);
                    // Executing the SQL query  
                    SqlDataReader sdr = cm.ExecuteReader();
                    System.Diagnostics.Debug.WriteLine("Reviews selection was successfull");
                    while (sdr.Read())
                    {
                        ReviewsList.Add(new Review(sdr["restaurant"].ToString(), sdr["userName"].ToString(), float.Parse(sdr["rating"].ToString())));
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
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
                    // Opening Connection  
                    connection.Open();
                    // Creating SqlCommand objcet
                    SqlCommand cm = new SqlCommand("delete from Restaurant where name='"+restaurantName+"';", connection);
                    // Executing the SQL query  
                    SqlDataReader sdr = cm.ExecuteReader();
                    //stem.Diagnostics.Debug.WriteLine("hello1");
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
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
                    SqlCommand cm = new SqlCommand("delete from Users where name='" + name + "';", connection);
                    // Executing the SQL query  
                    SqlDataReader sdr = cm.ExecuteReader();
                    System.Diagnostics.Debug.WriteLine("Delete user was succesfull");
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
            }
        }

        public static void DeleteReview(string userName, string restaurant)
        {
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    // Opening Connection  
                    connection.Open();
                    // Creating SqlCommand objcet
                    SqlCommand cm = new SqlCommand("delete from reviews where userName='" + userName + "' and restaurant='"+ restaurant + "';", connection);
                    // Executing the SQL query  
                    SqlDataReader sdr = cm.ExecuteReader();
                    System.Diagnostics.Debug.WriteLine("deletion was successful");
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
            }
        }

        public static void IncertRestaurant(Restaurant r)
        {
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    // Opening Connection  
                    connection.Open();
                    // Creating SqlCommand objcet
                    SqlCommand cm = new SqlCommand("insert INTO Restaurant values ('" + r.Name+"' , "+r.Rating+", '"+r.ImgName+"' , '"+r.Type.ToString()+"' , '"+r.Description+"');", connection);
                    // Executing the SQL query  
                    SqlDataReader sdr = cm.ExecuteReader();
                    System.Diagnostics.Debug.WriteLine("Restaurant incert was successful");
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
            }
        }

        public static void IncertReview(Review r)
        {
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    // Opening Connection  
                    connection.Open();
                    // Creating SqlCommand objcet
                    SqlCommand cm = new SqlCommand("insert INTO Reviews values ('" + r.Username + "', '" + r.RestaurantName + "', " + r.Rating + ");", connection);
                    // Executing the SQL query  
                    SqlDataReader sdr = cm.ExecuteReader();
                    System.Diagnostics.Debug.WriteLine("Review incert was successful");
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
            }
        }

        public static void IncertUser(User u)
        {
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["RestaurantRatingApp"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    // Opening Connection  
                    connection.Open();
                    // Creating SqlCommand objcet
                    SqlCommand cm = new SqlCommand("insert INTO Reviews Users ('" + u.Username + "', '" + u.Type.ToString() + "');", connection);
                    // Executing the SQL query  
                    SqlDataReader sdr = cm.ExecuteReader();
                    System.Diagnostics.Debug.WriteLine("User incert was successful");
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("OOPs, something went wrong.\n" + e);
            }
        }
    }
}