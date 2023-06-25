using RestaurantRatingApp_V2.Models.RestaurantRatingApp_V2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantRatingApp_V2.Models
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext() : base("RestaurantRatingApp_V2")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}