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

            this.InitTable(ref this.Table1, 5, 3);
            this.InitTable(ref this.Table2, 7, 2);

            List<String> cousineTypesAsStrings = Restaurant.GetCousineTypes().ConvertAll(x => x.ToString());

            //this.TextBox1.Text = string.Join( ",", Restaurant.GetCousineTypes().Skip(1));
            this.TextBox1.Text = string.Join(",", cousineTypesAsStrings.Skip(1));
        }

        private void LoginAdmin()
        {
            this.user.Login("admin", "1234");
        }

        private void InitTable(ref Table table, uint numOfRows, uint numOfCells)
        {
            table.BorderStyle = BorderStyle.Solid;
            table.CellSpacing = 10;


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

                table.Rows.Add(row);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<Restaurant> restaurants = this.user.GetRestaurants(numOfRestaurants:-1);
                List<Restaurant> topRestaurants = Restaurant.SortRestaurantsByWilsonScore(restaurants);

                for (int i = 0; i < this.Table1.Rows.Count; ++i)
                {
                    this.Table1.Rows[i].Cells[0].Text = topRestaurants[i].Name;
                    this.Table1.Rows[i].Cells[1].Text = topRestaurants[i].Rating.ToString();
                    this.Table1.Rows[i].Cells[2].Text = topRestaurants[i].Owner;
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
                "x_username",
                2.2f
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            string inputStr = this.TextBox2.Text;

            List<Restaurant> matches = this.user.MakeSearch(inputStr, 7, searchByRestaurantName: true);

            for (int i = 0; i < this.Table2.Rows.Count; ++i)
            {
                this.Table2.Rows[i].Cells[0].Text = matches[i].Name;
                this.Table2.Rows[i].Cells[1].Text = matches[i].Description;
            }
        }
    }
}