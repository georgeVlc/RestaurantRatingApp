<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RestaurantPage.aspx.cs" Inherits="RestaurantRatingApp_V2.RestaurantPage" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

          <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        </asp:ScriptManagerProxy>

    <style type="text/css">
        .reviewstar {
            color: gold;
            font-size: 30px;
            margin-right: 5px;
        }

        .Star {
            background-image: url(Images/Star.gif);
            height: 16px;
            width: 16px;
        }

        .WaitingStar {
            background-image: url(Images/WaitingStar.gif);
            height: 16px;
            width: 16px;
        }

        .FilledStar {
            background-image: url(Images/FilledStar.gif);
            height: 16px;
            width: 16px;
        }
    </style>

    <asp:FormView ID="restaurantPage" runat="server" ItemType="RestaurantRatingApp_V2.Models.Restaurant" SelectMethod="GetRestaurant" RenderOuterTable="false">
        <ItemTemplate>
            <div class="card mb-2">
                <img src="<%#:Item.ImgName%>" class="card-img-top" alt="...">
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
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>

    </asp:FormView>

    <div class="row mt-4">
        <div class="card">
        <h2>Reviews</h2>
            <div class="card-body p-4">
                <div class="container justify-content-center">
                    <asp:ListView ID="ReviewsListView" runat="server" OnItemDataBound="ReviewsListView_ItemDataBound" ItemType="RestaurantRatingApp_V2.Models.RestaurantRatingApp_V2.Models.Review">
                        <ItemTemplate>
                            <div class="card shadow-lg" style="width: 500px;">
                                <div class="card-body p-2">
                                    <div class="row g-0">
                                        <div class="col-md-4 justify-content-center align-items-center">
                                            <img src="/Images/icons8-user-60.png" class="rounded mx-auto d-block" alt="...">
                                        </div>
                                        <div class="col-md-8">
                                            <div class="row p-3">
                                                <h4 class="card-title"><%#:Item.Username%><span class="badge bg-warning"><%#:Item.Rating %></span></h4>
                                            </div>
                                            <div class="rating" runat="server" id="ratingDiv">
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </div>
    <div class="row mt-4">
        <div class="card">
            <div class="card-body">
                <h2 class="card-title">Leave A Review</h2>
                <p>Share Your Experience With Others</p>
                <div class="card">
                    <div class="card-body">

                        <ajaxToolkit:Rating
                            ID="Rating1"
                            BehaviorID="RatingBehavior1"
                            runat="server"
                            OnChanged="Rating1_Changed"
                            MaxRating="5"
                            StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star"
                            FilledStarCssClass="FilledStar">
                        </ajaxToolkit:Rating>

                        <asp:LinkButton ID="SubmitBtn" runat="server" CssClass="btn btn-small btn-primary" OnClick="SubmitBtn_Click"><i class="icon icon-ok"></i>&nbsp;Submit</asp:LinkButton>
                    </div>
                </div>




            </div>
        </div>
    </div>

</asp:Content>
