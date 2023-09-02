<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RestaurantPage.aspx.cs" Inherits="RestaurantRatingApp_V2.RestaurantPage" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <asp:FormView ID="restaurantPage" runat="server" ItemType="RestaurantRatingApp_V2.Models.Restaurant" SelectMethod="GetRestaurant" RenderOuterTable="false">
        <ItemTemplate>
            <div class="card mb-2">
                <img src="Images/restaurant_search_img.jpg" class="card-img-top" alt="...">
                <div class="card-body">
                    <h2 class="card-title"><%#:Item.Name %></h2>
                    <p class="card-text"><small class="text-muted"><%#:Item.Type%> Cuisine</small></p>
                    <p class="card-text"><%#:Item.Description%> </p>
                </div>
                <div class="row p-3">
                    <h2 class="card-title">Information</h2>
                    <div class="col-md-8 ">
                        <div class="col-md-4 p-3">
                            <ul class="list-group list-group-flush">
                                <li class="list-group-item">Address:<asp:Label ID="addressLabel" runat="server" Text=" 123 Address Street "></asp:Label></li>
                                <li class="list-group-item">Phone Number:
                                    <asp:Label ID="phonenumberLabel" runat="server" Text=" 21042765872 "></asp:Label></li>
                                <li class="list-group-item">Price Rating:<asp:Label ID="priceRating" runat="server" Text=" $$ "></asp:Label></li>
                                <li class="list-group-item">Ratings:<asp:Label ID="restaurantRating" runat="server" Text=" 4/5 "></asp:Label></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>

    </asp:FormView>
     <div class="card mb-2">
          <div class="card-body">
               <h2 class="card-title">User Reviews</h2>
    <asp:ListView ID="ReviewListView" runat="server"
        GroupItemCount="1" SelectMethod="GetReviews"
        ItemType="RestaurantRatingApp_V2.Models.RestaurantRatingApp_V2.Models.Review">
        <EmptyDataTemplate>
            <table>
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <GroupTemplate>
            <tr id="itemPlaceholderContainer" runat="server">
                <td id="itemPlaceholder" runat="server"></td>
            </tr>
        </GroupTemplate>
        <ItemTemplate>
            <div class="card mb-2" style="max-width: 500px;">
                <div class="row g-0">
                    <div class="col-md-4">
                        <img src="/Images/icons8-user-60.png" alt="...">
                    </div>
                    <div class="col-md-8">
                        <div class="card-body p-1">
                            <h5 class="card-title"><%#:Item.Username%></h5>
                            <p class="card-text"><%#:Item.Rating%></p>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>

              </div>
         </div>

    <div class="card">
        <div class="card-body">
            <h2 class="card-title">Leave A Review</h2>
            <p>Share Your Experience With Others</p>
            <div class="card">
                <div class="card-body">
                    <asp:Button runat="server" ID="Star1" Text="&#9733;" OnClientClick="rate(1); return false;" />
                    <asp:Button runat="server" ID="Star2" Text="&#9733;" OnClientClick="rate(2); return false;" />
                    <asp:Button runat="server" ID="Star3" Text="&#9733;" OnClientClick="rate(3); return false;" />
                    <asp:Button runat="server" ID="Star4" Text="&#9733;" OnClientClick="rate(4); return false;" />
                    <asp:Button runat="server" ID="Star5" Text="&#9733;" OnClientClick="rate(5); return false;" />
                    <asp:LinkButton ID="SubmitBtn" runat="server" CssClass="btn btn-small btn-primary"><i class="icon icon-ok"></i>&nbsp;Submit</asp:LinkButton>
                </div>
            </div>

            <div class="col-md-8">
                


            </div>

          
        </div>
    </div>

</asp:Content>
