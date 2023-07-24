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


        public String Name { get { return this._name; } set { this._name = value; } }

        public float Rating { get { return this._rating; } set { this._rating = value; } }

        public String ImgName { get { return this._imgName; } set { this._imgName = value; } }

        public CousineType Type { get { return this._type; } set { this._type = value; } }

        public String Description { get { return this._description; } set { this._description = value; } }

        public String Owner { get { return this._owner; } set { this._owner = value; } }


        public Restaurant()
        {
            this.Name = this.ImgName = this.Description = this.Owner = String.Empty;
            this.Rating = 0.0f;
            this.Type = CousineType.NONE;
        }

        public Restaurant(String name, String imgName, CousineType type, String description, String owner)
        {
            this.Name = name;
            this.ImgName = imgName;
            this.Description = description;
            this.Owner = owner;
            this.Rating = 0.0f;
            this.Type = type;
        }
    }
}