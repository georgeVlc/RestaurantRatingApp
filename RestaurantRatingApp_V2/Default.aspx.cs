using RestaurantRatingApp_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestaurantRatingApp_V2.Tests;
using RestaurantRatingApp_V2.Models.RestaurantRatingApp_V2.Models;
using System.Diagnostics;



namespace RestaurantRatingApp_V2
{
    public partial class _Default : Page
    {
        private User user;

        protected void Page_Load(object sender, EventArgs e)
        {
            //RunTests();

            this.user = new User();
            this.LoginAdmin();

            const uint numOfRows = 3;
            const uint numOfCells = 3;

            this.InitTable(numOfRows, numOfCells);
        }

        private void LoginAdmin()
        {
            this.user.Login("admin_user", "1234");
        }

        private void InitTable(uint numOfRows, uint numOfCells)
        {
            this.Table1.BorderStyle = BorderStyle.Solid;
            this.Table1.CellSpacing = 10;


            Random rng = new Random(69 + 420);

            for (int i = 0; i < numOfRows; ++i)
            {
                TableRow row = new TableRow {
                        BorderStyle = BorderStyle.Solid
                    };

                for (int j = 0; j < numOfCells; ++j)
                {
                    TableCell cell = new TableCell {
                            BorderStyle = BorderStyle.Solid,
                            Text = rng.Next(10, 20).ToString()
                        };

                    row.Cells.Add(cell);
                }

                this.Table1.Rows.Add(row);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<User> users = this.user.GetUsers();

                for (int i = 0; i < this.Table1.Rows.Count; ++i)
                {
                    this.Table1.Rows[i].Cells[0].Text = users[i].Username;
                    this.Table1.Rows[i].Cells[1].Text = users[i].Type.ToString();
                    this.Table1.Rows[i].Cells[2].Text = users[i].RestaurantName;
                }
            }
            catch (Exception) {  }
        }

        protected void RunTests()
        {
            Test.SignUpUser("l_username", "4321");
            Test.LoginUser("x_username", "4321");
            Test.LogoutUser("x_username", "4321");

            Restaurant testRestaurant = new Restaurant(
                "test_restaurant2",
                "an_img.png",
                Restaurant.CousineType.ITALIAN,
                "some words",
                "x_username"
                );

            Test.AddRestaurant(testRestaurant);

            Review testReviw = new Review(
                "test_restaurant2",
                "x_username",
                3.9f
                );

            Test.MakeReview(testReviw);
            Test.RemoveReview(testReviw);
            Test.RemoveRestaurant(testRestaurant);
        }
    }
}