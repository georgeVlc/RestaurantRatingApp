using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using RestaurantRatingApp_V2.Models;
using System.Diagnostics;
using RestaurantRatingApp_V2.Models.RestaurantRatingApp_V2.Models;



namespace RestaurantRatingApp_V2.Tests
{
    public static class Test
    {
        private static void PrintUserAttributes(User user, char pref='\t')
        {
            Debug.WriteLine("Printing user's attributes:");
            Debug.WriteLine(pref + "Username: " + user.Username);
            Debug.WriteLine(pref + "User Type: " + user.Type.ToString());
            Debug.WriteLine(pref + "User Restaurant: " + user.RestaurantName);
        }

        private static void PrintRestaurantAttributes(Restaurant restaurant, char pref='\t')
        {
            Debug.WriteLine("Printing restaurant's attributes:");
            Debug.WriteLine(pref + "Name: " + restaurant.Name);
            Debug.WriteLine(pref + "Rating: " + restaurant.Rating);
            Debug.WriteLine(pref + "ImgName: " + restaurant.ImgName);
            Debug.WriteLine(pref + "Type: " + restaurant.Type.ToString());
            Debug.WriteLine(pref + "Description: " + restaurant.Description);
            Debug.WriteLine(pref + "Owner: " + restaurant.Owner);
        }

        private static void PrintResult(bool result, String action)
        {
            if (result)
                Debug.WriteLine(action + " complete");
            else
                Debug.WriteLine(action + " failed");
        }

        private static void PrintTestHeader(String action, bool isStart)
        {
            String tok;
            if (isStart)
                tok = "INIT";
            else
                tok = "END";

            Debug.WriteLine("\n\n=========================-" + action + " TEST " + tok + "-=========================");
        }

        public static bool SignUpUser(String username, String pwd)
        {
            PrintTestHeader("SIGN-UP", true);

            try
            {

                User testUser = new User();

                Debug.WriteLine("User created");
                PrintUserAttributes(testUser);

                Debug.WriteLine("Signing up guest user");
                bool res = testUser.SignUp(username, pwd);

                PrintResult(res, "SIGN-UP");

                PrintUserAttributes(testUser);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error occured: " + ex);
                return false;
            }
            finally
            {
                PrintTestHeader("SIGN-UP", false);
            }

        }

        public static bool LoginUser(String username, String pwd)
        {
            PrintTestHeader("LOG-IN", true);

            try
            {
                User testUser = new User();

                Debug.WriteLine("User created");
                PrintUserAttributes(testUser);

                Debug.WriteLine("Loging in guest user");

                bool res = testUser.Login(username, pwd);

                PrintResult(res, "LOG-IN");

                PrintUserAttributes(testUser);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error occured: " + ex);
                return false;
            }
            finally
            {
                PrintTestHeader("LOG-IN", false);
            }
        }

        public static bool LogoutUser(String username, String pwd)
        {
            PrintTestHeader("LOG-OUT", true);

            try
            {
                User testUser = new User();

                Debug.WriteLine("User created");
                PrintUserAttributes(testUser);

                Debug.WriteLine("Loging in guest user");

                bool res = testUser.Login(username, pwd);

                PrintResult(res, "LOG-IN");

                PrintUserAttributes(testUser);

                Debug.WriteLine("Loging out user " + testUser.Username);

                res = testUser.Logout();

                PrintResult(res, "LOG-OUT");

                PrintUserAttributes(testUser);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error occured: " + ex);
                return false;
            }
            finally
            {
                PrintTestHeader("LOG-OUT", false);
            }
        }

        public static bool AddRestaurant(Restaurant testRestaurant)
        {
            PrintTestHeader("RESTAURANT INSERT", true);

            try
            {
                User testUser = new User();

                Debug.WriteLine("User created");
                PrintUserAttributes(testUser);

                Debug.WriteLine("Logging-in user");

                bool res = testUser.Login(testRestaurant.Owner, "4321");
                PrintResult(res, "LOG-IN");

                PrintUserAttributes(testUser);

                Debug.WriteLine("Adding a restaurant for user: " + testUser.Username);

                PrintRestaurantAttributes(testRestaurant);

                Debug.WriteLine("Inserting restaurant");

                res = testUser.AddRestaurant(testRestaurant);
                PrintResult(res, "RESTAURANT INSERT");

                PrintUserAttributes(testUser);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error occured: " + ex);
                return false;
            }
            finally
            {
                PrintTestHeader("RESTAURANT INSERT", false);
            }
        }

        public static bool RemoveRestaurant(Restaurant testRestaurant)
        {
            PrintTestHeader("RESTAURANT DELETE", true);

            try
            {
                User testUser = new User();
                
                Debug.WriteLine("User created");
                PrintUserAttributes(testUser);

                Debug.WriteLine("Logging-in user");
                
                bool res = testUser.Login(testRestaurant.Owner, "4321");
                PrintResult(res, "LOG-IN");

                PrintRestaurantAttributes(testRestaurant);

                Debug.WriteLine("Removing restaurant: " + testRestaurant.Name);

                res = testUser.RemoveRestaurant(testRestaurant);
                PrintResult(res, "RESTAURANT DELETE");

                PrintUserAttributes(testUser);
                PrintRestaurantAttributes(testRestaurant);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error occured: " + ex);
                return false;
            }
            finally
            {
                PrintTestHeader("RESTAURANT DELETE", false);
            }
        }

        public static bool MakeReview(Review review)
        {
            PrintTestHeader("REVIEW INSERT", true);

            try
            {
                User testUser = new User();
                
                Debug.WriteLine("User created");
                PrintUserAttributes(testUser);

                Debug.WriteLine("Loggin in user");
                bool res = testUser.Login(review.Username, "4321");

                PrintResult(res, "LOG-IN");
                PrintUserAttributes(testUser);

                res = testUser.MakeReview(review);

                PrintResult(res, "REVIEW INSERT");

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error occured: " + ex);
            }
            finally
            {
                PrintTestHeader("REVIEW INSERT", false);
            }

            return true;
        }

        public static bool RemoveReview(Review review)
        {
            PrintTestHeader("REVIEW DELETE", true);

            try
            {
                User testUser = new User();
                Debug.WriteLine("User created");

                Debug.WriteLine("Logging in user");
                bool res = testUser.Login(review.Username, "4321");

                PrintResult(res, "LOG-IN");
                PrintUserAttributes(testUser);

                Debug.WriteLine("DELETING REVIEW");
                
                res = testUser.RemoveReview(review);

                PrintResult(res, "REVIEW DELETE");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error occured: " + ex);
            }
            finally
            {
                PrintTestHeader("REVIEW DELETE", false);
            }

            return true;
        }
    }
}