/*using RestaurantRatingApp_V2.Models.RestaurantRatingApp_V2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using static RestaurantRatingApp_V2.Models.Restaurant;

namespace RestaurantRatingApp_V2.Models
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<RestaurantContext>
    {
        protected override void Seed(RestaurantContext context)
        {
            GetRestaurants().ForEach(r => context.Restaurants.Add(r));
            GetReviews().ForEach(rv => context.Reviews.Add(rv));
            GetUsers().ForEach(u => context.Users.Add(u));
        }

        private static List<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant> {
                new Restaurant
                    ("ta tria aderfia",
<<<<<<< HEAD
                    ";alsjkhdg",CousineType.Greek),
=======
                    ";alsjkhdg",CousineType.CONTEMPORARY),
>>>>>>> 96349fe (added API functionality)
                new Restaurant
                {

                }
            };

            return restaurants;
        }

        private static List<Review> GetReviews()
        {
            var reviews = new List<Review> {
                new Review
                {

               },
                new Review
                {

               }
            };

            return reviews;
        }

        private static List<User> GetUsers()
        {
            var users = new List<User> {
                new User
                {

                },
                new User
                {

                },

            };

            return users;
        }
    }
}*/