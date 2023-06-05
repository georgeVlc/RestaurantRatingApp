using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Text;
using RestaurantRatingApp_V2.Models;

namespace RestaurantRatingApp_V2.Controllers
{
    public static class Utility
    {
        // code to verify requested actions
        // generated in runtime
        private static uint VERIFICATION_CODE = GenHashAsUint();

        // creates a byte array of sizeInKn*1024 random values
        private static byte[] GetByteArray(uint sizeInKb)
        {
            Random rnd = new Random();
            byte[] b = new byte[sizeInKb * 1024]; // convert kb to byte

            rnd.NextBytes(b);

            return b;
        }

        // generates hash code by SHA256
        private static uint GenHashAsUint() { return BitConverter.ToUInt32(SHA256.Create().ComputeHash(GetByteArray(1)), 0); }


        // is used as request API for given actions related to parameter settings
        public static bool VerifyAction(uint authCode) { return authCode == VERIFICATION_CODE; }

        public static void LoginUser(User user, String username, String pwd)
        {
            // DBHandler.LoginUser
            // update user info
        }

        public static void LogoutUser(User user)
        {
            // DBHandler.LogoutUser
            // update user info
        }

        public static void SignUpUser(User user, String username, String pwd)
        {
            // DBHandlr.SignUpUser
            // update user info
        }

        public static StringBuilder MakeSearch(StringBuilder sb)
        {
            // DBHandler.MakeSearch
            // return results (maybe not string builder)
            return new StringBuilder();
        }
    }
}