using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using RestaurantRatingApp_V2.Models;

namespace RestaurantRatingApp_V2.Tests
{
    public class Test
    {
        public StringBuilder User()
        {
            StringBuilder sb = new StringBuilder();

            User user = new User();
            sb.Append("Username:").Append(user.Username);
            
            try
            {
                user.SetAttr(123123, "Username", "other");
                sb.Append(Environment.NewLine).Append("Username:").Append(user.Username);
            } 
            catch (Exception e)
            {
                sb.Append(Environment.NewLine).Append(e.Message);
            }

            return sb;
        }
    }
}