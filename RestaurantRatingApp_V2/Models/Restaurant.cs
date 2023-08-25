using RestaurantRatingApp_V2.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RestaurantRatingApp_V2.Models
{
    public class Restaurant
    {
        public enum CousineType : byte
        {
            NONE = 0,
            GREEK,
            CONTEMPORARY,
            ASIAN,
            ITALIAN,
            MEXICAN
        }

        private String _name;
        private String _imgName;
        private float _rating;
        private CousineType _type;
        private String _description;
        private String _owner;
        private double _rank;


        public String Name { get { return this._name; } set { this._name = value; } }

        public float Rating { get { return this._rating; } set { this._rating = value; } }

        public String ImgName { get { return this._imgName; } set { this._imgName = value; } }

        public CousineType Type { get { return this._type; } set { this._type = value; } }

        public String Description { get { return this._description; } set { this._description = value; } }

        public String Owner { get { return this._owner; } set { this._owner = value; } }

        public double Rank { get { return this._rank; } protected set { this._rank = value; } }

        public Restaurant()
        {
            this.Name = this.ImgName = this.Description = this.Owner = String.Empty;
            this.Rating = 0.0f;
            this.Type = CousineType.NONE;
            this.Rank = 0.0;
        }

        public Restaurant(String name, String imgName, CousineType type, String description, String owner, float rating)
        {
            this.Name = name;
            this.ImgName = imgName;
            this.Description = description;
            this.Owner = owner;
            this.Rating = rating;
            this.Type = type;
            this.Rank = 0.0;
        }

        public static List<Restaurant.CousineType> GetCousineTypes()
        {
            return ((Restaurant.CousineType[])Enum.GetValues(typeof(Restaurant.CousineType))).ToList<Restaurant.CousineType>();
        }

        public static double GetWilsonScore(Restaurant restaurant, uint numOfReviews, double confidenceLevel=0.95)
        {
            double z = GetZScore(confidenceLevel);
            double p = restaurant.Rating / 5.0;

            double w = (p + z * z / (2 * numOfReviews) - z * Math.Sqrt((p * (1 - p) + z * z / (4 * numOfReviews)) / numOfReviews)) / (1 + z * z / numOfReviews);

            return w;
        }

        private static double GetZScore(double confidenceLevel=0.95)
        {
            if (confidenceLevel == 0.95)
                return 1.96;
            else
                throw new ArgumentException("Unsupported confidence level");
        }

        public static List<Restaurant> SortRestaurantsByWilsonScore(List<Restaurant> restaurants, double confidenceLevel=0.95)
        {
            foreach (var restaurant in restaurants)
            {
                uint totalResReviews = (uint) Utility.GetReviewsByRestaurantName(restaurant.Name, -1).Count;
                restaurant.Rank = Restaurant.GetWilsonScore(restaurant, totalResReviews, confidenceLevel);
            }

            return restaurants.OrderByDescending(r => r.Rank).ToList();
        }
    }
}