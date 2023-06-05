using RestaurantRatingApp_V2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantRatingApp_V2.Models
{
    public class Restaurant
    {
        public enum CousineType : byte
        {
            NONE = 0,
            TYPE1,
            TYPE2,
            TYPE3
        }

        private String _name;
        private String _imgName;
        private float _rating;
        private CousineType _type;
        private String _description;

        public String Name { get { return this._name; } protected set { this._name = value; } }
        public float Rating { get { return this._rating; } protected set { this._rating = value; } }
        public String ImgName { get { return this._imgName; } protected set { this._imgName = value; } }
        public CousineType Type { get { return this._type; } protected set { this._type = value; } }
        public String Description { get { return this._description; } protected set { this._description = value; } }

        public Restaurant()
        {
            this.Name = this.ImgName = this.Description = String.Empty;
            this.Rating = 0.0f;
            this.Type = CousineType.NONE;
        }

        public Restaurant(String name, String imgName, CousineType type)
        {
            this.Name = name;
            this.ImgName = imgName;
            this.Rating = 0.0f;
            this.Type = type;
        }

        // attributes are allowed to change only through verification by Utily Controller
        // if the action is verified then given attribute will be setted to given value
        // otherwise Exception is thrown

        public void SetAttr(uint authCode, String attr, ref Object value)
        {
            if (Utility.VerifyAction(authCode))
            {
                if (String.Equals(attr, "Name"))
                    this.Name = (String)value;
                else if (String.Equals(attr, "Rating"))
                    this.Rating = (float)value;
                else if (String.Equals(attr, "ImgName"))
                    this.ImgName = (String)value;
                else if (String.Equals(attr, "Type"))
                    this.Type = (CousineType)value;
                else if (String.Equals(attr, "Description"))
                    this.Description = (String)value;
                else
                    throw new Exception("unknown attribute: " + attr);
            }
            else
                throw new Exception("unauthorised action");
        }
    }
}